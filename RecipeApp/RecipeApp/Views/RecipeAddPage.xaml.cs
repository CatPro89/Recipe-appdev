using RecipeApp.Services;
using RecipeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeAddPage : ContentPage
    {
        public RecipeAddPage()
        {
            InitializeComponent();

            BindingContext = new RecipeAddViewModel(new RecipeService(), new AlertService());
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var recipeAddViewModel = (RecipeAddViewModel)BindingContext;

                recipeAddViewModel.BackCommand.Execute(null);
            });

            return true;
        }
    }
}