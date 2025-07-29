using CInfrastructure.Dbconnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        protected readonly ApplicationDbcontext _context;

        public BrandController(ApplicationDbcontext dbcontext)
        {
            _context = dbcontext;
        }
    }
}
