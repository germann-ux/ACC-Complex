using ACC.Shared.Enums;

namespace ACC.Shared.DTOs;

public sealed class LeccionAdminDto
{
    public int IdLeccion { get; set; }

    public int SubtemaId { get; set; }

    public int? AulaId { get; set; }

    public OrigenLeccion OrigenLeccion { get; set; } = OrigenLeccion.Oficial;

    public EstadoLeccion EstadoLeccion { get; set; } = EstadoLeccion.Borrador;

    public string TituloLeccion { get; set; } = string.Empty;

    public string DescripcionLeccion { get; set; } = string.Empty;

    public List<BloqueLeccionUpsertDto> Bloques { get; set; } = [];
}
