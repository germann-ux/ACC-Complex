using API_CompilerACC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API_CompilerACC.Models; 

namespace API_CompilerACC.Controllers
{
    [ApiController]
    [Route("api/compile")]
    public class CompileController : Controller
    {
        // ruta: https://localhost:7023/api/Compile
        private readonly ICompileService _compileService;

        public CompileController(ICompileService compileService) 
        {
            _compileService = compileService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Compile([FromBody] CompileRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Code))
        //    {
        //        return BadRequest(new { Message = "El código no puede estar vacío." });
        //    }

        //    var result = await _compileService.CompileAndRunAsync(request.Code, request.Input);
        //    return Ok(result);
        //}

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
