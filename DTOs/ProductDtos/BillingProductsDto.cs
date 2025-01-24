
using E_commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTOs.ProductDtos
{
    public class BillingProductsDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;
        public string? AddressDetails { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "Total price must be a positive number.")]
        public float TotalPrice { get; set; }
        public List<ReceiveProductDto> ProductsList { get; set; }
    }
}
