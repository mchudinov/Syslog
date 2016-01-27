using System;
using System.Collections.Generic;
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
        public bool? AutoScroll { get; set; } = true;
        private readonly MtObservableCollection<string> _collection = new MtObservableCollection<string>();
        private readonly ObservableCollectionView<string> _collectionView;
        private static readonly IMessageParser _parser = new MessageParser();
        private static ISyslogListener _listener;
        public uint Port { get; set; } = 514;
        private const int PeriodUpdateView = 2000;
        
        public MainPage()
        {
            InitializeComponent();
            _collectionView = new ObservableCollectionView<string>(_collection);
            StartSyslogServer();
            ThreadPoolTimer.CreatePeriodicTimer(MyTimerElapsedHandler, TimeSpan.FromMilliseconds(PeriodUpdateView));
        }
        
        private async void MyTimerElapsedHandler(ThreadPoolTimer timer)
        {
            if (_listener.MessagesQueue.Count > 0)
            {
                var list = new List<string>();
                while (_listener.MessagesQueue.Count > 0)
                {
                    string temp;
                    _listener.MessagesQueue.TryDequeue(out temp);
                    if (!string.IsNullOrEmpty(temp))
                        list.Add(_parser.Parse(temp));
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    _collection.AddRange(list);
                });
            }
            _listener.StartListener();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _collectionView.Filter = c => c.ToLower().Contains(FilterTextBox.Text.ToLower());
        }

        private void ControlSyslog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((bool)AutoScroll)
            {
                Scroller.ChangeView(0.0f, double.MaxValue, 1.0f);
            }
        }

        private void StartSyslogServer()
        {
            _listener = new DatagramListener(Port);
            _listener.StartListener();
        }

        private void CheckBoxAutoscroll_Checked(object sender, RoutedEventArgs e)
        {
            AutoScroll = ((CheckBox)sender).IsChecked;
        }
    }
}
