using System.Collections.Generic;
using System.Xml.Serialization;

namespace RokuDotNet.Client.Query
{
    [XmlRoot("apps")]
    public sealed class GetAppsResult
    {
        [XmlElement("app")]
        public RokuApp[] Apps { get; set; }
    }
}