using System;
using System.Collections.ObjectModel;
using Server.Models;

namespace Server
{
    public interface IMessageStorage
    {
        void Add(IMessage message);

        ObservableCollection<string> Messages { get; }

        event EventHandler MessageAdded;
    }
}
