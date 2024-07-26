using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using HotelWebApp.Services.Dao;
using System.Data.SqlClient;

namespace HotelWebApp.Services
{
    public class ReservationQueryService : BaseDao, IReservationQueryService
    {
        public ReservationQueryService(IConfiguration configuration) : base(configuration) { }

        private const string SELECT_BY_FC_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations WHERE FiscalCode_FK = @fiscalCode";
        private const string SELECT_FULLBOARD_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations WHERE [Type] = 2";

        public List<ReservationDto> GetReservationByFC(string fc)
        {
            var list = new List<ReservationDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_FC_COMMAND, conn);
                cmd.Parameters.AddWithValue("@fiscalCode", fc);
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

        public List<ReservationDto> GetReservationFullBoard()
        {
            var list = new List<ReservationDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_FULLBOARD_COMMAND, conn);
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
    }
}
