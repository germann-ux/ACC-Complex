using ACC.Shared.Enums;

namespace ACC.Shared.DTOs;

public class ExamenEstadoDto
{
    public ExamenTipo Tipo { get; set; }
    public int ExamenId { get; set; }
    public bool EstaHabilitado { get; set; }
    public int IntentosRealizados { get; set; }
    public int IntentosMaximos { get; set; }
    public int IntentosRestantes { get; set; }
    public bool PuedePresentar { get; set; }
    public bool EstaAprobado { get; set; }
    public ExamenIntentoDto? UltimoIntento { get; set; }
}
