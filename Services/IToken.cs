using E_commerce.Models;

namespace E_commerce.Services
{
    public interface IToken
    {
        public string CreateAccessToken(User user);
        public Task<RefreshToken> CreateRefreshTokenAsync(User user);
        public Task<string> RefreshAccessTokenAsync(string refreshTokenInput);

    }
}
