using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class CustomerDTM
{
    public long Id { get; set; }
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Middle Name")]
    public string? MiddleName { get; set; }
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Display(Name = "Date Of Birth")]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    public DateTime? DateOfBirth { get; set; }
    [Display(Name = "Place Of Birth")]
    public string? PlaceOfBirth { get; set; }
    [Display(Name = "Address")]
    public string? Address { get; set; }
    public virtual string Fullname { get; set; }
    public decimal? TotalAmountPaid { get; set; }
    public decimal? TotalDebt { get; set; }
    public decimal? TotalBalance { get; set; }
    public CustomerFlag CustomerFlag { get; set; } = CustomerFlag.GoodPayer;
}
public enum CustomerFlag
{

    [Description("Good Payer")]
    [Color("success")]
    GoodPayer = 0,
    [Description("Late Payer")]
    [Color("warning")]
    LatePayer = 1,
    [Description("Not Paying")]
    [Color("danger")]
    NotPaying = 2,
}

public class ColorAttribute : Attribute
{
    public string Color { get; private set; }

    public ColorAttribute(string Color)
    {
        this.Color = Color;
    }
}