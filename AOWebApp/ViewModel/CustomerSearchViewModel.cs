using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AOWebApp.ViewModel
{
    public class CustomerSearchViewModel
    {
        public string? SearchText { get; set; }
        public string? Suburb { get; set; }
        public SelectList? SuburbList { get; set; }
        public List<Customer> CustomerList { get; set; } = new();
    }
}