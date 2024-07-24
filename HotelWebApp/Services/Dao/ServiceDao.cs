using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using System.Data.SqlClient;

namespace HotelWebApp.Services.Dao
{
    public class ServiceDao : BaseDao, IServiceDao
    {
        public ServiceDao(IConfiguration configuration) : base(configuration) { }

        private const string INSERT_COMMAND = "INSERT INTO [Services]([Description], Price) VALUES( @description, @price)";
        private const string SELECT_ALL_COMMAND = "SELECT ServiceId, [Description], Price FROM [Services]";
        private const string SELECT_BY_ID_COMMAND = "SELECT ServiceId, [Description], Price FROM [Services] ServiceId = @serviceId";

        public void AddService(ServiceDto customer)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_COMMAND, conn);
                cmd.Parameters.Add(new SqlParameter("@description", customer.Description));
                cmd.Parameters.Add(new SqlParameter("@type", customer.Price));
                var result = cmd.ExecuteNonQuery();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione del stanze", ex);
            }
        }

        public List<ServiceDto> GetAllServices()
        {
            var list = new List<ServiceDto>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_COMMAND, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new ServiceDto
                    {
                        ServiceId = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Price = reader.GetDecimal(2)
                    });
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero stanze", ex);
            }
        }

        public ServiceDto GetServiceById(int id)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_ID_COMMAND, conn);
                cmd.Parameters.AddWithValue("@serviceId", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return new ServiceDto
                {
                    ServiceId = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                };
                throw new Exception("Room with id = {roomNumber} not found");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero stanza", ex);
            }
        }
    }
}
