namespace BESTTieBreaker.Models
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using BEST2014;

    public class FieldCommunicatorModel : Model
    {
        private FieldCommunicator Communicator = new FieldCommunicator();

        private ObservableCollection<string> AddressCollection =
            new ObservableCollection<string>();

        public FieldCommunicatorModel()
        {
            this.AddressCollection.CollectionChanged += AddressCollection_CollectionChanged;
        }

        private void AddressCollection_CollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs e)
        {
            this.DisconnectOldFields(e.OldItems, e.NewItems);
            this.ConnectNewFields(e.OldItems, e.NewItems);
        }

        public ObservableCollection<string> Addresses
        {
            get { return this.AddressCollection; }
        }

        private void DisconnectOldFields(IList oldFields, IList newFields)
        {
            foreach (var address in oldFields)
            {
                if (!newFields.Contains(address))
                {
                    Communicator.RemoveField(1);
                }
            }
        }

        private void ConnectNewFields(IList oldFields, IList newFields)
        {

        }
    }
}
