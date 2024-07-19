using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using VisualizzaVerbaliWebApp.Models;

namespace VisualizzaVerbaliWebApp.Services
{
    public class VerbaleService : IVerbaleService
    {
        private readonly SqlConnection _connection;
        public VerbaleService(IConfiguration config)
        {
            _connection = new SqlConnection(config.GetConnectionString("Verbali"));
        }
        //public VerbaleService(IConfiguration config) : base(config){}
        public void AddVerbale(Verbale impiegato)
        {
            throw new NotImplementedException();
        }
        private Verbale Create(DbDataReader reader)
        {
            return new Verbale
            {
                IdVerbale = reader.GetInt32(0),
                DataViolazione = reader.GetDateTime(1),
                IndirizzoViolazione = reader.GetString(2),
                Nominativo_Agente = reader.GetString(3),
                DataTrascrizioneVerbale = reader.GetDateTime(4),
                Importo = reader.GetDecimal(5),
                DecurtamentoPunti = reader.GetInt32(6),
                //IdAnagrafico_FK = reader.GetInt32(7),
                //IdViolazione_FK = reader.GetInt32(8),
            };
        }

        public void AggiornaVerbale(int id, Verbale impiegato)
        {
            throw new NotImplementedException();
        }

        public void CancellaVerbale(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Verbale> GetAll(decimal importo = 0)
        {
            try
            {
                _connection.Open();
                var query = "SELECT v.IdVerbale, v.Nominativo_Agente, v.DataViolazione, v.IndirizzoViolazione, v.DataTrascrizione, v.Importo, v.DecurtamentoPunti, a.Cognome, a.Nome, a.Cod_Fisc, a.Città, z.Descrizione" +
                    "FROM Verbale AS v INNER JOIN Anagrafica AS a ON v.IdAnagrafico_FK = a.IdAnagrafica INNER JOIN Violazione AS z ON v.IdViolazione_FK = z.IdViolazione" +
                    "WHERE Importo >= @importo";
                using var cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@importo", importo);
                using var reader = cmd.ExecuteReader();
                var list = new List<Verbale>();
                while (reader.Read())
                {
                    list.Add(new Verbale
                    {
                        IdVerbale = reader.GetInt32(0),
                        DataViolazione = reader.GetDateTime(1),
                        IndirizzoViolazione = reader.GetString(2),
                        Nominativo_Agente = reader.GetString(3),
                        DataTrascrizioneVerbale = reader.GetDateTime(4),
                        Importo = reader.GetDecimal(5),
                        DecurtamentoPunti = reader.GetInt32(6)
                    });
                }
                return list;
            }
            catch
            {
                return [];
            }
            finally
            {
                _connection.Close();
            }
        }

        public Verbale GetVerbale(int id)
        {
            try
            {
                _connection.Open();
                var query = "SELECT v.IdVerbale, v.Nominativo_Agente, v.DataViolazione, v.IndirizzoViolazione, v.DataTrascrizione, v.Importo, v.DecurtamentoPunti, a.Cognome, a.Nome, a.Cod_Fisc, a.Città, z.Descrizione" +
                    "FROM Verbale AS v INNER JOIN Anagrafica AS a ON v.IdAnagrafico_FK = a.IdAnagrafica INNER JOIN Violazione AS z ON v.IdViolazione_FK = z.IdViolazione" +
                    "WHERE IdVerbale = @id";
                using var cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                var list = new List<Verbale>();
                if (reader.Read())
                    return Create(reader);
                throw new Exception("Non trovato");
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
