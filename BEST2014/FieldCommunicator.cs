using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace BEST2014
{
    public class FieldCommunicator
    {
        private List<Field> fields = new List<Field>();

        public int Count
        {
            get { return fields.Count; }
        }

        private delegate object Validated(int id);

        public string ReadField(int id)
        {
            return (string)validateIdThen(readField, id);
        }

        private object readField(int id)
        {
            return fields[id].Read();
        }

        private object validateIdThen(Validated f, int id)
        {
            if (id >= 0 && id < Count)
            {
                return f(id);
            }
            else
            {
                return null;
            }
        }

        private bool isInRange(int id) { return id >= 0 && id < Count; }

        public async Task<string> ReadFieldAsync(int id)
        {
            if(isInRange(id))
            {
                return await fields[id].ReadAsync();
            }
            else
            {
                return null;
            }
        }

        public void ResetField(int id)
        {
            if(isInRange(id))
            {
                fields[id].Reset();
            }
        }

        public async Task ResetFieldAsync(int id)
        {
            if(isInRange(id))
            {
                await fields[id].ResetAsync();
            }
        }

        public string GetLastMessageFromField(int id)
        {
            return (string)validateIdThen(getLastMessage, id);
        }

        private object getLastMessage(int id)
        {
            return fields[id].LastMessage;
        }

        List<string> GetMessagesFromField(int id)
        {
            return (List<string>)validateIdThen(getMessages, id);
        }

        private object getMessages(int id)
        {
            return fields[id].Messages;
        }

        void AddField(IPAddress address)
        {
            Field f = new Field(Count, address, new UdpClientWrapper());
            fields.Add(f);
        }

        void RemoveField(int id)
        {
           if(isInRange(id))
           {
               fields.RemoveAt(id);
           }
        }

        IField PopField()
        {
            var last = fields.Last();
            fields.RemoveAt(Count - 1);
            return last;
        }
    }
}
