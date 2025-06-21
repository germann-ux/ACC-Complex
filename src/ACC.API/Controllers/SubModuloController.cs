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
    public class SubModuloController : ControllerBase
    {
        // uri: api/SubModulo
        private readonly ISubModuloService _subModuloService;

        public SubModuloController(ISubModuloService subModuloService)
        {
            _subModuloService = subModuloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubModulo>>> GetAllSubModulos()
        {
            var subModulos = await _subModuloService.GetAllSubModulosAsync();
            return Ok(subModulos);
        }

        [HttpGet("{id}")] // ruta: api/SubModulo/{id}
        public async Task<ActionResult<SubModulo>> GetSubModuloById(int id)
        {
            var subModulo = await _subModuloService.GetSubModuloByIdAsync(id);
            if (subModulo == null)
            {
                return NotFound();
            }
            return Ok(subModulo);
        }

        [HttpPost]
        public async Task<ActionResult<SubModulo>> CreateSubModulo(SubModuloDto subModulo)
        {
            var createdSubModulo = await _subModuloService.CreateSubModuloAsync(subModulo);
            return CreatedAtAction(nameof(GetSubModuloById), new { id = createdSubModulo.Id_SubModulo }, createdSubModulo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubModulo(int id, SubModuloDto subModulo)
        {
            if (id != subModulo.Id_SubModulo)
            {
                return BadRequest();
            }

            var result = await _subModuloService.UpdateSubModuloAsync(subModulo);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubModulo(int id)
        {
            var result = await _subModuloService.DeleteSubModuloAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SubModulo>>> SearchSubModulosByName(string nombre)
        {
            var subModulos = await _subModuloService.SearchSubModulosByNameAsync(nombre);
            return Ok(subModulos);
        }


        // biblioteca
        [HttpGet("{idModulo}/submodulos")] // ruta: api/SubModulo/{idModulo}/submodulos
        public async Task<ActionResult> GetSubModulosPorModulo(int idModulo)
        {
            var subModulos = await _subModuloService.GetSubModulosPorModuloAsync(idModulo);

            var resultado = subModulos.Select(s => new
            {
                s.Id_SubModulo,
                s.NombreSubModulo
            });

            return Ok(resultado);
        }

    }
}





