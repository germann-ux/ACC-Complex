using ACC.Shared.Enums;

namespace ACC.Shared.DTOs;

/// <summary>
/// Leccion lista para renderizarse con bloques interactivos ordenados.
/// </summary>
public class LeccionDto
{
    public int IdLeccion { get; set; }

    public string TituloLeccion { get; set; } = string.Empty;

    public string DescripcionLeccion { get; set; } = string.Empty;

    public int SubtemaId { get; set; }

    public int? AulaId { get; set; }

    public OrigenLeccion OrigenLeccion { get; set; }

    public EstadoLeccion EstadoLeccion { get; set; }

    public List<BloqueLeccionDto> Bloques { get; set; } = [];
}

public sealed class BloqueLeccionDto
{
    public int IdBloqueLeccion { get; set; }

    public int LeccionId { get; set; }

    public TipoBloqueLeccion TipoBloque { get; set; }

    public int Orden { get; set; }

    public string ConfiguracionJson { get; set; } = "{}";

    public string? Titulo { get; set; }

    public string? NivelBloom { get; set; }

    public bool EsObligatorio { get; set; }

    public decimal? Puntaje { get; set; }
}

public sealed class BloqueLeccionUpsertDto
{
    public int IdBloqueLeccion { get; set; }

    public TipoBloqueLeccion TipoBloque { get; set; }

    public int Orden { get; set; }

    public string ConfiguracionJson { get; set; } = "{}";

    public string? Titulo { get; set; }

    public string? NivelBloom { get; set; }

    public bool EsObligatorio { get; set; }

    public decimal? Puntaje { get; set; }
}
