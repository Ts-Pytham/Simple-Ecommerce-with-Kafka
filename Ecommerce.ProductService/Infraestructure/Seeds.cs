using Ecommerce.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductService.Infraestructure;

public static class Seeds
{
    public static void CreateSeeds(this ModelBuilder modelBuilder)
    {
        var categories = new List<Category>
        {
            new() { Id = 1, Name = "Electronics", Description = "Electronic devices" },
            new() { Id = 2, Name = "Clothes", Description = "Clothes for all ages" },
            new() { Id = 3, Name = "Books", Description = "Books for all ages" }
        };
        modelBuilder.Entity<Category>().HasData(categories);
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Description = "A laptop", CategoryId = 1, Price = 1000, Quantity = 10 },
            new Product { Id = 2, Name = "T-shirt", Description = "A t-shirt", CategoryId = 2, Price = 20, Quantity = 100 },
            new Product { Id = 3, Name = "Book", Description = "A book", CategoryId = 3, Price = 10, Quantity = 50 }
        );
    }
}
