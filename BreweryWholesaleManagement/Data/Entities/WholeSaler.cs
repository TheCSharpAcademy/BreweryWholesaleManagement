namespace BreweryWholesaleManagement.Data.Entities;

public class Wholesaler
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual Inventory Inventory { get; set; }
}
