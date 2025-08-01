using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
using Domain.Entity;
using Domain.IRepository;

namespace Application.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepo;

        public BasketServices(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }

        public async Task AddItemToBasketAsync(string basketId, int productId, int quantity)
        {
            var basket =await _basketRepo.GetBasketByIdAsync(basketId);
            if (basket == null)
            {
                basket = new Basket
                 {
                    BasketIdC = basketId,
                    Items = new List<BasketItems>()
                 };
                await _basketRepo.AddBasketAsync(basket);
            }
            var product = await _basketRepo.FindProductId(productId);
                
             var item=  basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if(item == null)
            {
                basket.Items.Add(new BasketItems { Quantity=quantity,Product=product });
            }
            else
            {
                item.Quantity += quantity;
            }
            await _basketRepo.SaveChangesAsync();


        }
    }
}

