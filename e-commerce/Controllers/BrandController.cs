using System.Runtime.InteropServices;
using Application.DTO.Brand;
using Application.DTO.BrandDtoF;
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
    public class BrandController : ControllerBase
    {
        protected readonly ApplicationDbcontext _context;
        protected readonly IMapper _mapper;

        public BrandController(ApplicationDbcontext dbcontext, IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBrand()
        {
            var result = await _context.Brand.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult> GetByBrandId(int id)
        {
            var result = await _context.Brand.FirstOrDefaultAsync(x => x.BrandId == id);
            return Ok(result);
        }
        [HttpPost]
        public async Task <ActionResult<BrandDto>> CreateBrand(BrandDto brandDto)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Images","BrandLogo");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var newfile = Guid.NewGuid().ToString() + Path.GetExtension(brandDto.Logo.FileName).ToLower();

            var fullname = Path.Combine(folder,newfile);

            using (var stream= new FileStream(fullname,FileMode.Create))
            {
                await brandDto.Logo.CopyToAsync(stream);
            }

            var imageUrl = "/Images/BrandLogo/" + newfile;

            var brand = new Brand { Logo = imageUrl ,
            Name=brandDto.Name};

            _context.Brand.Add(brand);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateBrand),new {id=brand.BrandId},brandDto);

        }
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brand.FirstOrDefaultAsync(x => x.BrandId == id);
            if (brand == null)
            {
                return BadRequest($" Id:{id} not found");
            }
            if (brand.BrandId == id)
            {
                var filename = Path.GetFileName(brand.Logo);
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "BrandLogo", filename);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
            }
            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpPut]
        public async Task <ActionResult> UpdateBrand([FromForm] UBrandDto brandDto)
        {
            var brand =await _context.Brand.FirstOrDefaultAsync(x=>x.BrandId==brandDto.Id);

            if (brand==null)
            {
                return BadRequest("Id is not Match");

            }
            if (brand.BrandId==brandDto.Id)
            {
                var filename=Path.GetFileName(brand.Logo);
                var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "BrandLogo", filename);
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete( oldpath);
                }

                var newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "BrandLogo");
                var newfile = Guid.NewGuid().ToString() + Path.GetExtension(brandDto.Logo.FileName).ToLower();
                var fullname=Path.Combine(newPath, newfile);

                var imageurl = "/Images/BrandLogo/" + newfile;

                brand.Logo = imageurl;
                brand.Name = brandDto.Name; 
                brand.BrandId = brandDto.Id;

            }
            _context.Brand.Update(brand);
            await _context.SaveChangesAsync();
            return Ok(brand);
        }
        

    }
}
