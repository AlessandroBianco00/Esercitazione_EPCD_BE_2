using HotelWebApp.Dto;
using HotelWebApp.Interfaces;

namespace HotelWebApp.Services.Dao
{
    public class ReservationDao : BaseDao, IReservationDao
    {
        public ReservationDao(IConfiguration configuration) : base(configuration) { }

        private const string INSERT_COMMAND = "INSERT INTO Reservations(BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber) VALUES(@bookingDate, @checkInDate, @checkOutDate, @deposit, @dailyCost, @roomNumber, @fiscalCode, @type, @year, @yearProgNum)";
        private const string SELECT_ALL_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations";
        private const string SELECT_BY_ID_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations WHERE ReservationId = @id";
        private const string DELETE_COMMAND = "DELETE FROM Reservations WHERE ReservationId = @id";

        public void AddReservation(ReservationDto customer)
        {
            throw new NotImplementedException();
        }

        public List<ReservationDto> GetAllReservations()
        {
            throw new NotImplementedException();
        }

        public ReservationDto GetReservationById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
