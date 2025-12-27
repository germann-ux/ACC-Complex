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
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTagById(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag(TagDto tag)
        {
            var createdTag = await _tagService.CreateTagAsync(tag);
            return CreatedAtAction(nameof(GetTagById), new { id = createdTag.IdTag }, createdTag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, TagDto tag)
        {
            if (id != tag.IdTag)
            {
                return BadRequest();
            }

            var result = await _tagService.UpdateTagAsync(tag);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var result = await _tagService.DeleteTagAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("modulo/{moduloId}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsByModuloId(int moduloId)
        {
            var tags = await _tagService.GetTagsByModuloIdAsync(moduloId);
            return Ok(tags);
        }

        // TagController, para la bibliotecaa
        [HttpGet("tags")]
        public async Task<ActionResult> GetTags()
        {
            var tags = await _tagService.GetTagsAsync();

            var resultado = tags.Select(t => new
            {
                t.IdTag,
                t.Nombre
            });

            return Ok(resultado);
        }



    }
}
