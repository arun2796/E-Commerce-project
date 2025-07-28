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

        public required int ProductId { get; set; }

        public required int BrandId { get; set; }

        public required int CategoryId { get; set; }

        public required int BasketId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Products { get; set; } = null!;

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; } = null!; 

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }= null!;

        [ForeignKey(nameof(BasketId))]
        public Basket  Basket { get; set; } = null!;

    }
}
