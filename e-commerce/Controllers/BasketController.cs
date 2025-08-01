using Application.DTO.BasketDtoF;

using Application.IServices;
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
        private readonly IBasketServices _basketServices;
        private readonly ApplicationDbcontext _context;
        public BasketController(IBasketServices basketServices, ApplicationDbcontext context)
        {
            _basketServices = basketServices;
            _context = context;
        }
        [HttpGet]

        public  async Task<ActionResult<BasketDto >>GetAllBasket()
        {
            var basket = await RetriveBasket() ?? throw new Exception("No basket ") ;

            return Ok(basket.ReturnDto());

        }

        [HttpPost]
        public async Task <ActionResult<BasketDto>> AddBasketItems(int productid ,int quantity)
        {
            var basket = await RetriveBasket();

            basket ??= CreateBasket();
            if (string.IsNullOrEmpty(basket.BasketIdC))
            {
                return BadRequest("basket not create");
            }

            var product =await _context.Products.FindAsync(productid);
            if (product == null)
            {
                return BadRequest("problem in adding item in basket");
            }
            if (quantity == 0)
            {
                return Ok(product);
            }
            _basketServices.AddItemToBasketAsync(basket.BasketIdC, productid, quantity);

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

        [HttpDelete]
        public async Task <ActionResult<BasketDto>> RemoveItemBasket( int product,int quantity)
        {
            var basket = await RetriveBasket();

            if (basket == null) return BadRequest("Your Basket Is Empty ");

            basket.RemoveItem(product, quantity);

            var result = await _context.SaveChangesAsync() >0 ;

            if(result) return Ok();

            if (result == false)
            {
                return BadRequest("No changes in basket");
            }
           return Ok(basket);
        }
    }

}
