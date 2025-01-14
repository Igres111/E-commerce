﻿using E_commerce.Data;
using E_commerce.DTOs;
using E_commerce.Models;
using E_commerce.Repositories;
using E_commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly Context _context;
        public readonly TokenGenerator _token;
        public IUser _methods;
        public UserController(Context context, TokenGenerator token, IUser methods)
        {
            _context = context;
            _token = token;
            _methods = methods;
        }
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var result = await _methods.GetUsers();
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserDto user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }  
            await _methods.RegisterUser(user);
            return Ok("Registered");
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUserDto user)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest("Invalid data");
            }
            await _methods.LoginUser(user);
            return Ok();
        }
        [HttpPost("Refresh-Token")]
        public async Task<ActionResult> RefreshToken(string refreshToken)
        {
            var newAccessToken = await _token.RefreshAccessTokenAsync(refreshToken);
            return Ok(newAccessToken);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInfo(Guid id, UpdateUserDto user )
        {
            var target = await _context.Users.FirstOrDefaultAsync(el => el.Id == id);
            if (target == null)
            {
                return NotFound("User not found");
            }
            target.Name = user.Name;
            target.LastName = user.LastName;
            target.Address = user.Address;
            target.PhoneNumber = user.PhoneNumber;
            target.Email = user.Email;
            target.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _context.SaveChangesAsync();
            return Ok(target);
        }
    }
}
