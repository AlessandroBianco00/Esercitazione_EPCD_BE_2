using PizzeriaWebApp.Models.Entities;

namespace PizzeriaWebApp.Interfaces
{
    public interface IIngredientService
    {
        public Task<Ingredient> CreateIngredient(Ingredient ingredient);
        public Task<IEnumerable<Ingredient>> GetAll();
    }
}
