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
    public class SubTemaController : ControllerBase
    {
        private readonly ISubTemaService _subTemaService;

        public SubTemaController(ISubTemaService subTemaService)
        {
            _subTemaService = subTemaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTema>>> GetAllSubTemas()
        {
            var subTemas = await _subTemaService.GetAllSubTemasAsync();
            return Ok(subTemas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubTema>> GetSubTemaById(int id)
        {
            var subTema = await _subTemaService.GetSubTemaByIdAsync(id);
            if (subTema == null)
            {
                return NotFound();
            }
            return Ok(subTema);
        }

        [HttpPost]
        public async Task<ActionResult<SubTema>> CreateSubTema(SubTemaDto subTema)
        {
            var createdSubTema = await _subTemaService.CreateSubTemaAsync(subTema);
            return CreatedAtAction(nameof(GetSubTemaById), new { id = createdSubTema.Id_SubTema }, createdSubTema);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubTema(int id, SubTemaDto subTema)
        {
            if (id != subTema.Id_SubTema)
            {
                return BadRequest();
            }

            var result = await _subTemaService.UpdateSubTemaAsync(subTema);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTema(int id)
        {
            var result = await _subTemaService.DeleteSubTemaAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SubTema>>> SearchSubTemasByName(string nombre)
        {
            var subTemas = await _subTemaService.SearchSubTemasByNameAsync(nombre);
            return Ok(subTemas);
        }


        // uri: https://localhost:7245/api/SubTema/tema/1
        [HttpGet("tema/{temaId}")]
        public async Task<ActionResult<IEnumerable<SubTema>>> GetSubTemasByTema(int temaId)
        {
            var subTemas = await _subTemaService.GetSubTemasByTemaAsync(temaId);
            return Ok(subTemas);
        }

    }
}







