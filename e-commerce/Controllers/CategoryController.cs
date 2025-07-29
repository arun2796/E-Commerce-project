using System.Runtime.InteropServices;
using Application.DTO.CategoryDtoF;
using Application.DTO.SubCategory;
using AutoMapper;
using CInfrastructure.Dbconnection;
using CInfrastructure.Migrations;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubCategory = Domain.Entity.SubCategory;


namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        protected readonly ApplicationDbcontext _context;
        protected readonly IMapper _mapper;

        public CategoryController(ApplicationDbcontext dbcontext ,IMapper mapper)
        {
            _context = dbcontext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var entity = await _context.Category.
                Include(s=>s.SubCategories).ToListAsync();
            var result = _mapper.Map<List<CategoryDto>>(entity);

            return Ok(result);
        }

        [HttpGet ("detail/{id}")]
        //[Route("Detail")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var entity = await _context.Category.Include(s => s.SubCategories).FirstOrDefaultAsync(x => x.CategoryId == id);
                if(entity == null)
                {
                    return BadRequest($"Given id :{id} is not found");
                }

                var resultDto = _mapper.Map<CategoryDto>(entity);
                return Ok(resultDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost]
        public async Task <ActionResult<CcategoryDto>> Create(CcategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);

                await _context.Category.AddAsync(category);

                await _context.SaveChangesAsync();

                var result = _mapper.Map<CcategoryDto>(category);
                return CreatedAtAction(nameof(GetById), new {id=category.CategoryId},result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest($"Id:{id} should not be Zero");
                }
                var entity =await  _context.Category.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (entity == null)
                {
                    return BadRequest($"Id:{id} Not-Match or Null");
                }
                 _context.Category.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut]
        public async Task <ActionResult<UcategoryDto>> Update([FromBody]UcategoryDto category)
        {
            try
            {
                if (category.CategoryId== 0)
                {
                    return BadRequest($"Id:{category.CategoryId} should not be Zero");
                }
                var entity = await _context.Category.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);
                if (entity == null)
                {
                    return BadRequest("Id not match ");
                }
                _mapper.Map(category,entity);

                _context.Category.Update(entity);
                await _context.SaveChangesAsync();
                var result = _mapper.Map<UcategoryDto>(entity);
                return Ok(result);
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("SubCategory")]
        public async Task<ActionResult> GetAllSubCategory()
        {
            var entity = await _context.SubCategories.Include(x => x.Category).ToListAsync();

            var categoryDtos = _mapper.Map<List<SubcategoryDto>>(entity);

            return Ok(categoryDtos);
        }

        [HttpGet("{id}/SubCategoryDetail")]
        public async Task<ActionResult> GetSubCategoryById(int id)
        {
            var entity = await _context.SubCategories.Include(c => c.Category).FirstOrDefaultAsync(x => x.SubCategoryId == id);
            if (entity == null)
            {
                return BadRequest("record not found");
            }
            var result = _mapper.Map<SubcategoryDto>(entity);

            return Ok(result);
        }
        [HttpPost("{categoryId}/SubCategory")]

        public async Task <IActionResult> CreateSubCategory([FromRoute] int categoryId, [FromBody] CSubCategoryDto dto)
        {
            var category = await _context.Category.FirstOrDefaultAsync(x=>x.CategoryId==categoryId);
            if (category == null)
            {
                return NotFound("Parent category not found.");
            }

            var subcategory = _mapper.Map<SubCategory>(dto);
            subcategory.CategoryId= categoryId;

            _context.SubCategories.Add(subcategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubCategoryById), new { id = subcategory.SubCategoryId }, subcategory);
        }

        [HttpPut("SubCategory")]

        public async Task <ActionResult> UpdateSubCategory([FromBody] USubcategoryDto dto)
        {
            var entity= await _context.SubCategories.FindAsync(dto.SubCategoryId);
            if (entity == null)
            {
                return BadRequest("not found data");
            }
            _mapper.Map(dto, entity);

           _context.SubCategories.Update(entity);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<USubcategoryDto>(entity);
            return Ok(resultDto);

        }

        [HttpDelete("{id}/SubCategory")]

        public async Task<ActionResult> DeleteSubCategory(int id)
        {
            var subcategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.SubCategoryId == id);

            if (subcategory == null)
            {
                return BadRequest($"Id:{id} not found");
            }
            _context.SubCategories.Remove(subcategory);
            await _context.SaveChangesAsync();
            return Ok();

        }



    }
}
