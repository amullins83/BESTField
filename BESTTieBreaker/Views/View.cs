namespace BESTTieBreaker.Views
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class View : Control
    {
        /// <summary>
        /// The ViewModel dependency property
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel", // Property Name
                typeof(INotifyPropertyChanged), // Property type
                typeof(View), // Owner class
                new PropertyMetadata(ViewModels.ViewModel.Default, ViewModelChanged));

        /// <summary>
        /// Gets or sets the ViewModel attached to this view
        /// </summary>
        public INotifyPropertyChanged ViewModel
        {
            get { return (INotifyPropertyChanged)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// Update DataContext to the newly injected ViewModel
        /// </summary>
        /// <param name="d">
        /// The View that raised the event
        /// </param>
        /// <param name="e">
        /// The event arguments, including new and old values
        /// </param>
        private static void ViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (View)d;
            view.DataContext = e.NewValue;
            view.AfterViewModelChanged(e.OldValue, e.NewValue);
        }

        protected virtual void AfterViewModelChanged(object oldVM, object newVM)
        {
        }
    }
}

