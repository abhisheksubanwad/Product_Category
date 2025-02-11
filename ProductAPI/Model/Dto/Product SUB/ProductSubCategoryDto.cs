using ProductAPI.Model.Dto;
namespace ProductAPI.Model.Dto
{
    public class ProductSubCategoryDto
    {
        public Guid ProductCategoryId { get; set; }  
        public List<LanguageData> LanguageData { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
