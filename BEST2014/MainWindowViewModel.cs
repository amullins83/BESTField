using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEST2014
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(string initMessage)
        {
            Message = initMessage;
        }

        public string Message
        {
            get;
            private set;
        }
    }
}
