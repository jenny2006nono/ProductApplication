using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateProductController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public CreateProductController(ShopDbContext context)
        {
            _context = context;
        }
        //新建商品
        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            try
            {
                // 檢查欄位
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //商品金額
                if (product.Price<=0)
                {
                    return StatusCode(500, "商品金額不得低於0");
                }

                // 設置日期時間
                product.DateTime = DateTime.Now;
                product.UpdateTime = DateTime.Now;

                // 新增到資料庫
                _context.Product.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "新增商品失敗");
            }
        }
        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }

    
}
