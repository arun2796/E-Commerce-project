using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.ProductImageDto
{
    public class UProductImageDto
    {
        public int Id { get; set; }

        public IFormFile ImageUrl { get; set; } = null!;
        public string AltText { get; set; } = null!;

        public int ProductId { get; set; }

        public string PublicId { get; set; }=null!;

    }
}
