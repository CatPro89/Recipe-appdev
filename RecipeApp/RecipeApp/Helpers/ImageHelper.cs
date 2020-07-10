using Xamarin.Forms;

namespace RecipeApp.Helpers
{
    public static class ImageHelper
    {
        public static ImageSource GetImageSource(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return ImageSource.FromResource(Constants.Resource_NoImage);

            return ImageSource.FromFile(imagePath);
        }
    }
}