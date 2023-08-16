using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Unit.Tests.DataTests.EntitiesTests
{
    public class BreweryTest
    {
        private readonly BreweryContext _context;
        public BreweryTest()
        {
            var options = new DbContextOptionsBuilder<BreweryContext>()
                .UseInMemoryDatabase(nameof(BreweryTest) + DateTime.Now.Ticks)
                .Options;

            _context = new BreweryContext(options);
        }

        [Fact]
        public void Add_Brewery_Should_Save()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory",
                Address = "New address",
                Email = "email",
                Description = "description",
                PhoneNumber = "phone number",
            };
            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery!.Id.Should().BeGreaterOrEqualTo(1);
            dbBrewery.Name.Should().Be(brewery.Name);
            dbBrewery.Address.Should().Be(brewery.Address);
            dbBrewery.Description.Should().Be(brewery.Description);
            dbBrewery.PhoneNumber.Should().Be(brewery.PhoneNumber);
            dbBrewery.Email.Should().Be(brewery.Email);
        }

        [Fact]
        public void Delete_Brewery_Should_Remove_From_Database()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory",
                Address = "New address",
                Email = "email",
                Description = "description",
                PhoneNumber = "phone number",
            };

            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery.Should().NotBeNull();
            _context.Breweries.Remove(dbBrewery!);
            _context.SaveChanges();

            dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery.Should().BeNull();
        }

        [Fact]
        public void Update_Brewery_Should_Update_From_Database()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory",
                Address = "New address",
                Email = "email",
                Description = "description",
                PhoneNumber = "phone number",
            };
        

        _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery.Should().NotBeNull();
            dbBrewery!.Address = "Update Address";
            _context.SaveChanges();

            dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery!.Address.Should().Be("Update Address");
        }

        [Fact]
        public void Add_Brewery_With_Beers_Should_Add()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory",
                Address = "New address",
                Email = "email",
                Description = "description",
                PhoneNumber = "phone number",
            };
            var beer = new Beer()
            {
                Name = "Stella",
                Price = 2.5,
                Alcohol = "5",
                Description = "good beer",
                BreweryId = brewery.Id,
            };
            brewery.Beers.Add(beer);
            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context
                .Breweries
                .Include(b => b.Beers)
                .FirstOrDefault();

            dbBrewery.Should().NotBeNull();

            var dbBeer = dbBrewery.Beers.FirstOrDefault();
            dbBeer.Should().NotBeNull();

            dbBeer.Id.Should().BeGreaterOrEqualTo(1);
            dbBeer.Name.Should().Be(beer.Name);
            dbBeer.Price.Should().Be(beer.Price);
            dbBeer.Description.Should().Be(beer.Description);
            dbBeer.Alcohol.Should().Be(beer.Alcohol);
        }
    }
}
