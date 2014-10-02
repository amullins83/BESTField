namespace BESTTieBreaker.Models
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
 
    using BEST2014;

    /// <summary>
    /// Model class to instantiate field communicator objects and make them available to XAML
    /// </summary>
    public class FieldCommunicatorModel : Model
    {
        /// <summary>
        /// The factory for generating the Communicator property
        /// </summary>
        private IFieldCommunicatorFactory factory = new FieldCommunicatorFactory();

        /// <summary>
        /// Model object for a collection of fields
        /// </summary>
        private ObservableCollection<FieldModel> fields =
            new ObservableCollection<FieldModel>();

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FieldCommunicatorModel"/> class
        /// </summary>
        public FieldCommunicatorModel()
        {
            this.fields.CollectionChanged += Fields_CollectionChanged;
        }

        /// <summary>
        /// Make sure the Communicator object is updated whenever a field
        /// in the Fields collection changes
        /// </summary>
        /// <param name="sender">
        /// The observable collection that raised the event (ignored, assumed to be the Fields property)
        /// </param>
        /// <param name="e">
        /// The event arguments, including lists of the old and new FieldModels
        /// </param>
        private void Fields_CollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            foreach (var obj in e.OldItems)
            {
                if (obj is FieldModel)
                {
                    var fieldModel = (FieldModel)obj;
                    fieldModel.PropertyChanged -= this.FieldChanged;
                }
            }

            foreach (var obj in e.NewItems)
            {
                if (obj is FieldModel)
                {
                    var fieldModel = (FieldModel)obj;
                    fieldModel.PropertyChanged += this.FieldChanged;
                }
            }

            RaisePropertyChanged("Communicator");
        }


        /// <summary>
        /// Gets the observable Fields collection
        /// </summary>
        public ObservableCollection<FieldModel> Fields
        {
            get { return this.fields; }
        }

        /// <summary>
        /// Gets or sets the factory for generating the Communicator property
        /// </summary>
        public IFieldCommunicatorFactory Factory
        {
            get { return this.factory; }
            set
            {
                SetProperty(ref this.factory, value);
                RaisePropertyChanged("Communicator");
            }
        }

        /// <summary>
        /// Gets an IFieldCommunicator instance based on the field models in the Fields property
        /// </summary>
        public IFieldCommunicator Communicator
        {
            get
            {
                var communicator = this.factory.Create();
                foreach (var fieldModel in this.fields)
                {
                    communicator.AddField(fieldModel.Field);
                }

                return communicator;
            }
        }

        /// <summary>
        /// Update the communicator when a field model changes
        /// </summary>
        /// <param name="sender">
        /// The field model that raised the PropertyChanged event (ignored)
        /// </param>
        /// <param name="e">
        /// The event arguments, including the name of the changed property (ignored)
        /// </param>
        private void FieldChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Communicator");
        }
    }
}
 