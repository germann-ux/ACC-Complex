using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class ContenidoCapitulo
    {
        [Key]
        public int IdContenido { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = string.Empty; // Documentación, Ejercicios, Ejemplos, etc.

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Subtitulo { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Duracion { get; set; } = string.Empty;

        public string? Dificultad { get; set; } // Opcional (solo para ejercicios)

        public string? Tags { get; set; } // HTML, CSS, JS, etc.

        [MaxLength(100)]
        public string IconoBadge { get; set; } = "fas fa-file";

        [MaxLength(50)]
        public string EtiquetaNivel { get; set; } = "General";

        public DateTime FechaActualizacion { get; set; } = DateTime.Now;

        // Relación con Capitulo
        [Required]
        public int CapituloId { get; set; }

        [Required]
        public string HtmlBody { get; set; } = string.Empty; // Contenido en formato HTML

        [ForeignKey("CapituloId")]
        public Capitulo Capitulo { get; set; } = null!;
    }

}
