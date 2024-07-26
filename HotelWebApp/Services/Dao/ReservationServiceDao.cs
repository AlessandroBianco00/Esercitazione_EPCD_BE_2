using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using System.Data.SqlClient;

namespace HotelWebApp.Services.Dao
{
    public class ReservationServiceDao : BaseDao, IReservationService
    {
        public ReservationServiceDao(IConfiguration configuration) : base(configuration) { }

        private const string INSERT_COMMAND = "INSERT INTO [ReservationServices]([ReservationId_FK], ServiceId_FK, Quantity, [Date]) VALUES( @reservationId, @serviceId, @quantity, @date)";
        private const string SELECT_ALL_COMMAND = "SELECT ReservationServiceId, ReservationId_FK, ServiceId_FK, Quantity, [Date] FROM ReservationServices";
        private const string SELECT_BY_RESERVATION_COMMAND = "SELECT ReservationServiceId, ReservationId_FK, ServiceId_FK, Quantity, [Date] FROM ReservationServices WHERE ReservationId_FK = @reservationId";

        public void AddReservationService(ReservationServiceDto reservationService)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_COMMAND, conn);
                cmd.Parameters.Add(new SqlParameter("@reservationId", reservationService.ReservationId_FK));
                cmd.Parameters.Add(new SqlParameter("@serviceId", reservationService.ServiceId_FK));
                cmd.Parameters.Add(new SqlParameter("@quantity", reservationService.Quantity));
                cmd.Parameters.Add(new SqlParameter("@date", reservationService.Date));
                var result = cmd.ExecuteNonQuery();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione del servizio prenotazione", ex);
            }
        }

        public List<ReservationServiceDto> GetAllReservationServices()
        {
            var list = new List<ReservationServiceDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_COMMAND, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new ReservationServiceDto
                    {
                        ReservationServiceId = reader.GetInt32(0),
                        ReservationId_FK = reader.GetInt32(1),
                        ServiceId_FK = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        Date = reader.GetDateTime(4)
                    });
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero servizi prenotazioni", ex);
            }
        }

        public List<ReservationServiceDto> GetReservationServiceByReservation(int id)
        {
            var list = new List<ReservationServiceDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_RESERVATION_COMMAND, conn);
                cmd.Parameters.AddWithValue("@reservationId", id);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new ReservationServiceDto
                    {
                        ReservationServiceId = reader.GetInt32(0),
                        ReservationId_FK = reader.GetInt32(1),
                        ServiceId_FK = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        Date = reader.GetDateTime(4)
                    });
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero servizi prenotazioni", ex);
            }
        }
    }
}
