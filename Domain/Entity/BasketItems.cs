using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BasketItems
    {
        public int Id { get; set; }

        public required int Quantity { get; set; }

        public  int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        //public required int BrandId { get; set; }
        //public Brand Brand { get; set; } = null!;

        //public required int CategoryId { get; set; }
        //public Category Category { get; set; } = null!;

        public  int BasketId { get; set; }
        public Basket Basket { get; set; } = null!;

    }
}
