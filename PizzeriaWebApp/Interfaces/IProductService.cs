using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Models.ViewModels;

namespace PizzeriaWebApp.Interfaces
{
    public interface IProductService
    {
        public string ConvertImage(IFormFile file);
        public Task<Product> CreateProduct(ProductViewModel model);
        public Task<IEnumerable<Product>> GetAll();
    }
}
