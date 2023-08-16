namespace BreweryWholesaleManagement.Data.Entities;

public class Beer
{
    public int Id { get; set; }
    public int BreweryId { get; set; }
    public string? Name { get; set; }
    public string? Alcohol { get; set; }
    public double? Price { get; set; }
    public string? Description { get; set; }
    public List<Order> Orders { get; set; }
}
