using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Models
{
    public class SyslogMessage : IMessage
    {
        public const Facility DefaultFacility = Facility.UserLevelMessages;
        public const Severity DefaultSeverity = Severity.Informational;
        private const string DefaultHostName = "localhost";

        public SyslogMessage(
            string message,
            Severity? severity = null,
            DateTimeOffset? dateTimeOffset = null,
            string hostName = DefaultHostName,
            Facility? facility = null,
            string appName = "",
            string procId = "",
            string msgId = "",
            params StructuredDataElement[] structuredDataElements)
        {
            DateTimeOffset = dateTimeOffset ?? DateTimeOffset.Now;
            Facility = facility ?? DefaultFacility;
            Severity = severity ?? DefaultSeverity;
            HostName = hostName;
            AppName = appName;
            MessageText = message;
            ProcId = procId;
            MsgId = msgId;
            StructuredDataElements = structuredDataElements;
        }

        public Facility Facility { get; }

        public Severity Severity { get; }

        public DateTimeOffset DateTimeOffset { get; }

        public string HostName { get; set; }

        public string AppName { get; }

        public string ProcId { get; }

        public string MsgId { get; }

        public string MessageText { get; }

        public IEnumerable<StructuredDataElement> StructuredDataElements { get; }

        public override string ToString()
        {
            return MessageText;
        }
    }
}
