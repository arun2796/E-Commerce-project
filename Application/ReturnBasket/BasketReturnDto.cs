using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.BasketDtoF;
using Application.DTO.BasketItemDtoF;
using Application.DTO.ProductImageDto;
using Domain.Entity;

namespace Application.ReturnBasket
{
    public static  class BasketReturnDto
    {

        public static BasketDto ReturnDto( this Basket basket) 
        {
            return new BasketDto
            {
                Id = basket.Id,
                BasketIdC = basket.BasketIdC,
                Items = basket.Items.Select(x => new BasketItemsDto
                {
                    Description = x.Product.Description,
                    ProductId = x.ProductId,
                    Price = x.Product.Price,
                    StockQuantity = x.Product.StockQuantity,
                    Name = x.Product.Name,
                    CategoryName = x.Product.Category?.Name ?? "Uncategorized",
                    BrandName = x.Product.Brand?.Name ?? "Uncategorized",
                    SubCategoryName = x.Product.SubCategory?.Name ?? "Uncategorized",
                    BrandLogo = x.Product.Brand?.Logo ?? "Uncategorized",
                    Images = x.Product.Images.FirstOrDefault()?.ImageUrl,

                }).ToList()
            };
        }
    }
}
