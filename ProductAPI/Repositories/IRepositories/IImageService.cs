namespace ProductAPI.Repositories.IRepositories
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
        Task DeleteImageAsync(string imageUrl);
    }
}
