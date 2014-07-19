using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace BEST2014
{
    public interface IField
    {
        int Id { get; }
        int Port { get; }

        IPAddress Address { get; }

        List<string> Messages { get; }

        string LastMessage { get; }

        string Read();
        async Task<string> ReadAsync();

        void Reset();
        async Task ResetAsync();
    }
}
