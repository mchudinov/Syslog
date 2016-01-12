using System;
using System.Collections.ObjectModel;
using Server.Models;

namespace Server
{
    public class MemoryStorage : IMessageStorage
    {
        public ObservableCollection<string> Messages { get; }

        public MemoryStorage()
        {
            Messages = new ObservableCollection<string>();
        }

        public void Add(IMessage message)
        {
            Messages.Add(message.ToString());
        }
    }
}
