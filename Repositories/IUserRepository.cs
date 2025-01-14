﻿using E_commerce.Data;
using E_commerce.DTOs;
using E_commerce.Models;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositories
{
    public class IUserRepository : IUser
    {
        public readonly Context _context;
        public readonly TokenGenerator _token;
        public IUserRepository(Context context, TokenGenerator token)
        {
            _context = context;
            _token = token;
        }
        public async Task<List<User>> GetUsers()
        {
            var result = await _context.Users.ToListAsync();
            return result;
        }
        public async Task RegisterUser(RegisterUserDto user)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(el => el.Email == user.Email || el.PhoneNumber == user.PhoneNumber);
            if (exist != null)
            {
                throw new Exception("User already exists");
            }
            var result = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = user.Role
            };
            await _context.Users.AddAsync(result);
            await _context.SaveChangesAsync();
        }
        public async Task LoginUser(LoginUserDto user)
        {
            var emailOrPhone = await _context.Users
                .FirstOrDefaultAsync(el => el.Email == user.EmailOrPhone || el.PhoneNumber == user.EmailOrPhone);

            if (emailOrPhone != null && BCrypt.Net.BCrypt.Verify(user.Password, emailOrPhone.Password))
            {
                var tokenExist = await _context.RefreshTokens.FirstOrDefaultAsync(el => el.UserId == emailOrPhone.Id);
                if (tokenExist != null) 
                { 
                 _context.RefreshTokens.Remove(tokenExist);
                }
                _token.CreateAccessToken(emailOrPhone);
                var result = await _token.CreateRefreshTokenAsync(emailOrPhone);
            } 
        }
    }
}
