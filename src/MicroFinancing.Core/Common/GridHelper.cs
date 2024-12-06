using Newtonsoft.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor;
using System.ComponentModel;

namespace MicroFinancing.Core.Common;

public static class GridHelper
{
    public static T? GetByKey<T>(this DataManagerRequest dm, string key)
    {
        try
        {
            var value = dm.Params.FirstOrDefault(x => String.Equals(x.Key, key, StringComparison.CurrentCultureIgnoreCase)).Value?.ToString();
            T? parseValue = default;
            if (typeof(T) == typeof(Guid))
            {
                parseValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
            }

            else
            {
                parseValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
            }
            return parseValue;
        }
        catch (Exception e)
        {

        }
        return default;
    }

    public static T? GetItemObject<T>(this DataManagerRequest dm) where T : class
    {
        try
        {
            var key = typeof(T).Name;
            return GetItemObject<T>(dm, key);
        }
        catch (Exception e)
        {
        }
        return default;
    }
    public static T? GetItemObject<T>(this DataManagerRequest dm, string key) where T : class
    {
        try
        {
            var item = dm.Params.FirstOrDefault(c => c.Key == key).Value?.ToString();
            return JsonConvert.DeserializeObject<T>(item);
        }
        catch (Exception e)
        {
        }
        return default;
    }

    public static Query AddItemObject<T>(this Query query, T item)
    {
        var key = typeof(T).Name;
        query.AddParams(key, item);
        return query;
    }
    public static Query RemoveItemObject<T>(this Query query)
    {
        var key = typeof(T).Name;
        query = RemoveByKey(query, key);
        return query;
    }
    public static Query RemoveByKey(this Query query, string key)
    {
        if (query.Queries.Params != null) query.Queries.Params.Remove(key);
        return query;
    }
}
