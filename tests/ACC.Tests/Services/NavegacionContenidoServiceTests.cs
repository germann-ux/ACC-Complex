using ACC.API.Services;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ACC.Tests.Services
{
    /// <summary>
    /// Pruebas unitarias para el servicio de navegación de contenido. Seguire haciendo pruebas de cada servicio como una buena practica.
    /// </summary>
    public class NavegacionContenidoServiceTests
    {
        private ACCDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ACCDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ACCDbContext(options);
        }

       [Fact]
        public async Task ObtenerLeccionAsync_CuandoExisteLeccion_DeberiaRetornarDto()
        {
            // Arrange
            var db = GetInMemoryDbContext();

            db.Lecciones.Add(new Leccion
            {
                IdLeccion = 1,
                TituloLeccion = "Lección de prueba",
                SubtemaId = 5,
                DescripcionLeccion = "Descripción"
            });

            await db.SaveChangesAsync();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<LeccionDto>(It.IsAny<Leccion>()))
                .Returns<Leccion>(l => new LeccionDto
                {
                    IdLeccion = l.IdLeccion,
                    TituloLeccion = l.TituloLeccion,
                    DescripcionLeccion = l.DescripcionLeccion,
                    HtmlBody = l.HtmlBody
                });

            var servicio = new NavegacionContenidoService(db, mockMapper.Object);

            // Act
            var resultado = await servicio.ObtenerLeccionAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Lección de prueba", resultado.TituloLeccion);
            Assert.Equal("Descripción", resultado.DescripcionLeccion);
        }
    }
}