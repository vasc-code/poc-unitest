using AutoMapper;
using DomvsUnitTestPoc.Application.AutoMappers;
using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Application.Handlers;
using DomvsUnitTestPoc.Application.Queries;
using DomvsUnitTestPoc.Domain.DTOs;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Domain.Interfaces;
using DomvsUnitTestPoc.Exposure.AutoMappers;
using DomvsUnitTestPoc.Infrastructure.AutoMappers;
using DomvsUnitTestPoc.Infrastructure.Contexts;
using DomvsUnitTestPoc.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace DomvsUnitTestPoc.Exposure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddDbContext<TransactionContext>(options => options.UseMySql(appSettings.TransactionConnectionString, new MySqlServerVersion(new Version(8, 0, 11))));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DomvsUnitTestPoc.Exposure", Version = "v1" });
            });
            dependencyInjection(services);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DomvsUnitTestPoc.Exposure v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void dependencyInjection(IServiceCollection services)
        {
            services.AddAutoMapper(
                            typeof(Startup),
                            typeof(AutoMappingInfrastructure),
                            typeof(AutoMappingExposure),
                            typeof(AutoMappingApplication));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<IRequestHandler<CreateSaleCommand, bool>, CreateSaleHandler>();
            services.AddTransient<IRequestHandler<CreateProductCommand, bool>, CreateProductHandler>();
            services.AddTransient<IRequestHandler<ListProductCommand, PagedModel<Product>>, ListProductQuery>();
            var mapperConfig = new MapperConfiguration(a =>
            {
                a.AddProfile<AutoMappingExposure>();
                a.AddProfile<AutoMappingApplication>();
                a.AddProfile<AutoMappingInfrastructure>();
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
