using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface IReservationDao
    {
        void AddReservation(ReservationDto customer);
        ReservationDto GetReservationById(int id);
        List<ReservationDto> GetAllReservations();
    }
}
