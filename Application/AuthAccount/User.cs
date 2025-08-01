using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.AspNetCore.Identity;


namespace Application.AuthAccount
{
    public class User:IdentityUser
    {
        public int AddressId { get; set; }

        public Address? Address { get; set; }

    }
}
