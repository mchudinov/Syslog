using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SyslogServer
{
    public class UDPListener
    {
        private int _port;
        private const int MaxMessageLength = 1000;     //in bytes

        public UDPListener(int port)
        {
            _port = port;
        }

        public void StartListener(IMessageStorage storage, IMessageParser parser)
        {
            bool done = false;
            UdpClient listener = new UdpClient(_port);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _port);

            try
            {
                while (!done)
                {
                    byte[] bytes = listener.Receive(ref endPoint);
                    if (bytes.Length > MaxMessageLength)
                        continue;

                    var rawmessage = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    storage.Add(parser.Parse(rawmessage, endPoint.Address));
                }
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                listener.Close();
            }
        }
    }
}
