using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers;

[ApiController]
[Authorize(Roles = "Administrador,Docente")]
[Route("api/admin/lecciones")]
public sealed class LeccionesAdminController : ControllerBase
{
    private readonly ILeccionesAdminService _service;

    public LeccionesAdminController(ILeccionesAdminService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResult<List<LeccionAdminDto>>>> Listar([FromQuery] int? subTemaId, CancellationToken cancellationToken)
    {
        var result = await _service.ListarAsync(subTemaId, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpGet("{idLeccion:int}")]
    public async Task<ActionResult<ServiceResult<LeccionAdminDto>>> Obtener([FromRoute] int idLeccion, CancellationToken cancellationToken)
    {
        var result = await _service.ObtenerAsync(idLeccion, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResult<LeccionAdminDto>>> Crear([FromBody] LeccionAdminDto dto, CancellationToken cancellationToken)
    {
        var result = await _service.CrearAsync(dto, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpPut("{idLeccion:int}")]
    public async Task<ActionResult<ServiceResult<LeccionAdminDto>>> Actualizar([FromRoute] int idLeccion, [FromBody] LeccionAdminDto dto, CancellationToken cancellationToken)
    {
        var result = await _service.ActualizarAsync(idLeccion, dto, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }

    [HttpPost("{idLeccion:int}/publicar")]
    public async Task<ActionResult<ServiceResult>> Publicar([FromRoute] int idLeccion, CancellationToken cancellationToken)
    {
        var result = await _service.PublicarAsync(idLeccion, cancellationToken);
        return StatusCode(result.StatusCode ?? HttpStatusCodes.OK, result);
    }
}

