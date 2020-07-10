using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionButton : Button
    {
        public FloatingActionButton()
        {
            InitializeComponent();
        }
    }
}