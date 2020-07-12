using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public class RecipeService : IRecipeService
    {
        private RecipeDbContext Context { get; }

        public RecipeService()
        {
            Context = new RecipeDbContext();

            Context.Database.EnsureCreated();
            // TODO: Switch to database with migrations
            //Context.Database.Migrate();
        }

        public async Task<List<Recipe>> GetRecipesAsync(string searchText = null, bool includeRelatedData = false)
        {
            IQueryable<Recipe> recipes = Context.Recipes;

            if (includeRelatedData)
                recipes = recipes.Include(recipe => recipe.Ingredients).Include(recipe => recipe.Directions);

            recipes = recipes.AsNoTracking();

            if (!string.IsNullOrEmpty(searchText))
                recipes = recipes.Where(recipe => recipe.Name.ToLower().Contains(searchText.ToLower()));

            return await recipes.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task SaveRecipeAsync(Recipe recipe)
        {
            if (!Context.Recipes.Any(r => r.Id == recipe.Id))
                Context.Recipes.Add(recipe);

            await Context.SaveChangesAsync();
        }

        public async Task SaveRecipesAsync(IEnumerable<Recipe> recipes)
        {
            Context.Recipes.AddRange(recipes);

            await Context.SaveChangesAsync();
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            return await Context.Recipes
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Directions)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteRecipeAsync(Recipe recipe)
        {
            Context.Recipes.Remove(recipe);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAllRecipesAsync()
        {
            // From a performance point of view ok, because the amount of recipes is very manageable
            Context.RemoveRange(Context.Recipes);

            await Context.SaveChangesAsync();
        }
    }
}