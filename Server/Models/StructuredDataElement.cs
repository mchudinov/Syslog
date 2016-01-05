using System.Collections.Generic;

namespace Server.Models
{
    public class StructuredDataElement
    {
        // RFC 5424 specifies that you must provide a private enterprise number. If none specified, using example number reserved for documentation (see RFC)
        public const string DefaultPrivateEnterpriseNumber = "32473";

        public StructuredDataElement(string sdId, Dictionary<string, string> parameters)
        {
            this.SdId = sdId.Contains("@") ? sdId : sdId + "@" + DefaultPrivateEnterpriseNumber;
            this.Parameters = parameters;
        }

        public string SdId { get; }

        public Dictionary<string, string> Parameters { get; }
    }
}
