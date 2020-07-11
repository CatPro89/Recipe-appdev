using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class IngredientEditViewModel : BaseModel
    {
        public IngredientEditViewModel(Ingredient ingredient)
        {
            Ingredient = ingredient;
            UnitViewModels = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(unit => new UnitViewModel(unit)).ToList();
        }

        public Ingredient Ingredient
        {
            get
            {
                return ingredient;
            }
            set
            {
                if (ingredient != value)
                {
                    ingredient = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Ingredient ingredient;

        public IList<UnitViewModel> UnitViewModels
        {
            get
            {
                return unitViewModels;
            }
            set
            {
                if (unitViewModels != value)
                {
                    unitViewModels = value;
                    RaisePropertyChanged();
                }
            }
        }

        private IList<UnitViewModel> unitViewModels;

        public UnitViewModel SelectedUnitViewModel
        {
            get
            {
                return UnitViewModels.Single(unitViewModel => unitViewModel.Unit == Ingredient.Unit);
            }
            set
            {
                Ingredient.Unit = value?.Unit ?? Unit.Undefined;
                RaisePropertyChanged();
            }
        }

        public int SelectedIndex => UnitViewModels.IndexOf(SelectedUnitViewModel);

        private string DebuggerDisplay => $"{Ingredient?.Quantity} {Ingredient?.Unit} {Ingredient?.Name}";
    }
}