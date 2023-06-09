using MicroFinancing.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Entities;


public sealed class Customers
{
    public Customers()
    {
        Lending = new HashSet<Lending>();
        Payments = new HashSet<Payment>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
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
}