using System;
using System.Collections.Generic;

namespace RecipeApp.Models
{
    public class JsonRecipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Base64EncodedImage { get; set; }

        public string ImageExtension { get; set; }

        public int Servings { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan RestTime { get; set; }

        public TimeSpan BakingCookingTime { get; set; }

        public TimeSpan OverallTime { get; set; }

        public IEnumerable<JsonIngredient> Ingredients { get; set; }

        public IEnumerable<JsonDirection> Directions { get; set; }
    }
}