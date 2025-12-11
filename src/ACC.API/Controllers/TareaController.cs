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
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // Crear una tarea personal
        [HttpPost("personal")]
        public async Task<IActionResult> CreateTareaPersonal([FromBody] TareaPersonalDto tareaPersonal, [FromQuery] string userId)
        {
            var result = await _tareaService.CreateTareaPersonalAsync(tareaPersonal, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Borrar una tarea personal
        [HttpDelete("personal/{tareaPersonalId}")]
        public async Task<IActionResult> DeleteTareaPersonal(int tareaPersonalId, [FromQuery] string userId)
        {
            var result = await _tareaService.DeleteTareaPersonalAsync(tareaPersonalId, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Obtener una tarea personal
        [HttpGet("personal/{tareaPersonalId}")]
        public async Task<IActionResult> GetTareaPersonal(int tareaPersonalId, [FromQuery] string userId)
        {
            var result = await _tareaService.GetTareaPersonalByUserAsync(tareaPersonalId, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        // Actualizar una tarea personal
        [HttpPut("personal")]
        public async Task<IActionResult> UpdateTareaPersonal([FromBody] TareaPersonalDto tareaPersonal)
        {
            var result = await _tareaService.UpdateTareaPersonalAsync(tareaPersonal);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Obtener todas las tareas personales de un usuario
        [HttpGet("personal/lista/{userId}")]
        public async Task<IActionResult> GetTareasPersonales(string userId)
        {
            // ServiceResult<List<TareaPersonalDto>>
            var result = await _tareaService.GetTareasPersonalesByUserAsync(userId);

            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        // Crear una tarea asignada
        [HttpPost("asignada")]
        public async Task<IActionResult> CreateTareaAsignada([FromBody] TareaAsignadaDto tareaAsignada)
        {
            var result = await _tareaService.CreateTareaAsignadaAsync(tareaAsignada);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Borrar una tarea asignada
        [HttpDelete("asignada/{tareaAsignadaId}")]
        public async Task<IActionResult> DeleteTareaAsignada(int tareaAsignadaId)
        {
            var result = await _tareaService.DeleteTareaAsignadaAsync(tareaAsignadaId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Actualizar una tarea asignada
        [HttpPut("asignada")]
        public async Task<IActionResult> UpdateTareaAsignada([FromBody] TareaAsignadaDto tareaAsignada)
        {
            var result = await _tareaService.UpdateTareaAsignadaAsync(tareaAsignada);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Obtener todas las tareas asignadas de un aula
        [HttpGet("asignada/lista/aula/{aulaId}")]
        public async Task<IActionResult> GetTareasAsignadas(int aulaId)
        {
            var result = await _tareaService.GetTareasAsignadasByAulaAsync(aulaId);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        // obtener todas las tareas asignadas de un usuario por medio de su id
        [HttpGet("asignada/lista/{userId}")]
        public async Task<IActionResult> GetTareasAsignadasByUser(string userId)
        {
            var result = await _tareaService.GetTareasAsignadasByUserAsync(userId);
            // solo retorno sin validacion, no busco regresar un 404 sin querer 
            return Ok(result);
        }
    }
}