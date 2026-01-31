using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class Tarea
{
    public int TareaId { get; set; }
    public int AulaId { get; set; }
    
    public string Titulo { get; set; } = null!;
    public string Enunciado { get; set; } = null!;   

    public DateTime CreatedAt { get; set; } // UTC
    public DateTime FechaLimite { get; set; } // UTC

    public TareaScope Scope { get; set; } = TareaScope.AulaCompleta;
    public Aula Aula { get; set; } = null!;
    public ICollection<TareasAsignaciones> Asignaciones { get; set; } = [];
}
