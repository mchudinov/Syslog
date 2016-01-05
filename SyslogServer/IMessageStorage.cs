using System;
using SyslogServer.Models;

namespace SyslogServer
{
    public interface IMessageStorage
    {
        void Add(SyslogMessage message);

        event EventHandler MessageAdded;
    }
}
