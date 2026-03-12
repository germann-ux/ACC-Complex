using ACC.Compiler.Interfaces;
using ACC.Compiler.Models;
using Microsoft.AspNetCore.Mvc;

namespace ACC.Compiler.Controllers
{
    [ApiController]
    [Route("api/compile")]
    public class CompileController : ControllerBase
    {
        private readonly ICompileService _compileService;

        public CompileController(ICompileService compileService)
        {
            _compileService = compileService;
        }

        [HttpPost]
        public async Task<IActionResult> CompileRoslyn([FromBody] CompileRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(new { Message = "El código no puede estar vacío." });
            }

            var result = await _compileService.CompileAndRunAsync(request.Code, request.Input);
            return Ok(result);
        }
    }
}
