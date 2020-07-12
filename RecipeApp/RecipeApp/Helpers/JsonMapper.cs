using RecipeApp.Models;
using System.Linq;

namespace RecipeApp.Helpers
{
    public static class JsonMapper
    {
        public static JsonRecipe Build(Recipe recipe)
        {
            return new JsonRecipe
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Base64EncodedImage = ImageHelper.LoadBase64EncodedImage(recipe.ImagePath),
                ImageExtension = ImageHelper.GetExtension(recipe.ImagePath),
                Servings = recipe.Servings,
                PreparationTime = recipe.PreparationTime,
                RestTime = recipe.RestTime,
                BakingCookingTime = recipe.BakingCookingTime,
                OverallTime = recipe.OverallTime,
                Ingredients = recipe.Ingredients.Select(Build),
                Directions = recipe.Directions.Select(Build)
            };
        }

        private static JsonIngredient Build(Ingredient ingredient)
        {
            return new JsonIngredient
            {
                Id = ingredient.Id,
                Order = ingredient.Order,
                Quantity = ingredient.Quantity,
                Unit = ingredient.Unit,
                Name = ingredient.Name
            };
        }

        private static JsonDirection Build(Direction direction)
        {
            return new JsonDirection
            {
                Id = direction.Id,
                Order = direction.Order,
                Description = direction.Description,
            };
        }

        public static Recipe Parse(JsonRecipe jsonRecipe)
        {
            return new Recipe
            {
                Id = jsonRecipe.Id,
                Name = jsonRecipe.Name,
                ImagePath = ImageHelper.SaveBase64EncodedImage(jsonRecipe.Base64EncodedImage, jsonRecipe.ImageExtension),
                Servings = jsonRecipe.Servings,
                PreparationTime = jsonRecipe.PreparationTime,
                RestTime = jsonRecipe.RestTime,
                BakingCookingTime = jsonRecipe.BakingCookingTime,
                OverallTime = jsonRecipe.OverallTime,
                Ingredients = jsonRecipe.Ingredients.Select(Parse),
                Directions = jsonRecipe.Directions.Select(Parse)
            };
        }

        private static Ingredient Parse(JsonIngredient jsonIngredient)
        {
            return new Ingredient
            {
                Id = jsonIngredient.Id,
                Order = jsonIngredient.Order,
                Quantity = jsonIngredient.Quantity,
                Unit = jsonIngredient.Unit,
                Name = jsonIngredient.Name
            };
        }

        private static Direction Parse(JsonDirection jsonDirection)
        {
            return new Direction
            {
                Id = jsonDirection.Id,
                Order = jsonDirection.Order,
                Description = jsonDirection.Description,
            };
        }
    }
}