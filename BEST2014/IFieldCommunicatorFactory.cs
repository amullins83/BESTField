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
        IFieldCommunicator Create(
            params IPAddress[] addresses);
        IFieldCommunicator Create(
            params string[] addresses);
        IFieldCommunicator Create(
            IEnumerable<IPAddress> addresses);
        IFieldCommunicator Create(
            IEnumerable<string> addresses);
    }
}
