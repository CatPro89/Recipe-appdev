using Plugin.Media;
using Plugin.Media.Abstractions;
using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Resx;
using RecipeApp.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeEditViewModel : BaseModel
    {
        public RecipeEditViewModel(IRecipeService recipeService, INavigation navigation, IAlertService alertService, int? recipeId)
        {
            RecipeService = recipeService;
            Navigation = navigation;
            AlertService = alertService;
            RecipeId = recipeId;
            SelectImageCommand = new Command(async () => await SelectImage());
            SaveRecipeCommand = new Command(async () => await SaveRecipe());
            BackCommand = new Command(async () => await Back());
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

        private IAlertService AlertService { get; set; }

        private int? RecipeId { get; set; }

        public async Task Load()
        {
            Recipe = RecipeId == null ? new Recipe() : await LoadRecipe((int)RecipeId);
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

        public ICommand SelectImageCommand { get; private set; }

        private async Task SelectImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await AlertService.DisplayErrorAlert(nameof(AppResources.PickPhotoNotSupported));

                return;
            }

            var mediaOptions = new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await AlertService.DisplayErrorAlert(nameof(AppResources.NoPhotoPicked));

                return;
            }

            Recipe.ImagePath = ImageHelper.CopyImage(selectedImageFile.Path);
        }

        public ICommand SaveRecipeCommand { get; private set; }

        private async Task SaveRecipe()
        {
            await RecipeService.SaveRecipeAsync(Recipe);

            await Navigation.PopModalAsync();
        }

        public ICommand BackCommand { get; private set; }

        private async Task Back()
        {
            var saveChanges = await AlertService.DisplayQuestionAlert(nameof(AppResources.QuestionSaveChanges));

            if (saveChanges)
            {
                await SaveRecipe();
            }

            await Navigation.PopModalAsync();
        }
    }
}