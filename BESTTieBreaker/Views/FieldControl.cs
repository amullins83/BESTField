namespace BESTTieBreaker.Views
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;

    using BEST2014;
    using ViewModels;

    public class FieldControl : View
    {
        /// <summary>
        /// Dependency Property for controlling the Field ID
        /// </summary>
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(FieldControl), new PropertyMetadata(1));

        /// <summary>
        /// Dependency Property for controlling the address
        /// </summary>
        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(IPAddress), typeof(FieldControl), new PropertyMetadata(IPAddress.Loopback));

        /// <summary>
        /// Fires whenever the field represented by the control has been added to the field list
        /// </summary>
        public event EventHandler Added;

        /// <summary>
        /// Gets or sets the current value of the IdProperty
        /// </summary>
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current value of the AddressProperty
        /// </summary>
        public IPAddress Address
        {
            get { return (IPAddress)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        /// <summary>
        /// Add the current settings to a Field List if the current ViewModel supports it
        /// </summary>
        public void Add()
        {
            if (this.ViewModel is IFieldListViewModel)
            {
                var fieldListVM = (IFieldListViewModel)this.ViewModel;
                fieldListVM.AddField();
                var added = this.Added;
                if (added != null)
                {
                    added(this, EventArgs.Empty);
                }
            }
        }
    }
}
