namespace ACC.Shared.DTOs;

public class TareasPendientesResumenDto
{
    public int Vencidas { get; set; }
    public int ParaHoy { get; set; }
    public int Proximas { get; set; }

    public int TotalPendientes => Vencidas + ParaHoy + Proximas;
}
