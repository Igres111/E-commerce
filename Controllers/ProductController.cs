using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly Context _context;
        public ProductController(Context context)
        {
            _context = context;
        }
        [HttpGet("All-Products")]
        public async Task<IActionResult> GetProduct()
        {
            var result = await _context.Products.ToListAsync();
            return Ok(result);
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            var result = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                DiscountPrice = product.DiscountPrice,
                Color = product.Color,
                Favorite = false,
                Image = product.Image,
                Stock = product.Stock,
                Rating = product.Rating,
                ReviewCount = product.ReviewCount,
                Category = product.Category,
                DiscountPercent = product.DiscountPercent
            };
            await _context.Products.AddAsync(result);
            await _context.SaveChangesAsync();

            return Ok("Product Added");
        }

    }
}
