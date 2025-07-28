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

        public DbSet<Brand> Brand { get; set; }

        public DbSet<ProductImages> ProductImage { get; set; } 

        public DbSet<Basket> Basket { get; set; }

        public DbSet<BasketItems> BasketItem { get; set; }


    }
}
