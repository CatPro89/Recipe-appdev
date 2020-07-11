using RecipeApp.Models;

namespace RecipeApp.ViewModels
{
    public class DirectionEditViewModel : BaseModel
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
                    RaisePropertyChange();
                }
            }
        }

        private Direction direction;
    }
}
