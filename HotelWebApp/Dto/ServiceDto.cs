using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Dto
{
    public class ServiceDto
    {
        public int ServiceId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
