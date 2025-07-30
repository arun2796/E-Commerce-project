using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.ProductImageDto
{
    public class GProductImageDto
    {
      

        public required string ImageUrl { get; set; }
        public IFormFile AltText { get; set; } = null!;

        public int ProductId { get; set; }

        
    }
}
