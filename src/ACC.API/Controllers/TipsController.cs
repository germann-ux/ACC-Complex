using ACC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Controllers
{
    [Route("api/tips")]
    [ApiController]
    public class TipsController : ControllerBase
    {
        private readonly ACCDbContext _context;

        public TipsController(ACCDbContext context)
        {
            _context = context;
        }

        // Endpoint para obtener un tip aleatorio
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomTip()
        {
            int count = await _context.Tips.CountAsync();

            if (count == 0)
            {
                return NotFound("No hay tips disponibles.");
            }

            var random = new Random();
            int skip = random.Next(0, count);

            var tip = await _context.Tips.Skip(skip).FirstOrDefaultAsync();

            return Ok(tip);
        }
    }
}