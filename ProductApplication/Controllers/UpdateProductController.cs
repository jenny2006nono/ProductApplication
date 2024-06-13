using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;
using ProductApplication.ViewModel;

namespace ProductApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateProductController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public UpdateProductController(ShopDbContext context)
        {
            _context = context;
        }

        // PUT: api/updateproduct/productId
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateModel product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //搜尋商品
                var exProduct = _context.Product.Where(p =>p.ProductID == productId).FirstOrDefault();

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
    }

}
