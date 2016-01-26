using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace Server
{
    /// <summary>
    /// UDP port listener
    /// netstat -an | find "UDP" | more
    /// </summary>
    public class DatagramListener : ISyslogListener
    {
        private readonly uint _port;
        private const uint Buffer = 52428800;    //5242880 50Mb 2097152 //20Mb  1048576 //10Mb
        public ConcurrentQueue<string> MessagesQueue { get; } = new ConcurrentQueue<string>();

        public DatagramListener(uint port)
        {
            _port = port;
        }

        private async Task BindService()
        {
            var socket = new DatagramSocket();
            socket.MessageReceived += OnMessageReceived;
            socket.Control.InboundBufferSizeInBytes = Buffer;

            try
            {
                await socket.BindServiceNameAsync(_port.ToString());
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private async void OnMessageReceived(DatagramSocket soket, DatagramSocketMessageReceivedEventArgs args)
        {
            var result = args.GetDataStream();
            var resultStream = result.AsStreamForRead(4096);
            using (var reader = new StreamReader(resultStream))
            {
                var text = await reader.ReadToEndAsync();
                MessagesQueue.Enqueue(text);
            }
        }

        public async void StartListener()
        {
            await BindService();
        }
    }
}
