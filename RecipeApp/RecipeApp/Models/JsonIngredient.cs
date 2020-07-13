namespace RecipeApp.Models
{
    public class JsonIngredient
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public decimal? Quantity { get; set; }

        public Unit Unit { get; set; }
    }
}