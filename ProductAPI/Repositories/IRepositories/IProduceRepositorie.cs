using ProductAPI.Model.Dto;

namespace ProductAPI.Repositories.IRepositories
{
    public interface IProduceRepositorie
    {
        Task<ResponseDto> AddProductHierarchyAsync(LanguageDto productDto, string imageUrl);
    }
}