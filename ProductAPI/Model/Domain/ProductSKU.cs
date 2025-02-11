using ProductAPI.Date_Time;

namespace ProductAPI.Model.Domain
{
    public class ProductSKU
    {
        public Guid SkuId { get; set; }
        public Guid ProductId { get; set; }

        public decimal Size { get; set; }
        public string SizeUnit { get; set; } // (gm, kg, nos)
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; } = TimeHelper.GetIndianTime();

        // Navigation Property
        public Product Product { get; set; }
    }
}
