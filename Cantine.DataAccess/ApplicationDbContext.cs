using Cantine.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ClientCategory> ClientCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var internCategory = new ClientCategory()
            {
                CategoryId = new Guid("1efe7a31-8dcc-4ff0-9b2d-5f148e2989cc"),
                Name = "Stagiaire",
                DiscountType = "Fixed",
                DiscountValue = 10m
            };
            modelBuilder.Entity<ClientCategory>().HasData(
                new ClientCategory()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Interne",
                    DiscountType = "Fixed",
                    DiscountValue = 7.5m
                },
                new ClientCategory()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Prestataire",
                    DiscountType = "Fixed",
                    DiscountValue = 6m
                },
                new ClientCategory()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "VIP",
                    DiscountType = "Percentage",
                    DiscountValue = 100m
                },
                internCategory,
                new ClientCategory()
                {
                    CategoryId = Guid.NewGuid(),
                    Name = "Visiteur",
                    DiscountType = "None",
                    DiscountValue = 0m
                }
            );
            modelBuilder.Entity<Client>().HasData(
                new Client()
                {
                    Id = new Guid("3720DA0A-9109-48C5-89AA-ACC7B7684294"),
                    Name = "Michel Blanc",
                    CategoryId = internCategory.CategoryId,
                    Budget = 100m
                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Boisson",
                    Price = 1m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Fromage",
                    Price = 1m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Pain",
                    Price = 0.40m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Petit Salade Bar",
                    Price = 4m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Grand Salade Bar",
                    Price = 6m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Portion de fruit",
                    Price = 1m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Entrée",
                    Price = 3m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Plat",
                    Price = 6m
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Dessert",
                    Price = 3m
                }
            );

        }
    }
}
