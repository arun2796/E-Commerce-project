using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.IRepository
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketByIdAsync(string basketId);
        Task AddBasketAsync(Basket basket);
        Task SaveChangesAsync();
         Task<Product?> FindProductId(int productId);
    }
}
