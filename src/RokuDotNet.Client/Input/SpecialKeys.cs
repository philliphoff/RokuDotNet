namespace RokuDotNet.Client.Input
{
    public enum SpecialKeys
    {
        Unknown = 0,

        [SpecialKeyEncoding("Home")]
        Home,

        [SpecialKeyEncoding("Rev")]
        Reverse,

        [SpecialKeyEncoding("Fwd")]
        Forward,

        [SpecialKeyEncoding("Play")]
        Play,

        [SpecialKeyEncoding("Select")]
        Select,

        [SpecialKeyEncoding("Left")]
        Left,

        [SpecialKeyEncoding("Right")]
        Right,

        [SpecialKeyEncoding("Down")]
        Down,

        [SpecialKeyEncoding("Up")]
        Up,

        [SpecialKeyEncoding("Back")]
        Back,

        [SpecialKeyEncoding("InstantReplay")]
        InstantReplay,

        [SpecialKeyEncoding("Info")]
        Info,

        [SpecialKeyEncoding("Backspace")]
        Backspace,

        [SpecialKeyEncoding("Search")]
        Search,

        [SpecialKeyEncoding("Enter")]
        Enter,

        // Devices that support "Find Remote"

        [SpecialKeyEncoding("FindRemote")]
        FindRemote,

        // Roku TVs

        [SpecialKeyEncoding("VolumeDown")]
        VolumeDown,

        [SpecialKeyEncoding("VolumeMute")]
        VolumeMute,

        [SpecialKeyEncoding("VolumeUp")]
        VolumeUp,

        [SpecialKeyEncoding("PowerOff")]
        PowerOff,

        /// <remarks>
        /// This key is not officially listed in the Roku External Control API.
        /// </remarks>
        [SpecialKeyEncoding("PowerOn")]
        PowerOn,

        // Supported when watching the TV tuner

        [SpecialKeyEncoding("ChannelUp")]
        ChannelUp,

        [SpecialKeyEncoding("ChannelDown")]
        ChannelDown,

        // TVs also support changing inputs

        [SpecialKeyEncoding("InputTuner")]
        InputTuner,

        [SpecialKeyEncoding("InputHDMI1")]
        InputHdmi1,

        [SpecialKeyEncoding("InputHDMI2")]
        InputHdmi2,

        [SpecialKeyEncoding("InputHDMI3")]
        InputHdmi3,

        [SpecialKeyEncoding("InputHDMI4")]
        InputHdmi4,

        [SpecialKeyEncoding("InputAV1")]
        InputAv1
    }
}