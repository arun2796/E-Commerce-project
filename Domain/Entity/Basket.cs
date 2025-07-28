using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Basket
    {
        public int Id { get; set; }

        public required string BasketIdC { get; set; }

        public required List<BasketItems> Items { get; set; } = [];
    }
}
