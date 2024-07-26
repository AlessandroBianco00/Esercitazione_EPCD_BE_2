using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using System.Data.SqlClient;

namespace HotelWebApp.Services.Dao
{
    public class ReservationDao : BaseDao, IReservationDao
    {
        public ReservationDao(IConfiguration configuration) : base(configuration) { }

        private const string YEAR_NUMBER = "SELECT MAX(YearProgressiveNumber) AS PreviousYearNumber FROM Reservations WHERE [Year]= YEAR(GETDATE());";
        private const string INSERT_COMMAND = "INSERT INTO Reservations(BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber) VALUES(@bookingDate, @checkInDate, @checkOutDate, @deposit, @dailyCost, @roomNumber, @fiscalCode, @type, @year, @yearProgNum)";
        private const string SELECT_ALL_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations";
        private const string SELECT_BY_ID_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations WHERE ReservationId = @id";
        private const string DELETE_COMMAND = "DELETE FROM Reservations WHERE ReservationId = @id";

        public void AddReservation(ReservationDto reservation)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd_year = new SqlCommand(YEAR_NUMBER, conn);
                var year_prog = cmd_year.ExecuteScalar();
                var result_year = year_prog != DBNull.Value ? (int)year_prog : 1;
                using var cmd = new SqlCommand(INSERT_COMMAND, conn);
                cmd.Parameters.Add(new SqlParameter("@bookingDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@checkInDate", reservation.CheckInDate));
                cmd.Parameters.Add(new SqlParameter("@checkOutDate", reservation.CheckOutDate));
                cmd.Parameters.Add(new SqlParameter("@deposit", reservation.Deposit));
                cmd.Parameters.Add(new SqlParameter("@dailyCost", reservation.DailyCost));
                cmd.Parameters.Add(new SqlParameter("@roomNumber", reservation.RoomNumber_FK));
                cmd.Parameters.Add(new SqlParameter("@fiscalCode", reservation.FiscalCode_FK));
                cmd.Parameters.Add(new SqlParameter("@type", reservation.Type));
                cmd.Parameters.Add(new SqlParameter("@year", reservation.CheckInDate.Year));
                cmd.Parameters.Add(new SqlParameter("@yearProgNum", result_year + 1 ));
                var result = cmd.ExecuteNonQuery();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione della prenotazione", ex);
            }
        }

        public List<ReservationDto> GetAllReservations()
        {
            var list = new List<ReservationDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_COMMAND, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new ReservationDto
                    {
                        ReservationId = reader.GetInt32(0),
                        BookingDate = reader.GetDateTime(1),
                        CheckInDate = reader.GetDateTime(2),
                        CheckOutDate = reader.GetDateTime(3),
                        Deposit = reader.GetDecimal(4),
                        DailyCost = reader.GetDecimal(5),
                        RoomNumber_FK = reader.GetInt32(6),
                        FiscalCode_FK = reader.GetString(7),
                        Type = (ReservationType)reader.GetInt32(8),
                        Year = reader.GetInt32(9),
                        YearProgressiveNumber = reader.GetInt32(10)
                    });
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero prenotazioni", ex);
            }
        }

        public ReservationDto GetReservationById(int id)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_ID_COMMAND, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return new ReservationDto
                {
                    ReservationId = reader.GetInt32(0),
                    BookingDate = reader.GetDateTime(1),
                    CheckInDate = reader.GetDateTime(2),
                    CheckOutDate = reader.GetDateTime(3),
                    Deposit = reader.GetDecimal(4),
                    DailyCost = reader.GetDecimal(5),
                    RoomNumber_FK = reader.GetInt32(6),
                    FiscalCode_FK = reader.GetString(7),
                    Type = (ReservationType)reader.GetInt32(8),
                    Year = reader.GetInt32(9),
                    YearProgressiveNumber = reader.GetInt32(10)
                };
                throw new Exception("Reservation with id = {id} not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero prenotazione", ex);
            }
        }
    }
}
