using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace BEST2014
{
    public interface IFieldCommunicator
    {
        string ReadField(int id);
        Task<string> ReadFieldAsync(int id);

        void ResetField(int id);
        Task ResetFieldAsync(int id);

        string GetLastMessageFromField(int id);
        List<string> GetMessagesFromField(int id);

        void AddField(IPAddress address);
        void RemoveField(int id);
        IField PopField();

        int Count { get; }
    }
}
