using System.Xml.Serialization;

namespace RokuDotNet.Client.Apps
{
    [XmlRoot("active-app")]
    public sealed class GetActiveAppResult
    {
        [XmlElement("app")]
        public RokuApp ActiveApp { get; set; }
    }
}