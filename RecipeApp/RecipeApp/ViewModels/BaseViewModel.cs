using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        protected void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        // TODO: Implement a proper NavigationService as described here:
        // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/navigation
        protected INavigation Navigation => Application.Current.MainPage.Navigation;
    }
}