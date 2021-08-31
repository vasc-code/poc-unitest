using AutoMapper;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Infrastructure.Entities;

namespace DomvsUnitTestPoc.Infrastructure.AutoMappers
{
    public class AutoMappingInfrastructure : Profile
    {
        public AutoMappingInfrastructure()
        {
            CreateMap<ProductEntity, Product>().ReverseMap();
            CreateMap<SaleEntity, Sale>().ReverseMap();
        }
    }
}
