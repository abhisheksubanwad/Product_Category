namespace ProductAPI.Model.Dto
{
    public class CreateProductRequestDto
    {
        public ProductCategoryDto ProductCategory { get; set; }
        public ProductSubCategoryDto ProductSubCategory { get; set; }
        public ProductDto Product { get; set; }
    }
}
