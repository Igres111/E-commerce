using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositories.ProductRepos
{
    public class IProductRepository : IProduct
    {
        private readonly Context _context;
        public IProductRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<GetProductDto>> GetProductsList()
        {
            var result = await _context.Products.ToListAsync();
            return result.Select(x => new GetProductDto
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DiscountPercent = x.DiscountPercent,
                PurchasedCount = x.PurchasedCount,
                CreateDate = x.CreateDate,
                Category = x.Category,
                Image = x.Image,
                Stock = x.Stock,
                Rating = x.Rating,
                ReviewCount = x.ReviewCount,
                Color = x.Color,
                DiscountPrice = x.Price
            }).ToList();
        }
        public async Task<List<GetProductDto>> GetDiscounted()
        {
            var result = await _context.Products
                    .OrderByDescending(x => x.DiscountPercent)
                    .Take(5)
                    .ToListAsync();
            return result.Select(x => new GetProductDto
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DiscountPercent = x.DiscountPercent,
                PurchasedCount = x.PurchasedCount,
                CreateDate = x.CreateDate,
                Category = x.Category,
                Image = x.Image,
                Stock = x.Stock,
                Rating = x.Rating,
                ReviewCount = x.ReviewCount,
                Color = x.Color,
                DiscountPrice = x.Price
            }).ToList();
        }
        public async Task<List<GetProductDto>> GetSeller()
        {
            var result = await _context.Products
              .OrderByDescending(x => x.PurchasedCount)
              .Take(4)
              .ToListAsync();
            return result.Select(x => new GetProductDto
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DiscountPercent = x.DiscountPercent,
                PurchasedCount = x.PurchasedCount,
                CreateDate = x.CreateDate,
                Category = x.Category,
                Image = x.Image,
                Stock = x.Stock,
                Rating = x.Rating,
                ReviewCount = x.ReviewCount,
                Color = x.Color,
                DiscountPrice = x.Price
            }).ToList();
        }
        public async Task<List<GetProductDto>> GetExplore()
        {
            var result = await _context.Products
                .OrderByDescending(x => x.CreateDate)
                .Take(8)
                .ToListAsync();
            return result.Select(x => new GetProductDto
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DiscountPercent = x.DiscountPercent,
                PurchasedCount = x.PurchasedCount,
                CreateDate = x.CreateDate,
                Category = x.Category,
                Image = x.Image,
                Stock = x.Stock,
                Rating = x.Rating,
                ReviewCount = x.ReviewCount,
                Color = x.Color,
                DiscountPrice = x.Price
            }).ToList();
        }
        public async Task AddProduct(AddProductDto product)
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
                CreateDate = DateTime.Now,
                PurchasedCount = product.PurchasedCount
            };
            await _context.Products.AddAsync(result);
            await _context.SaveChangesAsync();
        }
        public async Task<List<GetProductDto>> GetCategory(string category)
        {
            var result = _context.Products.Where(x => x.Category == category);
            return result.Select(x => new GetProductDto
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DiscountPercent = x.DiscountPercent,
                PurchasedCount = x.PurchasedCount,
                CreateDate = x.CreateDate,
                Category = x.Category,
                Image = x.Image,
                Stock = x.Stock,
                Rating = x.Rating,
                ReviewCount = x.ReviewCount,
                Color = x.Color,
                DiscountPrice = x.Price
            }).ToList();
        }
    }
}
