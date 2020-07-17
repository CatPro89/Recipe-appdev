using Plugin.Media;
using Plugin.Media.Abstractions;
using RecipeApp.Controls;
using RecipeApp.Helpers;
using RecipeApp.Models;
using RecipeApp.Resx;
using RecipeApp.Services;
using RecipeApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class RecipeAddViewModel : BaseViewModel
    {
        public RecipeAddViewModel(IRecipeService recipeService, IAlertService alertService)
        {
            RecipeService = recipeService;
            AlertService = alertService;

            Steps = new ObservableCollection<Step>
            {
                new Step { Number = 1, Text = new ResourceManager(typeof(AppResources)).GetString(nameof(AppResources.BasicData)) },
                new Step { Number = 2, Text = new ResourceManager(typeof(AppResources)).GetString(nameof(AppResources.Ingredients)) },
                new Step { Number = 3, Text = new ResourceManager(typeof(AppResources)).GetString(nameof(AppResources.Directions)) }
            };
            CurrentStepNumber = 1;
            Servings = 1;
            IngredientAddViewModels = new ObservableCollection<IngredientAddViewModel>
            {
                new IngredientAddViewModel { Order = 1 }
            };
            DirectionAddViewModels = new ObservableCollection<DirectionAddViewModel>
            {
                new DirectionAddViewModel { Order = 1 }
            };

            ProceedWithIngredientsCommand = new Command(ProceedWithIngredients);
            GoBackToBasicDataCommand = new Command(GoBackToBasicData);
            ProceedWithDirectionsCommand = new Command(ProceedWithDirections);
            GoBackToIngredientsCommand = new Command(GoBackToIngredients);
            SelectImageCommand = new Command(async () => await SelectImage());
            SaveRecipeCommand = new Command(async () => await SaveRecipe());
            BackCommand = new Command(async () => await Back());
            AddIngredientCommand = new Command(AddIngredient);
            DecreaseIngredientOrderCommand = new Command<IngredientAddViewModel>(DecreaseIngredientOrder);
            IncreaseIngredientOrderCommand = new Command<IngredientAddViewModel>(IncreaseIngredientOrder);
            DeleteIngredientCommand = new Command<IngredientAddViewModel>(DeleteIngredient);
            AddDirectionCommand = new Command(AddDirection);
            DecreaseDirectionOrderCommand = new Command<DirectionAddViewModel>(DecreaseDirectionOrder);
            IncreaseDirectionOrderCommand = new Command<DirectionAddViewModel>(IncreaseDirectionOrder);
            DeleteDirectionCommand = new Command<DirectionAddViewModel>(DeleteDirection);
        }

        public ObservableCollection<Step> Steps
        {
            get
            {
                return steps;
            }
            set
            {
                if (steps != value)
                {
                    steps = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<Step> steps;

        public int CurrentStepNumber
        {
            get
            {
                return currentStepNumber;
            }
            set
            {
                if (currentStepNumber != value)
                {
                    currentStepNumber = value;
                    RaisePropertyChanged();

                    OnPropertyChanged(nameof(IsBasicDataVisible));
                    OnPropertyChanged(nameof(AreIngredientsVisible));
                    OnPropertyChanged(nameof(AreDirectionsVisible));
                }
            }
        }

        private int currentStepNumber;

        public bool IsBasicDataVisible => CurrentStepNumber == 1;

        public bool AreIngredientsVisible => CurrentStepNumber == 2;

        public bool AreDirectionsVisible => CurrentStepNumber == 3;

        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    RaisePropertyChanged();

                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        private string imagePath;

        public ImageSource ImageSource => ImageHelper.GetImageSource(ImagePath);

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string name;

        public int Servings
        {
            get
            {
                return servings;
            }
            set
            {
                if (servings != value)
                {
                    servings = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int servings;

        public TimeSpan PreparationTime
        {
            get
            {
                return preparationTime;
            }
            set
            {
                if (preparationTime != value)
                {
                    preparationTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan preparationTime;

        public TimeSpan RestTime
        {
            get
            {
                return restTime;
            }
            set
            {
                if (restTime != value)
                {
                    restTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan restTime;

        public TimeSpan BakingCookingTime
        {
            get
            {
                return bakingCookingTime;
            }
            set
            {
                if (bakingCookingTime != value)
                {
                    bakingCookingTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan bakingCookingTime;

        public TimeSpan OverallTime
        {
            get
            {
                return overallTime;
            }
            set
            {
                if (overallTime != value)
                {
                    overallTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan overallTime;

        public ObservableCollection<IngredientAddViewModel> IngredientAddViewModels
        {
            get
            {
                return ingredientAddViewModels;
            }
            set
            {
                if (ingredientAddViewModels != value)
                {
                    ingredientAddViewModels = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<IngredientAddViewModel> ingredientAddViewModels;

        public ObservableCollection<DirectionAddViewModel> DirectionAddViewModels
        {
            get
            {
                return directionAddViewModels;
            }
            set
            {
                if (directionAddViewModels != value)
                {
                    directionAddViewModels = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<DirectionAddViewModel> directionAddViewModels;

        private IRecipeService RecipeService { get; set; }

        private IAlertService AlertService { get; set; }

        public ICommand ProceedWithIngredientsCommand { get; private set; }

        private void ProceedWithIngredients()
        {
            CurrentStepNumber = 2;
        }

        public ICommand GoBackToBasicDataCommand { get; private set; }

        private void GoBackToBasicData()
        {
            CurrentStepNumber = 1;
        }

        public ICommand ProceedWithDirectionsCommand { get; private set; }

        private void ProceedWithDirections()
        {
            CurrentStepNumber = 3;
        }

        public ICommand GoBackToIngredientsCommand { get; private set; }

        private void GoBackToIngredients()
        {
            CurrentStepNumber = 2;
        }

        public ICommand SelectImageCommand { get; private set; }

        private async Task SelectImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await AlertService.DisplayErrorAlert(nameof(AppResources.PickPhotoNotSupported));

                return;
            }

            var mediaOptions = new PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 1080
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                await AlertService.DisplayErrorAlert(nameof(AppResources.NoPhotoPicked));

                return;
            }

            ImagePath = ImageHelper.CopyImage(selectedImageFile.Path);
        }

        public ICommand SaveRecipeCommand { get; private set; }

        private async Task SaveRecipe()
        {
            var recipe = new Recipe
            {
                Name = Name,
                ImagePath = ImagePath,
                Servings = Servings,
                PreparationTime = PreparationTime,
                RestTime = RestTime,
                BakingCookingTime = BakingCookingTime,
                OverallTime = OverallTime,
                Ingredients = IngredientAddViewModels.Select(CreateIngredient).ToList(),
                Directions = DirectionAddViewModels.Select(CreateDirection).ToList()
            };

            await RecipeService.SaveRecipeAsync(recipe);

            // Work around because RecipeListView.OnAppearing() isn't called as expected after Navigation.PopModalAsync().
            // MediaPlugin for some reason triggers RecipeListPage.OnAppearing() when returning from gallery.
            // Everything works as expected when CrossMedia.Current.PickPhotoAsync() isn't called. 
            var recipeListPage = Navigation.NavigationStack.Last() as RecipeListPage;
            if (recipeListPage != null)
            {
                var recipeListViewModel = recipeListPage.BindingContext as RecipeListViewModel;
                await recipeListViewModel.Load();
            }

            await Navigation.PopModalAsync();
        }

        private Ingredient CreateIngredient(IngredientAddViewModel ingredientAddViewModel)
        {
            return new Ingredient
            {
                Order = ingredientAddViewModel.Order,
                Quantity = string.IsNullOrEmpty(ingredientAddViewModel.Quantity) ? null : (decimal?)decimal.Parse(ingredientAddViewModel.Quantity),
                Unit = ingredientAddViewModel.SelectedUnitViewModel.Unit,
                Name = ingredientAddViewModel.Name
            };
        }

        private Direction CreateDirection(DirectionAddViewModel directionAddViewModel)
        {
            return new Direction
            {
                Order = directionAddViewModel.Order,
                Description = directionAddViewModel.Description
            };
        }

        public ICommand BackCommand { get; private set; }

        private async Task Back()
        {
            await SuggestSaveAndReturnToPreviousPage();
        }

        private async Task SuggestSaveAndReturnToPreviousPage()
        {
            var saveChanges = await AlertService.DisplayQuestionAlert(nameof(AppResources.QuestionSaveChanges));

            if (saveChanges)
            {
                await SaveRecipe();
            }

            await Navigation.PopModalAsync();
        }

        public ICommand AddIngredientCommand { get; private set; }

        private void AddIngredient()
        {
            var order = IngredientAddViewModels.Any() ? IngredientAddViewModels.Max(ingredientAddViewModel => ingredientAddViewModel.Order) + 1 : 1;
            IngredientAddViewModels.Add(new IngredientAddViewModel { Order = order });
        }

        public ICommand DecreaseIngredientOrderCommand { get; private set; }

        private void DecreaseIngredientOrder(IngredientAddViewModel ingredientAddViewModel)
        {
            if (ingredientAddViewModel.Order > 1)
            {
                var predecessor = IngredientAddViewModels.Single(i => i.Order == ingredientAddViewModel.Order - 1);

                predecessor.Order++;
                ingredientAddViewModel.Order--;

                var index = IngredientAddViewModels.IndexOf(ingredientAddViewModel);
                IngredientAddViewModels.Move(index, index - 1);
            }
        }

        public ICommand IncreaseIngredientOrderCommand { get; private set; }

        private void IncreaseIngredientOrder(IngredientAddViewModel ingredientAddViewModel)
        {
            if (ingredientAddViewModel.Order < IngredientAddViewModels.Count)
            {
                var successor = IngredientAddViewModels.Single(i => i.Order == ingredientAddViewModel.Order + 1);

                successor.Order--;
                ingredientAddViewModel.Order++;

                var index = IngredientAddViewModels.IndexOf(ingredientAddViewModel);
                IngredientAddViewModels.Move(index, index + 1);
            }
        }

        public ICommand DeleteIngredientCommand { get; private set; }

        private void DeleteIngredient(IngredientAddViewModel ingredientAddViewModel)
        {
            var successors = IngredientAddViewModels.Where(d => d.Order > ingredientAddViewModel.Order);

            foreach (var successor in successors)
            {
                successor.Order--;
            }

            IngredientAddViewModels.Remove(ingredientAddViewModel);
        }

        public ICommand AddDirectionCommand { get; private set; }

        private void AddDirection()
        {
            var order = DirectionAddViewModels.Any() ? DirectionAddViewModels.Max(directionAddViewModel => directionAddViewModel.Order) + 1 : 0;
            DirectionAddViewModels.Add(new DirectionAddViewModel { Order = order });
        }

        public ICommand DecreaseDirectionOrderCommand { get; private set; }

        private void DecreaseDirectionOrder(DirectionAddViewModel directionAddViewModel)
        {
            if (directionAddViewModel.Order > 1)
            {
                var predecessor = DirectionAddViewModels.Single(i => i.Order == directionAddViewModel.Order - 1);

                predecessor.Order++;
                directionAddViewModel.Order--;

                var index = DirectionAddViewModels.IndexOf(directionAddViewModel);
                DirectionAddViewModels.Move(index, index - 1);
            }
        }

        public ICommand IncreaseDirectionOrderCommand { get; private set; }

        private void IncreaseDirectionOrder(DirectionAddViewModel directionAddViewModel)
        {
            if (directionAddViewModel.Order < DirectionAddViewModels.Count)
            {
                var successor = DirectionAddViewModels.Single(d => d.Order == directionAddViewModel.Order + 1);

                successor.Order--;
                directionAddViewModel.Order++;

                var index = DirectionAddViewModels.IndexOf(directionAddViewModel);
                DirectionAddViewModels.Move(index, index + 1);
            }
        }

        public ICommand DeleteDirectionCommand { get; private set; }

        private void DeleteDirection(DirectionAddViewModel directionAddViewModel)
        {
            var successors = DirectionAddViewModels.Where(d => d.Order > directionAddViewModel.Order);

            foreach (var successor in successors)
            {
                successor.Order--;
            }

            DirectionAddViewModels.Remove(directionAddViewModel);
        }

        private string DebuggerDisplay => Name;
    }
}