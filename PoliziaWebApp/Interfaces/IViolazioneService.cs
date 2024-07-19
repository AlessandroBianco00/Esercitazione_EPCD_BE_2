using PoliziaWebApp.Models;

namespace PoliziaWebApp.Interfaces
{
    public interface IViolazioneService
    {
        void Create(Violazione violazione);
        public List<Violazione> GetAll();
    }
}
