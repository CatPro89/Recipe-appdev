using RecipeApp.Services;
using RecipeApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeEditPage : TabbedPage
    {
        public RecipeEditPage(int? recipeId = null)
        {
            InitializeComponent();

            RecipeId = recipeId;
        }

        private int? RecipeId { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var recipeEditViewModel = new RecipeEditViewModel(new RecipeService(), Navigation, new AlertService(), RecipeId);
            BindingContext = recipeEditViewModel;
            await recipeEditViewModel.Load();
        }

        void OnServingsChanged(object sender, ValueChangedEventArgs e)
        {
            var recipeEditViewModel = (RecipeEditViewModel)BindingContext;
            if (recipeEditViewModel == null)
                return;

            recipeEditViewModel.ChangeServingsCommand.Execute(e);
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var recipeEditViewModel = (RecipeEditViewModel)BindingContext;

                recipeEditViewModel.BackCommand.Execute(null);
            });

            return true;
        }
    }
}