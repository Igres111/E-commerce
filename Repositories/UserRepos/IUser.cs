using E_commerce.DTOs.ProductDtos;
using E_commerce.DTOs.TokenDtos;
using E_commerce.DTOs.UserDtos;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Repositories.UserRepos
{
    public interface IUser
    {
        public Task<IEnumerable<ReceiveUsersDto>> GetUsers();
        public Task RegisterUser(RegisterUserDto user);
        public Task<TokenResponseDto> LoginUser(LoginUserDto user);
        public Task UpdateUser(Guid id, UpdateUserDto user);
        public Task<List<Guid>> AddFav(UserFavProduct favorite);
        public Task<List<Guid>> RemoveFav(UserFavProduct favorite);
        public Task<IEnumerable<ReceiveFavProduct>> GetFav(Guid userId);
        public void SendEmail(SendMailDto mail);
    }
}
