using ACC.API.Extensions;
using ACC.API.Interfaces;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
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

        // Submódulos
        [HttpGet("ExamenesSubM/todos")]
        public async Task<ActionResult> ObtenerExamanesSubMAsync()
            => (await _subMService.ObtenerExamenesSubMAsync()).ToHttp(this);

        [HttpGet("ExamenesSubM/{id:int}")]
        public async Task<ActionResult> ObtenerExamenSubMAsync([FromRoute] int id)
            => (await _subMService.ObtenerExamenSubMAsync(id)).ToHttp(this);

        // Intentos por usuario
        [HttpGet("ExamIntento/usuario/{userId}")]
        public async Task<ActionResult> ObtenerIntentosPorUsuarioAsync([FromRoute] string userId)
            => (await _userService.ObtenerIntentosPorUsuarioAsync(userId)).ToHttp(this);

        [HttpGet("ExamIntento/usuario/{userId}/examen/{examenId:int}")]
        public async Task<ActionResult> ObtenerUltimoIntentoPorUsuarioYExamenAsync(
            [FromRoute] string userId, [FromRoute] int examenId)
            => (await _userService.ObtenerUltimoIntentoPorUsuarioYExamenAsync(userId, examenId)).ToHttp(this);

        // Registrar intento
        [HttpPost("ExamIntento/registrar")]
        public async Task<ActionResult> RegistrarIntentoExamenAsync([FromBody] ExamenIntentoDto intentoDto)
            => (await _userService.RegistrarIntentoAsync(intentoDto)).ToHttp(this);

        [HttpGet("ExamenesMod/todos")]
        public async Task<ActionResult> ObtenerExamenesModAsync()
            => (await _modService.ObtenerExamenesModAsync()).ToHttp(this);
        //ObtenerExamenesModAsync
    }
}
