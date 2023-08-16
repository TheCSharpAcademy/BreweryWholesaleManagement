using BreweryWholesaleManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreweryWholesaleManagement.Data.Entities;

namespace BreweryWholesaleManagement.Unit.Tests.DataTests.EntitiesTests
{
    public class WholesalerTests
    {
        private readonly BreweryContext _context;

        public WholesalerTests()
        {
            var options = new DbContextOptionsBuilder<BreweryContext>()
                .UseInMemoryDatabase(nameof(WholesalerTests) + DateTime.Now.Ticks)
                .Options;

            _context = new BreweryContext(options);
        }

        [Fact]
        public void Add_Sale_Of_Existing_Beer_In_Existing_Wholesaler()
        {
            var sale = new Wholesaler()
            {
                Name = "Name"
            };
            _context.Wholesalers.Add(sale);
            _context.SaveChanges();

            var dbSale = _context.Wholesalers.Include(s => s.Inventory).FirstOrDefault();
            dbSale.Should().NotBeNull();
            dbSale!.Name.Should().Be("Name");
            dbSale.Inventory.Should().BeNull();
        }

        [Fact]
        public void Add_Sale_With_Inventory()
        {
            var sale = new Wholesaler()
            {
                Name = "Name"
            };

            sale.Inventory = new Inventory()
            {
                Wholesaler = sale
            };
            _context.Wholesalers.Add(sale);
            _context.SaveChanges();

            var dbSale = _context.Wholesalers.Include(s => s.Inventory).FirstOrDefault();
            dbSale.Should().NotBeNull();
            dbSale!.Name.Should().Be("Name");
            dbSale.Inventory.Should().NotBeNull();
        }

        [Fact]
        public void Add_Sale_With_Inventory_Item()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory",
                Address = "New address",
                Email = "email",
                Description = "description",
                PhoneNumber = "phone number",
            };
            var beer = new BeerForWholesaleOrder()
            {
                Name = "Stella",
                Price = 2.5,
                Alcohol = "5",
                Description = "good beer",
                Brewery = brewery
            };
            brewery.Beers.Add(beer);
            _context.Brewerys.Add(brewery);
            _context.SaveChanges();

            var sale = new Wholesaler()
            {
                Name = "Name"
            };

            var inventory = new Inventory()
            {
                Wholesaler = sale
            };
            inventory.InventoryItems.Add(new InventoryItem()
            {
                Inventory = inventory,
                Beer = beer
            });
            sale.Inventory= inventory;
            _context.Wholesalers.Add(sale);
            _context.SaveChanges();

            var dbSale = _context
                .Wholesalers
                .Include(s => s.Inventory)
                .ThenInclude(i => i.InventoryItems)
                .ThenInclude(i => i.Beer)
                .FirstOrDefault();
            dbSale.Should().NotBeNull();
            dbSale!.Inventory.Should().NotBeNull();
            dbSale.Inventory.InventoryItems.Should().NotBeNull();
            dbSale.Inventory.InventoryItems.Count.Should().Be(1);
            var dbBeer = dbSale.Inventory.InventoryItems.FirstOrDefault()!.Beer;
            dbBeer.Should().NotBeNull();

            dbBeer.Id.Should().BeGreaterOrEqualTo(1);
            dbBeer.Name.Should().Be(beer.Name);
            dbBeer.Price.Should().Be(beer.Price);
            dbBeer.Description.Should().Be(beer.Description);
            dbBeer.Alcohol.Should().Be(beer.Alcohol);
        }
    }
}
