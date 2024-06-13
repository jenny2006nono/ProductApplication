using Microsoft.AspNetCore.Mvc;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteProductController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public DeleteProductController(ShopDbContext context)
        {
            _context = context;
        }

        // DELETE: api/deleteproduct/
        [HttpPost("{productId}")]
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

                return StatusCode(1,$"商品編號{productId}已成功刪除");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"刪除商品失敗: {ex.Message}");
            }
        }
    }
}
