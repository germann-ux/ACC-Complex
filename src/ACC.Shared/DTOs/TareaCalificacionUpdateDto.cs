using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 
//TODO: LO DEJO PARA ACC V2, DEMASIADO CONTENIDO POR CUMPLIR.
/// <summary>
/// DTO lado docente para actualizar la calificación y retroalimentación de una tarea entregada por un estudiante.
/// </summary>
public class TareaCalificacionUpdateDto
{
    public decimal Calificacion { get; set; }
    public string Retroalimentacion { get; set; } = null!;
}
