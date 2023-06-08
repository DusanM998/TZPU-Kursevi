using TZPU.Web.Models;
using TZPU.Web.Service.IService;
using TZPU.Web.Utility;

namespace TZPU.Web.Service
{
    public class CardService : ICardService
    {
        private readonly IBaseService _baseService;
        public CardService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CardDto cardDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cardDto,
                Url = SD.ShopingCardAPIBase + "/api/card/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> GetCardByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShopingCardAPIBase + "/api/card/GetCard/" + userId,
            });
        }

        public async Task<ResponseDto?> RemoveFromCardAsync(int cardDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cardDetailsId,
                Url = SD.ShopingCardAPIBase + "/api/card/RemoveCard"
            });
        }

        public async Task<ResponseDto?> UpsertCardAsync(CardDto cardDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cardDto,
                Url = SD.ShopingCardAPIBase + "/api/card/CardUpsert"
            });
        }
    }
}
