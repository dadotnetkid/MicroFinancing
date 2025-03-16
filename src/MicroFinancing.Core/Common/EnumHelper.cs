using System.ComponentModel;

using MicroFinancing.Core.Attributes;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Core.Common;

public static class EnumHelper
{
    public static string GetDescription(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
        {
            return attributes.First()
                             .Description;
        }

        return string.Empty;
    }

    public static T GetDefault<T>(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(DefaultValueAttribute), false) is DefaultValueAttribute[] attributes && attributes.Any())
        {
            return (T)Convert.ChangeType(attributes.First()
                                                   .Value,
                                         typeof(T));
        }

        return default!;
    }

    public static decimal? GetInterest(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(InterestAttribute), false) is InterestAttribute[] attributes && attributes.Any())
        {
            return attributes.FirstOrDefault()?.Rate;
        }

        return default!;
    }

    public static string GetColor(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(ColorAttribute), false) is ColorAttribute[] attributes && attributes.Any())
        {
            return attributes.First()
                             .Color;
        }

        return string.Empty;
    }

    public static LendingEnumeration.PaymentSchedule GetPaymentSchedule(this Enum enumeration)
    {
        var fi = enumeration.GetType()
                            .GetField(enumeration.ToString());

        if (fi?.GetCustomAttributes(typeof(PaymentScheduleAttribute), false) is PaymentScheduleAttribute[] attributes && attributes.Any())
        {
            return attributes.First()
                             .PaymentSchedule;
        }

        return LendingEnumeration.PaymentSchedule.Daily;
    }

    public static List<GenericDropItem<T>> GetDropDown<T>(this Type type)
    {
        return type.GetEnumValues()
            .OfType<T>()
            .ToDictionary(key => key, val => (val as Enum).GetDescription())
            .Select(c => new GenericDropItem<T>()
            {
                Value = c.Key,
                Text = c.Value
            }).ToList();
    }

    public static T GetValueFromDescription<T>(this string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
                                             typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }

        return default(T);

        // Or return default(T);
    }
}

public class GenericDropItem<T>
{
    public T Value { get; set; }
    public string Text { get; set; }
}