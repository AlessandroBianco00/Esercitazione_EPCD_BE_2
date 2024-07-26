using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Dto
{
    public class ReservationServiceDto
    {
        public int ReservationServiceId { get; set; }
        [Required]
        public int ReservationId_FK { get; set; }
        [Required]
        public int ServiceId_FK { get; set; }
        [Required]
        [Range(0, 10)]
        public int Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
