using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWebApp.Models.Entities
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
        [Column(TypeName = "nvarchar(max)")]
        public string ProductImage { get; set; }
        [Required]
        public int DeliveryMinutes { get; set; }
        public List<OrderItem> Orders { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
