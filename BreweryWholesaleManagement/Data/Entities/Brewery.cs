﻿namespace BreweryWholesaleManagement.Data.Entities;

public class Brewery
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public virtual List<Beer> Beers { get; set; }
}