namespace BESTTieBreaker.Views
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class View : Control
    {
        public INotifyPropertyChanged ViewModel
        {
            get { return (INotifyPropertyChanged)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel", // Property Name
                typeof(INotifyPropertyChanged), // Property type
                typeof(View), // Owner class
                new PropertyMetadata(BESTTieBreaker.ViewModels.ViewModel.Default));
    }
}

