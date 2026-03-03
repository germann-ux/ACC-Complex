using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ACC.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TareaController : ControllerBase
{
    private readonly ITareasPersonalesService _tareasPersonalesService;

    public TareaController(ITareasPersonalesService tareasPersonalesService)
    {
        _tareasPersonalesService = tareasPersonalesService;
    }

    [HttpPost("personal")]
    public async Task<IActionResult> CreateTareaPersonal([FromBody] TareaPersonalDto tareaPersonal)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<TareaPersonalDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasPersonalesService.CreateTareaPersonalAsync(tareaPersonal, userId);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpDelete("personal/{tareaPersonalId}")]
    public async Task<IActionResult> DeleteTareaPersonal(int tareaPersonalId)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<bool>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasPersonalesService.DeleteTareaPersonalAsync(tareaPersonalId, userId);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpGet("personal/{tareaPersonalId}")]
    public async Task<IActionResult> GetTareaPersonal(int tareaPersonalId)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<TareaPersonalDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasPersonalesService.GetTareaPersonalByUserAsync(tareaPersonalId, userId);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpPut("personal")]
    public async Task<IActionResult> UpdateTareaPersonal([FromBody] TareaPersonalDto tareaPersonal)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<TareaPersonalDto>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasPersonalesService.UpdateTareaPersonalAsync(tareaPersonal, userId);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpGet("personal/lista")]
    public async Task<IActionResult> GetTareasPersonales()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(ServiceResult<List<TareaPersonalDto>>.Unauthorized("No se pudo identificar al usuario autenticado."));
        }

        var result = await _tareasPersonalesService.GetTareasPersonalesByUserAsync(userId);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
    }
}
