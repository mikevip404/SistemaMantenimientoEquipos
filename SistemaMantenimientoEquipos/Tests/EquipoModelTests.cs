using SistemaMantenimientoEquipos.Models;
using Xunit;

namespace SistemaMantenimientoEquipos.Tests
{
    public class EquipoModelTests
    {
        [Fact]
        public void Equipo_CanBeCreated_WithValidData()
        {
            // Arrange & Act
            var equipo = new Equipo
            {
                EquipoId = 1,
                Nombre = "Computador Dell",
                Tipo = "Desktop",
                Descripcion = "Computador de escritorio",
                Estado = "Activo",
                FechaRegistro = new DateTime(2024, 1, 1)
            };

            // Assert
            Assert.Equal(1, equipo.EquipoId);
            Assert.Equal("Computador Dell", equipo.Nombre);
            Assert.Equal("Desktop", equipo.Tipo);
            Assert.Equal("Computador de escritorio", equipo.Descripcion);
            Assert.Equal("Activo", equipo.Estado);
            Assert.Equal(new DateTime(2024, 1, 1), equipo.FechaRegistro);
        }

        [Fact]
        public void Equipo_Estado_CanBeSetToDifferentValues()
        {
            // Arrange
            var equipo = new Equipo
            {
                Nombre = "Test",
                Tipo = "Test",
                Estado = "Activo"
            };

            // Act
            equipo.Estado = "Inactivo";

            // Assert
            Assert.Equal("Inactivo", equipo.Estado);
        }

        [Fact]
        public void Equipo_FechaRegistro_CanBeSet()
        {
            // Arrange
            var equipo = new Equipo
            {
                Nombre = "Test",
                Tipo = "Test",
                Estado = "Activo"
            };
            var fecha = DateTime.Now;

            // Act
            equipo.FechaRegistro = fecha;

            // Assert
            Assert.Equal(fecha, equipo.FechaRegistro);
        }
    }
}