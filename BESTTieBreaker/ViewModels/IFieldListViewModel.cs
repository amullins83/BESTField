namespace BESTTieBreaker.ViewModels
{
    using System.ComponentModel;
    using System.Net;

    using Models;

    public interface IFieldListViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets an observable collection of FieldModels
        /// </summary>
        FieldSource Fields { get; }

        /// <summary>
        /// Add a FieldModel with the given ID and Address
        /// </summary>
        void AddField();
    }
}
