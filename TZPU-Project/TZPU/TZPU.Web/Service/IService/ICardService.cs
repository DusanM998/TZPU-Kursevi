using TZPU.Web.Models;

namespace TZPU.Web.Service.IService
{
    public interface ICardService
    {
        Task<ResponseDto?> GetCardByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCardAsync(CardDto cardDto);
        Task<ResponseDto?> RemoveFromCardAsync(int cardDetailsId);
        Task<ResponseDto?> ApplyCouponAsync(CardDto cardDto);
    }
}
