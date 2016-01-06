using System;
using System.Net;
using Windows.Networking.Sockets;
using System.Text;

namespace Server
{
    public class UdpListener : IUdpListener
    {
        private int _port;
        private const int MaxMessageLength = 1000;     //in bytes

        public UdpListener(int port)
        {
            _port = port;
        }

        public void StartListener(IMessageStorage storage, IMessageParser parser)
        {
            bool done = false;
            //UdpClient listener = new UdpClient(_port);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _port);

            //try
            //{
            //    while (!done)
            //    {
            //        byte[] bytes = listener.Receive(ref endPoint);
            //        if (bytes.Length > MaxMessageLength)
            //            continue;

            //        var rawmessage = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            //        storage.Add(parser.Parse(rawmessage, endPoint.Address));
            //    }
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}
            //finally
            //{
            //    listener.Close();
            //}
        }
    }
}
