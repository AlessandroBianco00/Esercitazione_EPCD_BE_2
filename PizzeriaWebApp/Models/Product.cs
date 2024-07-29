﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWebApp.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required]
        public int DeliveryMinutes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}