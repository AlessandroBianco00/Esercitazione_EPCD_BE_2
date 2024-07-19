using PoliziaWebApp.Interfaces;

namespace PoliziaWebApp.Services
{
    public class DbContext
    {
        public IAnagraficaService Anagrafica { get; set; }
        public IVerbaleService Verbale { get; set; }
        public IViolazioneService Violazione { get; set;}

        public DbContext(IAnagraficaService anagraficaDao, IVerbaleService verbaleDao, IViolazioneService violazioneDao)
        {
            Anagrafica = anagraficaDao;
            Verbale = verbaleDao;
            Violazione = violazioneDao;
        }
    }
}
