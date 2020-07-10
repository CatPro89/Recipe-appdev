using RecipeApp.Models;
using System;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public class RecipeRowViewModel : BindableObject
    {
        public RecipeRowViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }

        public Recipe Recipe { get; set; }

        public ImageSource ImageSource
        {
            get
            {
                if (string.IsNullOrEmpty(Recipe.ImagePath))
                    return ImageSource.FromResource(Constants.Resource_NoImage);

                return ImageSource.FromFile(Recipe.ImagePath);
            }
        }

        public string PreparationTime => GetTimeSpanString(Recipe.PreparationTime);

        public string RestTime => GetTimeSpanString(Recipe.RestTime);

        public string BakingCookingTime => GetTimeSpanString(Recipe.BakingCookingTime);

        public string OverallTime => GetTimeSpanString(Recipe.OverallTime);

        private string GetTimeSpanString(TimeSpan timeSpan)
        {
            if (timeSpan.Hours > 0 && timeSpan.Minutes > 0)
                return $"{timeSpan.Hours}:{timeSpan.Minutes} h";

            if (timeSpan.Hours > 0)
                return $"{timeSpan.Hours} h";

            if (timeSpan.Minutes > 0)
                return $"{timeSpan.Minutes} min";

            return string.Empty;
        }
    }
}