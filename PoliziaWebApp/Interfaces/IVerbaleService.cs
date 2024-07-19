using PoliziaWebApp.Models;

namespace PoliziaWebApp.Interfaces
{
    public interface IVerbaleService
    {
        void Create(Verbale verbale);
        public List<Verbale> GetAll();
    }
}
