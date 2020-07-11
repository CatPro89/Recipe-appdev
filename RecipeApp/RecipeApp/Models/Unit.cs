using RecipeApp.Resx;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public enum Unit
    {
        [Display(Name = "Unit_Undefined", ResourceType = typeof(AppResources))]
        Undefined = 0,

        [Display(Name = "Unit_Piece", ResourceType = typeof(AppResources))]
        Piece = 1,

        [Display(Name = "Unit_Gram", ResourceType = typeof(AppResources))]
        Gram = 2,

        [Display(Name = "Unit_Milliliter", ResourceType = typeof(AppResources))]
        Milliliter = 3,

        [Display(Name = "Unit_Pack", ResourceType = typeof(AppResources))]
        Pack = 4,

        [Display(Name = "Unit_Teaspoon", ResourceType = typeof(AppResources))]
        Teaspoon = 5,

        [Display(Name = "Unit_Tablespoon", ResourceType = typeof(AppResources))]
        Tablespoon = 6,

        [Display(Name = "Unit_Pinch", ResourceType = typeof(AppResources))]
        Pinch = 7,

        [Display(Name = "Unit_Slice", ResourceType = typeof(AppResources))]
        Slice = 8
    }
}