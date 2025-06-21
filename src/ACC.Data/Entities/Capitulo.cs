using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class Capitulo
    {
        [Key]
        public int IdCapitulo { get; set; }

        [Required]
        [MaxLength(100)]
        public string TituloCapitulo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SubtituloCapitulo { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        //public string? HtmlBody { get; set; } // se mantiene para compatibilidad

        // Relación con Modulo
        [Required]
        public int? ModuloId { get; set; }

        [ForeignKey("ModuloId")]
        public Modulo? Modulo { get; set; }

        // Relación con SubModulo
        [Required]
        public int? SubmoduloId { get; set; }

        [ForeignKey("SubmoduloId")]
        public SubModulo? SubModulo { get; set; }

        // Relación con Tema
        [Required]
        public int? TemaId { get; set; }

        [ForeignKey("TemaId")]
        public Tema? Tema { get; set; }

        // Relación opcional con Lección (si un capítulo amplía una lección en particular)
        public int? LeccionId { get; set; }

        [ForeignKey("LeccionId")]
        public Leccion? Leccion { get; set; }

        // M:N con Tags => tabla puente CapituloTags
        public ICollection<CapituloTags>? CapituloTags { get; set; }
        // relacion con su contenido
        public ICollection<ContenidoCapitulo>? Contenidos { get; set; }
    }
}
