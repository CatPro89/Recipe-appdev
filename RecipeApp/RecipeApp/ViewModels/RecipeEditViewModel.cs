﻿using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeEditViewModel : BindableObject
    {
        public RecipeEditViewModel(IRecipeService recipeService, INavigation navigation, int? recipeId)
        {
            RecipeService = recipeService;
            Navigation = navigation;
            RecipeId = recipeId;
            SaveRecipeCommand = new Command(SaveRecipe);
        }

        public Recipe Recipe
        {
            get
            {
                return recipe;
            }
            set
            {
                if (recipe != value)
                {
                    recipe = value;

                    OnPropertyChanged(nameof(Recipe));
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        private Recipe recipe;

        public ImageSource ImageSource => ImageHelper.GetImageSource(Recipe?.ImagePath);

        private IRecipeService RecipeService { get; set; }

        private INavigation Navigation { get; set; }

        private int? RecipeId { get; set; }

        public async Task Load()
        {
            Recipe = RecipeId == null ? new Recipe() : await LoadRecipe((int)RecipeId);
        }

        private Task<Recipe> LoadRecipe(int recipeId)
        {
            return Task.Run(() =>
            {
                return RecipeService.GetRecipeAsync(recipeId);
            });
        }

        public ICommand SaveRecipeCommand { get; private set; }

        private async void SaveRecipe()
        {
            await RecipeService.SaveRecipeAsync(Recipe);

            await Navigation.PopModalAsync();
        }
    }
}