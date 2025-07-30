using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.Brand
{
    public  class UBrandDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public IFormFile Logo { get; set; } = null!;
    }
}
