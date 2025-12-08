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


            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 8,
                    Name = "Movie",
                    Description = "List of moview ",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = 9,
                    Name = "Tools",
                    Description = "Various kinds of tools",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id=16,
                    Name = "Beatifullboy",
                    Description = "Moview about a boy",
                    Price = 699.99m,
                    QuantityInStock = 50,
                    CategoryId = 8,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                   Id=17,
                    Name = "Hammer",
                    Description = "A durable tool for construction tasks",
                    Price = 19.99m,
                    QuantityInStock = 50,
                    CategoryId = 9,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id=18,
                    Name = "Science Fiction Novel",
                    Description = "A thrilling sci-fi adventure",
                    Price = 19.99m,
                    QuantityInStock = 100,
                    CategoryId = 8,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
           

        }



    }
  
}
