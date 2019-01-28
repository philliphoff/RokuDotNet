using System.Xml.Serialization;

namespace RokuDotNet.Client.Apps
{
    [XmlRoot("tv-channels")]
    public sealed class GetTvChannelsResult
    {
        [XmlElement("channel")]
        public TvChannel[] Channels { get; set; }
    }
}