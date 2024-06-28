using SneakersWebApp.Models;

namespace SneakersWebApp.Services
{
    public class SneakerService : ISneakerService
    {
        private static readonly List<Sneaker> sneakers = new List<Sneaker>() { new Sneaker { ProductName = "Air Jordan I", ProductDescription = "La scarpa da cui è nato tutto, la Air Jordan 1 è probabilmente il più amato modello di sempre, così come quello che più ha rivoluzionato la storia delle sneakers.", ProductId = 0, Price = 110 }  };
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
