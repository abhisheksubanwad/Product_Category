using ProductAPI.Model.Dto.Product;
using ProductAPI.Model.Dto;

namespace ProductAPI.Repositories.IRepositories
{
    public interface IProduct
    {
        Task<ResponseDto> CreateProductAsync(ProductDto productDto, string productCategoryId, string productSubCategoryId);
        Task<ResponseDto> GetAllProductsAsync();
    }
}
