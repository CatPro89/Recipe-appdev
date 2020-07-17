using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepProgressBar : StackLayout
    {
        private const string doneButtonStyle = "doneButtonStyle";
        private const string undoneButtonStyle = "undoneButtonStyle";
        private const string transparentBoxViewStyle = "transparentBoxViewStyle";
        private const string doneBoxViewStyle = "doneBoxViewStyle";
        private const string undoneBoxViewStyle = "undoneBoxViewStyle";
        private const string labelStyle = "labelStyle";

        public StepProgressBar()
        {
            InitializeComponent();
        }

        private Button currentButton;

        private IEnumerable<BoxView> currentBoxViews;

        public static readonly BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(ObservableCollection<Step>), typeof(StepProgressBar));

        public static readonly BindableProperty CurrentStepNumberProperty = BindableProperty.Create(nameof(CurrentStepNumber), typeof(int), typeof(StepProgressBar), 0, defaultBindingMode: BindingMode.TwoWay);

        public ObservableCollection<Step> Steps
        {
            get { return (ObservableCollection<Step>)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int CurrentStepNumber
        {
            get { return (int)GetValue(CurrentStepNumberProperty); }
            set { SetValue(CurrentStepNumberProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Steps):
                    OnStepsPropertyChanged();
                    break;
                case nameof(CurrentStepNumber):
                    OnCurrentStepNumberPropertyChanged();
                    break;
            }
        }

        /// <summary>
        /// Structure:
        /// - Horizontal StackLayout with
        ///     transparent BoxView (no ClassId) | Button (ClassId = 1) |
        ///     2 BoxViews (ClassId = 2) | Button (ClassId = 2) |
        ///     2 BoxViews (ClassId = 3) | Button (ClassId = 3) |
        ///     transparent BoxView (noClassId)
        /// - Grid with
        ///     1 star row | 1 absolute row | 2 star rows | 1 absolute row | 2 star rows | 1 absolute row | 1 star row
        /// BoxView + Button + BoxView = 1 star row + 1 absolute row + 1 star row
        /// Labels spans 3 rows such that they can be centered under the corresponding buttons.
        /// </summary>
        private void OnStepsPropertyChanged()
        {
            grid.RowDefinitions = new RowDefinitionCollection();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.ColumnDefinitions = new ColumnDefinitionCollection();

            for (int i = 0; i < Steps.Count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            var count = 0;

            foreach (var step in Steps)
            {
                var isFirstStep = count == 0;

                var stepBoxViewBefore = new BoxView
                {
                    ClassId = isFirstStep ? null : step.Number.ToString(),
                    Style = (Style)Resources[isFirstStep ? transparentBoxViewStyle : undoneBoxViewStyle]
                };

                stackLayout.Children.Add(stepBoxViewBefore);

                count++;

                var stepButton = new Button
                {
                    ClassId = step.Number.ToString(),
                    Text = step.Number.ToString(),
                    Style = (Style)Resources[undoneButtonStyle]
                };

                stackLayout.Children.Add(stepButton);

                var isLastStep = count == Steps.Count;

                var stepBoxViewAfter = new BoxView
                {
                    ClassId = isLastStep ? null : (step.Number + 1).ToString(),
                    Style = (Style)Resources[isLastStep ? transparentBoxViewStyle : undoneBoxViewStyle]
                };

                stackLayout.Children.Add(stepBoxViewAfter);

                var stepLabel = new Label
                {
                    ClassId = step.Number.ToString(),
                    Text = step.Text,
                    Style = (Style)Resources[labelStyle]
                };

                grid.Children.Add(stepLabel, (count - 1) * 3, 0);
                Grid.SetColumnSpan(stepLabel, 3);
            }
        }

        private void OnCurrentStepNumberPropertyChanged()
        {
            var button = GetByClassId<Button>(CurrentStepNumber).SingleOrDefault();
            var boxViews = GetByClassId<BoxView>(CurrentStepNumber);

            button.Style = (Style)Resources[doneButtonStyle];

            if (currentButton != null && Convert.ToInt32(currentButton.ClassId) > CurrentStepNumber)
                currentButton.Style = (Style)Resources[undoneButtonStyle];

            currentButton = button;

            if (boxViews != null)
            {
                foreach (var boxView in boxViews)
                {
                    boxView.Style = (Style)Resources[doneBoxViewStyle];
                }
            }

            if (currentBoxViews != null && currentBoxViews.Any() && Convert.ToInt32(currentBoxViews.First().ClassId) > CurrentStepNumber)
            {
                foreach (var currentBoxView in currentBoxViews)
                {
                    currentBoxView.Style = (Style)Resources[undoneBoxViewStyle];
                }
            }

            currentBoxViews = boxViews;
        }

        private IEnumerable<T> GetByClassId<T>(int classId) where T : Element
        {
            return stackLayout.Children.OfType<T>().Where(element => Convert.ToInt32(element.ClassId) == classId);
        }
    }
}