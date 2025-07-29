using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CategoryDtoF
{
    public class UcategoryDto
    {
        public int CategoryId { get; set; }
        public required string Name { get; set; }

        //public required string CategoryName {get;set;}
    }
}
