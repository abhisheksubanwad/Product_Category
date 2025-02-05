using ProductAPI.Date_Time;

namespace ProductAPI.Model.Domain
{
    public class ProductCategory
    {
        public Guid ProductCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Language Language { get; set; }
        public DateTime CreatedDate { get; set; } = TimeHelper.GetIndianTime();
        public DateTime LastUpdatedDate { get; set; } = TimeHelper.GetIndianTime();
    }
}
