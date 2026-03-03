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
public class TareasController : ControllerBase
{
    private readonly ITareasService _tareasService;

    public TareasController(ITareasService tareasService)
    {
        _tareasService = tareasService;
    }

    [HttpGet("aula/{aulaId:int}")]
    public async Task<ActionResult<ServiceResult<IReadOnlyList<TareaListadoDto>>>> GetByAula(
        int aulaId,
        CancellationToken cancellationToken)
    {
        var result = await _tareasService.GetByAulaAsync(aulaId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [Authorize(Roles = "Docente,Administrador")]
    [HttpGet("aula/{aulaId:int}/docente")]
    public async Task<ActionResult<ServiceResult<IReadOnlyList<TareaListadoDto>>>> GetByAulaDocente(
        int aulaId,
        CancellationToken cancellationToken)
    {
        var docenteId = GetUserId();
        if (string.IsNullOrWhiteSpace(docenteId))
        {
            return Unauthorized(ServiceResult<IReadOnlyList<TareaListadoDto>>.Unauthorized("No se pudo identificar al docente autenticado."));
        }

        var result = await _tareasService.GetByAulaDocenteAsync(aulaId, docenteId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [Authorize(Roles = "Docente,Administrador")]
    [HttpGet("aula/{aulaId:int}/alumnos")]
    public async Task<ActionResult<ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>>> GetAlumnosAsignables(
        int aulaId,
        CancellationToken cancellationToken)
    {
        var docenteId = GetUserId();
        if (string.IsNullOrWhiteSpace(docenteId))
        {
            return Unauthorized(ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.Unauthorized("No se pudo identificar al docente autenticado."));
        }

        var result = await _tareasService.GetAlumnosAsignablesAsync(aulaId, docenteId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [Authorize(Roles = "Docente,Administrador")]
    [HttpPost("aula/{aulaId:int}")]
    public async Task<ActionResult<ServiceResult<TareaListadoDto>>> Crear(
        int aulaId,
        [FromBody] TareaCreateDto createDto,
        CancellationToken cancellationToken)
    {
        var docenteId = GetUserId();
        if (string.IsNullOrWhiteSpace(docenteId))
        {
            return Unauthorized(ServiceResult<TareaListadoDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasService.CrearAsync(aulaId, createDto, docenteId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpPatch("{tareaId:int}/asignacion")]
    public async Task<ActionResult<ServiceResult<bool>>> ActualizarAsignacion(
        int tareaId,
        [FromBody] TareaAsignacionUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var usuarioId = GetUserId();
        if (string.IsNullOrWhiteSpace(usuarioId))
        {
            return Unauthorized(ServiceResult<bool>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasService.ActualizarAsignacionAsync(tareaId, usuarioId, updateDto, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
    }
}
