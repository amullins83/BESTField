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
        int Count { get; }

        FieldState ReadField(int id);
        Task<FieldState> ReadFieldAsync(int id);

        void ResetField(int id);
        Task ResetFieldAsync(int id);

        string GetLastMessageFromField(int id);
        List<string> GetMessagesFromField(int id);

        void AddField(IField field);

        void RemoveField(int id);
        void RemoveField(string address);

        IField PopField();
    }
}
