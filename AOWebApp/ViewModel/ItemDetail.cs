using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AOWebApp.ViewModel
{
    public class ItemDetail
    {

        public Item TheItem { get; set; }

        public int ReviewCount { get; set; }

        public double AverageRating { get; set; }

    }
}
