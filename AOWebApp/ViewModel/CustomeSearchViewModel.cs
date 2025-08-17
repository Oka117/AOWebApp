using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.ViewModel
   
{
    public class CustomeSearchViewModel
    {
        [Required(ErrorMessage ="You must provide a Customer Name")]
        public string SearchText { get; set; }
        public string Suburb { get; set; }
        public SelectList SuburbList { get; set; }
        public Lazy<List<Customer>> CustomerList { get; set; }
        public List<String> NameList { get; set; }


    }
}
