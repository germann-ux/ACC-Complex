using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

public class AnuncioDto
{
    public int AnuncioId { get; set; }
    public int AulaId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Contenido { get; set; } = null!;
    public DateTime CreatedAt { get; set; } // UTC
    public string DocenteId { get; set; } = null!; // legacy tenía DocenteId requerido
    public AulaDto Aula { get; set; } = null!;
}
