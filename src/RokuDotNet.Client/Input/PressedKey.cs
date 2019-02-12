using System;
using System.Diagnostics;

namespace RokuDotNet.Client.Input
{
    public struct PressedKey
    {
        public static implicit operator PressedKey(SpecialKeys key)
        {
            return new PressedKey(key);
        }

        public static implicit operator PressedKey(char key)
        {
            return new PressedKey(key);
        }

        private readonly char? literalKey;
        private readonly SpecialKeys? specialKey;

        public PressedKey(SpecialKeys key)
        {
            this.specialKey = key;
            this.literalKey = null;
        }

        public PressedKey(char key)
        {
            this.specialKey = null;
            this.literalKey = key;
        }

        public void Match(Action<SpecialKeys> onSpecialKey, Action<char> onLiteralKey)
        {
            if (specialKey.HasValue)
            {
                onSpecialKey(this.specialKey.Value);
            }
            else if (literalKey.HasValue)
            {
                onLiteralKey(this.literalKey.Value);
            }
            else
            {
                Debug.Fail("One of SpecialKeys or char should always have a value.");
            }
        }

        public TResult Match<TResult>(Func<SpecialKeys, TResult> onT1, Func<char, TResult> onT2)
        {
            if (specialKey.HasValue)
            {
                return onT1(this.specialKey.Value);
            }
            else if (literalKey.HasValue)
            {
                return onT2(this.literalKey.Value);
            }
            else
            {
                Debug.Fail("One of SpecialKeys or char should always have a value.");

                return default(TResult);
            }
        }

        public override string ToString()
        {
            return this.Match(key => key.ToString(), key => key.ToString());
        }
    }
}