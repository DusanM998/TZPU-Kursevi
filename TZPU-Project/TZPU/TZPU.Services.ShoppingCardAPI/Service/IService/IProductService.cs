using TZPU.Services.ShoppingCardAPI.Models.Dtos;

namespace TZPU.Services.ShoppingCardAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
