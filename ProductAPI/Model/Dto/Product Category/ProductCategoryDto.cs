namespace ProductAPI.Model.Dto.Product_Category
{
    public class ProductCategoryDto
    {
        public List<LanguageData> LanguageData { get; set; }
        public IFormFile ImageUrl { get; set; }
    }

    public class LanguageData
    {
        public int LanguageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
