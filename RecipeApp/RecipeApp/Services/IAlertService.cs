using System.Threading.Tasks;

namespace RecipeApp.Services
{
    public interface IAlertService
    {
        Task<bool> DisplayQuestionAlert(string questionResource);
    }
}