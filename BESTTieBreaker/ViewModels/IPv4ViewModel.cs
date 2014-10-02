namespace BESTTieBreaker.ViewModels
{
    using System.Net;

    public class IPv4ViewModel : ViewModel, IIPv4ViewModel
    {
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
