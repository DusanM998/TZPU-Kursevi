using AutoMapper;
using TZPU.Services.ProductAPI.Models;
using TZPU.Services.ProductAPI.Models.Dtos;

namespace TZPU.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
