using System.Diagnostics;

namespace RecipeApp.ViewModels
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class DirectionAddViewModel : BaseViewModel
    {
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

        private int order;

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

        private string description;

        private string DebuggerDisplay => $"{Order} {Description}";
    }
}