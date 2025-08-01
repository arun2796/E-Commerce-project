using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public  interface IBasketServices
    {
        Task AddItemToBasketAsync(string basketId, int productId, int quantity);
    }
}
