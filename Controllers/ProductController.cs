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
        [HttpGet("Get-Product/Category")]
        public async Task<IActionResult> GetProductCategory(string category)
        {
            var list = await _methods.GetCategory(category);
            return Ok(list);
        }
        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _methods.AddProduct(product);
            return Ok("Product Added");
        }
        [HttpPost("Billing")]
        public async Task<IActionResult> Billing(BillingProductsDto bill)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _methods.BillingProducts(bill);
            return Ok();
        }
        [HttpGet("Billing-Info")]
        public async Task<IActionResult> GetBillingInfo()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = await _methods.GetBillingInfo();
            return Ok(list);
        }
    }
}
