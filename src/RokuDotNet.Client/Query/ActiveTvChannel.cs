using System.Xml.Serialization;

namespace RokuDotNet.Client.Query
{
    public sealed class ActiveTvChannel : TvChannel
    {
        [XmlElement("active-input")]
        public bool ActiveInput { get; set; }

        [XmlElement("program-analog-audio")]
        public string ProgramAnalogAudio { get; set; }

        [XmlElement("program-audio-format")]
        public string ProgramAudioFormat { get; set; }

        [XmlElement("program-audio-formats")]
        public string ProgramAudioFormats { get; set; }

        [XmlElement("program-audio-language")]
        public string ProgramAudioLanguage { get; set; }

        [XmlElement("program-audio-languages")]
        public string ProgramAudioLanguages { get; set; }

        [XmlElement("program-description")]
        public string ProgramDescription { get; set; }

        [XmlElement("program-digital-audio")]
        public string ProgramDigitalAudio { get; set; }

        [XmlElement("program-has-cc")]
        public string ProgramHasClosedCaptioning { get; set; }

        [XmlElement("program-is-blocked")]
        public bool ProgramIsBlocked { get; set; }

        [XmlElement("program-ratings")]
        public string ProgramRatings { get; set; }

        [XmlElement("program-title")]
        public string ProgramTitle { get; set; }

        [XmlElement("signal-mode")]
        public string SignalMode { get; set; }

        [XmlElement("signal-quality")]
        public int SignalQuality { get; set; }

        [XmlElement("signal-stalled-pts-cnt")]
        public int SignalStalledPointsCount { get; set; }

        [XmlElement("signal-state")]
        public string SignalState { get; set; }

        [XmlElement("signal-strength")]
        public string SignalStrength { get; set; }
    }
}