using RecipeApp.Helpers;
using RecipeApp.Models;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeRowViewModel : BindableObject
    {
        public RecipeRowViewModel(Recipe recipe)
        {
            Recipe = recipe;
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
                }
            }
        }

        private Recipe recipe;

        public ImageSource ImageSource => ImageHelper.GetImageSource(Recipe.ImagePath);

        public string PreparationTime => TimeSpanFormatter.Format(Recipe.PreparationTime);

        public string RestTime => TimeSpanFormatter.Format(Recipe.RestTime);

        public string BakingCookingTime => TimeSpanFormatter.Format(Recipe.BakingCookingTime);

        public string OverallTime => TimeSpanFormatter.Format(Recipe.OverallTime);
    }
}