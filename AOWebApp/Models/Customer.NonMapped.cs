using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models
{
    public partial class Customer
    {
        [NotMapped]
        [Display(Name = "Customer Name")]
        public string FullName => $"{FirstName} {LastName}";

        [NotMapped]
        [Display(Name = "Contact Number")]
        public string ContactNumber
        {
            get
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(MainPhoneNumber))
                {
                    parts.Add(MainPhoneNumber);
                }
                if (!string.IsNullOrWhiteSpace(SecondaryPhoneNumber))
                {
                    parts.Add(SecondaryPhoneNumber);
                }
                return string.Join("<br />", parts);
            }
        }
    }
}