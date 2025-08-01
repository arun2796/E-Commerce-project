using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.AuthAccount;
using CInfrastructure.Dbconnection;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CInfrastructure.DefaultData
{
    public static class SeedData
    {
        public static async Task SeedataAsync(ApplicationDbcontext context,UserManager<User> usermanager,RoleManager<IdentityRole> roleManager)
        {
            if (!context.Database.IsSqlServer())
            {
                throw new Exception("Database is not SQL Server");
            }
            await context.Database.MigrateAsync();

            Address? address = null;
            if (!context.Addresses.Any())
            {
                 address = new Address
                {
                    Name = "John Doe",
                    StreetAddress = "123 Sample Street",
                    Landmark = "Near Big Mall",
                    City = "Chennai",
                    State = "Tamil Nadu",
                    PinCode = "600001",
                    Country = "India",
                    AddressType = AddressType.Home
                };

                context.Addresses.Add(address);
                await context.SaveChangesAsync();

            }
            else
            {
                address = await context.Addresses.FirstAsync();
            }

            if (!context.Users.Any())
            {
                if(!await roleManager.RoleExistsAsync("ADMIN"))
                {
                 var roleResult=   await roleManager.CreateAsync( new IdentityRole("ADMIN"));
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Failed to create ADMIN role: " +
                            string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }
                }
                if (! await roleManager.RoleExistsAsync("MEMBER"))
                {
                 var roles=  await roleManager.CreateAsync(new IdentityRole("MEMBER"));
                    if (!roles.Succeeded)
                    {
                        throw new Exception("Failed to create MEMBER role: " 
                            + string.Join(",",roles.Errors.Select(e=>e.Description)));

                    }
                }

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    AddressId=address.Id,
                    
                    
                };
                await usermanager.CreateAsync(admin, "Pa$$w0rd");
                await usermanager.AddToRoleAsync(admin,"ADMIN");

                var member = new User
                {
                    UserName = "member",
                    Email="member@gmail.com",
                    AddressId=address.Id
                };
                await usermanager.CreateAsync(member, "Pa$$w0rd");
                await usermanager.AddToRolesAsync(member, new[] { "MEMBER","ADMIN" });
            }

            
        }
    }
}
