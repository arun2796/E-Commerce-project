using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Brand
    {
        public int BrandId { get; set; }

        public required string Name { get; set; }

        public required string Logo { get; set; }
    }
}
