using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.ProductImageDto;
using Domain.Entity;

namespace Application.DTO.BasketItemDtoF
{
    public class BasketItemsDto
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public double Price { get; set; }
        public int StockQuantity { get; set; }

        //public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
       // public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string BrandLogo { get; set; } = null!;

        //public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; } = null!;

        public string Images { get; set; } =null!;
    }
}
