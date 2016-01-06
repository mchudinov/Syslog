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
            //await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction) => StartSyslogServer()));
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
            //#region SimpleInjector
            //var container = new SimpleInjector.Container();
            //container.Register<IMessageParser, MessageParser>();
            var mp = new MessageParser();
            var ms = new MemoryStorage(10000);
            //container.Register<IMessageStorage>(() => new MemoryStorage(10000));
            //container.Verify();
            //#endregion
            UdpListener listener = new UdpListener(8888);
            listener.StartListener(ms, mp);
        }
    }
}
