using System.Net;
using SyslogServer.Models;

namespace SyslogServer
{
    public interface IMessageParser
    {
        SyslogMessage Parse(string rawmessage, IPAddress address);
    }
}
