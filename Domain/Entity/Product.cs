using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
       
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public Category Category { get; set; } = null!;
        public Brand Brand { get; set; } = null!;

        public List<ProductImages> Images { get; set; } = [];
        
    }
}
