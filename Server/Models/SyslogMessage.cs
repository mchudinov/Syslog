using System;
using System.Collections.Generic;

namespace Server.Models
{
    public class SyslogMessage : MessageBase
    {
        public const Facility DefaultFacility = Facility.UserLevelMessages;
        public const Severity DefaultSeverity = Severity.Informational;
        private const string DateTimeFormat = "s";

        public SyslogMessage(
            string message,
            DateTimeOffset? dateTimeOffset = null,
            Severity? severity = null,
            string hostName = "",
            Facility? facility = null,
            string appName = "",
            string procId = "",
            string msgId = "",
            params StructuredDataElement[] structuredDataElements)
            : base(message, dateTimeOffset)
        {
            Facility = facility ?? DefaultFacility;
            Severity = severity ?? DefaultSeverity;
            HostName = hostName;
            AppName = appName;            
            ProcId = procId;
            MsgId = msgId;
            StructuredDataElements = structuredDataElements;
        }

        public Facility Facility { get; }

        public Severity Severity { get; }

        public string HostName { get; set; }

        public string AppName { get; }

        public string ProcId { get; }

        public string MsgId { get; }
        
        public IEnumerable<StructuredDataElement> StructuredDataElements { get; }

        public override string ToString()
        {
            return string.Format("[{0}] [{1}] {2}", DateTimeOffset.ToString(DateTimeFormat), Severity, MessageText); ;
        }
    }
}
