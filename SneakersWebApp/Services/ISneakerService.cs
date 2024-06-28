using SneakersWebApp.Models;

namespace SneakersWebApp.Services
{
    public interface ISneakerService
    {
        public void CreateSneaker(Sneaker sneaker);
        public IEnumerable<Sneaker> GetAllProducts();
        public Sneaker GetById(int productId);
    }
}
