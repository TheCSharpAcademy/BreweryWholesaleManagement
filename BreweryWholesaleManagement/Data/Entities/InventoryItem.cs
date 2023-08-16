using System.ComponentModel.DataAnnotations;

namespace BreweryWholesaleManagement.Data.Entities;

public class InventoryItem
{
    [Key]
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public int BeerId { get; set; }
    public virtual Beer Beer { get; set; }
    public virtual Inventory Inventory { get; set; }
}
