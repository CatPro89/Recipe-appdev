using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace RecipeApp.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Directions = new List<Direction>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int Servings { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan RestTime { get; set; }

        public TimeSpan BakingCookingTime { get; set; }

        public TimeSpan OverallTime { get; set; }

        public IEnumerable<Ingredient> Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                ingredients = value.ToList();
            }
        }

        private IList<Ingredient> ingredients;

        public IEnumerable<Direction> Directions
        {
            get
            {
                return directions;
            }
            set
            {
                directions = value.ToList();
            }
        }

        private IList<Direction> directions;

        public Ingredient CreateIngredient()
        {
            var ingredient = new Ingredient { Order = ingredients.Count() + 1 };

            ingredients.Add(ingredient);

            return ingredient;
        }

        public bool IncreaseOrder(Ingredient ingredient)
        {
            var successor = ingredients.FirstOrDefault(i => i.Order == ingredient.Order + 1);

            if (successor == null)
                return false;

            successor.Order--;
            ingredient.Order++;

            return true;
        }

        public bool DecreaseOrder(Ingredient ingredient)
        {
            var predecessor = ingredients.FirstOrDefault(i => i.Order == ingredient.Order - 1);

            if (predecessor == null)
                return false;

            predecessor.Order++;
            ingredient.Order--;

            return true;
        }

        public void Remove(Ingredient ingredient)
        {
            var successors = ingredients.Where(i => i.Order > ingredient.Order);

            foreach (var successor in successors)
            {
                successor.Order--;
            }

            ingredients.Remove(ingredient);
        }

        public Direction CreateDirection()
        {
            var direction = new Direction { Order = directions.Count() + 1 };

            directions.Add(direction);

            return direction;
        }

        public bool IncreaseOrder(Direction direction)
        {
            var successor = directions.FirstOrDefault(d => d.Order == direction.Order + 1);

            if (successor == null)
                return false;

            successor.Order--;
            direction.Order++;

            return true;
        }

        public bool DecreaseOrder(Direction direction)
        {
            var predecessor = directions.FirstOrDefault(d => d.Order == direction.Order - 1);

            if (predecessor == null)
                return false;

            predecessor.Order++;
            direction.Order--;

            return true;
        }

        public void Remove(Direction direction)
        {
            var successors = directions.Where(d => d.Order > direction.Order);

            foreach (var successor in successors)
            {
                successor.Order--;
            }

            directions.Remove(direction);
        }

        private string DebuggerDisplay => Name;
    }
}