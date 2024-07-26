using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Models
{
    public class ServiceAddition
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10)]
        public int Quantity { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ReservationId_FK { get; set; }
    }
}
