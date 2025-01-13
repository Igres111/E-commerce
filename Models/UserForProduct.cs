using System.Text.Json.Serialization;

namespace E_commerce.Models
{
    public class UserForProduct
    {
       public Guid Id { get; set; }
       public Guid UserId { get; set; }
       public User User { get; set; }
       [JsonIgnore]
       public Guid ProductId { get; set; }
       public Product Product { get; set; }
    }
}
