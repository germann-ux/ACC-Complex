namespace ACC.Shared.DTOs;

public class GuiaResumenDto
{
    public int TotalModulos { get; set; }
    public int TotalLecciones { get; set; }
    public int TotalEvaluaciones { get; set; }
    public int TotalPracticas { get; set; }
    public int TotalSubTemas { get; set; }
    public int SubTemasCompletados { get; set; }
    public int ProgresoPorcentaje { get; set; }
    public List<GuiaModuloResumenDto> Modulos { get; set; } = [];
}

public class GuiaModuloResumenDto
{
    public int ModuloId { get; set; }
    public string NombreModulo { get; set; } = string.Empty;
    public string DescripcionModulo { get; set; } = string.Empty;
    public int Orden { get; set; }
    public int SubModulosCount { get; set; }
    public int TemasCount { get; set; }
    public int SubTemasCount { get; set; }
    public int SubTemasCompletados { get; set; }
    public int LeccionesCount { get; set; }
    public int EvaluacionesCount { get; set; }
    public int PracticasCount { get; set; }
    public int ProgresoPorcentaje { get; set; }
}
