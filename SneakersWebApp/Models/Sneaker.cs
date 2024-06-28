using System.ComponentModel.DataAnnotations;

namespace SneakersWebApp.Models
{
    public class Sneaker
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
    }
}
