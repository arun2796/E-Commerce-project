using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.ProductExtension
{
    public static class ProductExtensionQuery
    {
        public static IQueryable<Product> SortProduct( this IQueryable<Product> products ,string? Orderby)
        {
            products = Orderby switch
            {
                "price" => products.OrderBy(x => x.Price),
                "pricedes" => products.OrderByDescending(x => x.Price),
                _ => products.OrderBy(x => x.Name),
            };
            return products;
        }
        public static IQueryable<Product>Search(this IQueryable<Product> products ,string? SerchName)
        {
            if (string.IsNullOrEmpty(SerchName)) return products;

            var lowercase= SerchName.Trim().ToLower();
            return products.Where(x=>x.Name.Contains(lowercase));
        }

        public static IQueryable<Product> Filter(this IQueryable<Product> products, string? category ,string? brand,string?subcategory)
        {
            var brandlist = new List<string>();

            var CategoryList = new List<string>();

            var SubcategoryList =new List<string>();

            if (!string.IsNullOrEmpty(brand))
            {
                brandlist.AddRange(brand.ToLower().Split(",").ToList());
            }

            if (!string.IsNullOrEmpty(category))
            {
                CategoryList.AddRange(category.ToLower().Split(",").ToList());
            }
            if (!string.IsNullOrEmpty(subcategory))
            {
                SubcategoryList.AddRange([.. subcategory.ToLower().Split(",")]);
            }
            products = products.Where(x => brandlist.Count ==0 || brandlist.Contains(x.Brand.Name.ToLower()));
            products = products.Where(x => CategoryList.Count == 0 || CategoryList.Contains(x.Category.Name.ToLower()));
            products = products.Where(x=>SubcategoryList.Count==0|| SubcategoryList.Contains(x.SubCategory.Name.ToLower()));

            return products;    

        }
    }
}
