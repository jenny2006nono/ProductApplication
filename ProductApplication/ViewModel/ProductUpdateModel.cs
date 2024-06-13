using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ProductApplication.ViewModel
{
    public class ProductUpdateModel
    {
        [Key]
        public int ProductID { get; set; }   // 商品ID

        public string? ProductName { get; set; }   // 商品名稱

        public string? Description { get; set; }   // 商品描述


        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }   // 價格


        public int? Quantity { get; set; }   // 庫存數量

        public int? CategoryID { get; set; }// 商品分類ID，預設1

        public DateTime DateTime { get; set; }    // 新增日期，默認為當前時間

        public DateTime UpdateTime { get; set; } = DateTime.Now;   // 更新日期，更新時自動更改

        public bool? IsActive { get; set; }   // 是否上架，預設上架
    }
}
