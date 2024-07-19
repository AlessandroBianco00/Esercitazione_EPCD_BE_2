using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;
using System.Data.SqlClient;

namespace PoliziaWebApp.Services
{
    public class AnagraficaService : SqlServerServiceBase, IAnagraficaService
    {
        public AnagraficaService(IConfiguration config) : base(config)
        {
        }
        private const string INSERT_COMMAND = "INSERT INTO Anagrafica(Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) VALUES(@cognome, @nome, @indirizzo, @citta, @cap, @codicefiscale)";
        private const string SELECT_ALL_COMMAND = "SELECT IdAnagrafica, Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc FROM Anagrafica";

        public void Create(Anagrafica anagrafica)
        {
            try
            {
                var cmd = GetCommand(INSERT_COMMAND);
                cmd.Parameters.Add(new SqlParameter("@cognome", anagrafica.Cognome));
                cmd.Parameters.Add(new SqlParameter("@nome", anagrafica.Nome));
                cmd.Parameters.Add(new SqlParameter("@indirizzo", anagrafica.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("@citta", anagrafica.Città));
                cmd.Parameters.Add(new SqlParameter("@cap", anagrafica.CAP));
                cmd.Parameters.Add(new SqlParameter("@codicefiscale", anagrafica.Cod_Fisc));
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

        public List<Anagrafica> GetAll()
        {
            var list = new List<Anagrafica>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(SELECT_ALL_COMMAND);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new Anagrafica
                    {
                        IdAnagrafica = reader.GetInt32(0),
                        Cognome = reader.GetString(1),
                        Nome = reader.GetString(2),
                        Indirizzo = reader.GetString(3),
                        Città = reader.GetString(4),
                        CAP = reader.GetString(5),
                        Cod_Fisc = reader.GetString(6)
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
