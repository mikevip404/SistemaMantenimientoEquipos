using SistemaMantenimientoEquipos.Models;
using Xunit;

namespace SistemaMantenimientoEquipos.Tests
{
    public class UsuarioModelTests
    {
        [Fact]
        public void Usuario_CanBeCreated_WithValidData()
        {
            // Arrange & Act
            var usuario = new Usuario
            {
                NombreCompleto = "Juan Pérez",
                Email = "juan@example.com",
                UserName = "juan@example.com"
            };

            // Assert
            Assert.Equal("Juan Pérez", usuario.NombreCompleto);
            Assert.Equal("juan@example.com", usuario.Email);
            Assert.Equal("juan@example.com", usuario.UserName);
        }

        [Fact]
        public void Usuario_InheritsFromIdentityUser()
        {
            // Arrange & Act
            var usuario = new Usuario();

            // Assert
            Assert.IsAssignableFrom<Microsoft.AspNetCore.Identity.IdentityUser>(usuario);
        }

        [Fact]
        public void Usuario_NombreCompleto_CanBeSetAndRetrieved()
        {
            // Arrange
            var usuario = new Usuario();

            // Act
            usuario.NombreCompleto = "María García";

            // Assert
            Assert.Equal("María García", usuario.NombreCompleto);
        }
    }
}