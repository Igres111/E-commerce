using E_commerce.Data;
using E_commerce.DTOs.UserDtos;
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
           var result = await _methods.LoginUser(user);
            return Ok(result);
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
            await _methods.UpdateUser(id, user);
            return Ok();
        }
        [HttpPost("Add-Favorite")]
        public async Task<ActionResult> AddFavorite(UserFavProduct favourite)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == favourite.UserId);
            if (result == null)
            {
                return BadRequest("User not found");
            }
            result.Favorite.Add(favourite.ProductId);
            await _context.SaveChangesAsync();
            return Ok(result.Favorite);
        }
        [HttpPost("Remove-Favorite")]
        public async Task<ActionResult> RemoveFavorite(UserFavProduct favourite)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == favourite.UserId);
            if (result == null)
            {
                return BadRequest("User not found");
            }
            result.Favorite.Remove(favourite.ProductId);
            await _context.SaveChangesAsync();
            return Ok(result.Favorite);
        }
    }
}
