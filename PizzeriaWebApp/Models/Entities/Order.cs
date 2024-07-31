using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWebApp.Models.Entities
{
    public enum Status
    {
        Incomplete = 1,
        Concluded = 2,
        Processed = 3
    }
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        [StringLength(70)]
        public string Address { get; set; }
        [Required]
        public string Notes { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public List<OrderItem> Products { get; set; }
    }
}
