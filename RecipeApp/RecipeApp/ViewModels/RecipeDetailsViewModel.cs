using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class RecipeDetailsViewModel : BaseViewModel
    {
        public RecipeDetailsViewModel(IRecipeService recipeService, int recipeId)
        {
            RecipeService = recipeService;
            RecipeId = recipeId;
            EditRecipeCommand = new Command(EditRecipe);
            DeleteRecipeCommand = new Command(DeleteRecipe);
            ChangeServingsCommand = new Command<ValueChangedEventArgs>(ChangeServings);
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
                    RaisePropertyChanged();

                    OnPropertyChanged(nameof(ImageSource));
                    OnPropertyChanged(nameof(PreparationTime));
                    OnPropertyChanged(nameof(RestTime));
                    OnPropertyChanged(nameof(BakingCookingTime));
                    OnPropertyChanged(nameof(OverallTime));

                    IngredientDetailsViewModels = new ObservableCollection<IngredientDetailsViewModel>(Recipe.Ingredients
                        .OrderBy(ingredient => ingredient.Order)
                        .Select(ingredient => new IngredientDetailsViewModel(ingredient)));

                    DirectionDetailsViewModels = new ObservableCollection<DirectionDetailsViewModel>(Recipe.Directions
                        .OrderBy(direction => direction.Order)
                        .Select(direction => new DirectionDetailsViewModel(direction)));
                }
            }
        }

        private Recipe recipe;

        public ImageSource ImageSource => ImageHelper.GetImageSource(Recipe?.ImagePath);

        public string PreparationTime => TimeSpanFormatter.Format(Recipe?.PreparationTime);

        public string RestTime => TimeSpanFormatter.Format(Recipe?.RestTime);

        public string BakingCookingTime => TimeSpanFormatter.Format(Recipe?.BakingCookingTime);

        public string OverallTime => TimeSpanFormatter.Format(Recipe?.OverallTime);

        public ObservableCollection<IngredientDetailsViewModel> IngredientDetailsViewModels
        {
            get
            {
                return ingredientDetailsViewModels;
            }
            set
            {
                if (ingredientDetailsViewModels != value)
                {
                    ingredientDetailsViewModels = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<IngredientDetailsViewModel> ingredientDetailsViewModels;

        public ObservableCollection<DirectionDetailsViewModel> DirectionDetailsViewModels
        {
            get
            {
                return directionDetailsViewModels;
            }
            set
            {
                if (directionDetailsViewModels != value)
                {
                    directionDetailsViewModels = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<DirectionDetailsViewModel> directionDetailsViewModels;

        private IRecipeService RecipeService { get; set; }

        private int RecipeId { get; set; }

        public async Task Load()
        {
            Recipe = await LoadRecipe(RecipeId);
            Recipe.PropertyChanged += Recipe_PropertyChanged;
        }

        private Task<Recipe> LoadRecipe(int recipeId)
        {
            return Task.Run(() =>
            {
                return RecipeService.GetRecipeAsync(recipeId);
            });
        }

        private void Recipe_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Recipe.ImagePath):
                    OnPropertyChanged(nameof(ImageSource));
                    break;
            }
        }

        public ICommand EditRecipeCommand { get; private set; }

        private async void EditRecipe()
        {
            await Navigation.PushModalAsync(new NavigationPage(new RecipeEditPage(RecipeId)));
        }

        public ICommand DeleteRecipeCommand { get; private set; }

        private async void DeleteRecipe()
        {
            await RecipeService.DeleteRecipeAsync(Recipe);

            await Navigation.PopAsync();
        }

        public ICommand ChangeServingsCommand { get; private set; }

        private void ChangeServings(ValueChangedEventArgs e)
        {
            if (e.OldValue == 0 || e.NewValue == 0 || IngredientDetailsViewModels == null)
                return;

            foreach (var ingredientDetailsViewModel in IngredientDetailsViewModels)
            {
                var oldValue = Convert.ToDecimal(e.OldValue);
                var newValue = Convert.ToDecimal(e.NewValue);
                ingredientDetailsViewModel.Ingredient.Quantity = ingredientDetailsViewModel.Ingredient.Quantity / oldValue * newValue;
            }
        }

        private string DebuggerDisplay => Recipe?.Name;
    }
}