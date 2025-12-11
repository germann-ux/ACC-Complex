using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

public class EstudianteListadoDto
{
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public int Progreso { get; set; }           // 0-100
    public string UltimaLeccion { get; set; } = string.Empty;
}
