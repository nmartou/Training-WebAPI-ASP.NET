using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store;
public class StoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCommand> ProductCommands { get; set; }
    public DbSet<Command> Commands { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Person> Persons { get; set; }

    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=Store.db");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>().HasData(
            new Price { PriceID = 1, Value = 34.99m, Discount = 0.0f },
            new Price { PriceID = 2, Value = 19.99m, Discount = 15f, DiscountEndDate = new DateTime(2026, 12, 1) }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductID = 1, Name = "White Hat", Description = "This is our latest white hat which is very useful against the sun.", Brand = "Micosoft", PriceID = 1, Stock = 42 },
            new Product { ProductID = 2, Name = "T-shirt", Description = "This object would fit perfectly with a white hat.", Brand = "MacroHard", PriceID = 2, Stock = 26 }
        );

        modelBuilder.Entity<Person>().HasData(
            new Person { PersonID = 1, FirstName = "Nicolas", LastName = "Martou", BirthDate = new DateOnly(2000, 10, 2) },
            new Person { PersonID = 2, FirstName = "Elon", LastName = "Musk", BirthDate = new DateOnly(1971, 6, 28) },
            new Person { PersonID = 3, FirstName = "Bill", LastName = "Gates", BirthDate = new DateOnly(1955, 10, 28) }
        );

        modelBuilder.Entity<Command>().HasData(
            new Command { CommandID = 1, SellerID = 1, BuyerID = 3, TotalQuanity = 8, IsPaid = true, TotalPrice = 10m },
            new Command { CommandID = 2, SellerID = 1, BuyerID = 2, TotalQuanity = 3, IsPaid = true, TotalPrice = 10m },
            new Command { CommandID = 3, SellerID = 1, BuyerID = 3, TotalQuanity = 2 }
        );

        modelBuilder.Entity<ProductCommand>().HasData(
            new ProductCommand { ProductCommandID = 1, CommandID = 1, ProductID = 1, Quantity = 4 },
            new ProductCommand { ProductCommandID = 2, CommandID = 1, ProductID = 2, Quantity = 4 },
            new ProductCommand { ProductCommandID = 3, CommandID = 2, ProductID = 1, Quantity = 1 },
            new ProductCommand { ProductCommandID = 4, CommandID = 2, ProductID = 2, Quantity = 2 },
            new ProductCommand { ProductCommandID = 5, CommandID = 3, ProductID = 1, Quantity = 1 },
            new ProductCommand { ProductCommandID = 6, CommandID = 3, ProductID = 1, Quantity = 1 }
        );
    }
}