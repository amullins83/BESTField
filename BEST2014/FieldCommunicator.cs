using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEST2014
{
    public class FieldCommunicator : IFieldCommunicator
    {
        private List<IField> fields = new List<IField>();

        public int Count
        {
            get { return fields.Count; }
        }

        private delegate object Validated(int id);

        public FieldState ReadField(int id)
        {
            return (FieldState)validateIdThen(readField, id);
        }

        private object readField(int id)
        {
            return fields[id - 1].Query();
        }

        private object validateIdThen(Validated f, int id)
        {
            if (isInRange(id))
            {
                return f(id);
            }
            else
            {
                return null;
            }
        }

        private bool isInRange(int id) { return id >= 1 && id <= Count; }

        public async Task<FieldState> ReadFieldAsync(int id)
        {
            if(isInRange(id))
            {
                return await fields[id - 1].QueryAsync();
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
                fields[id - 1].Reset();
            }
        }

        public async Task ResetFieldAsync(int id)
        {
            if(isInRange(id))
            {
                await fields[id - 1].ResetAsync();
            }
        }

        public string GetLastMessageFromField(int id)
        {
            return (string)validateIdThen(getLastMessage, id);
        }

        private object getLastMessage(int id)
        {
            return fields[id - 1].LastMessage;
        }

        public List<string> GetMessagesFromField(int id)
        {
            return (List<string>)validateIdThen(getMessages, id);
        }

        private object getMessages(int id)
        {
            return fields[id - 1].Messages;
        }

        public void AddField(IField field)
        {
            fields.Add(field);
        }

        public void RemoveField(int id)
        {
           fields.RemoveAll(f => f.Id == id);
        }

        public void RemoveField(string address)
        {
            fields.RemoveAll(f => f.Address.ToString() == address);
        }

        public IField PopField()
        {
            var last = fields.Last();
            fields.RemoveAt(Count - 1);
            return last;
        }
    }
}
