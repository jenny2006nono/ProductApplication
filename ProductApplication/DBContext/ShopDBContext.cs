using Microsoft.EntityFrameworkCore;
using ProductApplication.Models;


public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
    }

    // 定義資料表
    public DbSet<Product> Product { get; set; }
}

//[Table("Product", Schema = "dbo")]