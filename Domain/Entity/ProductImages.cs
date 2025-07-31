using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }
        
        public required string ImageUrl { get; set; }
        public required string AltText { get; set; }

        public string PublicId { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;


    }
}
