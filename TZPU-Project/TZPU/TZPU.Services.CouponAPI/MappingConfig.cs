using AutoMapper;
using TZPU.Services.CouponAPI.Models;
using TZPU.Services.CouponAPI.Models.Dtos;

namespace TZPU.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
