using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class EvaluacionResultado
{
    public int Id { get; set; }
    public int EvaluacionId { get; set; }
    public string UsuarioId { get; set; } = null!;
    public double Calificacion { get; set; } // 0..100 (o tu escala)
    public DateTime FechaCalificacion { get; set; } // UTC

    public Evaluacion Evaluacion { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;
}
