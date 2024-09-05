using System.Collections;

using Microsoft.EntityFrameworkCore;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.ComponentModel;

using Newtonsoft.Json;

namespace MicroFinancing.Core.Common;

public static class GridHelper
{
    public static async Task<object> ToDataResult<T>(this IQueryable<T> source,
                                                     DataManagerRequest dm)
    {
        if (dm.Search is { Count: > 0 })
        {
            // Searching
            source = DataOperations.PerformSearching(source, dm.Search);
        }

        if (dm.Sorted is { Count: > 0 })
        {
            // Sorting
            source = DataOperations.PerformSorting(source, dm.Sorted);
        }

        if (dm.Where is { Count: > 0 })
        {
            // Filtering
            source = DataOperations.PerformFiltering(source, dm.Where, dm.Where[0].Operator);
        }

        var count = source.Cast<T>()
                          .Count();

        if (dm.Skip != 0)
        {
            //Paging
            source = DataOperations.PerformSkip(source, dm.Skip);
        }

        if (dm.Take != 0)
        {
            source = DataOperations.PerformTake(source, dm.Take);
        }

        if (dm.Group != null)
        {
            IEnumerable query =  source.AsEnumerable();
            foreach (var grp in dm.Group)
            {
                query = DataUtil.Group<T>(source, grp, dm.Aggregates, 0, dm.GroupByFormatter);
            }

            return dm.RequiresCounts
                ? new DataResult
                    { Result = query, Count = count }
                : query;
        }

        return dm.RequiresCounts
            ? new DataResult
            { Result = await source.ToListAsync(), Count = count }
            : source;
    }

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
                parseValue = (T)Convert.ChangeType(value, typeof(T));
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
