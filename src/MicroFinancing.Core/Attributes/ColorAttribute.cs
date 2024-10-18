namespace MicroFinancing.Core.Attributes;

public class ColorAttribute : Attribute
{
    public ColorAttribute(string Color)
    {
        this.Color = Color;
    }

    public string Color { get; private set; }
}
public class InterestAttribute : Attribute
{
    public InterestAttribute(string Rate)
    {
        this.Rate = Convert.ToDecimal(Rate);
    }
    public decimal Rate { get; private set; }
}
