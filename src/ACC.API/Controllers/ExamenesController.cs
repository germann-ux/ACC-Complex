using ACC.API.Extensions;
using ACC.API.Interfaces;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ExamenesController : ControllerBase
    {
        private readonly IExamenesModService _modService;
        private readonly IExamenesSubMService _subMService;
        private readonly IExamenesUserService _userService;

        public ExamenesController(
            IExamenesUserService examenesUserService,
            IExamenesModService examenesModService,
            IExamenesSubMService examenesSubMService)
        {
            _modService = examenesModService;
            _subMService = examenesSubMService;
            _userService = examenesUserService;
        }

        [HttpGet("ExamenesSubM/todos")]
        public async Task<ActionResult> ObtenerExamanesSubMAsync()
            => (await _subMService.ObtenerExamenesSubMAsync()).ToHttp(this);

        [HttpGet("ExamenesSubM/{id:int}")]
        public async Task<ActionResult> ObtenerExamenSubMAsync([FromRoute] int id)
            => (await _subMService.ObtenerExamenSubMAsync(id)).ToHttp(this);

        [HttpGet("ExamIntento/usuario/{userId}")]
        public async Task<ActionResult> ObtenerIntentosPorUsuarioAsync([FromRoute] string userId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized();

            if (!string.Equals(authUserId, userId, StringComparison.Ordinal))
                return Forbid();

            return (await _userService.ObtenerIntentosPorUsuarioAsync(userId)).ToHttp(this);
        }

        [HttpGet("ExamIntento/usuario/{userId}/examen/{examenId:int}")]
        public async Task<ActionResult> ObtenerUltimoIntentoPorUsuarioYExamenAsync(
            [FromRoute] string userId, [FromRoute] int examenId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized();

            if (!string.Equals(authUserId, userId, StringComparison.Ordinal))
                return Forbid();

            return (await _userService.ObtenerUltimoIntentoPorUsuarioYExamenAsync(userId, examenId)).ToHttp(this);
        }

        [HttpGet("ExamIntento/usuario/{userId}/tipo/{tipo}/examen/{examenId:int}")]
        public async Task<ActionResult> ObtenerUltimoIntentoPorUsuarioYTipoDeExamenAsync(
            [FromRoute] string userId,
            [FromRoute] string tipo,
            [FromRoute] int examenId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized();

            if (!string.Equals(authUserId, userId, StringComparison.Ordinal))
                return Forbid();

            if (!Enum.TryParse<ExamenTipo>(tipo, ignoreCase: true, out var examenTipo))
                return BadRequest(new { Message = $"Tipo de examen inválido: '{tipo}'. Use SubModulo | Modulo | Libre." });

            return (await _userService.ObtenerUltimoIntentoPorUsuarioYExamenAsync(userId, examenTipo, examenId)).ToHttp(this);
        }

        [HttpGet("estado/{tipo}/{examenId:int}")]
        public async Task<ActionResult> ObtenerEstadoExamenAsync(
            [FromRoute] string tipo,
            [FromRoute] int examenId)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized();

            if (!Enum.TryParse<ExamenTipo>(tipo, ignoreCase: true, out var examenTipo))
                return BadRequest(new { Message = $"Tipo de examen invalido: '{tipo}'. Use SubModulo | Modulo | Libre." });

            return (await _userService.ObtenerEstadoExamenAsync(authUserId, examenTipo, examenId)).ToHttp(this);
        }

        [HttpPost("ExamIntento/registrar")]
        public async Task<ActionResult> RegistrarIntentoExamenAsync([FromBody] ExamenIntentoDto intentoDto)
        {
            var authUserId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(authUserId))
                return Unauthorized();

            if (!string.IsNullOrWhiteSpace(intentoDto.IdUsuario)
                && !string.Equals(authUserId, intentoDto.IdUsuario, StringComparison.Ordinal))
            {
                return Forbid();
            }

            intentoDto.IdUsuario = authUserId;
            return (await _userService.RegistrarIntentoAsync(intentoDto)).ToHttp(this);
        }

        [HttpGet("ExamenesMod/todos")]
        public async Task<ActionResult> ObtenerExamenesModAsync()
            => (await _modService.ObtenerExamenesModAsync()).ToHttp(this);

        [HttpGet("ExamenesMod/{id:int}")]
        public async Task<ActionResult> ObtenerExamenModAsync([FromRoute] int id)
            => (await _modService.ObtenerExamenModAsync(id)).ToHttp(this);
    }
}
