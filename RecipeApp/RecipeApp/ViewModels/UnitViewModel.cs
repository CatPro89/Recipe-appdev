using RecipeApp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class UnitViewModel : BaseModel
    {
        public UnitViewModel(Unit unit)
        {
            Unit = unit;
            var displayAttribute = typeof(Unit).GetMember(unit.ToString()).Single().GetCustomAttribute<DisplayAttribute>();
            DisplayName = new ResourceManager(displayAttribute.ResourceType).GetString(displayAttribute.Name);
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
                    RaisePropertyChanged();
                }
            }
        }

        private Unit unit;

        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string displayName;

        private string DebuggerDisplay => $"{Unit} {DisplayName}";

        public override bool Equals(object obj)
        {
            return obj is UnitViewModel model &&
                   Unit == model.Unit &&
                   DisplayName == model.DisplayName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Unit, DisplayName);
        }
    }
}