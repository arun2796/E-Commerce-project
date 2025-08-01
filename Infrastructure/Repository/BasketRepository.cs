using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CInfrastructure.Dbconnection;
using Domain.Entity;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CInfrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbcontext _context;
        public BasketRepository(ApplicationDbcontext context)
        {
            _context = context;
        }

        public async Task AddBasketAsync(Basket basket)
        {
             await _context.Basket.AddAsync(basket);
        }

        public async Task <Product?> FindProductId(int productId)
        {
           return await _context.Products.FirstOrDefaultAsync(x=>x.ProductId==productId);
           
        }

        public async Task<Basket?> GetBasketByIdAsync(string basketId)
        {
            var basket =await _context.Basket.Include(x=>x.Items)
                .ThenInclude(x=>x.Product)
                .FirstOrDefaultAsync(x=>x.BasketIdC==basketId);
            return basket;
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }


    }
}

