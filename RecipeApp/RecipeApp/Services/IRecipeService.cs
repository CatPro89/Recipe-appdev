using RecipeApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public interface IRecipeService
    {
        Task DeleteRecipeAsync(Recipe recipe);

        Task<Recipe> GetRecipeAsync(int id);

        Task<List<Recipe>> GetRecipesAsync(string searchText = null);

        Task SaveRecipeAsync(Recipe recipe);
    }
}