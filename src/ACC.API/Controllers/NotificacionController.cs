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
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacion>>> GetAllNotificaciones()
        {
            var notificaciones = await _notificacionService.GetAllNotificacionesAsync();
            return Ok(notificaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacion>> GetNotificacionById(int id)
        {
            var notificacion = await _notificacionService.GetNotificacionByIdAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            return Ok(notificacion);
        }

        [HttpPost]
        public async Task<ActionResult<Notificacion>> CreateNotificacion(NotificacionDto notificacion)
        {
            var createdNotificacion = await _notificacionService.CreateNotificacionAsync(notificacion);
            return CreatedAtAction(nameof(GetNotificacionById), new { id = createdNotificacion.Id }, createdNotificacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacion(int id, NotificacionDto notificacion)
        {
            if (id != notificacion.Id)
            {
                return BadRequest();
            }

            var result = await _notificacionService.UpdateNotificacionAsync(notificacion);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var result = await _notificacionService.DeleteNotificacionAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Notificacion>>> GetNotificacionesByUserId(string userId)
        {
            var notificaciones = await _notificacionService.GetNotificacionesByUserIdAsync(userId);
            return Ok(notificaciones);
        }
    }
}



