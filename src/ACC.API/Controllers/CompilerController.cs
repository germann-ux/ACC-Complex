using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using ACC.Shared.Core; 

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/acc-compile")]
    public class CompilerController : Controller
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

            if (result.StartsWith("Error de compilación"))
            {
                return BadRequest(new { Message = result });
            }

            return Ok(result);
        }
    }
}

