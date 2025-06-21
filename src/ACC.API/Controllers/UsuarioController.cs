using ACC.API.Interfaces;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
    {
        _usuarioService = usuarioService;
        _logger = logger;
    }

    // endpoint para sincronizar el usuario con la base de datos de identity en WebApp
    // url completa: http://localhost:7059/api/Usuario/sincronizar
    [HttpPost("sincronizar")] // POST Usuario/sincronizar
    public async Task<IActionResult> SincronizarUsuario([FromBody] ApplicationUserDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Id))
            return BadRequest("El usuario es inválido.");

        try
        {
            var resultado = await _usuarioService.SincUserAsync(dto);

            if (!resultado.Success)
                return StatusCode(500, resultado);

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al sincronizar usuario con ID {Id}", dto.Id);
            return StatusCode(500, "Error interno al sincronizar el usuario.");
        }
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<IActionResult> GetProgresoUserById(string idUsuario)
    {
        var usuario = await _usuarioService.GetUserByIdAsync(idUsuario);

        if (usuario == null)
        {
            return NotFound(new { Message = "Usuario no encontrado" });
        }

        return Ok(usuario);
    }
}


