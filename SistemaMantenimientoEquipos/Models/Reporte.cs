using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace SistemaMantenimientoEquipos.Models
{
    public class Reporte
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título del reporte")]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Descripción del daño")]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Fecha del reporte")]
        public DateTime FechaReporte { get; set; } = DateTime.Now;

        [Display(Name = "Estado del reporte")]
        public string Estado { get; set; } = "Pendiente";

        // Relación con Equipo
        [Display(Name = "Equipo")]
        public int? EquipoId { get; set; }

        [ForeignKey("EquipoId")]
        public Equipo? Equipo { get; set; }

        // Relación con Usuario (Identity)
        [Display(Name = "Registrado Por")]
        public string? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; }


    }
}
