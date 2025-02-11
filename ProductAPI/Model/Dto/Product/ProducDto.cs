namespace ProductAPI.Model.Dto.Product
{
    public class ProductDto
    {
        public bool Status { get; set; }
        public string Ingredients { get; set; }
        public string SeedRateDose { get; set; }
        public string PlantingSeason { get; set; }
        public string SowingPlantingSpacing { get; set; }
        public string FertilizerDoses { get; set; }
        public string NumberOfCuttings { get; set; }
        public string Yield { get; set; }
        public string AdvantagesBenefits { get; set; }
        public string PrecautionsStandards { get; set; }

        // SKUs
        public List<ProductSkuDto> SKUs { get; set; } = new();

        // **Updated to accept file uploads**
        public List<IFormFile> ImageUrls { get; set; } = new();
        public List<string> ProductVideoUrls { get; set; } = new();

        // Localized Data
        public List<LanguageData> LanguageData { get; set; } = new();
    }

}
