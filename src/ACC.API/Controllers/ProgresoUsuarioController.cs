using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoUsuarioController : ControllerBase
    {
        private readonly IProgresoUsuarioService _progresoService;

        public ProgresoUsuarioController(IProgresoUsuarioService progresoService)
        {
            _progresoService = progresoService;
        }

        [HttpGet("ultimo/{usuarioId}")]
        public async Task<IActionResult> ObtenerUltimoTema(string usuarioId)
        {
            var ultimoSubTema = await _progresoService.ObtenerUltimoTemaAsync(usuarioId);
            return ultimoSubTema.HasValue
                ? Ok(ultimoSubTema.Value)
                : NotFound(new { Message = "No se encontró progreso para el usuario." });
        }

        [HttpPost("guardar")]
        public async Task<IActionResult> GuardarProgreso([FromBody] ProgresoUsuarioDto progreso)
        {
            if (string.IsNullOrEmpty(progreso.UsuarioId) || progreso.SubTemaId <= 0)
                return BadRequest(new { Message = "Datos incompletos para guardar el progreso." });

            await _progresoService.GuardarProgresoAsync(progreso.UsuarioId, progreso.SubTemaId);
            return Ok(new { Message = "Progreso guardado." });
        }

        [HttpPost("completar")]
        public async Task<IActionResult> MarcarSubtemaComoCompletado([FromBody] ProgresoUsuarioDto progreso)
        {
            if (string.IsNullOrEmpty(progreso.UsuarioId) || progreso.SubTemaId <= 0)
                return BadRequest(new { Message = "Datos inválidos." });

            await _progresoService.MarcarSubtemaComoCompletadoAsync(progreso.UsuarioId, progreso.SubTemaId);
            return Ok(new { Message = "Subtema marcado como completado correctamente." });
        }

        [HttpGet("subtema-completado/{usuarioId}/{subTemaId}")]
        public async Task<IActionResult> EstaSubtemaCompletado(string usuarioId, int subTemaId)
        {
            if (string.IsNullOrEmpty(usuarioId) || subTemaId <= 0)
                return BadRequest(new { Message = "Datos inválidos." });

            var ok = await _progresoService.EstaSubtemaCompletadoAsync(usuarioId, subTemaId);
            return Ok(new { Completado = ok });
        }

        // ------------ NUEVO: endpoint genérico ------------
        // GET api/ProgresoUsuario/examen-habilitado/{userId}/{tipo}/{refId}
        // tipo: SubModulo | Modulo | Libre  (case-insensitive)
        [HttpGet("examen-habilitado/{userId}/{tipo}/{refId:int}")]
        public async Task<IActionResult> ExamenHabilitado(string userId, string tipo, int refId)
        {
            if (string.IsNullOrWhiteSpace(userId) || refId <= 0)
                return BadRequest(new { Message = "Parámetros inválidos." });

            if (!TryParseExamenTipo(tipo, out var examTipo))
                return BadRequest(new { Message = $"Tipo de examen inválido: '{tipo}'. Use SubModulo | Modulo | Libre." });

            var habilitado = await _progresoService.ExamenHabilitadoAsync(userId, new ExamenRef(examTipo, refId));
            return Ok(new { ExamenHabilitado = habilitado });
        }
        //ExamenHabilitadoAsync

        private static bool TryParseExamenTipo(string s, out ExamenTipo tipo)
        {
            // Permite strings como "submodulo", "SubModulo", "MODULO", etc.
            if (Enum.TryParse<ExamenTipo>(s, ignoreCase: true, out var t))
            {
                tipo = t;
                return true;
            }
            tipo = default;
            return false;
        }
    }
}
