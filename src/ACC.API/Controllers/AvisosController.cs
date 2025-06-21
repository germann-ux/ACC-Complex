using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ACC.Data.Entities;
using ACC.Shared.Interfaces;
using AutoMapper;
using ACC.Shared.DTOs;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvisosController : ControllerBase
    {
        private readonly IAvisosService _avisosService;
        private readonly IMapper _mapper; 

        public AvisosController(IAvisosService avisosService, IMapper mapper)
        {
            _avisosService = avisosService;
            _mapper = mapper;
        }

        // uri: api/Avisos/aula/{aulaId}
        [HttpGet("avisos/aula/{aulaId}")]
        public async Task<ActionResult<List<AvisoDto>>> GetAvisos([FromRoute] int aulaId)
        {
            var avisos = await _avisosService.GetAvisosAsync(aulaId);

            if (avisos == null || avisos.Count == 0)
                return NotFound();

            return Ok(avisos);
        }


        [HttpGet("get/{id}")]
        public async Task<ActionResult<Aviso>> GetAviso(int id)
        {
            var aviso = await _avisosService.GetAvisoAsync(id);
            if (aviso == null)
            {
                return NotFound();
            }
            return Ok(aviso);
        }

        [HttpPost]
        public async Task<ActionResult<Aviso>> CreateAviso(AvisoDto aviso)
        { 
            var createdAviso = await _avisosService.CreateAvisoAsync(aviso);
            return CreatedAtAction(nameof(GetAviso), new { id = createdAviso.IdAviso }, createdAviso);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> UpdateAviso(int id, AvisoDto aviso)
        {
            if (id != aviso.IdAviso)
            {
                return BadRequest();
            }

            var updatedAviso = await _avisosService.UpdateAvisoAsync(aviso);
            if (updatedAviso == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("borrar/{id}")]
        public async Task<IActionResult> DeleteAviso(int id)
        {
            var aviso = await _avisosService.GetAvisoAsync(id);
            if (aviso == null)
            {
                return NotFound();
            }

            await _avisosService.DeleteAvisoAsync(id);
            return NoContent();
        }
    }
}
