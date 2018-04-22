using System;
using System.Linq;
using System.Reflection;

namespace RokuDotNet.Client.Input
{
    public static class EnumExtensions
    {
        public static TAttributeType GetEnumValueCustomAttribute<TAttributeType>(this Enum value)
            where TAttributeType : Attribute
        {
            var type = value.GetType();

            return type.GetMember(Enum.GetName(type, value)).First().GetCustomAttribute<TAttributeType>();
        }
    }
}