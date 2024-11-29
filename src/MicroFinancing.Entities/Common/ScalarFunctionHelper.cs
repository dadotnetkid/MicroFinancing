using Microsoft.EntityFrameworkCore;

public class ScalarFunctionHelper
{
    [DbFunction("format", IsBuiltIn = true)]
    public static string Format(DateTimeOffset? inputDate, string format) => throw new Exception("Scalar functions are only usable inside linq query expression!");
    [DbFunction("datediff", IsBuiltIn = true)]
    public static long DateDiff(string datePart, DateTimeOffset from, DateTimeOffset to) => throw new Exception("Scalar functions are only usable inside linq query expression!");
}
