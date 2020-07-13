using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        /// <summary>
        /// Binding an entry to a property of type decimal? only works if the device language is set to English
        /// (and probably other languages with decimal separator ".").
        /// If the device language is set to German, it is not possible to enter "0,5".
        /// "0," can be entered. When entering "5", "0" and "," disappear and are replaced by "5".
        /// (Using "." as decimal separator doesn't work either.)
        /// </summary>
        public string StringQuantity
        {
            get
            {
                var nullableQuantity = Ingredient?.Quantity;
                if (nullableQuantity == null)
                    return null;

                var quantity = (decimal)nullableQuantity;

                return quantity.ToString("0.##", CultureInfo.CurrentCulture);
            }
            set
            {
                if (Ingredient == null)
                    return;

                var quantity = string.IsNullOrEmpty(value) ? null : (decimal?)decimal.Parse(value);

                if (Ingredient.Quantity != quantity)
                {
                    Ingredient.Quantity = quantity;
                    RaisePropertyChanged();
                }
            }
        }

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