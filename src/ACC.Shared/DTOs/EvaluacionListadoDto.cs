using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

public class EvaluacionListadoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public int TotalAlumnos { get; set; }       // recomendable setearlo desde servicio (total del aula)
    public double Promedio { get; set; }        // promedio de resultados (0 si no hay)
}
