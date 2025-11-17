using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMantenimientoEquipos.Controllers;
using SistemaMantenimientoEquipos.Data;
using SistemaMantenimientoEquipos.Models;
using Xunit;

namespace SistemaMantenimientoEquipos.Tests
{
    public class EquiposControllerTests
    {
        private SistemaMantenimientoEquiposContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<SistemaMantenimientoEquiposContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new SistemaMantenimientoEquiposContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewWithEquipos()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Equipos.Add(new Equipo { EquipoId = 1, Nombre = "Test Equipo", Tipo = "PC", Estado = "Activo", FechaRegistro = DateTime.Now });
            await context.SaveChangesAsync();

            var controller = new EquiposController(context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Equipo>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new EquiposController(context);
            var equipo = new Equipo
            {
                Nombre = "Nuevo Equipo",
                Tipo = "Laptop",
                Descripcion = "Equipo de prueba",
                Estado = "Activo",
                FechaRegistro = DateTime.Now
            };

            // Act
            var result = await controller.Create(equipo);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            Assert.Single(context.Equipos);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsViewWithEquipo()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var equipo = new Equipo { EquipoId = 1, Nombre = "Test Equipo", Tipo = "PC", Estado = "Activo", FechaRegistro = DateTime.Now };
            context.Equipos.Add(equipo);
            await context.SaveChangesAsync();

            var controller = new EquiposController(context);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Equipo>(viewResult.Model);
            Assert.Equal("Test Equipo", model.Nombre);
        }

        [Fact]
        public async Task Details_InvalidId_ReturnsNotFound()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new EquiposController(context);

            // Act
            var result = await controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}