using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }

        public required string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
