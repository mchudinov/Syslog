using System.Net;
using Server.Models;

namespace Server
{
    public interface IMessageParser
    {
        SyslogMessage Parse(string rawmessage, IPAddress address);
    }
}
