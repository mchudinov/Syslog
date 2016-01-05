using System;
using Server.Models;

namespace Server
{
    public interface IMessageStorage
    {
        void Add(SyslogMessage message);

        event EventHandler MessageAdded;
    }
}
