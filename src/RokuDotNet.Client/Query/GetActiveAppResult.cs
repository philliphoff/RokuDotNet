using System.Xml.Serialization;

namespace RokuDotNet.Client.Query
{
    [XmlRoot("active-app")]
    public sealed class GetActiveAppResult
    {
        [XmlElement("app")]
        public RokuApp ActiveApp { get; set; }
    }
}