using Android.OS;
using RecipeApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(RecipeApp.Droid.ExternalStorage))]

namespace RecipeApp.Droid
{
    public class ExternalStorage : IExternalStorage
    {
        public string GetPath()
        {
            return GetPath(null);
        }

        public string GetPicturesPath()
        {
            return GetPath(Environment.DirectoryPictures);
        }

        private string GetPath(string type)
        {
            return Android.App.Application.Context.GetExternalFilesDir(type).Path;
        }
    }
}