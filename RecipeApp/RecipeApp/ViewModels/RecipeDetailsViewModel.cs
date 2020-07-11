using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Services;
using RecipeApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeDetailsViewModel : BaseModel
    {
        public RecipeDetailsViewModel(IRecipeService recipeService, INavigation navigation, int recipeId)
        {
            RecipeService = recipeService;
            Navigation = navigation;
            RecipeId = recipeId;
            EditRecipeCommand = new Command(EditRecipe);
            DeleteRecipeCommand = new Command(DeleteRecipe);
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
                    OnPropertyChanged(nameof(PreparationTime));
                    OnPropertyChanged(nameof(RestTime));
                    OnPropertyChanged(nameof(BakingCookingTime));
                    OnPropertyChanged(nameof(OverallTime));

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
                    RaisePropertyChange();
                }
            }
        }

        private ObservableCollection<DirectionDetailsViewModel> directionDetailsViewModels;

        private IRecipeService RecipeService { get; set; }

        private INavigation Navigation { get; set; }

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
    }
}