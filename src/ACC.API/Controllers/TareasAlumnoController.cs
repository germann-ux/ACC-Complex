using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ACC.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TareasAlumnoController : ControllerBase
{
    private readonly ITareasAlumnoService _tareasAlumnoService;

    public TareasAlumnoController(ITareasAlumnoService tareasAlumnoService)
    {
        _tareasAlumnoService = tareasAlumnoService;
    }

    [HttpGet("resumen")]
    public async Task<ActionResult<ServiceResult<TareasPendientesResumenDto>>> GetResumenPendientes(
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<TareasPendientesResumenDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasAlumnoService.GetResumenPendientesAsync(userId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpGet("listado")]
    public async Task<ActionResult<ServiceResult<List<TareaAlumnoListadoDto>>>> GetListado(
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<List<TareaAlumnoListadoDto>>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasAlumnoService.GetListadoAsync(userId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
    }
}
