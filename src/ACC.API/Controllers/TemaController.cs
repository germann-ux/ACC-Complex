using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemaController : ControllerBase
    {
        private readonly ITemaService _temaService;

        public TemaController(ITemaService temaService)
        {
            _temaService = temaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tema>>> GetAllTemas()
        {
            var temas = await _temaService.GetAllTemasAsync();
            return Ok(temas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tema>> GetTemaById(int id)
        {
            var tema = await _temaService.GetTemaByIdAsync(id);
            if (tema == null)
            {
                return NotFound();
            }
            return Ok(tema);
        }

        [HttpPost]
        public async Task<ActionResult<Tema>> CreateTema(TemaDto tema)
        {
            var createdTema = await _temaService.CreateTemaAsync(tema);
            return CreatedAtAction(nameof(GetTemaById), new { id = createdTema.Id_Tema }, createdTema);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTema(int id, TemaDto tema)
        {
            if (id != tema.Id_Tema)
            {
                return BadRequest();
            }

            var result = await _temaService.UpdateTemaAsync(tema);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTema(int id)
        {
            var result = await _temaService.DeleteTemaAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Tema>>> SearchTemasByName(string nombre)
        {
            var temas = await _temaService.SearchTemasByNameAsync(nombre);
            return Ok(temas);
        }

        //// uri: https://localhost:7245/api/Tema/submodulo/1
        [HttpGet("submodulo/{idSubModulo}")]
        public async Task<ActionResult> GetTemasPorSubModulo(int idSubModulo)
        {
            var temas = await _temaService.GetTemasPorSubModuloAsync(idSubModulo);

            var resultado = temas.Select(t => new
            {
                t.Id_Tema,
                t.NombreTema,
                t.DescripcionTema,
            });

            return Ok(resultado);
        }

        //[HttpGet("Tema/{idTema}")]
        //public async Task<ServiceResult<DateTime>> ObtenerUltimaVisitaTema(int idTema, string userId)
        //{

        //}

    }
}
