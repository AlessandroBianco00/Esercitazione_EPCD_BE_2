using HotelWebApp.Dto;
using HotelWebApp.Models;

namespace HotelWebApp.Interfaces
{
    public interface IReservationQueryService
    {
        List<ReservationDto> GetReservationByFC(string fc);
        List<ReservationDto> GetReservationFullBoard();
        ReservationDto GetReservationCheckOut(int id);
        List<ServiceAddition> GetServicesCheckOut(int id);
    }
}
