using System.Collections;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.Entities.Common;

public static class GridHelper
{
    public static async Task<object> ToDataResult<T>(this IQueryable<T> source,
                                                     DataManagerRequest dm)
    {
        if (!source.Any())
        {
            return dm.RequiresCounts
                ? new DataResult
                    { Result =  source.ToList(), Count = source.Count() }
                : source;
        }
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

        IDictionary<string, object> aggregates=default!;

        if (dm.Aggregates != null && dm.Aggregates.Any())
        {
            aggregates = DataUtil.PerformAggregation(source, dm.Aggregates);
        }


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
            IEnumerable query = source.AsEnumerable();
            foreach (var grp in dm.Group)
            {
                query = DataUtil.Group<T>(source, grp, dm.Aggregates, 0, dm.GroupByFormatter);
            }

            return dm.RequiresCounts
                ? new DataResult
                { Result = query, Count = count }
                : query;
        }


        if (aggregates is null)
        {
            return dm.RequiresCounts
                ? new DataResult
                    { Result = await source.ToListAsync(), Count = count }
                : source;
        }

        return dm.RequiresCounts
            ? new DataResult
                { Result = await source.ToListAsync(), Count = count,Aggregates = aggregates}
            : source;
    }
    public class ReturnType<T>
    {
        public int Count { get; set; }

        public IEnumerable<T> Result { get; set; }

        public IDictionary<string, object> Aggregates { get; set; }
    }
  
}
