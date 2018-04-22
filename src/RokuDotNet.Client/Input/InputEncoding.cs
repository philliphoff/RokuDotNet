using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RokuDotNet.Client.Input
{
    public sealed class InputEncoding
    {
        private static ConcurrentDictionary<SpecialKeys, string> keyEncodingCache = new ConcurrentDictionary<SpecialKeys, string>();
    
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
            return $"Lit_{HttpUtility.UrlEncode(key.ToString())}";
        }
    }
}