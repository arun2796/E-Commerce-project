using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.BasketItemDtoF;
using Domain.Entity;

namespace Application.DTO.BasketDtoF
{
    public class BasketDto
    {
        public int Id { get; set; }

        public required string BasketIdC { get; set; }

        public required List<BasketItemsDto> Items { get; set; } = [];
    }
}
