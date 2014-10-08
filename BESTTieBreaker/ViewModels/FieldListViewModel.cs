namespace BESTTieBreaker.ViewModels
{
    using System.Net;

    using BEST2014;
    using Models;

    public class FieldListViewModel : ViewModel, IFieldListViewModel
    {
        /// <summary>
        /// Backing field for the Fields property
        /// </summary>
        private FieldSource fields = new FieldSource();

        /// <summary>
        /// The selected field model
        /// </summary>
        private FieldModel selected;

        /// <summary>
        /// The Id of the next Field Model to add
        /// </summary>
        private int id = 1;

        /// <summary>
        /// The factory used to create field objects from FieldModels
        /// </summary>
        private IFieldFactory factory;

        /// <summary>
        /// Gets an observable collection of Field Models
        /// </summary>
        public FieldSource Fields
        {
            get { return this.fields; }
        }

        /// <summary>
        /// Gets an enumerable collection of field models that will be removed
        /// on the next call to RemoveFields
        /// </summary>
        public FieldModel Selected
        {
            get { return this.selected; }
            set { SetProperty(ref this.selected, value); }
        }

        /// <summary>
        /// Gets or sets the ID of the next field model to add
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets the field factory object
        /// </summary>
        public IFieldFactory Factory
        {
            get { return this.factory; }
            set { SetProperty(ref this.factory, value); }
        }

        /// <summary>
        /// Add a FieldModel with the given ID and Address
        /// </summary>
        public void AddField()
        {
            var model = new FieldModel();
            model.Id = this.id > 0 ? this.id : 1;
            model.Address = this.Address;
            model.Factory = this.factory;
            this.fields.Add(model);
        }

        /// <summary>
        /// Remove the given field models from Fields
        /// </summary>
        public void RemoveFields()
        {
            this.fields.Remove(this.selected);
        }

        /// <summary>
        /// Backing field for octet1
        /// </summary>
        private string octet1;

        /// <summary>
        /// Backing field for octet2
        /// </summary>
        private string octet2;

        /// <summary>
        /// Backing field for octet3
        /// </summary>
        private string octet3;

        /// <summary>
        /// Backing field for octet4
        /// </summary>
        private string octet4;

        /// <summary>
        /// Gets or sets the first octet of the Address
        /// </summary>
        public string Octet1
        {
            get
            {
                return this.octet1;
            }

            set
            {
                SetProperty(ref this.octet1, value);
                RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the second octet of the Address
        /// </summary>
        public string Octet2
        {
            get
            {
                return this.octet2;
            }

            set
            {
                SetProperty(ref this.octet2, value);
                RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the third octet of the Address
        /// </summary>
        public string Octet3
        {
            get
            {
                return this.octet3;
            }

            set
            {
                SetProperty(ref this.octet3, value);
                RaisePropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets the last octet of the Address
        /// </summary>
        public string Octet4
        {
            get
            {
                return this.octet4;
            }

            set
            {
                SetProperty(ref this.octet4, value);
                RaisePropertyChanged("Address");
            }
        }

        public IPAddress Address
        {
            get
            {
                IPAddress ip;
                if (IPAddress.TryParse(this.CombineOctets(), out ip))
                {
                    return ip;
                }

                return null;
            }

            set
            {
                var address = value.ToString();
                var octets = address.Split('.');
                SetProperty(ref this.octet1, octets[0]);
                SetProperty(ref this.octet2, octets[1]);
                SetProperty(ref this.octet3, octets[2]);
                SetProperty(ref this.octet4, octets[3]);
            }
        }

        /// <summary>
        /// Combine the octets into one IPv4 address string
        /// </summary>
        /// <returns>
        /// The combined IPv4 address
        /// </returns>
        private string CombineOctets()
        {
            return string.Join(".", this.octet1, this.octet2, this.octet3, this.octet4);
        }
    }
}
