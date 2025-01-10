using E_commerce.Data;
using E_commerce.DTOs;
using E_commerce.Models;
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
        public UserController(Context context, TokenGenerator token)
        {
            _context = context;
            _token = token;
        }
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var result = await _context.Users.ToListAsync();
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserDto user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            var result = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
            await _context.Users.AddAsync(result);
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUserDto user)
        {
            var emailOrPhone = await _context.Users
                .FirstOrDefaultAsync(el => el.Email == user.EmailOrPhone || el.PhoneNumber == user.EmailOrPhone);
        
            if (emailOrPhone != null)
            {
                _token.CreateAccessToken(emailOrPhone);
                var result = await _token.CreateRefreshTokenAsync(emailOrPhone);
                return Ok(result);
            }
            return BadRequest("Invalid credentials");
        }
    }
}
