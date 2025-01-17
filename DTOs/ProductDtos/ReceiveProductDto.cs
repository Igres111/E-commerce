namespace E_commerce.DTOs.ProductDtos
{
    public class ReceiveProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public float DiscountPrice { get; set; }
        public string? Color { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        public string Category { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
