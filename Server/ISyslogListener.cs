namespace Server
{
    public interface ISyslogListener
    {
        void StartListener(IMessageStorage storage, IMessageParser parser);

        /// <summary>
        /// Maximum size of the syslog message that can be received in bytes
        /// </summary>
        uint MaxMessageLenghtInBytes { get; set; }
    }
}
