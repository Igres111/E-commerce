using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.DTOs.TokenDtos;
using E_commerce.DTOs.UserDtos;
using E_commerce.Models;
using E_commerce.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using static System.Net.Mime.MediaTypeNames;

namespace E_commerce.Repositories.UserRepos
{
    public class IUserRepository : IUser
    {
        public readonly Context _context;
        public readonly IToken _token;
        public IUserRepository(Context context, TokenGenerator token)
        {
            _context = context;
            _token = token;
        }
        public async Task<IEnumerable<ReceiveUsersDto>> GetUsers()
        {
            var result = await _context.Users.ToListAsync();
            return result.Select(x => new ReceiveUsersDto
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Password = x.Password,
                Role = x.Role,
                Coupon = x.Coupon,
                Favorite = x.Favorite
            }).ToList(); ;
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
                .FirstOrDefaultAsync(el => el.Email == user.Email);

            if (emailOrPhone != null && BCrypt.Net.BCrypt.Verify(user.Password, emailOrPhone.Password))
            {
                var tokenExist = await _context.RefreshTokens.FirstOrDefaultAsync(el => el.UserId == emailOrPhone.Id);
                if (tokenExist != null)
                {
                    _context.RefreshTokens.Remove(tokenExist);
                }
                var accessToken = _token.CreateAccessToken(emailOrPhone);
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
        public async Task<IEnumerable<ReceiveFavProduct>> GetFav(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                var result = await _context.Products
                     .Where(product => user.Favorite.Contains(product.Id))
                     .ToListAsync();
                return result.Select(x => new ReceiveFavProduct
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
                    ReviewCount = x.ReviewCount,
                    CreateDate = x.CreateDate,
                    PurchasedCount = x.PurchasedCount
                });
            }
            throw new Exception("User doesn't exist");
        }
        public void SendEmail(SendMailDto mail)
        {
            string fromMail = "sergikaralashvili@gmail.com";
            string fromPassword = "hogr jzlk chec eyxs\r\n";
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Baqari", fromMail));
            email.To.Add(new MailboxAddress(mail.Name, mail.Address));

            email.Subject = mail.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<html><body>{mail.Text}<body></html>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                smtp.Authenticate("sergikaralashvili@gmail.com", fromPassword);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
