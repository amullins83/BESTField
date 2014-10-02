namespace BESTTieBreaker.Models
{
    using System.Runtime.CompilerServices;
    using System.ComponentModel;

    public class Model : INotifyPropertyChanged
    {
        /// <summary>
        /// Fires when an observable property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Default setter for observable properties
        /// </summary>
        /// <typeparam name="T">
        /// The type of the property
        /// </typeparam>
        /// <param name="field">
        /// (Out) The underlying data field of the property, passed by reference
        /// </param>
        /// <param name="value">
        /// The new value for the property
        /// </param>
        /// <param name="propertyName">
        /// (Optional) The name of the property changed; defaults to the name of the calling property
        /// </param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T field, T value,
        [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value)) { return false; }

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raise the PropertyChanged event with the given name or
        /// the calling property's name if there are any listeners
        /// </summary>
        /// <param name="propertyName">
        /// (Optional) The name of the property changed; defaults to the name of the calling property
        /// </param>
        protected void RaisePropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raise the PropertyChanged event with the given EventArgs if there are any listeners
        /// </summary>
        /// <param name="e">
        /// Event arguments, including the name of the property changed
        /// </param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var propChanged = PropertyChanged;
            if (propChanged != null) { propChanged(this, e); }
        }
    }
}
