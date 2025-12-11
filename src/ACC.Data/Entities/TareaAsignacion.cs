using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

public class TareaAsignacion
{
    public int Id { get; set; }
    public int TareaId { get; set; }
    public string UsuarioId { get; set; } = null!;
    
    public decimal? Calificacion { get; set; } // 9.5, 88.6, etc.
    public string? Retroalimentacion { get; set; } // feedback del profesor

    public DateTime? FechaEntrega { get; set; } // UTC

    public TareaEstado Estado { get; set; } = TareaEstado.NoIniciada; 
    public TareaEstadoEntrega EstadoEntrega { get; set; } = TareaEstadoEntrega.NoEntregada;  

    public Tarea Tarea { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;
}
