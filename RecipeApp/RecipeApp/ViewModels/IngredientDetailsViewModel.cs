using RecipeApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
                    RaisePropertyChange();

                    OnPropertyChanged(nameof(UnitDisplayName));
                }
            }
        }

        private Ingredient ingredient;

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