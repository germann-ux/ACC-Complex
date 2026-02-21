using ACC.Shared.DTOs;
using ACC.Shared.Enums;

namespace ACC.WebApp.Client.Components.Pages.Resumen;

public readonly record struct EvaluacionResumenKey(ExamenTipo Tipo, int RefId);

public sealed record EvaluacionResumenItem(
    EvaluacionResumenKey Key,
    int ExamenId,
    string Nombre,
    string? Descripcion,
    bool EstaHabilitado,
    int IntentosRegistrados,
    int IntentosUsados,
    int IntentosIgnorados,
    int IntentosDisponibles,
    bool Aprobado,
    double? UltimaCalificacion,
    DateTimeOffset? FechaUltimoIntento,
    EstadoEvaluacionResumen Estado);

public static class ResumenEvaluacionesModelo
{
    public static IReadOnlyList<EvaluacionResumenKey> ObtenerClavesCatalogo(
        IEnumerable<ExamenSubModuloDto>? examenesSubModulo,
        IEnumerable<ExamenModuloDto>? examenesModulo)
    {
        var clavesSub = (examenesSubModulo ?? [])
            .Where(e => e.SubModuloId > 0)
            .Select(e => new EvaluacionResumenKey(ExamenTipo.SubModulo, e.SubModuloId));

        var clavesMod = (examenesModulo ?? [])
            .Where(e => e.ModuloId > 0)
            .Select(e => new EvaluacionResumenKey(ExamenTipo.Modulo, e.ModuloId));

        return clavesSub
            .Concat(clavesMod)
            .Distinct()
            .ToList();
    }

    public static IReadOnlyList<EvaluacionResumenItem> Construir(
        IEnumerable<ExamenSubModuloDto>? examenesSubModulo,
        IEnumerable<ExamenModuloDto>? examenesModulo,
        IEnumerable<ExamenIntentoDto>? intentosUsuario,
        IReadOnlyDictionary<EvaluacionResumenKey, bool>? habilitacionesPorEvaluacion)
    {
        var subCatalogo = (examenesSubModulo ?? [])
            .Where(e => e.Id > 0 && e.SubModuloId > 0)
            .ToList();
        var modCatalogo = (examenesModulo ?? [])
            .Where(e => e.Id > 0 && e.ModuloId > 0)
            .ToList();
        var intentos = (intentosUsuario ?? []).ToList();
        var habilitaciones = habilitacionesPorEvaluacion ?? new Dictionary<EvaluacionResumenKey, bool>();

        var intentosPorEvaluacion = AgruparIntentosPorEvaluacion(intentos, subCatalogo, modCatalogo);
        var items = new List<EvaluacionResumenItem>(subCatalogo.Count + modCatalogo.Count);

        foreach (var examen in subCatalogo)
        {
            var key = new EvaluacionResumenKey(ExamenTipo.SubModulo, examen.SubModuloId);
            items.Add(CrearItem(
                key: key,
                examenId: examen.Id,
                nombre: examen.Nombre,
                descripcion: examen.Descripcion,
                habilitaciones: habilitaciones,
                intentosPorEvaluacion: intentosPorEvaluacion));
        }

        foreach (var examen in modCatalogo)
        {
            var key = new EvaluacionResumenKey(ExamenTipo.Modulo, examen.ModuloId);
            items.Add(CrearItem(
                key: key,
                examenId: examen.Id,
                nombre: examen.Nombre,
                descripcion: examen.Descripcion,
                habilitaciones: habilitaciones,
                intentosPorEvaluacion: intentosPorEvaluacion));
        }

        return items
            .OrderBy(i => i.Key.Tipo)
            .ThenBy(i => i.Key.RefId)
            .ToList();
    }

