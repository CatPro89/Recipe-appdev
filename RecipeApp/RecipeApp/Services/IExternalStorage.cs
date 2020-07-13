namespace RecipeApp.Services
{
    public interface IExternalStorage
    {
        string GetPath();
        string GetPicturesPath();
    }
}