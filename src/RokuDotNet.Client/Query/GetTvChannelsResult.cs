using System.Xml.Serialization;

namespace RokuDotNet.Client.Query
{
    [XmlRoot("tv-channels")]
    public sealed class GetTvChannelsResult
    {
        [XmlElement("channel")]
        public TvChannel[] Channels { get; set; }
    }
}