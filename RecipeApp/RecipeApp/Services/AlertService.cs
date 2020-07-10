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
            return await Application.Current.MainPage.DisplayAlert(ResourceManager.GetString(Constants.Resource_Alert),
                    ResourceManager.GetString(questionResource),
                    ResourceManager.GetString(Constants.Resource_Yes),
                    ResourceManager.GetString(Constants.Resource_No));
        }
    }
}