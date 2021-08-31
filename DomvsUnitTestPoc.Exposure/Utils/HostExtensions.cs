using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DomvsUnitTestPoc.Exposure.Utils
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext, IDisposable
        {
            using var scope = host.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<T>();
            appContext.Database.Migrate();
            return host;
        }
    }
}
