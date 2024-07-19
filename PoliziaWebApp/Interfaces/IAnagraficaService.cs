using PoliziaWebApp.Models;

namespace PoliziaWebApp.Interfaces
{
    public interface IAnagraficaService
    {
        void Create(Anagrafica anagrafica);
        public List<Anagrafica> GetAll();
    }
}
