using RecipeApp.Models;

namespace RecipeApp.ViewModels
{
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
    }
}
