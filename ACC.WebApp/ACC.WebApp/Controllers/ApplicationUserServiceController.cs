using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACC.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserServiceController : Controller
    {
        //private readonly IApplicationUserService _usuarioService;

        //[HttpGet("simple/{idUsuario}")]
        //public async Task<IActionResult> GetProgresoUserById(string idUsuario)
        //{
        //    var usuario = await _usuarioService.GetProgresoUserByIdAsync(idUsuario);

        //    if (usuario == null)
        //    {
        //        return NotFound(new { Message = "Usuario no encontrado" });
        //    }

        //    return Ok(usuario);
        //}
    }
}
