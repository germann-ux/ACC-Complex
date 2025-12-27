using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities;
/// <summary>
/// Representa un Tag para categorizar Capitulos.
/// </summary>
public class Tag
{
    [Key]
    public int IdTag { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Descripcion { get; set; }

    [Required]
    public CategoriaTag Categoria { get; set; }

    /// <summary>
    /// Identificador visual del tag en fontawensome(ej. "fas fa-bolt", "fas fa-code").
    /// </summary>
    [MaxLength(100)]
    public string? Icono { get; set; }

    // M:N con Capitulo => tabla puente CapituloTag
    public ICollection<CapituloTag>? CapituloTags { get; set; }
}
