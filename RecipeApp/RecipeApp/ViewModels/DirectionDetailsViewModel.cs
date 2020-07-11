using RecipeApp.Models;
using System.Diagnostics;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class DirectionDetailsViewModel : BaseModel
    {
        public DirectionDetailsViewModel(Direction direction)
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
                    RaisePropertyChange();
                }
            }
        }

        private Direction direction;

        private string DebuggerDisplay => $"{Direction?.Order} {Direction?.Description}";
    }
}