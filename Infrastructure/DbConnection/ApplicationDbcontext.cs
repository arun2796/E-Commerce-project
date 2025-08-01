using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Application.AuthAccount;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CInfrastructure.Dbconnection
{
    public class ApplicationDbcontext : IdentityDbContext<User>
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

        public DbSet<Address> Addresses { get; set; }

        public DbSet<BasketItems> BasketItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole
            //    {
            //        Id = "e60a9a15-076c-4529-b86a-8bec53510584",
            //        Name = "Admin",
            //        NormalizedName = "ADMIN",
            //    },
            //    new IdentityRole
            //    {
            //        Id = "da791ffe-d132-43bd-9ca5-58a91e525b4b",
            //        Name = "Member",
            //        NormalizedName = "MEMBER",
            //    }
            //    );
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

            //modelBuilder.Entity<BasketItems>()
            //    .HasOne(b => b.Brand)
            //    .WithMany()
            //    .HasForeignKey(b => b.BrandId)
            //    .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            //modelBuilder.Entity<BasketItems>()
            //    .HasOne(b => b.Category)
            //    .WithMany()
            //    .HasForeignKey(b => b.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasketItems>()
                .HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<User>()
            //      .HasOne(u => u.Address)
            //       .WithMany()
            //    .HasForeignKey(u => u.AddressId)
            //    .OnDelete(DeleteBehavior.Restrict);

        }



    }
}
