using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.DTOs.UserDtos;
using E_commerce.Models;
using E_commerce.Repositories.UserRepos;
using E_commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        [Authorize(Roles = "Admin")]
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
                return BadRequest(ModelState);
            }  
            await _methods.RegisterUser(user);
            return Ok("Registered");
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUserDto user)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _methods.UpdateUser(id, user);
            return Ok();
        }
        [HttpPost("Add-Favorite")]
        public async Task<ActionResult> AddFavorite(UserFavProduct favorite)
        {
            var favs = await _methods.AddFav(favorite);
            return Ok(favs);
        }
        [HttpDelete("Remove-Favorite")]
        public async Task<ActionResult> RemoveFavorite(UserFavProduct favorite)
        {
            var favs = await _methods.RemoveFav(favorite);
            return Ok(favs);
        }
        [HttpGet("Get-Favorites/{userId}")]
        public async Task<ActionResult> GetFavorites(Guid userId)
        {
            var result = await _methods.GetFav(userId);
            return Ok(result);
        }
    }
}
