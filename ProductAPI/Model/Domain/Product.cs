using ProductAPI.Date_Time;

namespace ProductAPI.Model.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public Guid ProductSubCategoryId { get; set; }


        // Localized Product Name and Description
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }



        // Multiple Images
        public List<string> ImageUrls { get; set; } = new List<string>();

        // Product Video (YouTube URL)
        public List<string> ProductVideoUrls { get; set; } = new List<string>();




        // Additional Product Details
        public string Ingredients { get; set; }
        public string SeedRateDose { get; set; }
        public string PlantingSeason { get; set; }
        public string SowingPlantingSpacing { get; set; }
        public string FertilizerDoses { get; set; } 
        public string NumberOfCuttings { get; set; }
        public string Yield { get; set; }
        public string AdvantagesBenefits { get; set; }
        public string PrecautionsStandards { get; set; }



        // Related Products & Compatibility
        public List<Guid> RecommendedProductIds { get; set; } = new List<Guid>();
        public List<Guid> CompatibleProductIds { get; set; } = new List<Guid>();



        // Dates
        public DateTime CreatedDate { get; set; } = TimeHelper.GetIndianTime();
        public DateTime LastUpdatedDate { get; set; } = TimeHelper.GetIndianTime();

        // One Product Can Have Multiple SKUs
        public List<ProductSKU> SKUs { get; set; } = new List<ProductSKU>();

        // Language (EN, MA, HI)
        public Language Language { get; set; }
    }
}
