using E_commerce.Data;
using E_commerce.DTOs.ProductDtos;
using E_commerce.Models;
using E_commerce.Repositories.ProductRepos;
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
        public readonly IProduct _methods;
        public ProductController(Context context, IProduct methods)
        {
            _context = context;
            _methods = methods;
        }
        [HttpGet("All-Products")]
        public async Task<IActionResult> GetProducts()
        {
            var list = await _methods.GetProductsList();
            return Ok(list);
        }
        [HttpGet("Get-Product/Discount")]
        public async Task<IActionResult> GetProductDiscount()
        {
            var list = await _methods.GetDiscounted();
            return Ok(list);
        }
        [HttpGet("Get-Product/Seller")]
        public async Task<IActionResult> GetProductSeller()
        {
            var list = await _methods.GetSeller();
            return Ok(list);
        }
        [HttpGet("Get-Product/Explore")]
        public async Task<IActionResult> GetProductExplore()
        {
            var list = await _methods.GetExplore();
            return Ok(list);
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            await _methods.AddProduct(product);
            return Ok("Product Added");
        }
        [HttpGet("Get-Product")]
        public async Task<IActionResult> GetProductCategory(string category)
        {
            var list = await _methods.GetCategory(category);
            return Ok(list);
        }
        [HttpPost("Billing")]
        public async Task<IActionResult> Billing(BillingProductsDto bill)
        {
            var addBilling = _context.BillingInfos.Add(new BillingInfo
            {
                Id = Guid.NewGuid(),
                Name= bill.Name,
                CompanyName = bill.CompanyName,
                AddressDetails = bill.AddressDetails,
                UserId = bill.UserId,
                Address = bill.Address,
                City = bill.City,
                PhoneNumber = bill.PhoneNumber,
                Email = bill.Email,
                PurchaseDate = DateTime.Now
            });
            for (var i = 0; i < bill.ProductsList.Count; i++) 
            {
                var userToProduct = _context.UserForProducts.Add(new UserForProduct
                {
                    Id = Guid.NewGuid(),
                    ProductId = bill.ProductsList[i].Id,
                    PurchaseDate = DateTime.Now,
                    UserId = bill.UserId,
                    Quantity = bill.ProductsList[i].Quantity
                });
                var productForUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == bill.ProductsList[i].Id);
                if (productForUpdate != null)
                {
                    productForUpdate.Stock -= 1;
                    productForUpdate.PurchasedCount += 1;
                }
            }
            return Ok();
        }
    }
}
