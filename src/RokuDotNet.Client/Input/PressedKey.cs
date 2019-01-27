namespace RokuDotNet.Client.Input
{
    public sealed class PressedKey : OneOf<SpecialKeys, char>
    {
        public static implicit operator PressedKey(SpecialKeys key)
        {
            return new PressedKey(key);
        }

        public static implicit operator PressedKey(char key)
        {
            return new PressedKey(key);
        }

        public PressedKey(SpecialKeys key) : base(key)
        {
        }

        public PressedKey(char key) : base(key)
        {
        }
    }
}