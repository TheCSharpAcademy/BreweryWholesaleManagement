using BreweryWholesaleManagement.Data.Entities;

namespace BreweryWholesaleManagement.Data;

public class SeedData
{
    public static void AddBreweries(BreweryContext context)
    {
        context.Breweries.Add(new Brewery()
        {
          
            Name = "Jonny's Brewery",
            Address = "7 Hope Street, 400291",
            PhoneNumber = "039281920",
            Email = "jonnysbeer@gmail.com",
        });
        context.SaveChanges();
    }
}
