using ProductAPI.Model.Dto;

namespace ProductAPI.Repositories.IRepositories
{
    public interface IProductSubCategory
    {
        Task<ResponseDto> CreateProductSubCategoryAsync(ProductSubCategoryDto subCategoryDto);
        Task<ResponseDto> GetAllProductSubCategoriesAsync();
    }
}
