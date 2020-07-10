using RecipeApp.Models;
using RecipeApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeListViewModel : BindableObject
    {
        public RecipeListViewModel(IRecipeService recipeService)
        {
            RecipeService = recipeService;
            RecipeRowViewModels = new ObservableCollection<RecipeRowViewModel>();
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

        public ObservableCollection<RecipeRowViewModel> RecipeRowViewModels
        {
            get { return recipeRowViewModels; }

            set
            {
                if (recipeRowViewModels != value)
                {
                    recipeRowViewModels = value;
                    OnPropertyChanged(nameof(RecipeRowViewModels));
                }
            }
        }

        private ObservableCollection<RecipeRowViewModel> recipeRowViewModels;

        private IRecipeService RecipeService { get; set; }

        public async Task Load()
        {
            IsLoading = true;

            var recipes = await LoadRecipes();
            RecipeRowViewModels = new ObservableCollection<RecipeRowViewModel>(recipes.Select(recipe => new RecipeRowViewModel(recipe)));

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