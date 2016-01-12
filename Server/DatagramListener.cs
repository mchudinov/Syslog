using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Server.Models;

namespace Server
{
    public class DatagramListener : ISyslogListener
    {
        public uint MaxMessageLenghtInBytes { get; set; } = 1024;
        private readonly uint _port;
        private const uint Buffer = 2048;
        private IMessageStorage _storage;
        private IMessageParser _parser;

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
            var resultStream = result.AsStreamForRead((int)Buffer);

            using (var reader = new StreamReader(resultStream))
            {
                var text = await reader.ReadToEndAsync();
                SyslogMessage message = _parser.Parse(text, soket.Information.RemoteAddress);
                _storage.Add(message);
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
            _storage = storage;
            _parser = parser;
            await BindService();
        }
    }
}
