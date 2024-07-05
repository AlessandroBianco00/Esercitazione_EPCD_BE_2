using VisualizzaVerbaliWebApp.Models;

namespace VisualizzaVerbaliWebApp.Services
{
    public interface IVerbaleService
    {
        void AddVerbale(Verbale impiegato);
        IEnumerable<Verbale> GetAll();
        Verbale GetVerbale(int id);
        void AggiornaVerbale(int id, Verbale impiegato);
        void CancellaVerbale(int id);
    }
}
