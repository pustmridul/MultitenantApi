using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultitenantApi.Data;

namespace MultitenantApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TestsController(AppDbContext context)
        {
            _context= context;
        }
        [HttpGet]
        public ActionResult TestItemDb()
        {
            return Ok(_context.Database.CanConnect());
        }
        [HttpGet]
        public ActionResult TestMainDb()
        {
            using var mainCtx = new MainContext();
            return Ok(mainCtx.Database.CanConnect());
        }
    }
}
