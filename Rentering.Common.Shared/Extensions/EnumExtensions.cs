using System;
using System.ComponentModel;
using System.Reflection;

namespace Rentering.Common.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
