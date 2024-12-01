using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.Entities.Common;

public static class DataManagerHelper
{
    public static DataResultDto<T> ToDataResultDto<T>(this object obj)
    {
        if (obj is EntityQueryable<T> query)
        {
            return new DataResultDto<T>()
            {
                Count = query.Count(),
                Result = query
            };
        }

        var dmResult = obj as DataResult;

        return new DataResultDto<T>()
        {
            Result = dmResult.Result as IEnumerable<T>,
            Count = dmResult.Count
        };
    }

    public static string ToJson(this DataManagerRequest dm)
    {
        return JsonSerializer.Serialize(dm);
    }
}

public class DataResultDto<T>
{
    [JsonPropertyName("result")] public IEnumerable<T> Result { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
}