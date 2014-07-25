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

        FieldState Query();
        Task<FieldState> QueryAsync();

        void Reset();
        Task ResetAsync();
    }
}
