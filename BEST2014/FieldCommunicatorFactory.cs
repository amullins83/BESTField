using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace BEST2014
{
    public class FieldCommunicatorFactory : IFieldCommunicatorFactory
    {
        public IFieldCommunicator Create()
        {
            return new FieldCommunicator();
        }

        public IFieldCommunicator CreateWithIPAddresses(
            params IPAddress[] addresses)
        {
            FieldCommunicator communicator = new FieldCommunicator();
            for (int i = 0; i < 4 && i < addresses.Length; i++)
            {
                Field f = new Field(i + 1, addresses[i],
                    new UdpClientWrapper());
                communicator.AddField(f);
            }

            return communicator;
        }
    }
}
