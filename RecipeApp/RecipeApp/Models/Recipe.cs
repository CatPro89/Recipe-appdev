using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace RecipeApp.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Recipe : BaseModel
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Directions = new List<Direction>();
        }

        [Key]
        public int Id { get; set; }

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

        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string imagePath;

        public int Servings
        {
            get
            {
                return servings;
            }
            set
            {
                if (servings != value)
                {
                    servings = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int servings;

        public TimeSpan PreparationTime
        {
            get
            {
                return preparationTime;
            }
            set
            {
                if (preparationTime != value)
                {
                    preparationTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan preparationTime;

        public TimeSpan RestTime
        {
            get
            {
                return restTime;
            }
            set
            {
                if (restTime != value)
                {
                    restTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan restTime;

        public TimeSpan BakingCookingTime
        {
            get
            {
                return bakingCookingTime;
            }
            set
            {
                if (bakingCookingTime != value)
                {
                    bakingCookingTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan bakingCookingTime;

        public TimeSpan OverallTime
        {
            get
            {
                return overallTime;
            }
            set
            {
                if (overallTime != value)
                {
                    overallTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TimeSpan overallTime;

        public IEnumerable<Ingredient> Ingredients
        {
            get
            {
                return ingredients;
            }
            set
            {
                if (ingredients != value)
                {
                    ingredients = value.ToList();
                    RaisePropertyChanged();
                }
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
                if (directions != value)
                {
                    directions = value.ToList();
                    RaisePropertyChanged();
                }
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

        public override bool Equals(object obj)
        {
            return obj is Recipe recipe &&
                   Id == recipe.Id &&
                   Name == recipe.Name &&
                   ImagePath == recipe.ImagePath &&
                   Servings == recipe.Servings &&
                   PreparationTime.Equals(recipe.PreparationTime) &&
                   RestTime.Equals(recipe.RestTime) &&
                   BakingCookingTime.Equals(recipe.BakingCookingTime) &&
                   OverallTime.Equals(recipe.OverallTime) &&
                   EqualityComparer<IEnumerable<Ingredient>>.Default.Equals(Ingredients, recipe.Ingredients) &&
                   EqualityComparer<IEnumerable<Direction>>.Default.Equals(Directions, recipe.Directions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}