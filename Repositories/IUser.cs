using E_commerce.DTOs.TokenDtos;
using E_commerce.DTOs.UserDtos;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Repositories
{
    public interface IUser
    {
        public Task<List<User>> GetUsers();
        public Task RegisterUser(RegisterUserDto user);
        public Task<TokenResponseDto> LoginUser(LoginUserDto user);
        public Task UpdateUser(Guid id, UpdateUserDto user);
    }
}
