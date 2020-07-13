using RecipeApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class IngredientDetailsViewModel : BaseModel
    {
        public IngredientDetailsViewModel(Ingredient ingredient)
        {
            Ingredient = ingredient;
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

                    OnPropertyChanged(nameof(UnitDisplayName));
                }
            }
        }

        private Ingredient ingredient;

        /// <summary>
        /// When binding to a property of type decimal?, all values are displayed with at least one decimal place.
        /// Integer only values should be displayed without decimal places.
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
        }

        public string UnitDisplayName
        {
            get
            {
                if (Ingredient == null)
                    return null;

                var displayAttribute = typeof(Unit).GetMember(Ingredient.Unit.ToString()).Single().GetCustomAttribute<DisplayAttribute>();
                return new ResourceManager(displayAttribute.ResourceType).GetString(displayAttribute.Name);
            }
        }

        private string DebuggerDisplay => $"{Ingredient?.Quantity} {Ingredient?.Unit} {Ingredient?.Name}";
    }
}