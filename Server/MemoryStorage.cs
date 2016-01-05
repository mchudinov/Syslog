using System;
using Server.Models;

namespace Server
{
    public class MemoryStorage : IMessageStorage
    {
        private readonly LimitedQueue<SyslogMessage> _queue;
        public event EventHandler MessageAdded;

        public MemoryStorage(int capacity)
        {
            _queue = new LimitedQueue<SyslogMessage>(capacity);
        }

        public int Capacity {
            get { return _queue.Limit; }
            set { _queue.Limit = value; }
        }

        public void Add(SyslogMessage message)
        {
            _queue.Enqueue(message);
            MessageAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}
