using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.WebApp.Client.Components.Pages.Resumen;

namespace ACC.Tests.Resumen;

public class ResumenEvaluacionesTests
{
    [Fact]
    public void ClasificarEstado_Bloqueado_CuandoNoEstaHabilitado()
    {
        var estado = ResumenEvaluacionesReglas.ClasificarEstado(
            estaHabilitado: false,
            intentosRegistrados: 2,
            estaAprobado: true);

        Assert.Equal(EstadoEvaluacionResumen.Bloqueado, estado);
    }

    [Fact]
    public void ClasificarEstado_Pendiente_CuandoEstaHabilitadoSinIntentos()
    {
        var estado = ResumenEvaluacionesReglas.ClasificarEstado(
            estaHabilitado: true,
            intentosRegistrados: 0,
            estaAprobado: false);

        Assert.Equal(EstadoEvaluacionResumen.Pendiente, estado);
    }

    [Fact]
    public void TomarIntentosDentroDelLimite_TomaSoloTresEnOrdenCronologico()
    {
        var baseDate = new DateTimeOffset(2026, 02, 20, 10, 0, 0, TimeSpan.Zero);
        var intentos = new[]
        {
            NuevoIntento(1, 11, 2, baseDate.AddHours(1), 50, false),
            NuevoIntento(2, 11, 1, baseDate, 40, false),
            NuevoIntento(3, 11, 3, baseDate.AddHours(2), 60, false),
            NuevoIntento(4, 11, 4, baseDate.AddHours(3), 80, true)
        };

        var limitados = ResumenEvaluacionesReglas.TomarIntentosDentroDelLimite(intentos);

        Assert.Equal(3, limitados.Count);
        Assert.Equal(new[] { 2, 1, 3 }, limitados.Select(i => i.Id));
    }

    [Fact]
    public void Construir_ClasificaPendienteYBloqueadoCorrectamente()
    {
        var examenesSub = new List<ExamenSubModuloDto>
        {
            new() { Id = 10, SubModuloId = 100, Nombre = "Sub M1", Descripcion = "d1" },
            new() { Id = 11, SubModuloId = 101, Nombre = "Sub M2", Descripcion = "d2" }
        };

        var habilitaciones = new Dictionary<EvaluacionResumenKey, bool>
        {
            [new EvaluacionResumenKey(ExamenTipo.SubModulo, 100)] = true,
            [new EvaluacionResumenKey(ExamenTipo.SubModulo, 101)] = false
        };

        var items = ResumenEvaluacionesModelo.Construir(
            examenesSubModulo: examenesSub,
            examenesModulo: [],
            intentosUsuario: [],
            habilitacionesPorEvaluacion: habilitaciones);

        var pendiente = items.Single(i => i.Key.RefId == 100);
        var bloqueado = items.Single(i => i.Key.RefId == 101);

        Assert.Equal(EstadoEvaluacionResumen.Pendiente, pendiente.Estado);
        Assert.Equal(EstadoEvaluacionResumen.Bloqueado, bloqueado.Estado);
    }

    [Fact]
    public void Construir_ClasificaNoAprobadoConTresIntentos()
    {
        var examenesSub = new List<ExamenSubModuloDto>
        {
            new() { Id = 20, SubModuloId = 200, Nombre = "Sub M", Descripcion = "d" }
        };

        var habilitaciones = new Dictionary<EvaluacionResumenKey, bool>
        {
            [new EvaluacionResumenKey(ExamenTipo.SubModulo, 200)] = true
        };

        var fecha = new DateTimeOffset(2026, 02, 18, 10, 0, 0, TimeSpan.Zero);
        var intentos = new[]
        {
            NuevoIntento(1, 20, 1, fecha, 40, false),
            NuevoIntento(2, 20, 2, fecha.AddHours(1), 50, false),
            NuevoIntento(3, 20, 3, fecha.AddHours(2), 59, false)
        };

        var item = ResumenEvaluacionesModelo.Construir(examenesSub, [], intentos, habilitaciones).Single();

        Assert.Equal(EstadoEvaluacionResumen.NoAprobado, item.Estado);
        Assert.Equal(3, item.IntentosUsados);
        Assert.Equal(0, item.IntentosDisponibles);
    }

    [Fact]
    public void Construir_MarcaCompletadoEIgnoraIntentosExtra()
    {
        var examenesSub = new List<ExamenSubModuloDto>
        {
            new() { Id = 30, SubModuloId = 300, Nombre = "Sub M", Descripcion = "d" }
        };

        var habilitaciones = new Dictionary<EvaluacionResumenKey, bool>
        {
            [new EvaluacionResumenKey(ExamenTipo.SubModulo, 300)] = true
        };

        var fecha = new DateTimeOffset(2026, 02, 18, 10, 0, 0, TimeSpan.Zero);
        var intentos = new[]
        {
            NuevoIntento(1, 30, 1, fecha, 45, false),
            NuevoIntento(2, 30, 2, fecha.AddHours(1), 80, true),
            NuevoIntento(3, 30, 3, fecha.AddHours(2), 70, false),
            NuevoIntento(4, 30, 4, fecha.AddHours(3), 90, true)
        };

        var item = ResumenEvaluacionesModelo.Construir(examenesSub, [], intentos, habilitaciones).Single();

        Assert.Equal(EstadoEvaluacionResumen.Completado, item.Estado);
        Assert.Equal(3, item.IntentosUsados);
        Assert.Equal(1, item.IntentosIgnorados);
        Assert.Equal(70, item.UltimaCalificacion);
    }

    [Fact]
    public void Metricas_CalcularDesdeIntentos_IgnoraIntentosExtraYCalculaResumen()
    {
        var baseDate = new DateTimeOffset(2026, 02, 19, 10, 0, 0, TimeSpan.Zero);
        var intentos = new[]
        {
            NuevoIntento(1, 1001, 1, baseDate, 40, false),
            NuevoIntento(2, 1001, 2, baseDate.AddHours(1), 50, false),
            NuevoIntento(3, 1001, 3, baseDate.AddHours(2), 60, false),
            NuevoIntento(4, 1001, 4, baseDate.AddHours(3), 95, true),
            NuevoIntento(5, 1002, 1, baseDate.AddMinutes(30), 80, true)
        };

        var metricas = ResumenEvaluacionesMetricas.CalcularDesdeIntentos(intentos);

        Assert.True(metricas.TieneDatos);
        Assert.Equal(2, metricas.TotalEvaluacionesCalificadas);
        Assert.Equal(60, metricas.UltimaCalificacion);
        Assert.Equal(80, metricas.MejorCalificacion);
        Assert.Equal(60, metricas.PeorCalificacion);
        Assert.Equal(70, metricas.PromedioCalificacion);
    }

    private static ExamenIntentoDto NuevoIntento(
        int id,
        int examenSubModuloId,
        int numeroIntento,
        DateTimeOffset fecha,
        double calificacion,
        bool aprobado)
    {
        return new ExamenIntentoDto
        {
            Id = id,
            IdUsuario = "user-1",
            ExamenSubModuloId = examenSubModuloId,
            NumeroIntento = numeroIntento,
            FechaIntento = fecha,
            Calificacion = calificacion,
            Aprobado = aprobado
        };
    }
}
