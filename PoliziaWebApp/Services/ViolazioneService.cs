using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;
using System.Data.SqlClient;

namespace PoliziaWebApp.Services
{
    public class ViolazioneService : SqlServerServiceBase, IViolazioneService
    {
        public ViolazioneService(IConfiguration config) : base(config)
        {
        }

        private const string INSERT_COMMAND = "INSERT INTO Violazione(Descrizione) VALUES(@descrizione)";
        private const string SELECT_ALL_COMMAND = "SELECT IdViolazione, Descrizione FROM Violazione";

        public void Create(Violazione violazione)
        {
            try
            {
                var cmd = GetCommand(INSERT_COMMAND);
                cmd.Parameters.Add(new SqlParameter("@descrizione", violazione.Descrizione));
                var conn = GetConnection();
                conn.Open();
                var result = cmd.ExecuteNonQuery();
                conn.Close();
                if (result != 1) throw new Exception("Inserimento fallito");
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nella creazione del cliente", ex);
            }
        }

        public List<Violazione> GetAll()
        {
            var list = new List<Violazione>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(SELECT_ALL_COMMAND);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new Violazione
                    {
                        IdViolazione = reader.GetInt32(0),
                        Descrizione = reader.GetString(1),
                    });
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero clienti", ex);
            }
        }
    }
}
