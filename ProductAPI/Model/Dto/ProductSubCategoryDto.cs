namespace ProductAPI.Model.Dto
{
    public class ProductSubCategoryDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LanguageName { get; set; }
        public Guid ProductCategoryId { get; set; }
    }
}
