using ACC.API.Interfaces;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services;

public sealed class LeccionesAdminService : ILeccionesAdminService
{
    private static readonly HashSet<string> AllowedTokens = new(StringComparer.OrdinalIgnoreCase)
    {
        SeccionesContenido.Teoria,
        SeccionesContenido.Practica,
        SeccionesContenido.Ejemplo,
        SeccionesContenido.Actividad,
        SeccionesContenido.Compilador,
        SeccionesContenido.CharpTip,
        SeccionesContenido.CharpDialog,
        SeccionesContenido.Video
    };

    private static readonly TimeSpan PublishMarkerTtl = TimeSpan.FromDays(30);

    private readonly ACCDbContext _db;
    private readonly ICacheService _cache;

    public LeccionesAdminService(ACCDbContext db, ICacheService cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<ServiceResult<List<LeccionAdminDto>>> ListarAsync(int? subTemaId, CancellationToken cancellationToken = default)
    {
        var query = _db.Lecciones
            .AsNoTracking()
            .AsQueryable();

        if (subTemaId.HasValue)
            query = query.Where(x => x.SubtemaId == subTemaId.Value);

        var lecciones = await query
            .OrderBy(x => x.SubtemaId)
            .ThenBy(x => x.IdLeccion)
            .ToListAsync(cancellationToken);

        var result = lecciones.Select(MapEntityToDto).ToList();
        return ServiceResult<List<LeccionAdminDto>>.Ok(result);
    }

    public async Task<ServiceResult<LeccionAdminDto>> ObtenerAsync(int idLeccion, CancellationToken cancellationToken = default)
    {
        if (idLeccion <= 0)
            return ServiceResult<LeccionAdminDto>.Fail("El IdLeccion es invalido.");

        var leccion = await _db.Lecciones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IdLeccion == idLeccion, cancellationToken);

        if (leccion is null)
            return ServiceResult<LeccionAdminDto>.NotFound("Leccion no encontrada.");

        return ServiceResult<LeccionAdminDto>.Ok(MapEntityToDto(leccion));
    }

    public async Task<ServiceResult<LeccionAdminDto>> CrearAsync(LeccionAdminDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (!validation.Success)
            return ServiceResult<LeccionAdminDto>.Fail(validation.Message ?? "Datos invalidos.");

        var subTemaExiste = await _db.SubTemas.AnyAsync(x => x.Id_SubTema == dto.SubtemaId, cancellationToken);
        if (!subTemaExiste)
            return ServiceResult<LeccionAdminDto>.NotFound("Subtema no encontrado.");

        var entity = MapDtoToEntity(new Leccion(), dto);
        _db.Lecciones.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await InvalidateNavigationCacheAsync(entity.IdLeccion, entity.SubtemaId, cancellationToken);

        var created = MapEntityToDto(entity);
        return ServiceResult<LeccionAdminDto>.Ok(created, "Leccion creada.");
    }

    public async Task<ServiceResult<LeccionAdminDto>> ActualizarAsync(int idLeccion, LeccionAdminDto dto, CancellationToken cancellationToken = default)
    {
        if (idLeccion <= 0)
            return ServiceResult<LeccionAdminDto>.Fail("El IdLeccion es invalido.");

        var validation = Validate(dto);
        if (!validation.Success)
            return ServiceResult<LeccionAdminDto>.Fail(validation.Message ?? "Datos invalidos.");

        var entity = await _db.Lecciones.FirstOrDefaultAsync(x => x.IdLeccion == idLeccion, cancellationToken);
        if (entity is null)
            return ServiceResult<LeccionAdminDto>.NotFound("Leccion no encontrada.");

        var subTemaExiste = await _db.SubTemas.AnyAsync(x => x.Id_SubTema == dto.SubtemaId, cancellationToken);
        if (!subTemaExiste)
            return ServiceResult<LeccionAdminDto>.NotFound("Subtema no encontrado.");

        MapDtoToEntity(entity, dto);
        await _db.SaveChangesAsync(cancellationToken);
        await InvalidateNavigationCacheAsync(entity.IdLeccion, entity.SubtemaId, cancellationToken);

        var updated = MapEntityToDto(entity);
        return ServiceResult<LeccionAdminDto>.Ok(updated, "Leccion actualizada.");
    }

