using RecipeApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetRecipesAsync(string searchText = null, bool includeRelatedData = false);

        Task SaveRecipeAsync(Recipe recipe);

        Task SaveRecipesAsync(IEnumerable<Recipe> recipes);

        Task<Recipe> GetRecipeAsync(int id);

        Task DeleteRecipeAsync(Recipe recipe);

        Task DeleteAllRecipesAsync();
    }
}