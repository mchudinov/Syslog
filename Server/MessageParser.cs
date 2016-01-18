using System.Text.RegularExpressions;

namespace Server
{
    public class MessageParser : IMessageParser
    {
        public string Parse(string rawmessage)
        {
            var retValue = string.Empty;
            if (!string.IsNullOrEmpty(rawmessage))
            {
                retValue = rawmessage.Trim();
                if ('<' == retValue[0])
                {
                    retValue = Regex.Replace(retValue, @"^<\d+>", "");
                }
            }
            return retValue;
        }
    }
}
