using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using VisualizzaVerbaliWebApp.Models;

namespace VisualizzaVerbaliWebApp.Services
{
    public class VerbaleService : SqlServerServiceBase, IVerbaleService
    {
        public VerbaleService(IConfiguration config) : base(config)
        {
        }
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

        public IEnumerable<Verbale> GetAll()
        {
            using var conn = GetConnection();
            conn.Open();
            using var cmd = GetCommand("SELECT [IdVerbale], [DataViolazione], [IndirizzoViolazione], [Nominativo_Agente], [DataTrascrizioneVerbale], [Importo], [DecurtamentoPunti] FROM Verbale");
            using var reader = cmd.ExecuteReader();
            var list = new List<Verbale>();
            while (reader.Read())
                list.Add(Create(reader));
            conn.Close();
            return list;
        }

        public Verbale GetVerbale(int id)
        {
            var cmd = GetCommand("SELECT [IdVerbale], [DataViolazione], [IndirizzoViolazione], [Nominativo_Agente], [DataTrascrizioneVerbale], [Importo], [DecurtamentoPunti], [IdAnagrafico_FK] [IdViolazione_FK] FROM Verbale");
            cmd.Parameters.Add(new SqlParameter("@id", id));
            using var conn = GetConnection();
            conn.Open();
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return Create(reader);
            throw new Exception("Non trovato");
        }
    }
}
