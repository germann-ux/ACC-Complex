namespace ACC.Shared.DTOs;

/// <summary>
/// DTO que representa la respuesta exitosa de redención de invitación.
/// </summary>
public class AulaInscripcionRedeemDto
{
    public int AulaId { get; set; }
    public string AulaNombre { get; set; } = null!;
    public string? AulaDescripcion { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public string Mensaje { get; set; } = null!;
}
