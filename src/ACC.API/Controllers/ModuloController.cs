using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuloController : ControllerBase
    {
        // ruta completa de la API: https://localhost:7245/api/Modulo
        private readonly IModuloService _moduloService;

        public ModuloController(IModuloService moduloService)
        {
            _moduloService = moduloService;
        }

        //ruta: api/Modulo/todos
        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<Modulo>>> GetAllModulos()
        {
            var modulos = await _moduloService.GetAllModulosAsync();
            return Ok(modulos);
        }

        //ruta: api/Modulo/1002
        [HttpGet("{id}")]
        public async Task<ActionResult<Modulo>> GetModuloById(int id)
        {
            var modulo = await _moduloService.GetModuloByIdAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }
            return Ok(modulo);
        }

        // para la biblioteca, ruta: https://localhost:7245/api/Modulo/biblioteca
        [HttpGet("biblioteca")]
        public async Task<ActionResult> GetModuloSeleccionado()
        {
            var modulo = await _moduloService.GetModuloSeleccionadoAsync();
            return Ok(new { modulo.Id_Modulo, modulo.NombreModulo });
        }

        [HttpPost]
        public async Task<ActionResult<Modulo>> CreateModulo(ModuloDto modulo)
        {
            var createdModulo = await _moduloService.CreateModuloAsync(modulo);
            return CreatedAtAction(nameof(GetModuloById), new { id = createdModulo.Id_Modulo }, createdModulo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModulo(int id, ModuloDto modulo)
        {
            if (id != modulo.Id_Modulo)
            {
                return BadRequest();
            }

            var updatedModulo = await _moduloService.UpdateModuloAsync(modulo);
            if (updatedModulo == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModulo(int id)
        {
            var result = await _moduloService.DeleteModuloAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Modulo>>> SearchModulosByName(string nombre)
        {
            var modulos = await _moduloService.SearchModulosByNameAsync(nombre);
            return Ok(modulos);
        }

        //ruta completa: https://localhost:7245/api/Modulo/progreso/1002

        [HttpGet("progreso/{moduloId}")]
        public async Task<ActionResult<UsuarioModulos>> GetProgresoUsuarioModulos(int moduloId) // no funciona, el problema es la autorizacion...
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var progreso = await _moduloService.GetProgresoUsuarioModulos(userId, moduloId);

            if (progreso == null)
            {
                return NotFound();
            }

            return Ok(progreso);
        }
    }
}
