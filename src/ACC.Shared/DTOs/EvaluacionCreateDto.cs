using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

public class EvaluacionCreateDto
{
    public string Titulo { get; set; } = null!;
    public DateTime Fecha { get; set; }
}
