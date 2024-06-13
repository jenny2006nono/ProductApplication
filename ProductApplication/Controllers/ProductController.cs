using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;
using ProductApplication.ViewModel;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ShopDbContext _context;

        public ProductController(ShopDbContext context)
        {
            _context = context;
        }

        //新建商品
        // POST: api/CreateProduct
        [HttpPost("Create")]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                // 檢查欄位
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //商品金額
                if (product.Price <= 0)
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
        
        // GET: api/products
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

        [HttpGet("Search/{productId}")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(int productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return BadRequest("請輸入正確的商品編號");
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(p => p.ProductID == productId);

                if (product == null)
                {
                    return NotFound($"商品編號{productId}不存在");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "查詢失敗");
            }
        }

        [HttpPost("Update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateModel product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //搜尋商品
                var exProduct = _context.Product.Where(p => p.ProductID == productId).FirstOrDefault();

                if (exProduct == null)
                {
                    return NotFound($"商品編號{productId}不存在");
                }

                // 更新商品資訊
                exProduct.ProductName = product.ProductName ?? exProduct.ProductName;
                exProduct.Description = product.Description ?? exProduct.Description;
                exProduct.Price = product.Price ?? exProduct.Price;
                exProduct.Quantity = product.Quantity ?? exProduct.Quantity;
                exProduct.CategoryID = product.CategoryID ?? exProduct.CategoryID;
                exProduct.UpdateTime = DateTime.Now;
                exProduct.IsActive = product.IsActive ?? exProduct.IsActive;

                _context.Product.Update(exProduct);
                await _context.SaveChangesAsync();

                return Ok(exProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "更新商品失敗");
            }
        }

        [HttpPost("Delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                var product = await _context.Product.FindAsync(productId);
                if (product == null)
                {
                    return NotFound($"找不到商品編號 {productId}");
                }

                _context.Product.Remove(product);
                await _context.SaveChangesAsync();

                return StatusCode(1, $"商品編號{productId}已成功刪除");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"刪除商品失敗: {ex.Message}");
            }
        }
    }
}
