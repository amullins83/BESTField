namespace BESTTieBreaker.Models
{
    using System.Net;

    using BEST2014;

    public class FieldModel : Model
    {
        /// <summary>
        /// The field ID
        /// </summary>
        private int id = 1;

        /// <summary>
        /// The IP address of the field
        /// </summary>
        private IPAddress address = IPAddress.Loopback;

        /// <summary>
        /// Backing field for the Factory property
        /// </summary>
        private IFieldFactory factory;

        /// <summary>
        /// Gets or sets the ID of the field
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { SetProperty(ref this.id, value); }           
        }

        /// <summary>
        /// Gets or sets the IP address of the field
        /// </summary>
        public IPAddress Address
        {
            get { return this.address; }
            set { SetProperty(ref this.address, value); }
        }

        /// <summary>
        /// Gets or sets the FieldFactory used to generate
        /// IField objects with the given properties
        /// </summary>
        public IFieldFactory Factory
        {
            get { return this.factory; }
            set { SetProperty(ref this.factory, value); }
        }

        /// <summary>
        /// Gets an IField object created by Factory with the Id and Address properties
        /// </summary>
        public IField Field
        {
            get { return this.factory.Create(this.id, this.address); }
        }
    }
}
