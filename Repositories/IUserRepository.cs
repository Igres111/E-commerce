using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.DTOs.TokenDtos;
using E_commerce.DTOs.UserDtos;
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
                Role = user.Role,
                Favorite = new List<Guid>(),
            };
            await _context.Users.AddAsync(result);
            await _context.SaveChangesAsync();
        }
        public async Task<TokenResponseDto> LoginUser(LoginUserDto user)
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
                var accessToken =_token.CreateAccessToken(emailOrPhone);
                var refreshToken = await _token.CreateRefreshTokenAsync(emailOrPhone);
                return new TokenResponseDto 
                {
                    Id = emailOrPhone.Id,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                };
            }
           throw new Exception("Invalid credentials");
        }
        public async Task UpdateUser(Guid id, UpdateUserDto user)
        {
            var target = await _context.Users.FirstOrDefaultAsync(el => el.Id == id) 
                ?? throw new Exception("User not found"); ;
            target.Name = user.Name;
            target.LastName = user.LastName;
            target.Address = user.Address;
            target.PhoneNumber = user.PhoneNumber;
            target.Email = user.Email;
            target.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Guid>> AddFav(UserFavProduct favorite)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == favorite.UserId);
            if (result == null)
            {
                throw new Exception("User Not Found");
            }
            result.Favorite.Add(favorite.ProductId);
            await _context.SaveChangesAsync();
            return result.Favorite;
        }
        public async Task<List<Guid>> RemoveFav(UserFavProduct favorite)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Id == favorite.UserId);
            if (result == null || !result.Favorite.Contains(favorite.ProductId))
            {
                throw new Exception("User not found or Product is not selected as Favourite");
            }
            result.Favorite.Remove(favorite.ProductId);
            await _context.SaveChangesAsync();
            return result.Favorite;
        }
       public async Task<IEnumerable<AddProductDto>> GetFav(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                var result = await _context.Products
                     .Where(product => user.Favorite.Contains(product.Id))
                     .ToListAsync();
                return result.Select(x => new AddProductDto
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
                });
            }
            throw new Exception("User doesn't exist");
        }
    }
}
