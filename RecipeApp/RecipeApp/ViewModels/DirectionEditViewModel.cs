using RecipeApp.Models;
using System.Diagnostics;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class DirectionEditViewModel : BaseViewModel
    {
        public DirectionEditViewModel(Direction direction)
        {
            Direction = direction;
        }

        public Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                if (direction != value)
                {
                    direction = value;
                    RaisePropertyChanged();
                }
            }
        }

        private Direction direction;

        private string DebuggerDisplay => $"{Direction?.Order} {Direction?.Description}";
    }
}