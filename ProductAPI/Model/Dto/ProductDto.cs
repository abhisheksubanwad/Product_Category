namespace ProductAPI.Model.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string ProductVideoUrl { get; set; }
        public string Ingredients { get; set; }
        public string SeedRateDose { get; set; }
        public string PlantingSeason { get; set; }
        public string SowingPlantingSpacing { get; set; }
        public string FertilizerDoses { get; set; }
        public string NumberOfCuttings { get; set; }
        public string Yield { get; set; }
        public string AdvantagesBenefits { get; set; }
        public string PrecautionsStandards { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> SubCategoryIds { get; set; }
        public List<Guid> RecommendedProductIds { get; set; }
        public List<Guid> CompatibleProductIds { get; set; }
        public string LanguageName { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
