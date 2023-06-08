using TZPU.Services.OrderAPI.Models.Dtos;

namespace TZPU.Services.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
