using ProductAPI.Model.Dto.Product_Category;
using ProductAPI.Model.Dto;

namespace ProductAPI.Repositories.IRepositories
{
    public interface IProductCategory
    {
        Task<ResponseDto> CreateProductCategoryAsync(ProductCategoryDto productCategoryDto);
        Task<ResponseDto> GetAllProductCategoriesAsync();
    }
}
