using MicroFinancing.Entities;
using MicroFinancing.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.DataTransferModel;

public class CustomerBaseDTM
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
    public virtual string FullName { get; set; }
    public decimal? TotalAmountPaid { get; set; }
    public decimal? TotalDebt { get; set; }
    public decimal? TotalBalance { get; set; }
    public CustomerFlag CustomerFlag { get; set; } = CustomerFlag.GoodPayer;
    public bool HasActiveLoan { get; set; }
}
public sealed class CustomerGridDTM : CustomerBaseDTM
{

}
public sealed class CreateCustomerDTM : CustomerBaseDTM
{


}
public sealed class CustomerDetailDTM : CustomerBaseDTM
{
    public override string FullName => $"{FirstName} {LastName}";
   
}