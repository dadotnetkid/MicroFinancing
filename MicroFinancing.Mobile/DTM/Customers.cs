using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SQLite;

public sealed class Customers
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    [Required]
    [Display(Name = "First Name")]
    [JsonProperty("firstName")]
    public string FirstName { get; set; }
    [Display(Name = "Middle Name")]
    [JsonProperty("middleName")]
    public string? MiddleName { get; set; }
    [Display(Name = "Last Name")]
    [JsonProperty("lastName")]
    public string LastName { get; set; }
    [JsonProperty("dateOfBirth")]
    [Display(Name = "Date Of Birth")]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    public DateTime? DateOfBirth { get; set; }
    [Display(Name = "Place Of Birth")]
    [JsonProperty("placeOfBirth")]
    public string? PlaceOfBirth { get; set; }
    [Display(Name = "Address")]
    [JsonProperty("address")]
    public string? Address { get; set; }
    [JsonProperty("fullName")]
    public string Fullname { get; set; }
    [JsonProperty("totalAmountPaid")]
    public decimal? TotalAmountPaid { get; set; }
    [JsonProperty("totalDebt")]
    public decimal? TotalDebt { get; set; }
    [JsonProperty("totalBalance")]
    public decimal? TotalBalance { get; set; }
    [JsonProperty("customerFlag")]
    public CustomerFlag CustomerFlag { get; set; } = CustomerFlag.GoodPayer;
}