namespace BESTTieBreaker.ViewModels
{
    using System.Net;

    using Models;

    public class FieldListViewModel : ViewModel, IFieldListViewModel
    {
        /// <summary>
        /// Backing field for the Fields property
        /// </summary>
        private FieldSource fields = new FieldSource();

        /// <summary>
        /// Gets an observable collection of Field Models
        /// </summary>
        public FieldSource Fields
        {
            get { return this.fields; }
        }

        /// <summary>
        /// Add a FieldModel with the given ID and Address
        /// </summary>
        /// <param name="id">
        /// The ID of the new field
        /// </param>
        /// <param name="address">
        /// The address of the new field
        /// </param>
        public void AddField(int id, IPAddress address)
        {
            var model = new FieldModel();
            model.Id = id;
            model.Address = address;
            this.fields.Add(model);
        }
    }
}
