using ACC.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACC.Data.Entities; 
public class ContenidoCapitulo
{
    [Key]
    public int IdContenido { get; set; }

    /// <summary>
    /// Tipo de contenido del capítulo (enum cerrado)
    /// </summary>
    [Required]
    public TipoContenidoCapitulo Tipo { get; set; } = TipoContenidoCapitulo.Documentacion;

    [Required]
    [MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Subtitulo { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Duración sugerida (texto corto, ej. "5 min", "10–15 min")
    /// </summary>
    [MaxLength(20)]
    public string Duracion { get; set; } = string.Empty;

    /// <summary>
    /// Dificultad (solo aplica cuando Tipo == Ejercicio).
    /// Mantener nullable para no forzar en DB; validar en capa de aplicación.
    /// </summary>
    public DificultadContenido? Dificultad { get; set; }

    /// <summary>
    /// Nivel/audiencia del contenido (profundidad)
    /// </summary>
    [Required]
    public NivelContenido Nivel { get; set; } = NivelContenido.General;

    /// <summary>
    /// Icono del contenido (clase FontAwesome u otro identificador visual)
    /// </summary>
    [MaxLength(100)]
    public string IconoBadge { get; set; } = "fas fa-file";

    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Relación con Capitulo
    public int CapituloId { get; set; }
    public Capitulo Capitulo { get; set; } = null!;

    [Required]
    public string HtmlBody { get; set; } = string.Empty;
}
