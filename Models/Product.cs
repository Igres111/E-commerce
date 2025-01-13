namespace E_commerce.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public string? Color { get; set; } = string.Empty;
        public string? Size { get; set; } = string.Empty;
        public bool Favorite { get; set; }
        public string Image { get; set; } = string.Empty;
        public List<string> ImageList { get; set; }
        public int Stock { get; set; }
        public int PurchasedCount { get; set; }
        public List<UserForProduct> UserForProducts { get; set; }
    }
}
