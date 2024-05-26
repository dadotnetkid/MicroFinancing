using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.Core.Common
{
    public static class GridHelper
    {
        public static Task<object> ToDataResult<T>(this IQueryable<T> source, DataManagerRequest dm)
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
            var count =  source.Cast<T>().Count();
            if (dm.Skip != 0)
            {
                //Paging
                source = DataOperations.PerformSkip(source, dm.Skip);
            }
            if (dm.Take != 0)
            {
                source = DataOperations.PerformTake(source, dm.Take);
            }

            return Task.FromResult(dm.RequiresCounts ? new DataResult() { Result =  source.ToList(), Count = count } : (object)source);
        }
    }
}
