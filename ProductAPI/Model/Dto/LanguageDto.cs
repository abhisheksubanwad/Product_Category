using ProductAPI.Model.Domain;

namespace ProductAPI.Model.Dto
{
    public class LanguageDto
    {
        public List<LanguageData> LanguageData { get; set; } 
        public IFormFile ImageUrl { get; set; } 
        public bool Status { get; set; }

    }

    public class LanguageData
    {
        public Language language { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
