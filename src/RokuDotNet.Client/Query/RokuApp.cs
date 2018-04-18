using System.Xml.Serialization;

namespace RokuDotNet.Client.Query
{
    public sealed class RokuApp
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("version")]
        public string VersionString { get; set; }

        [XmlText]
        public string Name { get; set; }
    }
}