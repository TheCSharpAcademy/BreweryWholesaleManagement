using BreweryWholesaleManagement.Data.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;

namespace BreweryWholesaleManagement.Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("server=DESKTOP-PM4IG1E;database=Brewery;Integrated Security=true;Trusted_Connection=True;TrustServerCertificate=true;");
        }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brewery>()
                .HasData(new List<Brewery>
                {
                    new Brewery()
                    {
                        Id = 1,
                        Name = "Jonny's Brewery",
                        Address = "7 Hope Street, 400291",
                        PhoneNumber = "039281920",
                        Email = "jonnysbeer@gmail.com"
                    }
                });

            modelBuilder.Entity<Beer>()
               .HasData(new List<Beer>
               {
                    new Beer()
                    {
                        Id = 1,
                        BreweryId = 1,
                        Name = "Corona"
                    }
               });

            modelBuilder.Entity<Wholesaler>()
               .HasData(new List<Wholesaler>
               {
                    new Wholesaler()
                    {
                        Id = 1,
                        Name = "Danny's distribution"
                    }
               });
        }

    }
}
