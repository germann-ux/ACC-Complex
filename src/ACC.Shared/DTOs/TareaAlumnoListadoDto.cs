using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

/// <summary>
/// Visualizacion de una tarea para el alumno en el listado de tareas. Ej:"Tus tareas de esta aula". 
/// </summary>
public class TareaAlumnoListadoDto
{
    public int TareaId { get; set; }
    public int TareaAsignacionId { get; set; }

    public string Titulo { get; set; } = null!;
    public string? EnunciadoCorto { get; set; } = null!;

    public DateTime FechaLimite { get; set; }
    public TareaEstado Estado { get; set; }
    public TareaEstadoEntrega EstadoEntrega { get; set; }
    public decimal? Calificacion { get; set; }
}