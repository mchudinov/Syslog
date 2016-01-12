using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public abstract class MessageBase : IMessage
    {
        protected MessageBase(string message, DateTimeOffset? dateTimeOffset = null)
        {
            DateTimeOffset = dateTimeOffset ?? DateTimeOffset.Now;
            MessageText = message;
        }

        public System.DateTimeOffset DateTimeOffset { get; }

        public string MessageText { get; }

        public override string ToString()
        {
            return string.Format("[{0}] {1}",DateTimeOffset,MessageText);
        }
    }
}
