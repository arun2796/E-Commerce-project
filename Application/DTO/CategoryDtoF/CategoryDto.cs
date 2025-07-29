using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.SubCategory;

namespace Application.DTO.CategoryDtoF
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<SubcategoryDto> SubCategories { get; set; } = new();
    }
}
