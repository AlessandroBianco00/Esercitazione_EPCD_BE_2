using HotelWebApp.Dto;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Models
{
    public class CheckOutModel
    {
        public int ReservationId { get; set; }

        public int RoomNumber_FK { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal Deposit { get; set; }

        public decimal DailyCost { get; set; }

        public List<ServiceAddition> ServiceAdditionList { get; set; } = [];
    }
}
