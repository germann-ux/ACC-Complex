using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class Evaluacion
{
    public int Id { get; set; }
    public int AulaId { get; set; }
    public string Titulo { get; set; } = null!;
    public DateTime Fecha { get; set; } // UTC programada
    public DateTime CreatedAt { get; set; } // UTC

    public Aula Aula { get; set; } = null!;
    public ICollection<EvaluacionResultado> Resultados { get; set; } = new List<EvaluacionResultado>();
}
