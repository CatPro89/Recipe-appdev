using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Resx;
using RecipeApp.Services;
using RecipeApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class RecipeListViewModel : BaseModel
    {
        public RecipeListViewModel(IRecipeService recipeService, INavigation navigation, IAlertService alertService)
        {
            RecipeService = recipeService;
            Navigation = navigation;
            AlertService = alertService;
            RecipeRowViewModels = new ObservableCollection<RecipeRowViewModel>();
            SearchCommand = new Command(Search);
            BackupCommand = new Command(Backup);
            RestoreCommand = new Command(Restore);
            AddRecipeCommand = new Command(AddRecipe);
            ShowRecipeDetailsCommand = new Command<int>(ShowRecipeDetails);
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    RaisePropertyChanged();
                    Search();
                }
            }
        }

        private string searchText;

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    RaisePropertyChanged();

                    OnPropertyChanged(nameof(IsLoaded));
                }
            }
        }

        private bool isLoading;

        public bool IsLoaded => !IsLoading;

        public ObservableCollection<RecipeRowViewModel> RecipeRowViewModels
        {
            get { return recipeRowViewModels; }

            set
            {
                if (recipeRowViewModels != value)
                {
                    recipeRowViewModels = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<RecipeRowViewModel> recipeRowViewModels;

        private IRecipeService RecipeService { get; set; }

        private INavigation Navigation { get; set; }

        private IAlertService AlertService { get; set; }

        public async Task Load()
        {
            IsLoading = true;

            var recipes = await LoadRecipes();
            RecipeRowViewModels = new ObservableCollection<RecipeRowViewModel>(recipes.Select(recipe => new RecipeRowViewModel(recipe)));

            IsLoading = false;
        }

        private Task<List<Recipe>> LoadRecipes()
        {
            return Task.Run(() =>
            {
                return RecipeService.GetRecipesAsync(SearchText);
            });
        }

        public ICommand SearchCommand { get; private set; }

        private async void Search()
        {
            await Load();
        }

        public ICommand BackupCommand { get; private set; }

        private async void Backup()
        {
            var recipes = await RecipeService.GetRecipesAsync(null, true);

            var jsonRecipes = recipes.Select(JsonMapper.Build);

            var json = JsonSerializer.Serialize(jsonRecipes, GetJsonSerializerOptions());

            File.WriteAllText(GetBackupPath(), json);
        }

        public ICommand RestoreCommand { get; private set; }

        private async void Restore()
        {
            var restore = await AlertService.DisplayQuestionAlert(nameof(AppResources.QuestionDeleteAndRestore));
            if (!restore)
                return;

            await RecipeService.DeleteAllRecipesAsync();

            var backupPath = GetBackupPath();

            if (!File.Exists(backupPath))
            {
                await AlertService.DisplayErrorAlert(nameof(AppResources.NoBackupFound));

                return;
            }

            var json = File.ReadAllText(backupPath);

            var jsonRecipes = JsonSerializer.Deserialize<IEnumerable<JsonRecipe>>(json, GetJsonSerializerOptions());

            var recipes = jsonRecipes.Select(JsonMapper.Parse);

            await RecipeService.SaveRecipesAsync(recipes);

            await Load();
        }

        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.Converters.Add(new JsonTimeSpanConverter());

            return jsonSerializerOptions;
        }

        private string GetBackupPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.BackupFileName);
        }

        public ICommand AddRecipeCommand { get; private set; }

        private async void AddRecipe()
        {
            await Navigation.PushModalAsync(new NavigationPage(new RecipeEditPage()));
        }

        public ICommand ShowRecipeDetailsCommand { get; private set; }

        private async void ShowRecipeDetails(int recipeId)
        {
            await Navigation.PushAsync(new RecipeDetailsPage(recipeId));
        }

        private string DebuggerDisplay => $"{SearchText} {RecipeRowViewModels?.Count}";
    }
}