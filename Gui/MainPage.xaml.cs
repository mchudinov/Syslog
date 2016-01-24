using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System.Threading;
using Windows.UI.Core;
using Server;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MyToolkit.Collections;

namespace Gui
{
    public sealed partial class MainPage : Page
    {
        public bool AutoScroll { get; set; } = true;
        private readonly MtObservableCollection<string> _collectionView = new MtObservableCollection<string>();
        private static readonly IMessageParser _parser = new MessageParser();
        private static ISyslogListener _listener;
        public uint Port { get; set; } = 514;
        private const int PeriodUpdateView = 2000;
        
        public MainPage()
        {
            InitializeComponent();
            StartSyslogServer();
            ThreadPoolTimer.CreatePeriodicTimer(MyTimerElapsedHandler, TimeSpan.FromMilliseconds(PeriodUpdateView));
        }
        
        private async void MyTimerElapsedHandler(ThreadPoolTimer timer)
        {
            var list = new List<string>();
            while (!_listener.MessagesQueue.IsEmpty)
            {
                string temp;
                _listener.MessagesQueue.TryDequeue(out temp);
                if (!string.IsNullOrEmpty(temp))
                    list.Add(_parser.Parse(temp));
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                _collectionView.AddRange(list);
            });
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
    }
}
