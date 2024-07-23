using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Dto
{
    public enum ReservationType
    {
        HalfBoard = '1',        
        FullBoard = '2',        
        BedAndBreakfast = '3'   
    }
    public class ReservationDto
    {
        public int ReservationId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public decimal Deposit { get; set; }

        [Required]
        public decimal DailyCost { get; set; }

        [Required]
        public int RoomNumber_FK { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve essere lungo 16 caratteri.")]
        public string FiscalCode_FK { get; set; }

        [Required]
        public ReservationType Type { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int YearProgressiveNumber { get; set; }
    }
}
