using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RecipeApp.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Direction
    {
        [Key]
        public int Id { get; set; }

        public int Order { get; set; }

        public string Description { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        private string DebuggerDisplay => $"{Order} {Description}";
    }
}