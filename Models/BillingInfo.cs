namespace E_commerce.Models
{
    public class BillingInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string Adress {  get; set; } = string.Empty;
        public string? AdressDetails { get; set; }
        public string City { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
