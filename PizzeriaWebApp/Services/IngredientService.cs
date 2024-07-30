using Microsoft.EntityFrameworkCore;
using PizzeriaWebApp.Context;
using PizzeriaWebApp.Interfaces;
using PizzeriaWebApp.Models.Entities;
using PizzeriaWebApp.Models.ViewModels;

namespace PizzeriaWebApp.Services
{
    public class IngredientService : IIngredientService
    {

        private readonly DataContext _ctx;

        public IngredientService(DataContext dbContext)
        {
            _ctx = dbContext;
        }

        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            _ctx.Ingredients.Add(ingredient);
            await _ctx.SaveChangesAsync();
            return ingredient;
        }
        public async Task<IEnumerable<Ingredient>> GetAll()
        {
            var list = await _ctx.Ingredients.ToListAsync();
            return list;
        }
    }
}
