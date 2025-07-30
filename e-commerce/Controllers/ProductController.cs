using System.Collections;
using System.Runtime.InteropServices;
using Application.Common;
using Application.DTO.ProductDto;
using Application.ProductExtension;
using AutoMapper;
using CInfrastructure.Dbconnection;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AProductController : ControllerBase
    {
        protected readonly ApplicationDbcontext _context;
        protected readonly IMapper _mapper;

        public AProductController(ApplicationDbcontext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<GProductDto>>> GetAllProduct([FromQuery] ProductParams productParams)
        {
            var product= _context.Products
                .Include(b=>b.Brand).Include(c=>c.Category)
                .Include(sc=>sc.SubCategory).AsQueryable();

            product.SortProduct(productParams.Orderby);
            product.Search(productParams.ProductName);
            product.Filter(productParams.Category,productParams.Brand,productParams.SubCategory);

            var products = await product.Select(x=> new GProductDto {
                Description=x.Description,
                Price=x.Price,
                CategoryId=x.CategoryId,
                BrandId=x.BrandId,
                CategoryName=x.Category.Name,
                ProductId=x.ProductId,
                SubCategoryId=x.SubCategoryId,
                SubCategoryName=x.SubCategory.Name,
                BrandName=x.Brand.Name,
                Name=x.Name,
               
            }).ToListAsync();

            return Ok(products);
        }
        [HttpGet("{id}/Detail")]
        public async Task <ActionResult> GetByProductId(int id)
        {
            var product= await _context.Products.Include(b=>b.Brand)
                .Include(c=>c.Category).Include(sc=>sc.SubCategory)
                .FirstOrDefaultAsync(p=>p.ProductId == id);

            var resultDto = _mapper.Map<GProductDto>(product);
            return Ok(resultDto);
          
        }
        [HttpGet("filter")]

        public  async Task<ActionResult> GetFilter()
        {
            var categories = await _context.Products
                 .Select(x => new { x.Category.CategoryId, x.Category.Name })
                 .Distinct()
                 .ToListAsync();

            var brands = await _context.Products
                .Select(x => new { x.Brand.BrandId, x.Brand.Name })
                .Distinct()
                .ToListAsync();

            var subcategories = await _context.Products
                .Select(x => new { x.SubCategory.SubCategoryId, x.SubCategory.Name, x.SubCategory.CategoryId })
                .Distinct()
                .ToListAsync();

            return Ok( new { categories, brands, subcategories });
        }

        [HttpPost]

        public async Task <ActionResult<CProductDto>> CreateProduct([FromBody]CProductDto productDto)
        {
            var brand = await _context.Brand.FindAsync(productDto.BrandId);

            var category = await _context.Category
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(x=>x.CategoryId==productDto.CategoryId);

            if(brand == null || category==null)
            {
                return BadRequest(ModelState);
            }

            var subcategory = category.SubCategories?.FirstOrDefault(x=>x.SubCategoryId==productDto.SubCategoryId);
            if(subcategory == null)
            {
                return BadRequest("Subcategory doesn't belong to this category.");
            }

            var product=new Product {
                Name = productDto.Name,
                BrandId = productDto.BrandId,
                CategoryId = productDto.CategoryId,
                SubCategoryId = productDto.SubCategoryId,
                StockQuantity = productDto.StockQuantity,
                Price = productDto.Price,
                Description=productDto.Description
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByProductId), new {id=product.ProductId},productDto);
        }

        [HttpPut]
        public async Task <ActionResult<UProductDto>> UpdateProduct(UProductDto productDto)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.ProductId==productDto.ProductId);
            if(product == null) { return BadRequest(ModelState); }

            var products = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                StockQuantity = productDto.StockQuantity,
                SubCategoryId = productDto.SubCategoryId,
                BrandId = productDto.BrandId,
                ProductId = productDto.ProductId,
                CategoryId = productDto.CategoryId,  
            };
            _context.Products.Update(products);
            await _context.SaveChangesAsync();

            return Ok(products);
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p=>p.ProductId==id);

            if (product!=null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync() ;
            }
            return Ok();
        }

    }
}
