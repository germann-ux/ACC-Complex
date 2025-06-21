using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialCalificacionesController : ControllerBase
    {
        private readonly IHistorialCalificacionesService _historialCalificacionesService;

        public HistorialCalificacionesController(IHistorialCalificacionesService historialCalificacionesService)
        {
            _historialCalificacionesService = historialCalificacionesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCalificaciones>>> GetAllHistoriales()
        {
            var historiales = await _historialCalificacionesService.GetAllHistorialesAsync();
            return Ok(historiales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialCalificaciones>> GetHistorialById(int id)
        {
            var historial = await _historialCalificacionesService.GetHistorialByIdAsync(id);
            if (historial == null)
            {
                return NotFound();
            }
            return Ok(historial);
        }

        [HttpPost]
        public async Task<ActionResult<HistorialCalificaciones>> CreateHistorial(HistorialCalificacionesDto historial)
        {
            var createdHistorial = await _historialCalificacionesService.CreateHistorialAsync(historial);
            return CreatedAtAction(nameof(GetHistorialById), new { id = createdHistorial.Id_Historial }, createdHistorial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistorial(int id, HistorialCalificacionesDto historial)
        {
            if (id != historial.Id_Historial)
            {
                return BadRequest();
            }

            var result = await _historialCalificacionesService.UpdateHistorialAsync(historial);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorial(int id)
        {
            var result = await _historialCalificacionesService.DeleteHistorialAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<HistorialCalificaciones>>> GetHistorialesByUserId(string userId)
        {
            var historiales = await _historialCalificacionesService.GetHistorialesByUserIdAsync(userId);
            return Ok(historiales);
        }

        // Obtener la calificación de un módulo
        [HttpGet("modulo/{userId}/{moduloId}")]
        public async Task<IActionResult> GetCalificacionModulo(string userId, int moduloId)
        {
            var calificacion = await _historialCalificacionesService.GetCalificacionModuloAsync(userId, moduloId);
            if (calificacion.HasValue)
            {
                return Ok(calificacion.Value);
            }

            return NotFound(new { Message = "No se encontró una calificación para el módulo especificado." });
        }

        // Obtener la calificación de un submódulo
        [HttpGet("submodulo/{userId}/{subModuloId}")]
        public async Task<IActionResult> GetCalificacionSubModulo(string userId, int subModuloId)
        {
            var calificacion = await _historialCalificacionesService.GetCalificacionSubModuloAsync(userId, subModuloId);
            if (calificacion.HasValue)
            {
                return Ok(calificacion.Value);
            }

            return NotFound(new { Message = "No se encontró una calificación para el submódulo especificado." });
        }

        // Registrar la calificación de un módulo
        [HttpPost("modulo")]
        public async Task<IActionResult> PostCalificacionModulo([FromBody] CalificacionRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserId))
            {
                return BadRequest(new { Message = "Los datos proporcionados no son válidos." });
            }

            await _historialCalificacionesService.PostCalificacionModuloAsync(request.UserId, request.ModuloId, request.Calificacion);
            return Ok(new { Message = "Calificación del módulo registrada con éxito." });
        }

        // Registrar la calificación de un submódulo
        [HttpPost("submodulo")]
        public async Task<IActionResult> PostCalificacionSubModulo([FromBody] CalificacionRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.UserId))
            {
                return BadRequest(new { Message = "Los datos proporcionados no son válidos." });
            }

            await _historialCalificacionesService.PostCalificacionSubModuloAsync(request.UserId, request.SubModuloId, request.Calificacion);
            return Ok(new { Message = "Calificación del submódulo registrada con éxito." });
        }
    }

    // Clase auxiliar para recibir los datos de las solicitudes POST
    public class CalificacionRequest
    {
        public string UserId { get; set; }
        public int ModuloId { get; set; }
        public int SubModuloId { get; set; }
        public decimal Calificacion { get; set; }
    }

}




