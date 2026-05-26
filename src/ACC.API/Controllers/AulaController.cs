using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace ACC.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AulaController : ControllerBase
{
    private readonly IAulaService _aulaService;
    private readonly IAulasEstudiantesService _aulasEstudiantesService;
    public AulaController(IAulaService aulaService, IAulasEstudiantesService aulasEstudiantesService)
    {
        _aulaService = aulaService;
        _aulasEstudiantesService = aulasEstudiantesService;
    }
    [Authorize(Roles = "Docente,Administrador")]
    [HttpGet("{aulaId:int}")]
    public async Task<ActionResult<ServiceResult<AulaConfigDto>>> GetConfig(int aulaId, CancellationToken cancellationToken)
    {
        var currentUserId = GetUserId();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return Unauthorized(ServiceResult<AulaConfigDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _aulaService.GetConfigAsync(aulaId, currentUserId, IsAdministrator(), cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [Authorize(Roles = "Docente,Administrador")]
    [HttpPut("{aulaId:int}")]
    public async Task<ActionResult<ServiceResult<AulaConfigDto>>> UpdateConfig(
        int aulaId,
        [FromBody] AulaConfigUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var currentUserId = GetUserId();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return Unauthorized(ServiceResult<AulaConfigDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _aulaService.UpdateConfigAsync(aulaId, updateDto, currentUserId, IsAdministrator(), cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [Authorize(Roles = "Docente,Administrador")]
    [HttpPost("{aulaId:int}/invitaciones")]
    public async Task<ActionResult<ServiceResult<InvitacionGeneradaDto>>> GenerarInvitacion(int aulaId, CancellationToken cancellationToken)
    {
        var currentUserId = GetUserId();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return Unauthorized(ServiceResult<InvitacionGeneradaDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _aulaService.GenerarInvitacionAsync(aulaId, currentUserId, IsAdministrator(), cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [Authorize(Roles = "Docente,Administrador")]
    [HttpGet("{aulaId:int}/estudiantes")]
    public async Task<ActionResult<ServiceResult<IReadOnlyList<EstudianteListadoDto>>>> GetEstudiantes(int aulaId, CancellationToken cancellationToken)
    {
        var currentUserId = GetUserId();
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return Unauthorized(ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _aulasEstudiantesService.GetEstudiantesAsync(aulaId, currentUserId, IsAdministrator(), cancellationToken);

        [HttpPost("redeem")]
        async Task<ActionResult<ServiceResult<AulaInscripcionRedeemDto>>> RedeemInvitation(
            [FromBody] AulaInvitacionRedeemDto dto,
            CancellationToken cancellationToken)
        {
            var currentUserId = GetUserId();
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return Unauthorized(ServiceResult<AulaInscripcionRedeemDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
            }

            var result = await _aulaService.RedeemInvitationAsync(dto.Token, currentUserId, cancellationToken);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }

        [HttpGet("mis-aulas")]
        async Task<ActionResult<ServiceResult<IReadOnlyList<AulaDto>>>> GetMisAulas(CancellationToken cancellationToken)
        {
            var currentUserId = GetUserId();
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return Unauthorized(ServiceResult<IReadOnlyList<AulaDto>>.Unauthorized("No se pudo identificar al usuario autenticado."));
            }

            var result = await _aulaService.GetMisAulasAsync(currentUserId, cancellationToken);
            return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
        }
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }
    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
    }
    private bool IsAdministrator()
    {
        return User.IsInRole("Administrador");
    }
}

