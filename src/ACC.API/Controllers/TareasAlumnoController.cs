using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasAlumnoController : ControllerBase
{
    private readonly ITareasAlumnoService _tareasAlumnoService;

    public TareasAlumnoController(ITareasAlumnoService tareasAlumnoService)
    {
        _tareasAlumnoService = tareasAlumnoService;
    }

    [HttpGet("resumen/{userId}")]
    public async Task<ActionResult<ServiceResult<TareasPendientesResumenDto>>> GetResumenPendientes(
        string userId,
        CancellationToken cancellationToken)
    {
        var result = await _tareasAlumnoService.GetResumenPendientesAsync(userId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpGet("listado/{userId}")]
    public async Task<ActionResult<ServiceResult<List<TareaAlumnoListadoDto>>>> GetListado(
        string userId,
        CancellationToken cancellationToken)
    {
        var result = await _tareasAlumnoService.GetListadoAsync(userId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }
}
