using AutoMapper;
using DomvsUnitTestPoc.Domain.Entities;
using DomvsUnitTestPoc.Exposure.Models;

namespace DomvsUnitTestPoc.Exposure.AutoMappers
{
    public class AutoMappingExposure : Profile
    {
        public AutoMappingExposure()
        {
            CreateMap<SaleModel, Sale>().ReverseMap();
        }
    }
}
