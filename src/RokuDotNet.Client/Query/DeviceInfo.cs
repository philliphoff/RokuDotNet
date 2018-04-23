using System.Xml.Serialization;

namespace RokuDotNet.Client.Query
{
    [XmlRoot("device-info")]
    public sealed class DeviceInfo
    {
        [XmlElement("udn")]
        public string Udn { get; set; }

        [XmlElement("serial-number")]
        public string SerialNumber { get; set; }

        [XmlElement("device-id")]
        public string DeviceId { get; set; }

        [XmlElement("advertising-id")]
        public string AdvertisingId { get; set; }

        [XmlElement("vendor-name")]
        public string VendorName { get; set; }

        [XmlElement("model-name")]
        public string ModelName { get; set; }

        [XmlElement("model-number")]
        public string ModelNumber { get; set; }

        [XmlElement("model-region")]
        public string ModelRegion { get; set; }

        [XmlElement("is-tv")]
        public bool IsTv { get; set; }

        [XmlElement("is-stick")]
        public bool IsStick { get; set; }

        [XmlElement("screen-size")]
        public int ScreenSize { get; set; }

        [XmlElement("panel-id")]
        public int PanelId { get; set; }

        [XmlElement("tuner-type")]
        public string TunerType { get; set; }

        [XmlElement("supports-ethernet")]
        public bool SupportsEthernet { get; set; }

        [XmlElement("wifi-mac")]
        public string WifiMacAddress { get; set; }

        [XmlElement("network-type")]
        public string NetworkType { get; set; }

        [XmlElement("user-device-name")]
        public string UserDeviceName { get; set; }

        [XmlElement("software-version")]
        public string SoftwareVersion { get; set; }

        [XmlElement("software-build")]
        public int SoftwareBuild { get; set; }

        [XmlElement("secure-device")]
        public bool SecureDevice { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("locale")]
        public string Locale { get; set; }

        [XmlElement("time-zone")]
        public string TimeZone { get; set; }

        [XmlElement("time-zone-offset")]
        public int TimeZoneOffset { get; set; }

        [XmlElement("power-mode")]
        public string PowerMode { get; set; }

        [XmlElement("supports-suspend")]
        public bool SupportsSuspend { get; set; }

        [XmlElement("supports-find-remote")]
        public bool SupportsFindRemote { get; set; }

        [XmlElement("supports-audio-guide")]
        public bool SupportsAudioGuide { get; set; }

        [XmlElement("developer-enabled")]
        public bool DeveloperEnabled { get; set; }

        [XmlElement("keyed-developer-id")]
        public string KeyedDeveloperId { get; set; }

        [XmlElement("search-enabled")]
        public bool SearchEnabled { get; set; }

        [XmlElement("search-channels-enabled")]
        public bool SearchChannelsEnabled { get; set; }

        [XmlElement("voice-search-enabled")]
        public bool VoiceSearchEnabled { get; set; }

        [XmlElement("notifications-enabled")]
        public bool NotificationsEnabled { get; set; }

        [XmlElement("notifications-first-use")]
        public bool NotificationsFirstUse { get; set; }

        [XmlElement("supports-private-listening")]
        public bool SupportsPrivateListening { get; set; }

        [XmlElement("supports-private-listening-dtv")]
        public bool SupportsPrivateListeningDtv { get; set; }

        [XmlElement("supports-warm-standby")]
        public bool SupportsWarmStandby { get; set; }

        [XmlElement("headphones-connected")]
        public bool HeadphonesConnected { get; set; }

        [XmlElement("supports-ecs-textedit")]
        public bool SupportsEcsTextEdit { get; set; }

        [XmlElement("supports-ecs-microphone")]
        public bool SupportsEcsMicrophone { get; set; }

        [XmlElement("supports-wake-on-wlan")]
        public bool SupportsWakeOnLan { get; set; }

        [XmlElement("has-play-on-roku")]
        public bool HasPlayOnRoku { get; set; }

        [XmlElement("has-mobile-screensaver")]
        public bool HasMobileScreensaver { get; set; }
    }
}