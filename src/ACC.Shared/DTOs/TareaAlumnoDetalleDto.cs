using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class TareaAlumnoDetalleDto
    {
        public int TareaId { get; set; }
        public int TareaAsignacionId { get; set; }

        public string Titulo { get; set; } = null!;
        public string Enunciado { get; set; } = null!;
        public DateTime FechaLimite { get; set; }

        public TareaEstado Estado { get; set; }
        public TareaEstadoEntrega EstadoEntrega { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public Decimal? Calificacion { get; set; }
        public string? Retroalimentacion { get; set; }
    }
}
