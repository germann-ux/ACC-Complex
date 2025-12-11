using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class AulaEstudiante
{
    public int AulaEstudianteId { get; set; }
    public int AulaId { get; set; }
    public string UsuarioId { get; set; } = null!;
    public DateTime FechaInscripcion { get; set; } // UTC

    public Aula Aula { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;
}
