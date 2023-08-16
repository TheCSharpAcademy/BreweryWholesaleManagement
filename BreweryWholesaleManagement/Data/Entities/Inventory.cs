namespace BreweryWholesaleManagement.Data.Entities;

public class Inventory
{
    public int Id { get; set; }
    public int WholesalerId { get; set; }
    public virtual Wholesaler Wholesaler { get; set; }
    public virtual ICollection<InventoryItem> InventoryItems { get; set; }
}
