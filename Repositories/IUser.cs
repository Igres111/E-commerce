using E_commerce.DTOs;
using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Repositories
{
    public interface IUser
    {
        public Task<List<User>> GetUsers();
        public Task RegisterUser(RegisterUserDto user);
        public Task LoginUser(LoginUserDto user);
    }
}
