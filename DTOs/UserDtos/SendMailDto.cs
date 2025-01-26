using System.ComponentModel.DataAnnotations;

namespace E_commerce.DTOs.UserDtos
{
    public class SendMailDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
