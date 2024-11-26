using System.ComponentModel;

namespace MicroFinancing.Core.Enumeration;

public class PaymentEnum
{
    public enum PaymentType
    {
        Cash,
        GCash,
        System
    }
}
public class LendingEnum
{
    public enum Type
    {

    }
}

public class BranchEnum
{
    public enum Branch
    {
        [Description("Nueva Vizcaya")]
        NuevaVizcaya,
        [Description("Isabela")]
        Isabela
    }
}
