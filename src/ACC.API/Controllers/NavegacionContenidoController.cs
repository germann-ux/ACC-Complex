using ACC.API.Services;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
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
        public async Task<ActionResult<ServiceResult<List<NodoJerarquicoDto>>>> ObtenerHijos(TipoNodoJerarquico tipo, int id)
        {
            var result = await _servicio.ObtenerHijosAsync(id, tipo);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }

        [HttpGet("modulos")]
        public async Task<ActionResult<ServiceResult<List<NodoJerarquicoDto>>>> ObtenerModulos()
        {
            var result = await _servicio.ObtenerModulosAsync();
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }

        [HttpGet("padre/{tipo}/{id}")]
        public async Task<ActionResult<ServiceResult<NodoJerarquicoDto>>> ObtenerPadre(TipoNodoJerarquico tipo, int id)
        {
            var result = await _servicio.ObtenerPadreAsync(id, tipo);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }

        [HttpGet("leccion/{leccionId}")]
        public async Task<ActionResult<ServiceResult<LeccionDto>>> ObtenerLeccion(int leccionId)
        {
            var result = await _servicio.ObtenerLeccionAsync(leccionId);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }

        [HttpGet("ruta/{tipo}/{id}")]
        public async Task<ActionResult<ServiceResult<List<NodoJerarquicoDto>>>> ObtenerRuta(TipoNodoJerarquico tipo, int id)
        {
            var result = await _servicio.ObtenerRutaDesdeRaizAsync(id, tipo);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }

        [HttpPost("tema/{id}/registrar-ultima-visita")]
        public async Task<ActionResult<ServiceResult>> RegistrarUltimaVisitaTema(int id)
        {
            var result = await _servicio.RegistrarUltimaVisitaTemaAsync(id);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }
    }
}
