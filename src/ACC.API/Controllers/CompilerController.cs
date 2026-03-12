using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/compile")]
    public class CompilerController : ControllerBase
    {
        private readonly CompileService _compilerService;

        public CompilerController(CompileService compilerService)
        {
            _compilerService = compilerService;
        }

        [HttpPost]
        public async Task<IActionResult> Compile([FromBody] CompileRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest(new { Message = "El código no puede estar vacío." });
            }

            var result = await _compilerService.CompileCodeAsync(request.Code, request.Input);

            if (result.StartsWith("Error de compilación", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { Message = result });
            }

            return Ok(result);
        }

        [HttpPost("/api/acc-compile")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CompileLegacy([FromBody] CompileRequest request)
        {
            Response.Headers.Append("Warning", "299 - Deprecated route. Use /api/compile");
            return await Compile(request);
        }
    }
}
