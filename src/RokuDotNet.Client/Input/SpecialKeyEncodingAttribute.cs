using System;

namespace RokuDotNet.Client.Input
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class SpecialKeyEncodingAttribute : Attribute
    {
        public SpecialKeyEncodingAttribute(string encoding)
        {
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public string Encoding { get; }
    }
}