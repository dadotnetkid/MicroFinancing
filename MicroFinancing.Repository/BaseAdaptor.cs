using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.HeatMap.Internal;

namespace MicroFinancing.Repositories
{
    public class BaseAdaptor<T,TKey> : DataAdaptor
    where T : BaseEntity<TKey>
    {
   
        private readonly IRepository<T, TKey> _repository;

        public BaseAdaptor(IRepository<T, TKey> repository)
        {
            _repository = repository;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
        {
            try
            {
                var source = _repository.Entity.AsQueryable();

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
                int count = await source.Cast<T>().CountAsync();
                if (dm.Skip != 0)
                {
                    //Paging
                    source = DataOperations.PerformSkip(source, dm.Skip);
                }
                if (dm.Take != 0)
                {
                    source = DataOperations.PerformTake(source, dm.Take);
                }

                var properties = dm.Params?.FirstOrDefault(x => x.Key == "IncludedProperties").Value?.ToString();
                if (!string.IsNullOrEmpty(properties))
                {
                    foreach (var i in properties.Split(","))
                    {
                        source = source.Include(properties);
                    }
                    
                }
                return dm.RequiresCounts ? new DataResult() { Result = await source.ToListAsync(), Count = count } : (object)source;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
