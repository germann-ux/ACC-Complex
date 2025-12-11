using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs;
/// <summary>
/// Visualización para que el alumno actualice el estado y la fecha de entrega de una tarea asignada.
/// </summary>
public class TareaAsignacionUpdateDto
{
    public TareaEstado Estado { get; set; }
    public DateTime? FechaEntrega { get; set; }
}
