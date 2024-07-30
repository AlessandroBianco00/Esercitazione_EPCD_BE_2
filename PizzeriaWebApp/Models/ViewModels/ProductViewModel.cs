using PizzeriaWebApp.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWebApp.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }
        [Required]
        public IFormFile ProductImage { get; set; }
        [Required]
        [Range(1, 120)]
        public int DeliveryMinutes { get; set; }
    }
}
