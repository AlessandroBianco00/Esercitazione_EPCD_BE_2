using PoliziaWebApp.Interfaces;
using PoliziaWebApp.Models;

namespace PoliziaWebApp.Services
{
    public class VerbaleReportService : SqlServerServiceBase, IVerbaleReportService
    {
        public VerbaleReportService(IConfiguration config) : base(config)
        {
        }

        public const string TOTALE_VERBALI_COMMAND = "SELECT a.IdAnagrafica, a.Nome,  a.Cognome, a.Indirizzo, a.Città, a.CAP, a.Cod_Fisc, COUNT(v.IdVerbale) AS TotaleVerbali  FROM  Verbale AS v RIGHT JOIN  Anagrafica AS a ON v.IdAnagrafico_FK = a.IdAnagrafica GROUP BY  a.IdAnagrafica, a.Nome,  a.Cognome, a.Indirizzo, a.Città, a.CAP, a.Cod_Fisc ORDER BY  Cognome ASC";
        public const string TOTALE_PUNTI_COMMAND = "SELECT a.IdAnagrafica, a.Nome,  a.Cognome, a.Indirizzo, a.Città, a.CAP, a.Cod_Fisc, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati  FROM  Verbale AS v RIGHT JOIN  Anagrafica AS a ON v.IdAnagrafico_FK = a.IdAnagrafica GROUP BY  a.IdAnagrafica, a.Nome,  a.Cognome, a.Indirizzo, a.Città, a.CAP, a.Cod_Fisc ORDER BY  TotalePuntiDecurtati DESC";
        public const string DIECI_PUNTI_COMMAND = "SELECT v.Importo, a.Nome, a.Cognome, v.DataViolazione, v.DecurtamentoPunti FROM Verbale AS v INNER JOIN Anagrafica AS a ON a.IdAnagrafica = v.IdAnagrafico_FK WHERE v.DecurtamentoPunti > 10";
        public const string MULTA_400_COMMAND = "SELECT IdVerbale, DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafico_FK, IdViolazione_FK FROM Verbale WHERE Importo > 400";

        public List<VerbaleEAnagrafica> GetDieciPunti()
        {
            var list = new List<VerbaleEAnagrafica>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(DIECI_PUNTI_COMMAND);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new VerbaleEAnagrafica
                    {
                        Importo = reader.GetDecimal(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        DataViolazione = reader.GetDateTime(3),
                        DecurtamentoPunti = reader.GetInt32(4),
                    });
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero clienti", ex);
            }
        }

        public List<Verbale> GetMulte400()
        {
            var list = new List<Verbale>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(MULTA_400_COMMAND);
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

        public List<AnagraficaInt> GetTotalePunti()
        {
            var list = new List<AnagraficaInt>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(TOTALE_PUNTI_COMMAND);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new AnagraficaInt
                    {
                        IdAnagrafica = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        Indirizzo = reader.GetString(3),
                        Città = reader.GetString(4),
                        CAP = reader.GetString(5),
                        Cod_Fisc = reader.GetString(6),
                        queryValue = reader.IsDBNull(7) ? 0 : reader.GetInt32(7)
                    });
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore nel recupero clienti", ex);
            }
        }

        public List<AnagraficaInt> GetTotaleVerbali()
        {
            var list = new List<AnagraficaInt>();
            try

            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = GetCommand(TOTALE_VERBALI_COMMAND);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(new AnagraficaInt
                    {
                        IdAnagrafica = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        Indirizzo = reader.GetString(3),
                        Città = reader.GetString(4),
                        CAP = reader.GetString(5),
                        Cod_Fisc = reader.GetString(6),
                        queryValue = reader.GetInt32(7),
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
