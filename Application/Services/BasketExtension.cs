using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Entity;

namespace Application.Services
{
    public class BasketExtension:IBasketExtension
    {

        private readonly List <Basket> _basket =new();
        
        public void AddBasket(Basket? basket,Product product ,int quantity)
        {
            if (product == null) ArgumentNullException.ThrowIfNull(product);
            if (basket == null) throw new ArgumentNullException(nameof(basket));
            if (quantity <=0) throw new ArgumentException("Quantity shoud be greater than zero", nameof(quantity));

            var products = basket.Items.FirstOrDefault(x=>x.ProductId == product.ProductId);
            if (products == null)
            {
                basket.Items.Add(new BasketItems
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else {
                quantity += products.Quantity;
            }
        }
        public void RemoveBasket(int productid, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity shoud be greater than zero", nameof(quantity));
            foreach (var basket in _basket)
            {
                var product = basket.Items.FirstOrDefault(x => x.ProductId == productid);
                if (product != null)
                {
                    product.Quantity -= quantity;

                    if (product.Quantity<=0)
                    {
                        basket.Items.Remove(product);
                    }
                }
                break;
            } 
        }

       
    }
}
