using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class Anuncio
{
    public int AnuncioId { get; set; }
    public int AulaId { get; set; }
    public string Titulo { get; set; } = null!;
    public string Cuerpo { get; set; } = null!;
    public DateTime Fecha { get; set; } // visible
    public string DocenteId { get; set; } = null!; // legacy tenía DocenteId requerido
    public Aula Aula { get; set; } = null!;
}
