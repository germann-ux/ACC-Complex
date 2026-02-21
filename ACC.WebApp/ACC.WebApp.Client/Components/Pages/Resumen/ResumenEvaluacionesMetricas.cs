using ACC.Shared.DTOs;
using ACC.Shared.Enums;

namespace ACC.WebApp.Client.Components.Pages.Resumen;

public sealed record NotaVigenteEvaluacionResumen(
    ExamenTipo Tipo,
    int ExamenId,
    int NumeroIntento,
    double Calificacion,
    DateTimeOffset FechaIntento);

public sealed record ResumenEvaluacionesMetricasResultado(
    int TotalEvaluacionesCalificadas,
    double? UltimaCalificacion,
    DateTimeOffset? FechaUltimaCalificacion,
    double? MejorCalificacion,
    double? PeorCalificacion,
    double? PromedioCalificacion)
{
    public bool TieneDatos => TotalEvaluacionesCalificadas > 0;
}

public static class ResumenEvaluacionesMetricas
{
    public static IReadOnlyList<NotaVigenteEvaluacionResumen> ObtenerNotasVigentes(IEnumerable<ExamenIntentoDto>? intentos)
    {
        if (intentos is null)
            return [];

        var notas = intentos
            .Where(TryGetExamenKey)
            .GroupBy(GetExamenKey)
            .Select(g =>
            {
                var intentosConsiderados = ResumenEvaluacionesReglas.TomarIntentosDentroDelLimite(g);
                if (intentosConsiderados.Count == 0)
                    return null;

                var ultimo = intentosConsiderados
                    .OrderByDescending(i => i.FechaIntento)
                    .ThenByDescending(i => i.NumeroIntento)
                    .ThenByDescending(i => i.Id)
                    .First();

                var (tipo, examenId) = GetExamenKey(ultimo);
                return new NotaVigenteEvaluacionResumen(
                    tipo,
                    examenId,
                    intentosConsiderados.Count,
                    ultimo.Calificacion,
                    ultimo.FechaIntento);
            })
            .Where(n => n is not null)
            .Cast<NotaVigenteEvaluacionResumen>()
            .ToList();

        return notas;
    }

    public static ResumenEvaluacionesMetricasResultado Calcular(IEnumerable<NotaVigenteEvaluacionResumen>? notasVigentes)
    {
        var notas = notasVigentes?.ToList() ?? [];
        if (notas.Count == 0)
        {
            return new ResumenEvaluacionesMetricasResultado(
                TotalEvaluacionesCalificadas: 0,
                UltimaCalificacion: null,
                FechaUltimaCalificacion: null,
                MejorCalificacion: null,
                PeorCalificacion: null,
                PromedioCalificacion: null);
        }

        var ultima = notas
            .OrderByDescending(n => n.FechaIntento)
            .ThenByDescending(n => n.NumeroIntento)
            .First();

        var mejor = notas.Max(n => n.Calificacion);
        var peor = notas.Min(n => n.Calificacion);
        var promedio = notas.Average(n => n.Calificacion);

        return new ResumenEvaluacionesMetricasResultado(
            TotalEvaluacionesCalificadas: notas.Count,
            UltimaCalificacion: Redondear2(ultima.Calificacion),
            FechaUltimaCalificacion: ultima.FechaIntento,
            MejorCalificacion: Redondear2(mejor),
            PeorCalificacion: Redondear2(peor),
            PromedioCalificacion: Redondear2(promedio));
    }

    public static ResumenEvaluacionesMetricasResultado CalcularDesdeIntentos(IEnumerable<ExamenIntentoDto>? intentos)
        => Calcular(ObtenerNotasVigentes(intentos));

    private static bool TryGetExamenKey(ExamenIntentoDto intento)
    {
        var (tipo, id) = GetExamenKey(intento);
        return id > 0 && tipo is ExamenTipo.SubModulo or ExamenTipo.Modulo or ExamenTipo.Libre;
    }

    private static (ExamenTipo Tipo, int Id) GetExamenKey(ExamenIntentoDto intento)
    {
        if (intento.ExamenSubModuloId is int examenSubM && examenSubM > 0)
            return (ExamenTipo.SubModulo, examenSubM);

        if (intento.ExamenModuloId is int examenMod && examenMod > 0)
            return (ExamenTipo.Modulo, examenMod);

        if (intento.ExamenId is int examenLibre && examenLibre > 0)
            return (ExamenTipo.Libre, examenLibre);

        return (default, 0);
    }

    private static double Redondear2(double value)
        => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}
