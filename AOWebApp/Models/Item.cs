using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models;

public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [Required(ErrorMessage = "Item name is required.")]
    [Display(Name = "Item Name")]
    [StringLength(150, ErrorMessage = "Item name should less than 150 characters.")]
    public string ItemName { get; set; } = null!;


    [Required(ErrorMessage = "Description is required.")]
    [Display(Name = "Item Description")]
    public string ItemDescription { get; set; } = null!;

    [Required(ErrorMessage = "Cost is required.")]
    [Range(1,10000)]
    [DataType(DataType.Currency)]
    [Display(Name = "Item Cost")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal ItemCost { get; set; }

    [Required(ErrorMessage = "Image URL is required.")]
    [Display(Name = "Item Image URL")]
    [DataType(DataType.ImageUrl)]
    public string ItemImage { get; set; } = null!;

    [Required(ErrorMessage = "Category ID is required.")]
    public int CategoryId { get; set; }

    public virtual ItemCategory Category { get; set; } = null!;

    public virtual ICollection<ItemMarkupHistory> ItemMarkupHistories { get; set; } = new List<ItemMarkupHistory>();

    public virtual ICollection<ItemsInOrder> ItemsInOrders { get; set; } = new List<ItemsInOrder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
