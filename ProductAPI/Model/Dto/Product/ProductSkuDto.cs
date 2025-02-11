namespace ProductAPI.Model.Dto.Product
{
    public class ProductSkuDto
    {
        public decimal Size { get; set; }
        public string SizeUnit { get; set; } // gm, kg, nos
        public decimal Price { get; set; }
    }
}
