namespace BreweryWholesaleManagement.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public int WholesalerId { get; set; }
    public int BreweryId { get; set; }
    public int Quantity { get; set; }   
}
