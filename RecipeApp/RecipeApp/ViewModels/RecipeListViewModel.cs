using RecipeApp.Models;
using RecipeApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeListViewModel : BindableObject
    {
        public RecipeListViewModel(IRecipeService recipeService)
        {
            RecipeService = recipeService;
            Recipes = new ObservableCollection<Recipe>();
        }

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                    OnPropertyChanged(nameof(IsLoaded));
                }
            }
        }

        private bool isLoading;

        public bool IsLoaded
        {
            get
            {
                return !IsLoading;
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get { return recipes; }

            set
            {
                if (recipes != value)
                {
                    recipes = value;
                    OnPropertyChanged(nameof(Recipes));
                }
            }
        }

        private ObservableCollection<Recipe> recipes;

        private IRecipeService RecipeService { get; set; }

        public async Task Load()
        {
            IsLoading = true;

            Recipes = new ObservableCollection<Recipe>(await LoadRecipes());

            IsLoading = false;
        }

        private Task<List<Recipe>> LoadRecipes()
        {
            return Task.Run(() =>
            {
                return RecipeService.GetRecipesAsync();
            });
        }
    }
}