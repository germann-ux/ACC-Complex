using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController()]
    public class PruebaController : Controller
    {
        [HttpGet]
        [Route("api/prueba")] // ruta: Prueba/api/prueba
        public IActionResult Get()
        {
            return Ok("Prueba exitosa");
        }
    }
}
