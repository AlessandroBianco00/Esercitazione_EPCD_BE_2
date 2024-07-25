using HotelWebApp.Dto;
using HotelWebApp.Interfaces;
using System.Data.SqlClient;

namespace HotelWebApp.Services.Dao
{
    public class RoomDao : BaseDao, IRoomDao
    {
        public RoomDao(IConfiguration configuration) : base(configuration) { }

        private const string INSERT_COMMAND = "INSERT INTO Rooms([Description], [Type]) VALUES(@description, @type)";
        private const string SELECT_ALL_COMMAND = "SELECT RoomNumber, [Description], [Type] FROM Rooms";
        private const string SELECT_BY_ID_COMMAND = "SELECT RoomNumber, [Description], [Type] FROM Rooms WHERE RoomNumber = @roomNumber";

        public void AddRoom(RoomDto customer)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(INSERT_COMMAND, conn);
                cmd.Parameters.Add(new SqlParameter("@description", customer.Description));
                cmd.Parameters.Add(new SqlParameter("@type", customer.Type));
                var result = cmd.ExecuteNonQuery();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione del stanze", ex);
            }
        }

        public List<RoomDto> GetAllRooms()
        {
            var list = new List<RoomDto>();
            try
            {
                var conn = new SqlConnection(connectionString);
                conn.Open();
                var cmd = new SqlCommand(SELECT_ALL_COMMAND, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new RoomDto
                    {
                        RoomNumber = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Type = reader.GetChar(2)
                    });
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero stanze", ex);
            }
        }

        public RoomDto GetRoomByNumber(int roomNumber)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_BY_ID_COMMAND, conn);
                cmd.Parameters.AddWithValue("@id", roomNumber);
                using var reader = cmd.ExecuteReader();
                if (reader.Read()) return new RoomDto   
                {
                    RoomNumber = reader.GetInt32(0),
                    Description = reader.GetString(1),
                    Type = reader.GetChar(2)
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
