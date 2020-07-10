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

            var recipeListViewModel = new RecipeListViewModel(new RecipeService(), Navigation);
            BindingContext = recipeListViewModel;
            await recipeListViewModel.Load();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var recipeListViewModel = (RecipeListViewModel)BindingContext;
            var recipeRowViewModel = e.Item as RecipeRowViewModel;

            if (recipeRowViewModel != null)
            {
                recipeListViewModel.ShowRecipeDetailsCommand.Execute(recipeRowViewModel.Recipe.Id);
            }
        }
    }
}