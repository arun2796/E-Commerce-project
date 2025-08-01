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

        public void RemoveItem(int productid, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity shoud be greater than zero", nameof(quantity));
            }
            var item = FindItem(productid);
            if (item == null) return ;
            item.Quantity -= quantity;

            if (item.Quantity <= 0) Items.Remove(item);

        }

        private BasketItems? FindItem(int productid)
        {
            return Items.FirstOrDefault(x => x.ProductId == productid);
        }
    }

}
