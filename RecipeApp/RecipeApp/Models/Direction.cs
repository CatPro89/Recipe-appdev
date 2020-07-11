﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RecipeApp.Models
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class Direction : BaseModel
    {
        private int order;
        private string description;

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
                    RaisePropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        private string DebuggerDisplay => $"{Order} {Description}";

        public override bool Equals(object obj)
        {
            return obj is Direction direction &&
                   Id == direction.Id &&
                   Order == direction.Order &&
                   Description == direction.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}