using ACC.API.Interfaces;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ACC.API.Services;

public sealed class LeccionesAdminService : ILeccionesAdminService
{
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
            .Include(x => x.Bloques)
            .AsQueryable();

        if (subTemaId.HasValue)
            query = query.Where(x => x.SubtemaId == subTemaId.Value);

        var lecciones = await query
            .OrderBy(x => x.SubtemaId)
            .ThenBy(x => x.IdLeccion)
            .ToListAsync(cancellationToken);

        return ServiceResult<List<LeccionAdminDto>>.Ok(lecciones.Select(MapEntityToDto).ToList());
    }

    public async Task<ServiceResult<LeccionAdminDto>> ObtenerAsync(int idLeccion, CancellationToken cancellationToken = default)
    {
        if (idLeccion <= 0)
            return ServiceResult<LeccionAdminDto>.Fail("El IdLeccion es invalido.");

        var leccion = await _db.Lecciones
            .AsNoTracking()
            .Include(x => x.Bloques)
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

        if (dto.OrigenLeccion == OrigenLeccion.App)
        {
            var aulaExiste = await _db.Aulas.AnyAsync(x => x.AulaId == dto.AulaId, cancellationToken);
            if (!aulaExiste)
                return ServiceResult<LeccionAdminDto>.NotFound("Aula no encontrada.");
        }

        var entity = new Leccion();
        MapHeader(dto, entity);
        entity.Bloques = dto.Bloques.Select(MapBlock).ToList();

        _db.Lecciones.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await InvalidateNavigationCacheAsync(entity.IdLeccion, entity.SubtemaId, cancellationToken);

        return ServiceResult<LeccionAdminDto>.Ok(MapEntityToDto(entity), "Leccion creada.");
    }

    public async Task<ServiceResult<LeccionAdminDto>> ActualizarAsync(int idLeccion, LeccionAdminDto dto, CancellationToken cancellationToken = default)
    {
        if (idLeccion <= 0)
            return ServiceResult<LeccionAdminDto>.Fail("El IdLeccion es invalido.");

        var validation = Validate(dto);
        if (!validation.Success)
            return ServiceResult<LeccionAdminDto>.Fail(validation.Message ?? "Datos invalidos.");

        var entity = await _db.Lecciones
            .Include(x => x.Bloques)
            .FirstOrDefaultAsync(x => x.IdLeccion == idLeccion, cancellationToken);

        if (entity is null)
            return ServiceResult<LeccionAdminDto>.NotFound("Leccion no encontrada.");

        var subTemaExiste = await _db.SubTemas.AnyAsync(x => x.Id_SubTema == dto.SubtemaId, cancellationToken);
        if (!subTemaExiste)
            return ServiceResult<LeccionAdminDto>.NotFound("Subtema no encontrado.");

        if (dto.OrigenLeccion == OrigenLeccion.App)
        {
            var aulaExiste = await _db.Aulas.AnyAsync(x => x.AulaId == dto.AulaId, cancellationToken);
            if (!aulaExiste)
                return ServiceResult<LeccionAdminDto>.NotFound("Aula no encontrada.");
        }

        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

        MapHeader(dto, entity);
        _db.BloquesLeccion.RemoveRange(entity.Bloques);
        await _db.SaveChangesAsync(cancellationToken);

        var nuevosBloques = dto.Bloques.Select(MapBlock).ToList();
        foreach (var bloque in nuevosBloques)
            bloque.LeccionId = entity.IdLeccion;

        await _db.BloquesLeccion.AddRangeAsync(nuevosBloques, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        entity.Bloques = nuevosBloques;

        await InvalidateNavigationCacheAsync(entity.IdLeccion, entity.SubtemaId, cancellationToken);

        return ServiceResult<LeccionAdminDto>.Ok(MapEntityToDto(entity), "Leccion actualizada.");
    }

    public async Task<ServiceResult> PublicarAsync(int idLeccion, CancellationToken cancellationToken = default)
    {
        if (idLeccion <= 0)
            return ServiceResult.Fail("El IdLeccion es invalido.");

        var leccion = await _db.Lecciones
            .Include(x => x.Bloques)
            .FirstOrDefaultAsync(x => x.IdLeccion == idLeccion, cancellationToken);

        if (leccion is null)
            return ServiceResult.NotFound("Leccion no encontrada.");

        var dto = MapEntityToDto(leccion);
        var validation = Validate(dto);
        if (!validation.Success)
            return ServiceResult.Fail($"No se puede publicar: {validation.Message}");

        leccion.EstadoLeccion = EstadoLeccion.Publicado;
        await _db.SaveChangesAsync(cancellationToken);

        await _cache.SetAsync(CacheKeys.LeccionPublicada(idLeccion), true, PublishMarkerTtl, cancellationToken);
        await InvalidateNavigationCacheAsync(idLeccion, leccion.SubtemaId, cancellationToken);
        return ServiceResult.Ok("Leccion publicada.");
    }

    private static void MapHeader(LeccionAdminDto dto, Leccion entity)
    {
        entity.TituloLeccion = dto.TituloLeccion.Trim();
        entity.DescripcionLeccion = dto.DescripcionLeccion.Trim();
        entity.SubtemaId = dto.SubtemaId;
        entity.OrigenLeccion = dto.OrigenLeccion;
        entity.EstadoLeccion = dto.EstadoLeccion;
        entity.AulaId = dto.OrigenLeccion == OrigenLeccion.Oficial ? null : dto.AulaId;

        if (entity.OrigenLeccion == OrigenLeccion.Oficial)
            entity.AulaId = null;
    }

    private static BloqueLeccion MapBlock(BloqueLeccionUpsertDto dto)
    {
        return new BloqueLeccion
        {
            TipoBloque = dto.TipoBloque,
            Orden = dto.Orden,
            ConfiguracionJson = NormalizeJson(dto.ConfiguracionJson),
            Titulo = NormalizeOptional(dto.Titulo),
            NivelBloom = NormalizeOptional(dto.NivelBloom),
            EsObligatorio = dto.EsObligatorio,
            Puntaje = dto.Puntaje
        };
    }

    private static LeccionAdminDto MapEntityToDto(Leccion entity)
    {
        return new LeccionAdminDto
        {
            IdLeccion = entity.IdLeccion,
            SubtemaId = entity.SubtemaId,
            AulaId = entity.AulaId,
            OrigenLeccion = entity.OrigenLeccion,
            EstadoLeccion = entity.EstadoLeccion,
            TituloLeccion = entity.TituloLeccion,
            DescripcionLeccion = entity.DescripcionLeccion,
            Bloques = entity.Bloques
                .OrderBy(x => x.Orden)
                .Select(x => new BloqueLeccionUpsertDto
                {
                    IdBloqueLeccion = x.IdBloqueLeccion,
                    TipoBloque = x.TipoBloque,
                    Orden = x.Orden,
                    ConfiguracionJson = x.ConfiguracionJson,
                    Titulo = x.Titulo,
                    NivelBloom = x.NivelBloom,
                    EsObligatorio = x.EsObligatorio,
                    Puntaje = x.Puntaje
                })
                .ToList()
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

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

        if (!Enum.IsDefined(typeof(OrigenLeccion), dto.OrigenLeccion))
            return ServiceResult.Fail("OrigenLeccion invalido.");

        if (!Enum.IsDefined(typeof(EstadoLeccion), dto.EstadoLeccion))
            return ServiceResult.Fail("EstadoLeccion invalido.");

        if (dto.OrigenLeccion == OrigenLeccion.App && (!dto.AulaId.HasValue || dto.AulaId <= 0))
            return ServiceResult.Fail("AulaId es requerida cuando el origen de la leccion es APP.");

        if (dto.OrigenLeccion == OrigenLeccion.Oficial && dto.AulaId.HasValue)
            return ServiceResult.Fail("Las lecciones oficiales no deben tener AulaId.");

        if (dto.Bloques is null || dto.Bloques.Count == 0)
            return ServiceResult.Fail("La leccion debe incluir al menos un bloque.");

        if (dto.Bloques.Any(x => x.Orden <= 0))
            return ServiceResult.Fail("Todos los bloques deben tener Orden mayor a 0.");

        if (dto.Bloques.Select(x => x.Orden).Distinct().Count() != dto.Bloques.Count)
            return ServiceResult.Fail("No se permiten bloques con Orden duplicado.");

        var ordered = dto.Bloques.OrderBy(x => x.Orden).ToList();
        for (var index = 0; index < ordered.Count; index++)
        {
            var bloque = ordered[index];
            bloque.Orden = index + 1;

            if (!Enum.IsDefined(typeof(TipoBloqueLeccion), bloque.TipoBloque))
                return ServiceResult.Fail($"Tipo de bloque invalido: {bloque.TipoBloque}.");

            if (bloque.Puntaje is < 0)
                return ServiceResult.Fail("Puntaje no puede ser negativo.");

            var validation = ValidateBlockConfiguration(bloque);
            if (!validation.Success)
                return validation;

            bloque.ConfiguracionJson = NormalizeJson(bloque.ConfiguracionJson);
        }

        dto.Bloques = ordered;
        return ServiceResult.Ok();
    }

    private static ServiceResult ValidateBlockConfiguration(BloqueLeccionUpsertDto bloque)
    {
        try
        {
            using var document = JsonDocument.Parse(bloque.ConfiguracionJson);
            var root = document.RootElement;

            if (root.ValueKind != JsonValueKind.Object)
                return ServiceResult.Fail($"El bloque {bloque.Orden} requiere una configuracion JSON de objeto.");

            return bloque.TipoBloque switch
            {
                TipoBloqueLeccion.TextoHtml => RequireString(root, "html", bloque),
                TipoBloqueLeccion.Imagen => RequireStrings(root, bloque, "url", "alt"),
                TipoBloqueLeccion.Video => ValidateVideo(root, bloque),
                TipoBloqueLeccion.Mermaid => RequireString(root, "codigo", bloque),
                TipoBloqueLeccion.CharpTip => RequireString(root, "texto", bloque),
                TipoBloqueLeccion.CharpDialog => RequireString(root, "texto", bloque),
                TipoBloqueLeccion.ActividadExterna => RequireString(root, "url", bloque),
                TipoBloqueLeccion.Compilador => RequireString(root, "lenguaje", bloque),
                TipoBloqueLeccion.OpcionMultiple => ValidateOpcionMultiple(root, bloque),
                TipoBloqueLeccion.VerdaderoFalso => ValidateVerdaderoFalso(root, bloque),
                TipoBloqueLeccion.RespuestaCorta => RequireString(root, "pregunta", bloque),
                TipoBloqueLeccion.Checklist => ValidateStringArray(root, "items", bloque),
                _ => ServiceResult.Fail($"Tipo de bloque invalido: {bloque.TipoBloque}.")
            };
        }
        catch (JsonException)
        {
            return ServiceResult.Fail($"ConfiguracionJson invalido en bloque {bloque.Orden}.");
        }
    }

    private static ServiceResult ValidateVideo(JsonElement root, BloqueLeccionUpsertDto bloque)
    {
        var required = RequireString(root, "videoId", bloque);
        if (!required.Success)
            return required;

        if (root.TryGetProperty("proveedor", out var proveedor) &&
            proveedor.ValueKind == JsonValueKind.String &&
            !string.Equals(proveedor.GetString(), "youtube", StringComparison.OrdinalIgnoreCase))
            return ServiceResult.Fail($"El bloque {bloque.Orden} solo soporta proveedor 'youtube' en v1.");

        return ServiceResult.Ok();
    }

    private static ServiceResult ValidateOpcionMultiple(JsonElement root, BloqueLeccionUpsertDto bloque)
    {
        var required = RequireStrings(root, bloque, "pregunta", "respuestaCorrecta");
        if (!required.Success)
            return required;

        return ValidateStringArray(root, "opciones", bloque, minimumItems: 2);
    }

    private static ServiceResult ValidateVerdaderoFalso(JsonElement root, BloqueLeccionUpsertDto bloque)
    {
        var required = RequireString(root, "afirmacion", bloque);
        if (!required.Success)
            return required;

        if (!root.TryGetProperty("respuestaCorrecta", out var respuesta) ||
            respuesta.ValueKind is not (JsonValueKind.True or JsonValueKind.False))
            return ServiceResult.Fail($"El bloque {bloque.Orden} requiere booleano 'respuestaCorrecta'.");

        return ServiceResult.Ok();
    }

    private static ServiceResult RequireStrings(JsonElement root, BloqueLeccionUpsertDto bloque, params string[] properties)
    {
        foreach (var property in properties)
        {
            var result = RequireString(root, property, bloque);
            if (!result.Success)
                return result;
        }

        return ServiceResult.Ok();
    }

    private static ServiceResult RequireString(JsonElement root, string property, BloqueLeccionUpsertDto bloque)
    {
        if (!root.TryGetProperty(property, out var value) ||
            value.ValueKind != JsonValueKind.String ||
            string.IsNullOrWhiteSpace(value.GetString()))
            return ServiceResult.Fail($"El bloque {bloque.Orden} ({bloque.TipoBloque}) requiere '{property}'.");

        return ServiceResult.Ok();
    }

    private static ServiceResult ValidateStringArray(JsonElement root, string property, BloqueLeccionUpsertDto bloque, int minimumItems = 1)
    {
        if (!root.TryGetProperty(property, out var value) || value.ValueKind != JsonValueKind.Array)
            return ServiceResult.Fail($"El bloque {bloque.Orden} ({bloque.TipoBloque}) requiere arreglo '{property}'.");

        var validItems = value.EnumerateArray()
            .Count(x => x.ValueKind == JsonValueKind.String && !string.IsNullOrWhiteSpace(x.GetString()));

        if (validItems < minimumItems)
            return ServiceResult.Fail($"El bloque {bloque.Orden} ({bloque.TipoBloque}) requiere al menos {minimumItems} elementos en '{property}'.");

        return ServiceResult.Ok();
    }

    private static string NormalizeJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return "{}";

        using var document = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(document.RootElement);
    }
}
