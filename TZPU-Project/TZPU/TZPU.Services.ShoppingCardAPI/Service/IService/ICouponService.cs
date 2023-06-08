using TZPU.Services.ShoppingCardAPI.Models.Dtos;

namespace TZPU.Services.ShoppingCardAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
