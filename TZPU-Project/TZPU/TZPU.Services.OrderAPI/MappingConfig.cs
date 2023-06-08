using AutoMapper;
using TZPU.Services.OrderAPI.Models;
using TZPU.Services.OrderAPI.Models.Dtos;

namespace TZPU.Services.ShoppingCardAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeaderDto, CardHeaderDto>()
                .ForMember(dest => dest.CardTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

                config.CreateMap<CardDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                config.CreateMap<OrderDetailsDto, CardDetailsDto>();

                config.CreateMap<OrderHeaderDto, OrderHeader>().ReverseMap();
                config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
