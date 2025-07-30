using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CInfrastructure.Dbconnection
{
    public class ApplicationDbcontext:DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options):base(options)
        {
            
        }

        public DbSet<Product>  Products { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Brand> Brand { get; set; }

        public DbSet<ProductImages> ProductImage { get; set; } 

        public DbSet<Basket> Basket { get; set; }

        public DbSet<BasketItems> BasketItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Or .NoAction()

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany()
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Keep this if needed


            modelBuilder.Entity<BasketItems>()
                .HasOne(b => b.Basket)
                .WithMany(b=>b.Items)
                .HasForeignKey(b => b.BasketId)
                .OnDelete(DeleteBehavior.Cascade); // Keep one cascade if needed

            modelBuilder.Entity<BasketItems>()
                .HasOne(b => b.Brand)
                .WithMany()
                .HasForeignKey(b => b.BrandId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            modelBuilder.Entity<BasketItems>()
                .HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasketItems>()
                .HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