    private static EvaluacionResumenItem CrearItem(
        EvaluacionResumenKey key,
        int examenId,
        string nombre,
        string? descripcion,
        IReadOnlyDictionary<EvaluacionResumenKey, bool> habilitaciones,
        IReadOnlyDictionary<EvaluacionResumenKey, List<ExamenIntentoDto>> intentosPorEvaluacion)
    {
        var estaHabilitado = habilitaciones.TryGetValue(key, out var habilitado) && habilitado;

        var intentosRegistrados = intentosPorEvaluacion.TryGetValue(key, out var intentos)
            ? intentos.Where(i => i.NumeroIntento > 0).ToList()
            : [];

        var intentosConsiderados = ResumenEvaluacionesReglas.TomarIntentosDentroDelLimite(intentosRegistrados);
        var intentosUsados = intentosConsiderados.Count;
        var intentosIgnorados = Math.Max(0, intentosRegistrados.Count - intentosUsados);

        var ultimoIntento = intentosConsiderados
            .OrderByDescending(i => i.FechaIntento)
            .ThenByDescending(i => i.NumeroIntento)
            .ThenByDescending(i => i.Id)
            .FirstOrDefault();

        var aprobado = intentosConsiderados.Any(i => i.Aprobado);
        var estado = ResumenEvaluacionesReglas.ClasificarEstado(estaHabilitado, intentosUsados, aprobado);

        return new EvaluacionResumenItem(
            Key: key,
            ExamenId: examenId,
            Nombre: nombre,
            Descripcion: descripcion,
            EstaHabilitado: estaHabilitado,
            IntentosRegistrados: intentosRegistrados.Count,
            IntentosUsados: intentosUsados,
            IntentosIgnorados: intentosIgnorados,
            IntentosDisponibles: ResumenEvaluacionesReglas.CalcularIntentosDisponibles(intentosUsados),
            Aprobado: aprobado,
            UltimaCalificacion: ultimoIntento?.Calificacion,
            FechaUltimoIntento: ultimoIntento?.FechaIntento,
            Estado: estado);
    }

    private static Dictionary<EvaluacionResumenKey, List<ExamenIntentoDto>> AgruparIntentosPorEvaluacion(
        IEnumerable<ExamenIntentoDto> intentos,
        IEnumerable<ExamenSubModuloDto> examenesSubModulo,
        IEnumerable<ExamenModuloDto> examenesModulo)
    {
        var subRefPorExamen = examenesSubModulo
            .GroupBy(e => e.Id)
            .ToDictionary(g => g.Key, g => g.First().SubModuloId);

        var modRefPorExamen = examenesModulo
            .GroupBy(e => e.Id)
            .ToDictionary(g => g.Key, g => g.First().ModuloId);

        var intentosPorEvaluacion = new Dictionary<EvaluacionResumenKey, List<ExamenIntentoDto>>();
        foreach (var intento in intentos)
        {
            if (!TryResolverEvaluacionKey(intento, subRefPorExamen, modRefPorExamen, out var key))
                continue;

            if (!intentosPorEvaluacion.TryGetValue(key, out var lista))
            {
                lista = [];
                intentosPorEvaluacion[key] = lista;
            }

            lista.Add(intento);
        }

        return intentosPorEvaluacion;
    }

    private static bool TryResolverEvaluacionKey(
        ExamenIntentoDto intento,
        IReadOnlyDictionary<int, int> subRefPorExamen,
        IReadOnlyDictionary<int, int> modRefPorExamen,
        out EvaluacionResumenKey key)
    {
        if (intento.ExamenSubModuloId is int examenSubId &&
            examenSubId > 0 &&
            subRefPorExamen.TryGetValue(examenSubId, out var subModuloRef) &&
            subModuloRef > 0)
        {
            key = new EvaluacionResumenKey(ExamenTipo.SubModulo, subModuloRef);
            return true;
        }

        if (intento.ExamenModuloId is int examenModId &&
            examenModId > 0 &&
            modRefPorExamen.TryGetValue(examenModId, out var moduloRef) &&
            moduloRef > 0)
        {
            key = new EvaluacionResumenKey(ExamenTipo.Modulo, moduloRef);
            return true;
        }

        key = default;
        return false;
    }
}
