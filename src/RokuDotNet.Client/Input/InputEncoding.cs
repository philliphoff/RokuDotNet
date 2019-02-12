using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

namespace RokuDotNet.Client.Input
{
    public sealed class InputEncoding
    {
        private const string LiteralPrefix = "Lit_";

        private static ConcurrentDictionary<SpecialKeys, string> keyEncodingCache = new ConcurrentDictionary<SpecialKeys, string>();
    
        public static PressedKey? DecodeString(string key)
        {
            if (key.StartsWith(LiteralPrefix))
            {
                string keyString = HttpUtility.UrlDecode(key.Substring(LiteralPrefix.Length));
                char keyChar = keyString[0];

                return keyChar;
            }

            var specialKey = 
                Enum
                    .GetValues(typeof(SpecialKeys))
                    .Cast<SpecialKeys>()
                    .FirstOrDefault(k => StringComparer.Ordinal.Equals(k.GetEnumValueCustomAttribute<SpecialKeyEncodingAttribute>()?.Encoding, key));

            if (specialKey != SpecialKeys.Unknown)
            {
                return specialKey;
            }

            return null;
        }

        public static string EncodeKey(PressedKey key)
        {
            return key.Match(k => EncodeSpecialKey(k), k => EncodeChar(k));
        }

        public static string EncodeSpecialKey(SpecialKeys key)
        {
            return keyEncodingCache.GetOrAdd(
                key,
                newKey =>
                {
                    var attribute = newKey.GetEnumValueCustomAttribute<SpecialKeyEncodingAttribute>();

                    return attribute.Encoding;
                });
        }

        public static string EncodeChar(char key)
        {
            return $"{LiteralPrefix}{HttpUtility.UrlEncode(key.ToString())}";
        }
    }
}