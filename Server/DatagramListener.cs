using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.UI.Core;
using Server.Models;

namespace Server
{
    public class DatagramListener : ISyslogListener
    {
        public uint MaxMessageLenghtInBytes { get; set; } = 1024;
        private readonly uint _port;
        private const uint Buffer = 100;
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
                
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _storage.Add(message);
                });
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