    public async Task<ServiceResult> PublicarAsync(int idLeccion, CancellationToken cancellationToken = default)
    {
        if (idLeccion <= 0)
            return ServiceResult.Fail("El IdLeccion es invalido.");

        var leccion = await _db.Lecciones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IdLeccion == idLeccion, cancellationToken);

        if (leccion is null)
            return ServiceResult.NotFound("Leccion no encontrada.");

        var dto = MapEntityToDto(leccion);
        var validation = Validate(dto);
        if (!validation.Success)
            return ServiceResult.Fail($"No se puede publicar: {validation.Message}");

        await _cache.SetAsync(CacheKeys.LeccionPublicada(idLeccion), true, PublishMarkerTtl, cancellationToken);
        await InvalidateNavigationCacheAsync(idLeccion, leccion.SubtemaId, cancellationToken);
        return ServiceResult.Ok("Leccion publicada.");
    }

    private static Leccion MapDtoToEntity(Leccion entity, LeccionAdminDto dto)
    {
        entity.TituloLeccion = dto.TituloLeccion.Trim();
        entity.DescripcionLeccion = dto.DescripcionLeccion.Trim();
        entity.SubtemaId = dto.SubtemaId;
        entity.Teoria = dto.Teoria ?? string.Empty;
        entity.Practica = dto.Practica ?? string.Empty;
        entity.Ejemplo = dto.Ejemplo ?? string.Empty;
        entity.CharpTip = NormalizeOptional(dto.CharpTip);
        entity.CharpDialog = NormalizeOptional(dto.CharpDialog);
        entity.NivelBloom = dto.NivelBloom ?? string.Empty;
        entity.TieneActividad = dto.TieneActividad;
        entity.UrlActividad = NormalizeOptional(dto.UrlActividad);
        entity.TieneCompilador = dto.TieneCompilador;
        entity.TieneVideo = dto.TieneVideo;
        entity.VideoId = NormalizeOptional(dto.VideoId);
        entity.OrdenSecciones = dto.OrdenSecciones.Select(NormalizeToken).ToList();
        return entity;
    }

    private static LeccionAdminDto MapEntityToDto(Leccion entity)
    {
        return new LeccionAdminDto
        {
            IdLeccion = entity.IdLeccion,
            SubtemaId = entity.SubtemaId,
            TituloLeccion = entity.TituloLeccion,
            DescripcionLeccion = entity.DescripcionLeccion,
            Teoria = entity.Teoria,
            Practica = entity.Practica,
            Ejemplo = entity.Ejemplo,
            CharpTip = entity.CharpTip,
            CharpDialog = entity.CharpDialog,
            NivelBloom = entity.NivelBloom,
            TieneActividad = entity.TieneActividad,
            UrlActividad = entity.UrlActividad,
            TieneCompilador = entity.TieneCompilador,
            TieneVideo = entity.TieneVideo,
            VideoId = entity.VideoId,
            OrdenSecciones = entity.OrdenSecciones?.ToList() ?? []
        };
    }

    private static string? NormalizeOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return value.Trim();
    }

    private async Task InvalidateNavigationCacheAsync(int idLeccion, int subTemaId, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(CacheKeys.NavigationLeccion(idLeccion), cancellationToken);
        await _cache.RemoveAsync(CacheKeys.NavigationSubTemaLecciones(subTemaId), cancellationToken);
        await _cache.RemoveAsync(CacheKeys.NavigationRuta(TipoNodoJerarquico.Leccion, idLeccion), cancellationToken);
    }

    private static ServiceResult Validate(LeccionAdminDto dto)
    {
        if (dto is null)
            return ServiceResult.Fail("Payload vacio.");

        if (string.IsNullOrWhiteSpace(dto.TituloLeccion))
            return ServiceResult.Fail("TituloLeccion es requerido.");

        if (string.IsNullOrWhiteSpace(dto.DescripcionLeccion))
            return ServiceResult.Fail("DescripcionLeccion es requerida.");

        if (dto.SubtemaId <= 0)
            return ServiceResult.Fail("SubtemaId es requerido.");

        if (dto.OrdenSecciones is null || dto.OrdenSecciones.Count == 0)
            return ServiceResult.Fail("OrdenSecciones debe incluir al menos un token.");

        var normalized = dto.OrdenSecciones
            .Select(NormalizeToken)
            .ToList();

        if (normalized.Any(string.IsNullOrWhiteSpace))
            return ServiceResult.Fail("OrdenSecciones contiene tokens vacios.");

        if (normalized.Count != normalized.Distinct(StringComparer.OrdinalIgnoreCase).Count())
            return ServiceResult.Fail("OrdenSecciones contiene tokens duplicados.");

        var invalidToken = normalized.FirstOrDefault(x => !AllowedTokens.Contains(x));
        if (invalidToken is not null)
            return ServiceResult.Fail($"Token invalido en OrdenSecciones: {invalidToken}.");

        bool hasTeoria = normalized.Contains(SeccionesContenido.Teoria);
        bool hasPractica = normalized.Contains(SeccionesContenido.Practica);
        bool hasEjemplo = normalized.Contains(SeccionesContenido.Ejemplo);
        bool hasActividad = normalized.Contains(SeccionesContenido.Actividad);
        bool hasCompilador = normalized.Contains(SeccionesContenido.Compilador);
        bool hasCharpTip = normalized.Contains(SeccionesContenido.CharpTip);
        bool hasCharpDialog = normalized.Contains(SeccionesContenido.CharpDialog);
        bool hasVideo = normalized.Contains(SeccionesContenido.Video);

        if (hasTeoria && string.IsNullOrWhiteSpace(dto.Teoria))
            return ServiceResult.Fail("Token 'teoria' requiere contenido en Teoria.");
        if (hasPractica && string.IsNullOrWhiteSpace(dto.Practica))
            return ServiceResult.Fail("Token 'practica' requiere contenido en Practica.");
        if (hasEjemplo && string.IsNullOrWhiteSpace(dto.Ejemplo))
            return ServiceResult.Fail("Token 'ejemplo' requiere contenido en Ejemplo.");
        if (hasCharpTip && string.IsNullOrWhiteSpace(dto.CharpTip))
            return ServiceResult.Fail("Token 'charptip' requiere contenido en CharpTip.");
        if (hasCharpDialog && string.IsNullOrWhiteSpace(dto.CharpDialog))
            return ServiceResult.Fail("Token 'charpdialog' requiere contenido en CharpDialog.");
        if (hasActividad && string.IsNullOrWhiteSpace(dto.UrlActividad))
            return ServiceResult.Fail("Token 'actividad' requiere UrlActividad.");
        if (hasVideo && string.IsNullOrWhiteSpace(dto.VideoId))
            return ServiceResult.Fail("Token 'video' requiere VideoId.");

        if (dto.TieneActividad != hasActividad)
            return ServiceResult.Fail("Inconsistencia: TieneActividad debe coincidir con token 'actividad'.");
        if (dto.TieneCompilador != hasCompilador)
            return ServiceResult.Fail("Inconsistencia: TieneCompilador debe coincidir con token 'compilador'.");
        if (dto.TieneVideo != hasVideo)
            return ServiceResult.Fail("Inconsistencia: TieneVideo debe coincidir con token 'video'.");

        dto.OrdenSecciones = normalized;
        return ServiceResult.Ok();
    }

    private static string NormalizeToken(string? rawToken)
    {
        var token = rawToken?.Trim();
        if (string.IsNullOrWhiteSpace(token))
            return string.Empty;

        if (token.Equals(SeccionesContenido.Teoria, StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.Teoria;
        if (token.Equals(SeccionesContenido.Practica, StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.Practica;
        if (token.Equals(SeccionesContenido.Ejemplo, StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.Ejemplo;
        if (token.Equals(SeccionesContenido.Actividad, StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.Actividad;
        if (token.Equals(SeccionesContenido.Compilador, StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.Compilador;
        if (token.Equals(SeccionesContenido.Video, StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.Video;
        if (token.Equals(SeccionesContenido.CharpTip, StringComparison.OrdinalIgnoreCase) ||
            token.Equals("charptip", StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.CharpTip;
        if (token.Equals(SeccionesContenido.CharpDialog, StringComparison.OrdinalIgnoreCase) ||
            token.Equals("charpdialog", StringComparison.OrdinalIgnoreCase)) return SeccionesContenido.CharpDialog;

        return token;
    }
}
