using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using HotelWebApp.Models;
using HotelWebApp.Services.Dao;
using System.Data.SqlClient;

namespace HotelWebApp.Services
{
    public class ReservationQueryService : BaseDao, IReservationQueryService
    {
        public ReservationQueryService(IConfiguration configuration) : base(configuration) { }

        private const string SELECT_BY_FC_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations WHERE FiscalCode_FK = @fiscalCode";
        private const string SELECT_FULLBOARD_COMMAND = "SELECT ReservationId, BookingDate, CheckInDate, CheckOutDate, Deposit, DailyCost, RoomNumber_FK, FiscalCode_FK, [Type], [Year], YearProgressiveNumber FROM Reservations WHERE [Type] = 2";
        private const string RESERVATION_CHECKOUT_COMMAND = "SELECT RoomNumber_FK, CheckInDate, CheckOutDate, DailyCost, Deposit FROM Reservations WHERE ReservationId = @reservationId";
        private const string SERVICES_CHECKOUT_COMMAND = "SELECT s.[Description], SUM(Quantity) AS TotalQuantity, s.Price FROM ReservationServices AS r INNER JOIN [Services] as s ON s.ServiceId = r.ServiceId_FK WHERE r.ReservationId_FK = @reservationId GROUP BY s.[Description], s.Price";

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

        public ReservationDto GetReservationCheckOut(int id)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(RESERVATION_CHECKOUT_COMMAND, conn);
                cmd.Parameters.AddWithValue("@reservationId", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return new ReservationDto
                {
                    RoomNumber_FK = reader.GetInt32(0),
                    CheckInDate = reader.GetDateTime(1),
                    CheckOutDate = reader.GetDateTime(2),
                    DailyCost = reader.GetDecimal(3),
                    Deposit = reader.GetDecimal(4),
                    
                };
                throw new Exception("Reservation with id = {reservationId} not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero prenotazione", ex);
            }
        }

        public List<ServiceAddition> GetServicesCheckOut(int id)
        {
            var list = new List<ServiceAddition>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SERVICES_CHECKOUT_COMMAND, conn);
                cmd.Parameters.AddWithValue("@reservationId", id);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new ServiceAddition
                    {
                        Description = reader.GetString(0),
                        Quantity = reader.GetInt32(1),
                        Price = reader.GetDecimal(2),
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
