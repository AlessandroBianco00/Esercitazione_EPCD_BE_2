using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Models.ViewModels;

namespace PizzeriaWebApp.Interfaces
{
    public interface IProductService
    {
        public string ConvertImage(IFormFile file);
        public Task<Product> CreateProduct(ProductViewModel model, IEnumerable<int> ingredients);
        public Task<IEnumerable<Product>> GetAll();
    }
}
