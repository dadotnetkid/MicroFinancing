using MicroFinancing.Core.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumeration)
        {
            FieldInfo? fi = enumeration.GetType().GetField(enumeration.ToString());

            if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }
            return string.Empty;
        }
        public static string GetColor(this Enum enumeration)
        {
            FieldInfo? fi = enumeration.GetType().GetField(enumeration.ToString());

            if (fi?.GetCustomAttributes(typeof(ColorAttribute), false) is ColorAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Color;
            }
            return string.Empty;
        }
    } 
    public static class StringHelper
    {
        public static string GetDescription(this string enumeration)
        {
            FieldInfo? fi = enumeration.GetType().GetField(enumeration);

            if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }
            return string.Empty;
        }
    }
}
