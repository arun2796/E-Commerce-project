//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Application.Interface;
//using CInfrastructure.Dbconnection;
//using Domain.Entity;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Services
//{
//    public class BasketExtension:IBasketExtension
//    {

//        private readonly ApplicationDbcontext _context;

//        public BasketExtension(ApplicationDbcontext context)
//        {
//            _context = context;
//        }
        
//        public void AddBasket(Basket? basket,Product product ,int quantity)
//        {
//            if (product == null) ArgumentNullException.ThrowIfNull(product);
//            if (basket == null) throw new ArgumentNullException(nameof(basket));
//            if (quantity <=0) throw new ArgumentException("Quantity shoud be greater than zero", nameof(quantity));

//            var products = basket.Items.FirstOrDefault(x=>x.ProductId == product.ProductId);
//            if (products == null)
//            {
//                basket.Items.Add(new BasketItems
//                {
//                    Product = product,
//                    Quantity = quantity
//                });
//            }
//            else {
//                quantity += products.Quantity;
//            }
//        }
       
//    }
//}
