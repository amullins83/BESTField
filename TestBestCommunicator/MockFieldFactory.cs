namespace TestBestCommunicator
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    using BEST2014;

    public class MockFieldFactory : IFieldFactory
    {
        private static string xmlPath = "Resources/FieldStateValid.txt";
        private static XElement baseFieldStateElement =
            XElement.Parse(File.ReadAllText(xmlPath));

        /// <summary>
        /// Backing store for determining field ID
        /// </summary>
        private int fieldCount = 0;

        /// <summary>
        /// Create a new instance of an IField object with an ID determined
        /// by the order of creation, and a loopback IPAddress
        /// </summary>
        /// <returns>
        /// An initialized IField object
        /// </returns>
        public IField Create()
        {
            fieldCount++;
            return this.Create(fieldCount);
        }

        /// <summary>
        /// Create a new instance of an IField object with an ID determined
        /// by the order of creation.
        /// </summary>
        /// <param name="address">
        /// The IPAddress assigned to this field
        /// </param>
        /// <returns>
        /// An initialized IField object
        /// </returns>
        public IField Create(IPAddress address)
        {
            fieldCount++;
            return Create(fieldCount, address);
        }

        /// <summary>
        /// Create a new instance of an IField object with an ID determined
        /// by the order of creation.
        /// </summary>
        /// <param name="address">
        /// The IPAddress assigned to this field, as a standard string representation
        /// </param>
        /// <returns>
        /// If the IPAddress can be parsed, An initialized IField object,
        /// otherwise null
        /// </returns>
        public IField Create(string address)
        {
            fieldCount++;
            return Create(fieldCount, address);
        }

        /// <summary>
        /// Create a new instance of an IField object with the given ID
        /// </summary>
        /// <param name="id">
        /// The integer ID assigned to the field
        /// </param>
        /// <returns>
        /// An initialized IField object
        /// </returns>
        public IField Create(int id)
        {
            var field = new MockField();
            field.QueryState = new FieldState(baseFieldStateElement.ToString());
            return field;
        }

        /// <summary>
        /// Create a new instance of an IField object with the given ID and address
        /// </summary>
        /// <param name="id">
        /// The integer ID assigned to the field
        /// </param>
        /// <param name="address">
        /// The IPAddress assigned to this field
        /// </param>
        /// <returns>
        /// An initialized IField object
        /// </returns>
        public IField Create(int id, IPAddress address)
        {
            var field = new MockField();
            field.QueryState = new FieldState(baseFieldStateElement.ToString());
            return field;
        }

        /// <summary>
        /// Create a new instance of an IField object with the given ID and address
        /// </summary>
        /// <param name="id">
        /// The integer ID assigned to the field
        /// </param>
        /// <param name="address">
        /// The IPAddress assigned to this field, as a standard string representation
        /// </param>
        /// <returns>
        /// If the IPAddress can be parsed, An initialized IField object,
        /// otherwise null
        /// </returns>
        public IField Create(int id, string address)
        {
            IPAddress ip;
            if (IPAddress.TryParse(address, out ip))
            {
                return Create(id, ip);
            }

            return null;
        }
    }
}
