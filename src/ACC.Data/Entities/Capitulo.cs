using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

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

    [Required]
    [MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    // Relación con Modulo(opcional)
    public int? ModuloId { get; set; }
    public Modulo? Modulo { get; set; }

    // Relación con SubModulo(opcional)
    public int? SubmoduloId { get; set; }
    public SubModulo? SubModulo { get; set; }

    // Relación con Tema(opcional)
    public int? TemaId { get; set; }
    public Tema? Tema { get; set; }

    // Relación opcional con Lección (si un capítulo amplía una lección en particular(opcional))
    public int? LeccionId { get; set; }
    public Leccion? Leccion { get; set; }

    // M:N con Tags => tabla puente CapituloTags
    public ICollection<CapituloTag>? CapituloTags { get; set; }
    // relacion con su contenido
    public ICollection<ContenidoCapitulo>? Contenidos { get; set; }
}
