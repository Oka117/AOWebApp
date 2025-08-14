using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.ViewModel


{
    public class ItemSearchViewModel
    {
        public string? SearchText { get; set; }
        public int? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
        public List<ItemDetail> ItemList { get; set; } = new();
    }
}
