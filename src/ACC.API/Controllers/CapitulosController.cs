// ACC.API/Controllers/CapitulosController.cs
using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CapitulosController : ControllerBase
{
    private readonly ICapitulosService _capitulosService;
    public CapitulosController(ICapitulosService capitulosService)
    {
        _capitulosService = capitulosService;
    }

    [HttpPost("recomendados")]
    public async Task<ServiceResult<List<ContenidoCapituloDto>>> GetCapitulosRecomendados([FromBody] List<int> seleccionados)
    {
        var capitulos = await _capitulosService.CapitulosRecomendados(seleccionados);
        if (capitulos == null || capitulos.Count == 0)
            return ServiceResult<List<ContenidoCapituloDto>>.Fail("No se encontraron capítulos recomendados.", HttpStatusCodes.NotFound);

        return ServiceResult<List<ContenidoCapituloDto>>.Ok(capitulos, "Capítulos recomendados obtenidos exitosamente.");
    }

    // NUEVO: backend decide aleatoriamente
    // GET api/Capitulos/recomendados/random?max=100&count=5
    [HttpGet("recomendados/random")]
    public async Task<ServiceResult<List<ContenidoCapituloDto>>> GetRandom([FromQuery] int max = 100, [FromQuery] int count = 5)
    {
        var capitulos = await _capitulosService.CapitulosRecomendadosRandom(max, count);
        if (capitulos == null || capitulos.Count == 0)
            return ServiceResult<List<ContenidoCapituloDto>>.Fail("No hay capítulos para recomendar por ahora.", HttpStatusCodes.NotFound);

        return ServiceResult<List<ContenidoCapituloDto>>.Ok(capitulos, "Capítulos recomendados obtenidos exitosamente.");
    }
}
