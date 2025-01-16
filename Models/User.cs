namespace E_commerce.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Coupon { get; set; } = string.Empty;
        public List<Guid> Favorite { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public List<UserForProduct> UserForProducts { get; set; }
        public List<BillingInfo> billingInfos { get; set; }
    }
}
