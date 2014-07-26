using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace BEST2014
{
    public interface IFieldCommunicatorFactory
    {
        IFieldCommunicator Create();
        IFieldCommunicator CreateWithIPAddresses(
            params IPAddress[] addresses);
    }
}
