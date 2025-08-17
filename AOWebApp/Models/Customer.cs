using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AOWebApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [NotMapped]
    [Display(Name = "Customer Name")]
    public string FullName => FirstName + " " + LastName;

    [NotMapped]
    [Display(Name = "Contact Number")]
    public string ContactNumber
    {
        get
        {
            var contact = "";
            if (!string.IsNullOrWhiteSpace(MainPhoneNumber)) { contact = MainPhoneNumber; }
            if (!string.IsNullOrWhiteSpace(SecondaryPhoneNumber))
            {
                contact += (contact.Length > 0 ? " / " : "") + SecondaryPhoneNumber;
            }
            return contact;
        }
    }

    public string Email { get; set; } = null!;

    public string MainPhoneNumber { get; set; } = null!;

    public string? SecondaryPhoneNumber { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
