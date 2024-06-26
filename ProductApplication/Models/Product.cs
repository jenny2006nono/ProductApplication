﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ProductApplication.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }   // 商品ID

        [Required(ErrorMessage = "商品名稱未填寫")] 
        [StringLength(255)]
        public string ProductName { get; set; }   // 商品名稱

        public string Description { get; set; }   // 商品描述

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }   // 價格

        [Required]
        public int Quantity { get; set; }   // 庫存數量

        public int? CategoryID { get; set; } = 1;// 商品分類ID，預設1

        public DateTime DateTime { get; set; } = DateTime.Now;   // 新增日期，默認為當前時間

        public DateTime UpdateTime { get; set; } = DateTime.Now;   // 更新日期，更新時自動更改

        [Required]
        public bool IsActive { get; set; } = true;   // 是否上架，預設上架
    }
}
