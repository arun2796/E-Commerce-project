using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.ProductDto
{
    public class UProductDto
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int SubCategoryId { get; set; }

    }
}
