namespace BESTTieBreaker.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Models;
    using ViewModels;

    public class TieBreakerResult : View
    {
        /// <summary>
        /// The Field Selection Source which raises selection events
        /// </summary>
        public static readonly DependencyProperty FieldModelProperty =
            DependencyProperty.Register(
                "FieldModel",
                typeof(FieldModel),
                typeof(TieBreakerResult),
                new PropertyMetadata(null, UpdateFieldModel));

        /// <summary>
        /// Gets or sets the active field model
        /// </summary>
        public FieldModel FieldModel
        {
            get { return (FieldModel)GetValue(FieldModelProperty); }
            set { SetValue(FieldModelProperty, value); }
        }

        /// <summary>
        /// Update the FieldModel property of the ViewModel
        /// </summary>
        /// <param name="d">
        /// The TieBreakerResult control that raised the event
        /// </param>
        /// <param name="e">
        /// The event arguments
        /// </param>
        private static void UpdateFieldModel(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tieBreak = (TieBreakerResult)d;
            
            if (tieBreak.ViewModel is TieBreakerViewModel)
            {
                var tieBreakVM = (TieBreakerViewModel)tieBreak.ViewModel;
                tieBreakVM.FieldModel = (FieldModel)e.NewValue;
            }
        }
    }
}
