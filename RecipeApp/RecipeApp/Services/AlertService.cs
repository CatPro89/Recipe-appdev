using RecipeApp.Resx;
using System.Resources;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeApp.Services
{
    public class AlertService : IAlertService
    {
        public AlertService()
        {
            ResourceManager = new ResourceManager(typeof(AppResources));
        }

        private ResourceManager ResourceManager { get; set; }

        public async Task<bool> DisplayQuestionAlert(string questionResource)
        {
            return await Application.Current.MainPage.DisplayAlert(ResourceManager.GetString(nameof(AppResources.Alert)),
                    ResourceManager.GetString(questionResource),
                    ResourceManager.GetString(nameof(AppResources.Yes)),
                    ResourceManager.GetString(nameof(AppResources.No)));
        }

        public async Task DisplayErrorAlert(string errorMessageResource)
        {
            await Application.Current.MainPage.DisplayAlert(ResourceManager.GetString(nameof(AppResources.Error)),
                    ResourceManager.GetString(errorMessageResource),
                    ResourceManager.GetString(nameof(AppResources.Ok)));
        }

        public void DisplayToast(string messageResource)
        {
            DependencyService.Get<IToast>().Show(ResourceManager.GetString(messageResource));
        }
    }
}