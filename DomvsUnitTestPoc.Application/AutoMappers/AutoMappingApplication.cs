using AutoMapper;
using DomvsUnitTestPoc.Application.Commands;
using DomvsUnitTestPoc.Domain.Entities;

namespace DomvsUnitTestPoc.Application.AutoMappers
{
    public class AutoMappingApplication : Profile
    {
        public AutoMappingApplication()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
        }
    }
}
