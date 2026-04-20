using ACC.API.Extensions;
using ACC.API.Services;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace ACC.Tests.Services;

public class ExamenesServiceTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ACCmappingProfile>());
        return config.CreateMapper();
    }

    private static ACCDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ACCDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new ACCDbContext(options);
    }

    private static Mock<IPrerrequisitosService> CreatePrerequisitosMock(bool estaHabilitado = true)
    {
        var mock = new Mock<IPrerrequisitosService>();
        mock.Setup(service => service.EstaHabilitadoAsync(It.IsAny<string>(), It.IsAny<ExamenRef>()))
            .ReturnsAsync(estaHabilitado);
        mock.Setup(service => service.EvaluarDesbloqueosPorAprobacionAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        mock.Setup(service => service.EvaluarDesbloqueosPorProgresoAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        mock.Setup(service => service.EvaluarDesbloqueoSubmoduloAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        return mock;
    }

    [Fact]
    public async Task RegistrarIntentoSubModulo_CalculaPorcentajeYNumeroIntento()
    {
        await using var db = CreateDbContext();
        db.ExamenesSubModulo.Add(new ExamenSubModulo
        {
            Id = 11,
            SubModuloId = 101,
            Nombre = "Examen Sub 1",
            Descripcion = "Desc",
            NumeroPreguntas = 5,
            PuntajeAprobacion = 70,
            IntentosMaximos = 3,
            ContenidoHtml = "<p>test</p>"
        });
        await db.SaveChangesAsync();

        var service = new ExamenesService(db, CreateMapper(), CreatePrerequisitosMock().Object);
        var intento = new ExamenIntentoDto
        {
            IdUsuario = "user-1",
            ExamenSubModuloId = 11,
            NumeroAciertos = 3,
            TotalPreguntas = 5,
            TiempoSegundos = 120
        };

        var result = await service.RegistrarIntentoAsync(intento);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data!.NumeroIntento);
        Assert.Equal(60, result.Data.PorcentajeObtenido);
        Assert.Equal(60, result.Data.Calificacion);
        Assert.False(result.Data.Aprobado);
    }

    [Fact]
    public async Task RegistrarIntentoModulo_MarcaAprobadoSegunPuntajeAprobacion()
    {
        await using var db = CreateDbContext();
        db.ExamenesModulos.Add(new ExamenModulo
        {
            Id = 21,
            ModuloId = 201,
            Nombre = "Examen Mod 1",
            Descripcion = "Desc",
            NumeroPreguntas = 5,
            PuntajeAprobacion = 70,
            IntentosMaximos = 3,
            ContenidoHtml = "<p>test</p>"
        });
        await db.SaveChangesAsync();

        var service = new ExamenesService(db, CreateMapper(), CreatePrerequisitosMock().Object);
        var intento = new ExamenIntentoDto
        {
            IdUsuario = "user-1",
            ExamenModuloId = 21,
            NumeroAciertos = 4,
            TotalPreguntas = 5,
            TiempoSegundos = 90
        };

        var result = await service.RegistrarIntentoAsync(intento);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.True(result.Data!.Aprobado);
        Assert.Equal(80, result.Data.PorcentajeObtenido);
        Assert.Equal(80, result.Data.Calificacion);
    }

    [Fact]
    public async Task RegistrarIntento_FallaSiNumeroAciertosExcedeTotalPreguntas()
    {
        await using var db = CreateDbContext();
        var service = new ExamenesService(db, CreateMapper(), CreatePrerequisitosMock().Object);

        var result = await service.RegistrarIntentoAsync(new ExamenIntentoDto
        {
            IdUsuario = "user-1",
            ExamenSubModuloId = 11,
            NumeroAciertos = 6,
            TotalPreguntas = 5
        });

        Assert.False(result.Success);
        Assert.Contains("aciertos", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task RegistrarIntento_FallaSiTotalPreguntasNoCoincideConConfiguracion()
    {
        await using var db = CreateDbContext();
        db.ExamenesSubModulo.Add(new ExamenSubModulo
        {
            Id = 11,
            SubModuloId = 101,
            Nombre = "Examen Sub 1",
            Descripcion = "Desc",
            NumeroPreguntas = 5,
            PuntajeAprobacion = 70,
            IntentosMaximos = 3,
            ContenidoHtml = "<p>test</p>"
        });
        await db.SaveChangesAsync();

        var service = new ExamenesService(db, CreateMapper(), CreatePrerequisitosMock().Object);
        var result = await service.RegistrarIntentoAsync(new ExamenIntentoDto
        {
            IdUsuario = "user-1",
            ExamenSubModuloId = 11,
            NumeroAciertos = 4,
            TotalPreguntas = 4
        });

        Assert.False(result.Success);
        Assert.Contains("no coincide", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task RegistrarIntento_FallaSiAlcanzaIntentosMaximos()
    {
        await using var db = CreateDbContext();
        db.ExamenesSubModulo.Add(new ExamenSubModulo
        {
            Id = 11,
            SubModuloId = 101,
            Nombre = "Examen Sub 1",
            Descripcion = "Desc",
            NumeroPreguntas = 5,
            PuntajeAprobacion = 70,
            IntentosMaximos = 2,
            ContenidoHtml = "<p>test</p>"
        });
        db.ExamenesIntentos.AddRange(
            new ExamenIntento
            {
                Id = 1,
                IdUsuario = "user-1",
                ExamenSubModuloId = 11,
                NumeroAciertos = 2,
                TotalPreguntas = 5,
                PorcentajeObtenido = 40,
                Calificacion = 40,
                NumeroIntento = 1,
                FechaIntento = DateTimeOffset.UtcNow.AddMinutes(-10)
            },
            new ExamenIntento
            {
                Id = 2,
                IdUsuario = "user-1",
                ExamenSubModuloId = 11,
                NumeroAciertos = 3,
                TotalPreguntas = 5,
                PorcentajeObtenido = 60,
                Calificacion = 60,
                NumeroIntento = 2,
                FechaIntento = DateTimeOffset.UtcNow.AddMinutes(-5)
            });
        await db.SaveChangesAsync();

        var service = new ExamenesService(db, CreateMapper(), CreatePrerequisitosMock().Object);
        var result = await service.RegistrarIntentoAsync(new ExamenIntentoDto
        {
            IdUsuario = "user-1",
            ExamenSubModuloId = 11,
            NumeroAciertos = 4,
            TotalPreguntas = 5
        });

        Assert.False(result.Success);
        Assert.Contains("numero maximo de intentos", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ObtenerEstadoExamen_DevuelveDisponibilidadYAprobacion()
    {
        await using var db = CreateDbContext();
        db.ExamenesSubModulo.Add(new ExamenSubModulo
        {
            Id = 11,
            SubModuloId = 101,
            Nombre = "Examen Sub 1",
            Descripcion = "Desc",
            NumeroPreguntas = 5,
            PuntajeAprobacion = 70,
            IntentosMaximos = 3,
            ContenidoHtml = "<p>test</p>"
        });
        db.ExamenesIntentos.AddRange(
            new ExamenIntento
            {
                Id = 1,
                IdUsuario = "user-1",
                ExamenSubModuloId = 11,
                NumeroAciertos = 2,
                TotalPreguntas = 5,
                PorcentajeObtenido = 40,
                Calificacion = 40,
                NumeroIntento = 1,
                Aprobado = false,
                FechaIntento = DateTimeOffset.UtcNow.AddMinutes(-20)
            },
            new ExamenIntento
            {
                Id = 2,
                IdUsuario = "user-1",
                ExamenSubModuloId = 11,
                NumeroAciertos = 4,
                TotalPreguntas = 5,
                PorcentajeObtenido = 80,
                Calificacion = 80,
                NumeroIntento = 2,
                Aprobado = true,
                FechaIntento = DateTimeOffset.UtcNow.AddMinutes(-10)
            });
        db.ExamenesAprobatorios.Add(new ExamenAprobatorio
        {
            Id = 1,
            UsuarioId = "user-1",
            Tipo = ExamenTipo.SubModulo,
            ExamenId = 11,
            ExamenIntentoId = 2,
            FechaAprobacion = DateTimeOffset.UtcNow.AddMinutes(-10),
            Calificacion = 80
        });
        await db.SaveChangesAsync();

        var service = new ExamenesService(db, CreateMapper(), CreatePrerequisitosMock(estaHabilitado: true).Object);
        var result = await service.ObtenerEstadoExamenAsync("user-1", ExamenTipo.SubModulo, 11);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.True(result.Data!.EstaHabilitado);
        Assert.True(result.Data.EstaAprobado);
        Assert.True(result.Data.PuedePresentar);
        Assert.Equal(2, result.Data.IntentosRealizados);
        Assert.Equal(1, result.Data.IntentosRestantes);
        Assert.Equal(2, result.Data.UltimoIntento?.Id);
    }
}
