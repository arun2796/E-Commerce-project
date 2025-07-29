using System.Runtime.InteropServices;
using Application.DTO.SubCategory;
using AutoMapper;
using CInfrastructure.Dbconnection;
using CInfrastructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        protected readonly ApplicationDbcontext _context;
        protected readonly IMapper _mapper;

        public SubCategoryController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task <ActionResult> GetAll()
        {
            var entity = await _context.SubCategories.Include(x=>x.Category).ToListAsync();

            var result = _mapper.Map<List<SubcategoryDto>>(entity);

            var file = Request.Form.Files;
            return Ok( entity);
            
        }

      
    }
}
