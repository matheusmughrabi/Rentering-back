using System;
using System.ComponentModel;
using System.Reflection;

namespace Rentering.Common.Shared.Enums
{
    public static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
