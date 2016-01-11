using Server.Models;

namespace Server
{
    public interface IMessageParser
    {
        SyslogMessage Parse(string rawmessage, System.Net.IPAddress address);

        SyslogMessage Parse(string rawmessage, Windows.Networking.HostName hostname);
    }
}
