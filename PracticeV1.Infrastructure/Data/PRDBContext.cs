using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Data
{
    public class PRDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public PRDBContext(DbContextOptions<PRDBContext> options) : base(options) 
        {

        }
        //public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);


        //    builder.Entity<Category>().HasData(
        //        new Category
        //        {
        //            Id = 1,
        //            Name = "Electronics",
        //            Description = "Electronic gadgets and devices",
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        },
        //        new Category
        //        {
        //            Id = 2,
        //            Name = "Books",
        //            Description = "Various kinds of books",
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        }
        //    );

        //    builder.Entity<Product>().HasData(
        //        new Product
        //        {
        //            Id = 1,
        //            Name = "Smartphone",
        //            Description = "Latest model smartphone",
        //            Price = 699.99m,
        //            QuantityInStock = 50,
        //            CategoryId = 1,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        },
        //        new Product
        //        {
        //            Id = 2,
        //            Name = "Laptop",
        //            Description = "High performance laptop",
        //            Price = 1299.99m,
        //            QuantityInStock = 30,
        //            CategoryId = 1,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        },
        //        new Product
        //        {
        //            Id = 2,
        //            Name = "Science Fiction Novel",
        //            Description = "A thrilling sci-fi adventure",
        //            Price = 19.99m,
        //            QuantityInStock = 100,
        //            CategoryId = 2,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        }
        //    );
        //    builder.Entity<Order>().HasData(
        //        new Order
        //        {
        //            Id = 1,
        //            UserId = 11,
        //            OrderDate = DateTime.UtcNow,
        //            TotalAmount = 719.98m

        //        },
        //        new Order
        //        {
        //            Id = 2,
        //            UserId = 11,
        //            OrderDate = DateTime.UtcNow,
        //            TotalAmount = 699.99m
        //        }
        //    );
        //    builder.Entity<OrderItem>().HasData(
        //        new OrderItem
        //        {
        //            Id = 1,
        //            OrderId = 1,
        //            ProductId = 11,
        //            Quantity = 1,
        //            UnitPrice = 699.99m
        //        },
        //        new OrderItem
        //        {
        //            Id = 2,
        //            OrderId = 2,
        //            ProductId = 12,
        //            Quantity = 1,
        //            UnitPrice = 19.99m
        //        }
        //    );

        }



    }
  
}
