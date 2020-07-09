using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RecipeApp.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public double? Quantity { get; set; }

        public Unit Unit { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        private string DebuggerDisplay => $"{Quantity} {Unit} {Name}";
    }
}