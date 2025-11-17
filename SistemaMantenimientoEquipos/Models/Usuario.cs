using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SistemaMantenimientoEquipos.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;
    }
}

