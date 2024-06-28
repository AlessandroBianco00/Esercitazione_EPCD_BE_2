using SneakersWebApp.Models;

namespace SneakersWebApp.Services
{
    public class SneakerService : ISneakerService
    {
        private static readonly List<Sneaker> sneakers = new List<Sneaker>();
        private static int lastId = 0;

        public void CreateSneaker(Sneaker sneaker)
        {
            sneaker.ProductId = ++lastId;
            sneakers.Add(sneaker);
        }

        public IEnumerable<Sneaker> GetAllProducts() => sneakers;

        public Sneaker GetById(int productId) =>
           sneakers.Single(s => s.ProductId == productId);
    }
}
