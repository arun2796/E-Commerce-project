using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Interface
{
    public interface IBasketExtension
    {
        void AddBasket( Basket basket,Product product, int quantity);

        void RemoveBasket(int productid,int productId);
    }
}
