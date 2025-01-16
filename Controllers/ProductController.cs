﻿using E_commerce.Data;
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
        public async Task<IActionResult> GetProducts()
        {
            var result = await _context.Products.ToListAsync();
            return Ok(result);
        }
        [HttpGet("Get-Product/Discount")]
        public async Task<IActionResult> GetProductDiscount()
        {
            var result = await _context.Products
                .OrderByDescending(x => x.DiscountPercent)
                .Take(5)
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("Get-Product/Seller")]
        public async Task<IActionResult> GetProductSeller()
        {
            var result = await _context.Products
                .OrderByDescending(x => x.PurchasedCount)
                .Take(4)
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("Get-Product/Explore")]
        public async Task<IActionResult> GetProductExplore()
        {
            var result = await _context.Products
                .OrderByDescending(x => x.CreateDate)
                .Take(8)
                .ToListAsync();
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
                Image = product.Image,
                Stock = product.Stock,
                Rating = product.Rating,
                ReviewCount = product.ReviewCount,
                Category = product.Category,
                DiscountPercent = product.DiscountPercent,
                CreateDate = DateTime.Now
            };
            await _context.Products.AddAsync(result);
            await _context.SaveChangesAsync();

            return Ok("Product Added");
        }
        [HttpGet("Get-Product/{category}")]
        public async Task<IActionResult> GetCategory(string category)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Category == category);
            return Ok(result);
        }
    }
}
