namespace Server.Models
{
    public interface IMessage
    {
        string ToString();

        System.DateTimeOffset DateTimeOffset { get; }

        string MessageText { get; }
    }
}
