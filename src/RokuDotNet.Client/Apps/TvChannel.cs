using System.Xml.Serialization;

namespace RokuDotNet.Client.Apps
{
    public class TvChannel
    {
        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlElement("number")]
        public string Number { get; set; }

        [XmlElement("physical-frequency")]
        public int PhysicalFrequency { get; set; }

        [XmlElement("physical-number")]
        public int PhysicalNumber { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("user-favorite")]
        public string UserFavorite { get; set; }

        [XmlElement("user-hidden")]
        public string UserHidden { get; set; }
    }
}