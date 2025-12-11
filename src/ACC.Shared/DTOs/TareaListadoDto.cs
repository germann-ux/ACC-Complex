using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs;
/// <summary>
/// Visualización básica de una tarea en el listado de tareas de un aula (lado docente).
/// </summary>
public class TareaListadoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public DateTime FechaLimite { get; set; }
    public TareaScope Scope { get; set; }

    // Derivado (opcional): NoIniciada / EnProgreso / Completada
    // Calculado por servicio a partir de AulaEstudiante y TareaAsignacion;
    // si no lo calculas, puedes dejar null y la UI mostrar solo título+fecha.
    public TareaEstado? EstadoGlobal { get; set; }
}
