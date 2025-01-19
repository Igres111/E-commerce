
using E_commerce.Models;

namespace E_commerce.DTOs.ProductDtos
{
    public class BillingProductsDto
    {
        public string Name { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? AddressDetails { get; set; }
        public string City { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public float TotalPrice { get; set; }
        public List<ReceiveProductDto> ProductsList { get; set; }
    }
}
