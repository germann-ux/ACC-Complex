using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgresoUsuarioController : ControllerBase
    {
        private readonly IMapper _mapper; 
        private readonly IExamenesHabilitadosService _examenesHabilitadosService;
        private readonly IProgresoUsuarioService _progresoService;

        public ProgresoUsuarioController(IProgresoUsuarioService progresoService, IExamenesHabilitadosService examenesHabilitadosService, IMapper mapper)
        {
            _progresoService = progresoService;
            _examenesHabilitadosService = examenesHabilitadosService;
            _mapper = mapper;
        }

        [HttpGet("ultimo/{usuarioId}")]
        public async Task<IActionResult> ObtenerUltimoTema(string usuarioId)
        {
            var ultimoProgreso = await _progresoService.ObtenerUltimoTemaAsync(usuarioId);

            if (ultimoProgreso.HasValue)
            {
                return Ok(ultimoProgreso.Value);
            }

            // Cambia el 404 por un mensaje más claro
            return NotFound(new { Message = "No se encontró progreso para el usuario." });
        }

        // controladores para guardar el progreso del usuario
        [HttpPost("guardar")]
        public async Task<IActionResult> GuardarProgreso([FromBody] ProgresoUsuario progreso)
        {
            if (string.IsNullOrEmpty(progreso.UsuarioId) || progreso.SubTemaId == 0)
            {
                return BadRequest(new { Message = "Datos incompletos para guardar el progreso." });
            }

            await _progresoService.GuardarProgresoAsync(progreso.UsuarioId, progreso.SubTemaId);
            return Ok();
        }

        /// <summary>
        /// Marca un subtema como completado y verifica si el examen del submódulo debe habilitarse.
        /// </summary>
        [HttpPost("completar")]
        public async Task<IActionResult> MarcarSubtemaComoCompletado([FromBody] ProgresoUsuarioDto progreso)
        {
            if (string.IsNullOrEmpty(progreso.UsuarioId) || progreso.SubTemaId < 0)
            {
                return BadRequest(new { Message = "Datos inválidos." });
            }

            try
            {
                
                await _progresoService.MarcarSubtemaComoCompletadoAsync(progreso.UsuarioId, progreso.SubTemaId);
                return Ok(new { Message = "Subtema marcado como completado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al marcar el subtema como completado.", Error = ex.Message });
            }
        }

        /// <summary>
        /// Verifica si el examen de un submódulo está habilitado para el usuario.
        /// </summary>
        [HttpGet("examen-habilitado/{userId}/{subModuloId}")]
        public async Task<IActionResult> VerificarExamenHabilitado(string userId, int subModuloId)
        {
            var examen = await _examenesHabilitadosService.VerificarExamenHabilitadoAsync(userId, subModuloId);

            return Ok(new { ExamenHabilitado = examen });
        }

        // obtiene el estado de un subtema para el usuario
        [HttpGet("subtema-completado/{usuarioId}/{subTemaId}")]
        public async Task<IActionResult> EstaSubtemaCompletado(string usuarioId, int subTemaId)
        {
            if (string.IsNullOrEmpty(usuarioId) || subTemaId <= 0)
            {
                return BadRequest(new { Message = "Datos inválidos." });
            }
            var estaCompletado = await _progresoService.EstaSubtemaCompletadoAsync(usuarioId, subTemaId);
            return Ok(new { Completado = estaCompletado });
        }
    }
}
