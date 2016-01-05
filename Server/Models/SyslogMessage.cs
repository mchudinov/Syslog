using System;
using System.Collections.Generic;

namespace Server.Models
{
    public class SyslogMessage
    {
        public static Facility DefaultFacility = Facility.UserLevelMessages;
        public static Severity DefaultSeverity = Severity.Informational;
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
            Message = message;
            ProcId = procId;
            MsgId = msgId;
            StructuredDataElements = structuredDataElements;
        }

        public Facility Facility { get; }

        public Severity Severity { get; }

        public DateTimeOffset DateTimeOffset { get; }

        public string HostName { get; }

        public string AppName { get; }

        public string ProcId { get; }

        public string MsgId { get; }

        public string Message { get; }

        public IEnumerable<StructuredDataElement> StructuredDataElements { get; }
    }
}
