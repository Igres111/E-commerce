using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTOs.ProductDtos
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public float Price { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "Discount Price must be a positive number.")]
        public float DiscountPrice { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Discount Percent must be a positive number.")]
        public int DiscountPercent { get; set; }
        public string? Color { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock can't be a negative number.")]
        public int Stock { get; set; }
        public int PurchasedCount { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
