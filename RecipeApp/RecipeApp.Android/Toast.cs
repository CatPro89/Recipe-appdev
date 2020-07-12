using Android.App;
using Android.Widget;
using RecipeApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(RecipeApp.Droid.Toast))]

namespace RecipeApp.Droid
{
    public class Toast : IToast
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}