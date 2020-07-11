using RecipeApp.Services;
using RecipeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailsPage : TabbedPage
    {
        public RecipeDetailsPage(int recipeId)
        {
            InitializeComponent();

            RecipeId = recipeId;
        }

        private int RecipeId { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var recipeDetailsViewModel = new RecipeDetailsViewModel(new RecipeService(), Navigation, RecipeId);
            BindingContext = recipeDetailsViewModel;
            await recipeDetailsViewModel.Load();
        }

        void OnServingsChanged(object sender, ValueChangedEventArgs e)
        {
            var recipeDetailsViewModel = (RecipeDetailsViewModel)BindingContext;
            if (recipeDetailsViewModel == null)
                return;

            recipeDetailsViewModel.ChangeServingsCommand.Execute(e);
        }
    }
}