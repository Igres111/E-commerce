using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.DTOs.UserDtos;
using E_commerce.Models;
using E_commerce.Repositories;
using E_commerce.Services;
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
        [HttpDelete("Remove-Favorite")]
        public async Task<ActionResult> RemoveFavorite(UserFavProduct favourite)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == favourite.UserId);
            if (result == null || !result.Favorite.Contains(favourite.ProductId))
            {
                return BadRequest("User not found or Product is not selected as Favourite");
            }
            result.Favorite.Remove(favourite.ProductId);
            await _context.SaveChangesAsync();
            return Ok(result.Favorite);
        }
        [HttpGet("Get-Favorites/{userId}")]
        public async Task<ActionResult> GetFavorites(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null) 
            {
                var result = await _context.Products
                     .Where(product => user.Favorite.Contains(product.Id))
                     .ToListAsync();
                return Ok(result.Select(x => new AddProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Image = x.Image,
                    Category = x.Category,
                    Color = x.Color,
                    DiscountPercent = x.DiscountPercent,
                    DiscountPrice = x.DiscountPrice,
                    Stock = x.Stock,
                    Rating = x.Rating,
                    ReviewCount = x.ReviewCount
                }));
            }
            return BadRequest();
        }
    }
}
