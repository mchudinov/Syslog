using System.Collections.Generic;
using System.Threading.Tasks;
using Server;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Gui
{
    public sealed partial class MainPage : Page
    {
        public bool AutoScroll { get; set; } = true;
        private readonly List<string> _collectionView = new List<string>();
        private readonly IMessageParser _parser = new MessageParser();
        private ISyslogListener _listener;
        public uint Port { get; set; } = 514;

        public MainPage()
        {
            this.InitializeComponent();
            StartSyslogServer();
            //UpdateCollectionView();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ControlSyslog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (AutoScroll)
            //{
            //    Scroller.ChangeView(0.0f, double.MaxValue, 1.0f);
            //}
        }

        private void StartSyslogServer()
        {
            _listener = new DatagramListener(Port);
            _listener.StartListener();
        }

        private async Task UpdateCollectionView()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(1500);
                    var list = new List<string>();
                    while (!_listener.MessagesQueue.IsEmpty)
                    {
                        string temp;
                        _listener.MessagesQueue.TryDequeue(out temp);
                        if(!string.IsNullOrEmpty(temp))
                            list.Add(_parser.Parse(temp));
                    }
                    _collectionView.AddRange(list);
                }
            });
        }
    }
}
