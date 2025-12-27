using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers; 

[ApiController]
[Route("api/[controller]")]
public class BibliotecaController : ControllerBase
{
    private readonly IBibliotecaService _service;

    public BibliotecaController(IBibliotecaService service)
    {
        _service = service;
    }

    [HttpGet("capitulos")]
    public async Task<ActionResult<ServiceResult<List<CapituloDto>>>> ObtenerCapitulos()
    {
        var result = await _service.ObtenerCapitulosAsync();
        return StatusCode(result.StatusCode ?? 200, result);
    }

    [HttpGet("capitulos/{idCapitulo:int}")]
    public async Task<ActionResult<ServiceResult<CapituloDto>>> ObtenerCapitulo(int idCapitulo)
    {
        var result = await _service.ObtenerCapituloPorIdAsync(idCapitulo);
        return StatusCode(result.StatusCode ?? 200, result);
    }

    [HttpGet("contenidos/recomendados")]
    public async Task<ActionResult<ServiceResult<List<ContenidoCapituloDto>>>> ObtenerContenidosRecomendados(
       [FromQuery] int count = 5,
       [FromQuery] int? maxIdContenido = null
   )
    {
        var result = await _service.ObtenerContenidosRecomendadosRandomAsync(count, maxIdContenido);
        return StatusCode(result.StatusCode ?? 200, result);
    }
}