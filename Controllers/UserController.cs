using E_commerce.Data;
using E_commerce.Models;
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
        public UserController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var result = await _context.Users.ToListAsync();
            return Ok(result);
        }
    }
}
