namespace Server
{
    public interface IUdpListener
    {
        void StartListener(IMessageStorage storage, IMessageParser parser);
    }
}
