using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Entities;

public sealed class Customers : BaseEntity<long>
{
    public Customers()
    {
        Lending = new HashSet<Lending>();
        Payments = new HashSet<Payment>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }

    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string PlaceOfBirth { get; set; }
    public Gender Gender { get; set; }
    public bool IsDeleted { get; set; }
    public CustomerFlag Flag { get; set; }
    public string FullName { get; set; }

    public ICollection<Lending> Lending { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public ICollection<BatchInCustomer>? Batch { get; set; } = new HashSet<BatchInCustomer>();
    public string PhoneNumber { get; set; }
}