using Microsoft.EntityFrameworkCore;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Controllers;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Models.ViewModels;

namespace PizzeriaWebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _ctx;

        public ProductService(DataContext dbContext)
        {
            _ctx = dbContext;
        }
        public string ConvertImage(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                string urlImg = $"data:image/jpeg;base64,{base64String}";

                return urlImg;
            }
        }

        public async Task<Product> CreateProduct(ProductViewModel model, IEnumerable<int> ingrIds)
        {
            var ingredients = await _ctx.Ingredients
                   .Where(i => ingrIds.Contains(i.IngredientId))
                   .ToListAsync();

            var product = new Product
            {
                ProductName = model.ProductName!,
                ProductImage = ConvertImage(model.ProductImage),
                Price = model.Price!,
                DeliveryMinutes = model.DeliveryMinutes!,
                Ingredients = ingredients
            };

            _ctx.Products.Add(product);
            await _ctx.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var list = await _ctx.Products.Include(p => p.Ingredients).ToListAsync();
            return list;
        }

    }
}
