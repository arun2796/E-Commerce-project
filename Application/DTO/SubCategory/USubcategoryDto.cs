using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.SubCategory
{
    public class USubcategoryDto
    {
        public required int SubCategoryId { get; set; }
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        
    }
}
