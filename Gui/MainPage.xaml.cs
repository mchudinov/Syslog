using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Server;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MyToolkit.Collections;

namespace Gui
{
    public sealed partial class MainPage : Page
    {
        public bool AutoScroll { get; set; } = true;
        private readonly ObservableCollectionView<string> _collectionView;
        private readonly IMessageParser _parser;
        private readonly IMessageStorage _storage;
        private DatagramListener _listener;
        public uint Port { get; set; } = 514;

        public MainPage()
        {
            _parser = new MessageParser();
            _storage = new MemoryStorage();
            this.InitializeComponent();
            var temp = new List<string>
            {
                "asdasdasd",
                "sdfsdf",
                "sfsdf",
                "sfsdfsdf",
                "fgfghfhfh",
                "sdfsdfsdf",
                "asdasdasd",
                "sdfsdf",
                "sfsdf",
                "sfsdfsdf",
                "fgfghfhfh",
                "sdfsdfsdf",
                "asdasdasd",
                "sdfsdf",
                "sfsdf",
                "sfsdfsdf",
                "fgfghfhfh",
                "sdfsdfsdf"
            };
            _collectionView = new ObservableCollectionView<string>(temp);
            temp.Add("aaa");
            ((List<string>)(_collectionView.Items)).AddRange(new List<string>{"xxx","zzz"});
            //var _books = new ObservableCollection<string>();
            //new ObservableCollectionView<string>(_storage.Messages);
            StartSyslogServer(); 
            this.UpdateLayout();
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
            _listener.StartListener(_storage, _parser);
        }
    }
}
