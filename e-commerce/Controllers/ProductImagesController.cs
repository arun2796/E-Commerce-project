using Application.Common;
using Application.DTO.ProductImageDto;
using AutoMapper;
using CInfrastructure.Dbconnection;
using CloudinaryDotNet.Actions;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AProductImagesController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly ImageServicesC _imageServicesC;
        private readonly IMapper _mapper;

        public AProductImagesController(ApplicationDbcontext context,ImageServicesC imageServicesC ,IMapper mapper)
        {
            _context = context;
            _imageServicesC = imageServicesC;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult> GetAllProductImage()
        {
            var ImageData= await _context.ProductImage .Where(p=>!string.IsNullOrEmpty(p.PublicId)).ToListAsync();
            var resultDtos = _mapper.Map<List<GProductImageDto>>(ImageData);

           //if (resultDtos.Any(dto => string.IsNullOrEmpty(dto.ImageUrl) || string.IsNullOrEmpty(dto.PublicId)))
           // {
           //     return BadRequest("Image data is incomplete.");

           // }
            return Ok(resultDtos);
        }

        [HttpPost]
        public async Task<ActionResult<CProductImageDto>> CreateProductImage(CProductImageDto cProduct)
        {
            if (cProduct.ImageUrl==null||cProduct.ImageUrl.Length==0)
            {
                return BadRequest("product Image is not");
            }

            var result = await _imageServicesC.AddProductImage(cProduct.ImageUrl);

            if (result.Error!=null)
            {
                BadRequest(result.Error);
            }

            var productImage = new ProductImages 
            {
                AltText=cProduct.AltText,
                ProductId=cProduct.ProductId,
                ImageUrl=result.SecureUrl.AbsoluteUri,
                PublicId=result.PublicId,
                
                
            };
            _context.ProductImage.Add(productImage);
            await _context.SaveChangesAsync();
            var resultDto = _mapper.Map<GProductImageDto>(productImage);
            return Ok(resultDto);
        }

        [HttpDelete]
        public async Task <ActionResult> DeleteImage(string publicId)
        {
            var result = await _context.ProductImage.FirstOrDefaultAsync(x=>x.PublicId==publicId);
            if (result==null)
            {
                return BadRequest("enter a pulicId ");
            }
            if (!string.IsNullOrEmpty(result.PublicId))
            {
                await _imageServicesC.DeleteProductImage(result.PublicId);
                _context.ProductImage.Remove(result);
                await _context.SaveChangesAsync();

            }  
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateImage( [FromForm] UProductImageDto uProductImage)
        {
            var result = await _context.ProductImage.FirstOrDefaultAsync(x=>x.PublicId==uProductImage.PublicId);

            if (result==null)
            {
               return BadRequest("id is not null");
            }
            //delet old path
            await _imageServicesC.DeleteProductImage(result.PublicId);

            //update path
             var uploadResult =   await _imageServicesC.AddProductImage(uProductImage.ImageUrl);

            if (uploadResult == null)
            {
                return BadRequest("Failed to upload new image.");
            }
            result.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
            result.PublicId=uploadResult.PublicId;

            _context.ProductImage.Update(result);
            await _context.SaveChangesAsync();

            return Ok(result);

        }

        [HttpGet("{id}/PI_filter")]

        public async Task<ActionResult> ProImageFilter(int id)
        {
            var filter = await _context.ProductImage.Where(x => x.ProductId == id).ToListAsync();

            var filterdto =_mapper.Map<List<GProductImageDto>>(filter);

            return Ok(filterdto);
        }
    }
}
