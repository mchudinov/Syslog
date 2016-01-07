using System.Collections.ObjectModel;
using Server;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Gui
{
    public sealed partial class MainPage : Page
    {
        public bool AutoScroll { get; set; } = true;
        ObservableCollection<string> dc = new ObservableCollection<string> { "sdfsdf", "123123" };

        public MainPage()
        {
            this.InitializeComponent();
            StartSyslogServer();
        }

        void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        void ControlSyslog_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AutoScroll)
            {
                Scroller.ChangeView(0.0f, double.MaxValue, 1.0f);
            }
        }

        static void StartSyslogServer()
        {
            var mp = new MessageParser();
            var ms = new MemoryStorage(10000);
            DatagramListener listener = new DatagramListener(8888);
            listener.StartListener(ms, mp);
        }
    }
}
