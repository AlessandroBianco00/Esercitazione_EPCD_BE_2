using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;
using System.Data.SqlClient;

namespace PoliziaWebApp.Services
{
    public class VerbaleService : SqlServerServiceBase, IVerbaleService
    {
        public VerbaleService(IConfiguration config) : base(config)
        {
        }

        private const string INSERT_COMMAND = "INSERT INTO Verbale(DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafico_FK, IdViolazione_FK) VALUES(@dataviolazione, @indirizzoViolazione, @nominativoAgente, @dataTrascrizioneVerbale, @importo, @decurtamentoPunti, @idAnagrafica, @idViolazione)";
        private const string SELECT_ALL_COMMAND = "SELECT IdVerbale, DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafico_FK, IdViolazione_FK FROM Verbale";

        public void Create(Verbale verbale)
        {
            try
            {
                var cmd = GetCommand(INSERT_COMMAND);
                cmd.Parameters.Add(new SqlParameter("@dataviolazione", verbale.DataViolazione));
                cmd.Parameters.Add(new SqlParameter("@indirizzoViolazione", verbale.IndirizzoViolazione));
                cmd.Parameters.Add(new SqlParameter("@nominativoAgente", verbale.Nominativo_Agente));
                cmd.Parameters.Add(new SqlParameter("@dataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale));
                cmd.Parameters.Add(new SqlParameter("@importo", verbale.Importo));
                cmd.Parameters.Add(new SqlParameter("@decurtamentoPunti", verbale.DecurtamentoPunti));
                cmd.Parameters.Add(new SqlParameter("@idAnagrafica", verbale.IdAnagrafico_FK));
                cmd.Parameters.Add(new SqlParameter("@idViolazione", verbale.IdViolazione_FK));
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

        public List<Verbale> GetAll()
        {
            var list = new List<Verbale>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(SELECT_ALL_COMMAND);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new Verbale
                    {
                        IdVerbale = reader.GetInt32(0),
                        DataViolazione = reader.GetDateTime(1),
                        IndirizzoViolazione = reader.GetString(2),
                        Nominativo_Agente = reader.GetString(3),
                        DataTrascrizioneVerbale = reader.GetDateTime(4),
                        Importo = reader.GetDecimal(5),
                        DecurtamentoPunti = reader.GetInt32(6),
                        IdAnagrafico_FK = reader.GetInt32(7),
                        IdViolazione_FK = reader.GetInt32(8),

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
