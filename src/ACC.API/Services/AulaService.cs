using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using ACC.Shared.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ACC.API.Services;

public class AulaService : IAulaService
{
    private readonly ACCDbContext _context;
    private readonly IMapper _mapper;
    private readonly ServiceEndpointsOptions _endpoints;

    public AulaService(ACCDbContext context, IMapper mapper, IOptions<ServiceEndpointsOptions> endpointOptions)
    {
        _context = context;
        _mapper = mapper;
        _endpoints = endpointOptions.Value;
    }

    public async Task<ServiceResult<AulaConfigDto>> GetConfigAsync(
        int aulaId,
        string currentUserId,
        bool esAdministrador = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<AulaConfigDto>.Fail("El ID del aula es inválido.");
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return ServiceResult<AulaConfigDto>.Unauthorized("Usuario no autenticado.");
            }

            var aula = await _context.Aulas
                .AsNoTracking()
                .Include(a => a.SubModulo)
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<AulaConfigDto>.NotFound("El aula no existe.");
            }

            if (!esAdministrador && !string.Equals(aula.DocenteId, currentUserId, StringComparison.Ordinal))
            {
                return ServiceResult<AulaConfigDto>.Forbidden("No tienes permisos para consultar esta aula.");
            }

            return ServiceResult<AulaConfigDto>.Ok(MapAulaConfigDto(aula), "Configuración del aula obtenida correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<AulaConfigDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<AulaConfigDto>> UpdateConfigAsync(
        int aulaId,
        AulaConfigUpdateDto updateDto,
        string currentUserId,
        bool esAdministrador = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<AulaConfigDto>.Fail("El ID del aula es inválido.");
            }

            if (updateDto == null)
            {
                return ServiceResult<AulaConfigDto>.Fail("El cuerpo de actualización es requerido.");
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return ServiceResult<AulaConfigDto>.Unauthorized("Usuario no autenticado.");
            }

            var aula = await _context.Aulas
                .Include(a => a.SubModulo)
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<AulaConfigDto>.NotFound("El aula no existe.");
            }

            if (!esAdministrador && !string.Equals(aula.DocenteId, currentUserId, StringComparison.Ordinal))
            {
                return ServiceResult<AulaConfigDto>.Forbidden("No tienes permisos para modificar esta aula.");
            }

            if (aula.ArchivarAula && !esAdministrador)
            {
                return ServiceResult<AulaConfigDto>.Forbidden("El aula está archivada.");
            }

            if (updateDto.Nombre is not null)
            {
                var nombre = updateDto.Nombre.Trim();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return ServiceResult<AulaConfigDto>.Fail("El nombre del aula no puede quedar vacío.");
                }

                aula.Nombre = nombre;
            }

            if (updateDto.Descripcion is not null)
            {
                aula.Descripcion = string.IsNullOrWhiteSpace(updateDto.Descripcion)
                    ? null
                    : updateDto.Descripcion.Trim();
            }

            if (updateDto.SubModuloId.HasValue)
            {
                if (updateDto.SubModuloId.Value <= 0)
                {
                    return ServiceResult<AulaConfigDto>.Fail("El identificador del submódulo es inválido.");
                }

                var subModuloExiste = await _context.SubModulos
                    .AsNoTracking()
                    .AnyAsync(sm => sm.Id_SubModulo == updateDto.SubModuloId.Value, cancellationToken);

                if (!subModuloExiste)
                {
                    return ServiceResult<AulaConfigDto>.NotFound("El submódulo especificado no existe.");
                }

                aula.SubModuloId = updateDto.SubModuloId.Value;
            }

            if (updateDto.CerrarAula.HasValue)
            {
                aula.CerrarAula = updateDto.CerrarAula.Value;
            }

            if (updateDto.ArchivarAula.HasValue)
            {
                aula.ArchivarAula = updateDto.ArchivarAula.Value;
                if (aula.ArchivarAula)
                {
                    aula.CerrarAula = true;
                }
            }

            aula.FechaActualizacion = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(aula).Reference(a => a.SubModulo).LoadAsync(cancellationToken);

            return ServiceResult<AulaConfigDto>.Ok(MapAulaConfigDto(aula), "Configuración del aula actualizada correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<AulaConfigDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<InvitacionGeneradaDto>> GenerarInvitacionAsync(
        int aulaId,
        string currentUserId,
        bool esAdministrador = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<InvitacionGeneradaDto>.Fail("El ID del aula es inválido.");
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return ServiceResult<InvitacionGeneradaDto>.Unauthorized("Usuario no autenticado.");
            }

            var aula = await _context.Aulas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<InvitacionGeneradaDto>.NotFound("El aula no existe.");
            }

            if (!esAdministrador && !string.Equals(aula.DocenteId, currentUserId, StringComparison.Ordinal))
            {
                return ServiceResult<InvitacionGeneradaDto>.Forbidden("No tienes permisos para generar invitaciones de esta aula.");
            }

            if (aula.ArchivarAula && !esAdministrador)
            {
                return ServiceResult<InvitacionGeneradaDto>.Forbidden("El aula está archivada.");
            }

            var token = Guid.NewGuid().ToString("N");
            var invitacion = new InvitacionAula
            {
                AulaId = aulaId,
                Token = token,
                Activa = true,
                ExpiraEn = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                NumUsos = 0
            };

            await _context.InvitacionesAula.AddAsync(invitacion, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var webAppBase = NormalizeBaseUrl(_endpoints.WebAppBaseUrl);
            var link = $"{webAppBase}invitacion/aula/{token}";

            var invitacionDto = new InvitacionGeneradaDto
            {
                Token = token,
                LinkInvitacion = link,
                ExpiraEn = invitacion.ExpiraEn
            };

            return ServiceResult<InvitacionGeneradaDto>.Ok(invitacionDto, "Invitación generada correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<InvitacionGeneradaDto>.Error(ex);
        }
    }

    private AulaConfigDto MapAulaConfigDto(Aula aula)
    {
        var dto = _mapper.Map<AulaConfigDto>(aula);
        dto.Submodulo = aula.SubModulo?.NombreSubModulo;
        return dto;
    }

    private static string NormalizeBaseUrl(string? rawBaseUrl)
    {
        if (string.IsNullOrWhiteSpace(rawBaseUrl))
        {
            throw new InvalidOperationException("ServiceEndpoints:WebAppBaseUrl no esta configurado.");
        }

        var normalized = rawBaseUrl.Trim();
        return normalized.EndsWith("/", StringComparison.Ordinal) ? normalized : $"{normalized}/";
    }
    public async Task<ServiceResult<AulaInscripcionRedeemDto>> RedeemInvitationAsync(
        string token,
        string currentUserId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return ServiceResult<AulaInscripcionRedeemDto>.Fail("El token de invitación es requerido.");
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return ServiceResult<AulaInscripcionRedeemDto>.Unauthorized("Usuario no autenticado.");
            }

            var invitacion = await _context.InvitacionesAula
                .AsNoTracking()
                .Include(i => i.Aula)
                .FirstOrDefaultAsync(i => i.Token == token, cancellationToken);

            if (invitacion == null)
            {
                return ServiceResult<AulaInscripcionRedeemDto>.NotFound("La invitación no existe o es inválida.");
            }

            if (!invitacion.Activa)
            {
                return ServiceResult<AulaInscripcionRedeemDto>.Fail("La invitación está inactiva.");
            }

            if (invitacion.ExpiraEn.HasValue && DateTime.UtcNow > invitacion.ExpiraEn.Value)
            {
                return ServiceResult<AulaInscripcionRedeemDto>.Fail("La invitación ha expirado.");
            }

            var aula = invitacion.Aula;
            if (aula == null)
            {
                return ServiceResult<AulaInscripcionRedeemDto>.NotFound("El aula asociada a la invitación no existe.");
            }

            if (aula.CerrarAula || aula.ArchivarAula)
            {
                return ServiceResult<AulaInscripcionRedeemDto>.Fail("El aula está cerrada o archivada.");
            }

            var existingEnrollment = await _context.AulaEstudiantes
                .AsNoTracking()
                .FirstOrDefaultAsync(ae => ae.AulaId == aula.AulaId && ae.UsuarioId == currentUserId, cancellationToken);

            if (existingEnrollment != null)
            {
                return ServiceResult<AulaInscripcionRedeemDto>.Fail("Ya estás inscrito en esta aula.");
            }

            var aulaEstudiante = new AulaEstudiante
            {
                AulaId = aula.AulaId,
                UsuarioId = currentUserId,
                FechaInscripcion = DateTime.UtcNow
            };

            await _context.AulaEstudiantes.AddAsync(aulaEstudiante, cancellationToken);

            invitacion.NumUsos++;

            await _context.SaveChangesAsync(cancellationToken);

            var result = new AulaInscripcionRedeemDto
            {
                AulaId = aula.AulaId,
                AulaNombre = aula.Nombre,
                AulaDescripcion = aula.Descripcion,
                FechaInscripcion = aulaEstudiante.FechaInscripcion,
                Mensaje = $"Te has inscrito correctamente en el aula '{aula.Nombre}'."
            };

            return ServiceResult<AulaInscripcionRedeemDto>.Ok(result, "Inscripción completada correctamente.");
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            return ServiceResult<AulaInscripcionRedeemDto>.Fail("Ya estás inscrito en esta aula (duplicado detectado por base de datos).");
        }
        catch (Exception ex)
        {
            return ServiceResult<AulaInscripcionRedeemDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<IReadOnlyList<AulaDto>>> GetMisAulasAsync(
        string currentUserId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return ServiceResult<IReadOnlyList<AulaDto>>.Unauthorized("Usuario no autenticado.");
            }

            var aulasEstudiante = await _context.AulaEstudiantes
                .AsNoTracking()
                .Where(ae => ae.UsuarioId == currentUserId)
                .Include(ae => ae.Aula)
                    .ThenInclude(a => a!.SubModulo)
                .Where(ae => !ae.Aula!.CerrarAula && !ae.Aula!.ArchivarAula)
                .Select(ae => ae.Aula!)
                .ToListAsync(cancellationToken);

            var dtos = _mapper.Map<List<AulaDto>>(aulasEstudiante);

            return ServiceResult<IReadOnlyList<AulaDto>>.Ok(dtos.AsReadOnly(), "Aulas obtenidas correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<IReadOnlyList<AulaDto>>.Error(ex);
        }
    }
}
