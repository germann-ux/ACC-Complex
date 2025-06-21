using ACC.API.Services;
using ACC.Shared.Core;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //TODO: SEGUIR AQUI...
    public class NavegacionContenidoController : ControllerBase
    {
        private readonly INavegacionContenidoService _servicio;

        public NavegacionContenidoController(INavegacionContenidoService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        [HttpGet("hijos/{tipo}/{id}")]
        public async Task<IActionResult> ObtenerHijos(TipoNodoJerarquico tipo, int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor a 0.");
            }

            var hijos = await _servicio.ObtenerHijosAsync(id, tipo);
            return hijos.Count != 0 ? Ok(hijos) : NotFound("No se encontraron hijos para el nodo especificado.");
        }

        [HttpGet("modulos")]
        public async Task<IActionResult> ObtenerModulos()
        {
            var modulos = await _servicio.ObtenerModulosAsync();
            return modulos.Count != 0 ? Ok(modulos) : NotFound("No se encontraron módulos.");
        }

        [HttpGet("padre/{tipo}/{id}")]
        public async Task<IActionResult> ObtenerPadre(TipoNodoJerarquico tipo, int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor a 0.");
            }

            var padre = await _servicio.ObtenerPadreAsync(id, tipo);
            return padre != null ? Ok(padre) : NotFound("No se encontró el nodo padre.");
        }

        [HttpGet("leccion/{leccionId}")]
        public async Task<IActionResult> ObtenerLeccion(int leccionId)
        {
            if (leccionId <= 0)
            {
                return BadRequest("El ID de la lección debe ser mayor a 0.");
            }
            var leccion = await _servicio.ObtenerLeccionAsync(leccionId);
            return leccion != null ? Ok(leccion) : NotFound("No se encontró la lección especificada.");
        }

        [HttpGet("ruta/{tipo}/{id}")]
        public async Task<IActionResult> ObtenerRuta(TipoNodoJerarquico tipo, int id)
        {
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor a 0.");
            }

            var ruta = await _servicio.ObtenerRutaDesdeRaizAsync(id, tipo);
            return ruta.Count != 0 ? Ok(ruta) : NotFound("No se encontró una ruta desde la raíz para el nodo especificado.");
        }

        [HttpPost("tema/{id}/registrar-ultima-visita")]
        public async Task<IActionResult> RegistrarUltimaVisitaTema(int id)
        {
            var exito = await _servicio.RegistrarUltimaVisitaTemaAsync(id);
            if (!exito)
                return NotFound(ServiceResult.Fail("Tema no encontrado"));

            return Ok(ServiceResult.Ok("Fecha de última visita actualizada"));
        }
    }
}
