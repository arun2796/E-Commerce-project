using Application.DTO.BasketDtoF;
using Application.Interface;
using Application.ReturnBasket;
using CInfrastructure.Dbconnection;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketExtension _basketExtension;
        private readonly ApplicationDbcontext _context;
        public BasketController( IBasketExtension basketExtension , ApplicationDbcontext context)
        {
            _basketExtension = basketExtension;
            _context = context;
        }
        [HttpPost]
        public async Task <ActionResult<BasketDto>> AddBasketItems(int productid ,int quantity)
        {
            var basket = await RetriveBasket();

            basket ??= CreateBasket();

            var product =await _context.Products.FindAsync(productid);
            if (product == null)
            {
                return BadRequest("problem in adding item in basket");
            }
            _basketExtension.AddBasket(basket,product,quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                return CreatedAtAction(nameof(AddBasketItems),basket.ReturnDto());
            }

            return BadRequest("problem updating in basket");

        }

        private Basket CreateBasket()
        {
            var basketId= Guid.NewGuid().ToString();

            var cookies = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("basketIdc",basketId,cookies);
            var basket = new Basket { BasketIdC= basketId, Items = [] };
            _context.Basket.Add(basket);
           
            return basket;

        }

        private async Task<Basket?> RetriveBasket()
        {
            return await  _context.Basket
                .Include(x=>x.Items)
                .ThenInclude(x=>x.Product).ThenInclude(x=>x.Category)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product).ThenInclude(p => p.Brand)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product).ThenInclude(p => p.SubCategory)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product).ThenInclude(p => p.Images)

                .FirstOrDefaultAsync(x => x.BasketIdC == Request.Cookies["basketIdc"]);    
        }
        [HttpPost("reset")]
        public async Task<ActionResult<BasketDto>> ResetBasket()
        {
            Response.Cookies.Delete("basketIdc");
            var basket = CreateBasket();
            await _context.SaveChangesAsync();
            return Ok(basket.ReturnDto());
        }
    }

}
