namespace E_commerce.DTOs.TokenDtos
{
    public class TokenResponseDto
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
