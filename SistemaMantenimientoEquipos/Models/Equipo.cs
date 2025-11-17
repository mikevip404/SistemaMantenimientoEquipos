using System.ComponentModel.DataAnnotations;

namespace SistemaMantenimientoEquipos.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }


        [Required]
        [Display(Name = "Nombre del Equipo")]
        public required string Nombre { get; set; }


        [Required]
        [Display(Name = "Tipo de equipo")]
        public required string Tipo { get; set; }


        [Display(Name = "Descripcion")]
        public string? Descripcion { get; set; }


        [Required]
        [Display(Name = "Estado")]
        [StringLength(50)]
        public required string Estado { get; set; }


        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }


        // Relacion con los reportes
        public ICollection<Reporte>? Reportes { get; set; }
    }
}
