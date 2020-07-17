using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class IngredientAddViewModel : BaseViewModel
    {
        public IngredientAddViewModel()
        {
            UnitViewModels = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(unit => new UnitViewModel(unit)).ToList();
        }

        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                if (order != value)
                {
                    order = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int order;

        /// <summary>
        /// Quantity is of type string because binding an entry to a property of type decimal? only works if the device language is set to English
        /// (and probably other languages with decimal separator ".").
        /// If the device language is set to German, it is not possible to enter "0,5".
        /// "0," can be entered. When entering "5", "0" and "," disappear and are replaced by "5".
        /// (Using "." as decimal separator doesn't work either.)
        /// </summary>
        public string Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string quantity;

        public Unit Unit
        {
            get
            {
                return unit;
            }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Unit unit;

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
                return UnitViewModels.Single(unitViewModel => unitViewModel.Unit == Unit);
            }
            set
            {
                Unit = value?.Unit ?? Unit.Undefined;
                RaisePropertyChanged();
            }
        }

        public int SelectedIndex => UnitViewModels.IndexOf(SelectedUnitViewModel);

        private string DebuggerDisplay => $"{Quantity} {Unit} {Name}";
    }
}