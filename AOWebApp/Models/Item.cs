using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.Models;

public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Item name is required.")]
    [StringLength(150, ErrorMessage = "Item name cannot exceed 150 characters.")]
    public string ItemName { get; set; } = null!;

    [Required(ErrorMessage = "Description is required.")]
    public string ItemDescription { get; set; } = null!;

    [Required(ErrorMessage = "Cost is required.")]
    [Range(1,10000)]
    [DataType(DataType.Currency)]          
    //[Column(TypeName = "decimal(18, 2)")]
    public decimal ItemCost { get; set; }

    public string ItemImage { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual ItemCategory Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
