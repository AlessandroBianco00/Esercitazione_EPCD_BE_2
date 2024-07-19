using PoliziaWebApp.Models;

namespace PoliziaWebApp.Interfaces
{
    public interface IVerbaleReportService
    {
        List<AnagraficaInt> GetTotaleVerbali();
        List<AnagraficaInt> GetTotalePunti();
        List<VerbaleEAnagrafica> GetDieciPunti();
        List<Verbale> GetMulte400();
    }
}
