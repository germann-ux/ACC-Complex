using ACC.Shared.DTOs;

namespace ACC.WebApp.Client.Components.Pages.Resumen;

public enum EstadoEvaluacionResumen
{
    Bloqueado = 0,
    Pendiente = 1,
    NoAprobado = 2,
    Completado = 3
}

public static class ResumenEvaluacionesReglas
{
    public const int MaxIntentosPorExamen = 3;

    public static int NormalizarIntentos(int intentosRegistrados)
        => Math.Clamp(intentosRegistrados, 0, MaxIntentosPorExamen);

    public static int CalcularIntentosDisponibles(int intentosRegistrados)
        => MaxIntentosPorExamen - NormalizarIntentos(intentosRegistrados);

    public static IReadOnlyList<ExamenIntentoDto> TomarIntentosDentroDelLimite(IEnumerable<ExamenIntentoDto>? intentos)
    {
        return (intentos ?? [])
            .Where(i => i.NumeroIntento > 0)
            .OrderBy(i => i.FechaIntento)
            .ThenBy(i => i.NumeroIntento)
            .ThenBy(i => i.Id)
            .Take(MaxIntentosPorExamen)
            .ToList();
    }

    public static EstadoEvaluacionResumen ClasificarEstado(bool estaHabilitado, int intentosRegistrados, bool estaAprobado)
    {
        if (!estaHabilitado)
            return EstadoEvaluacionResumen.Bloqueado;

        var intentos = NormalizarIntentos(intentosRegistrados);
        if (intentos == 0)
            return EstadoEvaluacionResumen.Pendiente;

        return estaAprobado
            ? EstadoEvaluacionResumen.Completado
            : EstadoEvaluacionResumen.NoAprobado;
    }
}
