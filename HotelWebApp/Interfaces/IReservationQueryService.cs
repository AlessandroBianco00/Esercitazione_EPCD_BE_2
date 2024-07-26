using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface IReservationQueryService
    {
        List<ReservationDto> GetReservationByFC(string fc);
        List<ReservationDto> GetReservationFullBoard();
    }
}
