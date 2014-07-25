using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEST2014;

using System.Net;

namespace TestBestCommunicator
{
    public class MockField : IField
    {
        public int Id { get; set; }
        public int Port { get; set; }

        public IPAddress Address { get; set; }

        public List<string> Messages { get; set; }

        public string LastMessage { get { return Messages.Last(); } }

        public FieldState QueryState { get; set; }

        public FieldState Query()
        {
            return QueryState;
        }

        public delegate FieldState QueryAction();

        public async Task<FieldState> QueryAsync()
        {
            QueryAction qa = delegate {
                return QueryState;
            };

            return await Task<FieldState>.Run(new Func<FieldState>(qa));
        }

        private int timesResetCalled = 0;
        public int TimesResetCalled { get { return timesResetCalled; } }

        public void Reset() { timesResetCalled++; }
        public async Task ResetAsync() {
            await Task.Run(new Action(Reset));
        }
    }
}
