using CInfrastructure.Dbconnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly ApplicationDbcontext _context;

        public ProductController( ApplicationDbcontext dbcontext)
        {
            _context = dbcontext;
        }
    }
}
