using RokuDotNet.Client.Input;
using Xunit;

namespace RokuDotNet.Tests.Input
{
    public sealed class InputEncodingTests
    {
        [Theory]
        [InlineData("Unknown", null, null)]
        [InlineData("Lit_r", 'r', null)]
        [InlineData("Lit_%E2%82%AC", '€', null)]
        [InlineData("Home", null, SpecialKeys.Home)]
        [InlineData("Rev", null, SpecialKeys.Reverse)]
        [InlineData("Fwd", null, SpecialKeys.Forward)]
        [InlineData("Play", null, SpecialKeys.Play)]
        [InlineData("Select", null, SpecialKeys.Select)]
        [InlineData("Left", null, SpecialKeys.Left)]
        [InlineData("Right", null, SpecialKeys.Right)]
        [InlineData("Down", null, SpecialKeys.Down)]
        [InlineData("Up", null, SpecialKeys.Up)]
        [InlineData("Back", null, SpecialKeys.Back)]
        [InlineData("InstantReplay", null, SpecialKeys.InstantReplay)]
        [InlineData("Info", null, SpecialKeys.Info)]
        [InlineData("Backspace", null, SpecialKeys.Backspace)]
        [InlineData("Search", null, SpecialKeys.Search)]
        [InlineData("Enter", null, SpecialKeys.Enter)]
        [InlineData("FindRemote", null, SpecialKeys.FindRemote)]
        [InlineData("VolumeDown", null, SpecialKeys.VolumeDown)]
        [InlineData("VolumeMute", null, SpecialKeys.VolumeMute)]
        [InlineData("VolumeUp", null, SpecialKeys.VolumeUp)]
        [InlineData("PowerOff", null, SpecialKeys.PowerOff)]
        [InlineData("ChannelUp", null, SpecialKeys.ChannelUp)]
        [InlineData("ChannelDown", null, SpecialKeys.ChannelDown)]
        [InlineData("InputTuner", null, SpecialKeys.InputTuner)]
        [InlineData("InputHDMI1", null, SpecialKeys.InputHdmi1)]
        [InlineData("InputHDMI2", null, SpecialKeys.InputHdmi2)]
        [InlineData("InputHDMI3", null, SpecialKeys.InputHdmi3)]
        [InlineData("InputHDMI4", null, SpecialKeys.InputHdmi4)]
        [InlineData("InputAV1", null, SpecialKeys.InputAv1)]
        public void DecodeString(string encodedKey, char? expectedDecodedKey, SpecialKeys? expectedDecodedSpecialKey)
        {
            var (decodedKey, decodedSpecialKey) = InputEncoding.DecodeString(encodedKey);

            Assert.Equal(expectedDecodedKey, decodedKey);
            Assert.Equal(expectedDecodedSpecialKey, decodedSpecialKey);
        }
    }
}