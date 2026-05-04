using ACC.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACC.Data.Entities;

/// <summary>
/// Unidad ordenada de contenido o interaccion dentro de una leccion.
/// </summary>
public class BloqueLeccion
{
    [Key]
    public int IdBloqueLeccion { get; set; }

    [Required]
    public int LeccionId { get; set; }

    [ForeignKey(nameof(LeccionId))]
    public Leccion? Leccion { get; set; }

    [Required]
    public TipoBloqueLeccion TipoBloque { get; set; }

    [Required]
    public int Orden { get; set; }

    [Required]
    public string ConfiguracionJson { get; set; } = "{}";

    [MaxLength(160)]
    public string? Titulo { get; set; }

    [MaxLength(64)]
    public string? NivelBloom { get; set; }

    public bool EsObligatorio { get; set; }

    public decimal? Puntaje { get; set; }
}
