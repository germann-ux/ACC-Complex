using ACC.API.Extensions;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
    [Authorize]
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
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized(new { Message = "No se pudo identificar al usuario autenticado." });

            if (!string.Equals(authUserId, usuarioId, StringComparison.Ordinal))
                return Forbid();

            var ultimoSubTema = await _progresoService.ObtenerUltimoTemaAsync(usuarioId);
            return ultimoSubTema.HasValue
                ? Ok(ultimoSubTema.Value)
                : NotFound(new { Message = "No se encontró progreso para el usuario." });
        }

        [HttpPost("guardar")]
        public async Task<IActionResult> GuardarProgreso([FromBody] ProgresoUsuarioDto progreso)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized(new { Message = "No se pudo identificar al usuario autenticado." });

            if (progreso.SubTemaId <= 0)
                return BadRequest(new { Message = "Datos incompletos para guardar el progreso." });

            if (!string.IsNullOrWhiteSpace(progreso.UsuarioId)
                && !string.Equals(authUserId, progreso.UsuarioId, StringComparison.Ordinal))
            {
                return Forbid();
            }

            progreso.UsuarioId = authUserId;
            await _progresoService.GuardarProgresoAsync(progreso.UsuarioId, progreso.SubTemaId);
            return Ok(new { Message = "Progreso guardado." });
        }

        [HttpPost("completar")]
        public async Task<IActionResult> MarcarSubtemaComoCompletado([FromBody] ProgresoUsuarioDto progreso)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized(new { Message = "No se pudo identificar al usuario autenticado." });

            if (progreso.SubTemaId <= 0)
                return BadRequest(new { Message = "Datos inválidos." });

            if (!string.IsNullOrWhiteSpace(progreso.UsuarioId)
                && !string.Equals(authUserId, progreso.UsuarioId, StringComparison.Ordinal))
            {
                return Forbid();
            }

            progreso.UsuarioId = authUserId;
            await _progresoService.MarcarSubtemaComoCompletadoAsync(progreso.UsuarioId, progreso.SubTemaId);
            return Ok(new { Message = "Subtema marcado como completado correctamente." });
        }

        [HttpGet("subtema-completado/{usuarioId}/{subTemaId}")]
        public async Task<IActionResult> EstaSubtemaCompletado(string usuarioId, int subTemaId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized(new { Message = "No se pudo identificar al usuario autenticado." });

            if (string.IsNullOrEmpty(usuarioId) || subTemaId <= 0)
                return BadRequest(new { Message = "Datos inválidos." });

            if (!string.Equals(authUserId, usuarioId, StringComparison.Ordinal))
                return Forbid();

            var ok = await _progresoService.EstaSubtemaCompletadoAsync(usuarioId, subTemaId);
            return Ok(new { Completado = ok });
        }

        [HttpGet("resumen-guia/{usuarioId}")]
        public async Task<IActionResult> ObtenerResumenGuia(string usuarioId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized(new { Message = "No se pudo identificar al usuario autenticado." });

            if (string.IsNullOrWhiteSpace(usuarioId))
                return BadRequest(new { Message = "Datos invalidos." });

            if (!string.Equals(authUserId, usuarioId, StringComparison.Ordinal))
                return Forbid();

            var resumen = await _progresoService.ObtenerResumenGuiaAsync(usuarioId);
            return Ok(resumen);
        }

        [HttpGet("examen-habilitado/{userId}/{tipo}/{refId:int}")]
        public async Task<IActionResult> ExamenHabilitado(string userId, string tipo, int refId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized(new { Message = "No se pudo identificar al usuario autenticado." });

            if (string.IsNullOrWhiteSpace(userId) || refId <= 0)
                return BadRequest(new { Message = "Parámetros inválidos." });

            if (!string.Equals(authUserId, userId, StringComparison.Ordinal))
                return Forbid();

            if (!TryParseExamenTipo(tipo, out var examTipo))
                return BadRequest(new { Message = $"Tipo de examen inválido: '{tipo}'. Use SubModulo | Modulo | Libre." });

            var habilitado = await _progresoService.ExamenHabilitadoAsync(userId, new ExamenRef(examTipo, refId));
            return Ok(new { ExamenHabilitado = habilitado });
        }

        private static bool TryParseExamenTipo(string s, out ExamenTipo tipo)
        {
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
