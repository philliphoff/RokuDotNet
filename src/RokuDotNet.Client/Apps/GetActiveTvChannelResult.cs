using System.Xml.Serialization;

namespace RokuDotNet.Client.Apps
{
    [XmlRoot("tv-channel")]
    public sealed class GetActiveTvChannelResult
    {
        [XmlElement("channel")]
        public ActiveTvChannel ActiveChannel { get; set; }
    }
}