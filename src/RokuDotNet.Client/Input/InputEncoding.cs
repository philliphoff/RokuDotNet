using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RokuDotNet.Client.Input
{
    public sealed class InputEncoding
    {
        private const string LiteralPrefix = "Lit_";

        private static ConcurrentDictionary<SpecialKeys, string> keyEncodingCache = new ConcurrentDictionary<SpecialKeys, string>();
    
        public static (char?, SpecialKeys?) DecodeString(string key)
        {
            if (key.StartsWith(LiteralPrefix))
            {
                string keyString = HttpUtility.UrlDecode(key.Substring(LiteralPrefix.Length));
                char keyChar = keyString[0];

                return (keyChar, null);
            }

            var specialKey = 
                Enum
                    .GetValues(typeof(SpecialKeys))
                    .Cast<SpecialKeys>()
                    .FirstOrDefault(k => StringComparer.Ordinal.Equals(k.GetEnumValueCustomAttribute<SpecialKeyEncodingAttribute>()?.Encoding, key));

            if (specialKey != SpecialKeys.Unknown)
            {
                return (null, specialKey);
            }

            return (null, null);
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