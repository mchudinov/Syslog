//using SimpleInjector;

namespace SyslogServer
{
    class Program
    {
        static void Main(string[] args)
        {
//#region SimpleInjector
//            var container = new Container();
//            container.Register<IMessageParser, MessageParser>();
//            container.Register<IMessageStorage>(()=> new MemoryStorage(10000));
//            container.Verify();
//#endregion
//            UDPListener listener = new UDPListener(8888);
//            listener.StartListener(container.GetInstance<IMessageStorage>(), container.GetInstance<IMessageParser>());
        }
    }
}
