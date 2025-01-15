namespace E_commerce.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public string? Color { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        public int Stock { get; set; }
        public int PurchasedCount { get; set; }
        public DateTime CreateDate { get; set; }
        public List<UserForProduct> UserForProducts { get; set; }
    }
}
