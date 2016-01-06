using System;
using System.Linq;
using System.Net;
using SyslogServer.Models;

namespace SyslogServer
{
    public class MessageParser : IMessageParser
    {
        public SyslogMessage Parse(string rawmessage, IPAddress address)
        {
            var message = GetMessage(rawmessage);
            var severity = GetSeverity(rawmessage);
            var datetime = GetDateTime(rawmessage);
            var facility = GetFacility(rawmessage);
            var sm = new SyslogMessage(message, severity, datetime, address?.ToString() ?? string.Empty, facility);
            return sm;
        }

        private static Facility GetFacility(string rawmessage)
        {
            var retvalue = SyslogMessage.DefaultFacility;
            try
            {
                var temp = GetFacilityAndSeverity(rawmessage);
                retvalue = (Facility)(int.Parse(temp) / 8);
            }
            catch (Exception)
            {
                // ignored
            }
            return retvalue;
        }

        private static Severity GetSeverity(string rawmessage)
        {
            var retvalue = SyslogMessage.DefaultSeverity;
            try
            {
                var temp = GetFacilityAndSeverity(rawmessage);
                retvalue = (Severity)(int.Parse(temp) % 8);
            }
            catch (Exception)
            {
                // ignored
            }
            return retvalue;
        }

        private static string GetFacilityAndSeverity(string rawmessage)
        {
            var retvalue = string.Empty;
            try
            {
                retvalue = rawmessage.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries).First();
            }
            catch (Exception)
            {
                // ignored
            }
            return retvalue;
        }

        private static string GetMessage(string rawmessage)
        {
            var retValue = rawmessage ?? string.Empty;
            try
            {
                string[] words = rawmessage.Split(' ');
                if (IsStringPriority(words[0]) || IsStringDateTime(words[0]))
                {
                    retValue = retValue.Replace(words[0], "");
                }
                if (IsStringPriority(words[1]) || IsStringDateTime(words[1]))
                {
                    retValue = retValue.Replace(words[1], "");
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return retValue.Trim();
        }

        private static DateTime GetDateTime(string rawmessage)
        {
            var retValue = DateTime.Now;
            try
            {
                string[] words = rawmessage.Split(' ');
                var datestring = words[0];
                if (datestring.Contains("<"))
                {
                    datestring = words[1];
                    retValue = DateTime.Parse(datestring);
                }
                else
                {
                    retValue = DateTime.Parse(datestring);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return retValue;
        }

        private static bool IsStringPriority(string str)
        {
            return str.Contains("<");
        }

        private static bool IsStringDateTime(string str)
        {
            DateTime temp;
            return DateTime.TryParse(str, out temp); 
        }
    }
}
