using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface IReservationService
    {
        void AddReservationService(ReservationServiceDto customer);
        List<ReservationServiceDto> GetReservationServiceByReservation(int id);
        List<ReservationServiceDto> GetAllReservationServices();
    }
}
