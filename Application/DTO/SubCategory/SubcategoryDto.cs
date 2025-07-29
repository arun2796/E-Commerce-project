using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.SubCategory
{
    public class SubcategoryDto
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public required string CategoryName { get; set; }

    }
}
