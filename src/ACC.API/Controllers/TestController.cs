using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong! El servidor está funcionando.");
        }
    }
}
