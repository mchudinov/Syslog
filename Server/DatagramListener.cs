using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Server;

namespace Server
{
    public class DatagramListener : ISyslogListener
    {
        public int MaxMessageLenghtInBytes { get; set; } = 1024;
        private readonly int _port;

        public DatagramListener(int port)
        {
            _port = port;
        }

        private async Task BindService()
        {
            var socket = new DatagramSocket();
            socket.MessageReceived += OnMessageReceived;
            socket.Control.InboundBufferSizeInBytes = 2048;
            try
            {
                await socket.BindServiceNameAsync(_port.ToString());
            }
            catch (Exception)
            {
                //ignore
            }
        }

        private async void OnMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            var result = args.GetDataStream();
            var resultStream = result.AsStreamForRead(1024);

            using (var reader = new StreamReader(resultStream))
            {
                var text = await reader.ReadToEndAsync();
                //Deployment.Current.Dispatcher.BeginInvoke(() =>
                //{
                //    // Do what you need to with the resulting text
                //    // Doesn't have to be a messagebox
                //    MessageBox.Show(text);
                //});
            }
        }

        public async void StartListener(IMessageStorage storage, IMessageParser parser)
        {
            await BindService();
        }
    }
}
