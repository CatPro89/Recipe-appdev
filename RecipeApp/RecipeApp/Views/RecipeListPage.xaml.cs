using RecipeApp.Services;
using RecipeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListPage : ContentPage
    {
        public RecipeListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var recipeListViewModel = new RecipeListViewModel(new RecipeService());
            BindingContext = recipeListViewModel;
            await recipeListViewModel.Load();
        }
    }
}