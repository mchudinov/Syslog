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

        public MainPage()
        {
            _parser = new MessageParser();
            _storage = new MemoryStorage();
            this.InitializeComponent();
            _collectionView = new ObservableCollectionView<string>(_storage.Messages);
            StartSyslogServer();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ControlSyslog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AutoScroll)
            {
                Scroller.ChangeView(0.0f, double.MaxValue, 1.0f);
            }
        }

        private void StartSyslogServer()
        {
            DatagramListener listener = new DatagramListener(514);
            listener.StartListener(_storage, _parser);
        }
    }
}
