namespace Server
{
    public interface ISyslogListener
    {
        void StartListener();

        System.Collections.Concurrent.ConcurrentQueue<string> MessagesQueue { get; }
    }
}
