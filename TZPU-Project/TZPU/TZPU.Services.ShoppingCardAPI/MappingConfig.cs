using AutoMapper;
using TZPU.Services.ShoppingCardAPI.Models;
using TZPU.Services.ShoppingCardAPI.Models.Dtos;

namespace TZPU.Services.ShoppingCardAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CardHeaderDto, CardHeader>().ReverseMap();
                config.CreateMap<CardDetailsDto, CardDetails>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
