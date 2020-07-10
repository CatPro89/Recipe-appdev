using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RecipeApp.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Ingredient : BaseModel
    {
        private int order;
        private string name;
        private double? quantity;
        private Unit unit;

        [Key]
        public int Id { get; set; }

        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                if (order != value)
                {
                    order = value;
                    RaisePropertyChange();
                }
            }
        }

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
                    RaisePropertyChange();
                }
            }
        }

        public double? Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChange();
                }
            }
        }

        public Unit Unit
        {
            get
            {
                return unit;
            }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    RaisePropertyChange();
                }
            }
        }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        private string DebuggerDisplay => $"{Quantity} {Unit} {Name}";

        public override bool Equals(object obj)
        {
            return obj is Ingredient ingredient &&
                   Id == ingredient.Id &&
                   Order == ingredient.Order &&
                   Name == ingredient.Name &&
                   Quantity == ingredient.Quantity &&
                   Unit == ingredient.Unit &&
                   RecipeId == ingredient.RecipeId &&
                   EqualityComparer<Recipe>.Default.Equals(Recipe, ingredient.Recipe);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}