using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BEST2014
{
    public class FieldCommunicatorFactory : IFieldCommunicatorFactory
    {
        public IFieldCommunicator Create()
        {
            return new FieldCommunicator();
        }

        public IFieldCommunicator Create(
            params IPAddress[] addresses)
        {
            return this.CreateFromEnumerableAddresses(addresses);
        }

        public IFieldCommunicator Create(
            params string[] addresses)
        {
            return this.CreateFromEnumerableStrings(addresses);
        }

        public IFieldCommunicator Create(
            IEnumerable<string> addresses)
        {
            return this.CreateFromEnumerableStrings(addresses);
        }

        public IFieldCommunicator Create(
            IEnumerable<IPAddress> addresses)
        {
            return CreateFromEnumerableAddresses(addresses);
        }

        private IPAddress AddressFromString(string address)
        {
            IPAddress ip;
            if (IPAddress.TryParse(address, out ip))
            {
                return ip;
            }

            return null;
        }

        private IFieldCommunicator CreateFromEnumerableStrings(IEnumerable<string> addresses)
        {
            return this.CreateFromEnumerableAddresses(
                addresses.Select(this.AddressFromString));
        }

        private IFieldCommunicator CreateFromEnumerableAddresses(IEnumerable<IPAddress> addresses)
        {
            var communicator = new FieldCommunicator();
            var factory = new FieldFactory();

            int i = 0;
            foreach (var address in addresses)
            {
                if (i == 4)
                {
                    break;
                }

                communicator.AddField(factory.Create(i + 1, address));
                i++;
            }

            return communicator;
        }
    }
}
