﻿using System.Collections.Generic;
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

        public IFieldCommunicator CreateWithIPAddresses(
            params IPAddress[] addresses)
        {
            return this.CreateWithIPAddresses(addresses);
        }

        public IFieldCommunicator CreateWithIPAddresses(
            params string[] addresses)
        {
            return this.CreateWithIPAddresses(
                addresses.Select(a => IPAddress.Parse(a)));
        }

        public IFieldCommunicator CreateWithIPAddresses(
            IEnumerable<IPAddress> addresses)
        {
            FieldCommunicator communicator = new FieldCommunicator();
            
            int i = 0;
            foreach (var address in addresses)
            {
                if (i == 4)
                {
                    break;
                }
                Field f = new Field(i + 1, address,
                    new UdpClientWrapper());
                communicator.AddField(f);
                i++;
            }

            return communicator;
        }
    }
}
