using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductApplication.Models;
using System.Linq;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchProductController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public SearchProductController(ShopDbContext context)
        {
            _context = context;
        }

        //查詢商品
        // GET: api/searchproduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(int productID)
        {
            try
            {
                if (productID <= 0)
                {
                    return BadRequest("請輸入正確的商品編號");
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(p => p.ProductID == productID);

                if (product == null)
                {
                    return NotFound($"商品編號{productID}不存在");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "查詢失敗");
            }
        }
    }
}
