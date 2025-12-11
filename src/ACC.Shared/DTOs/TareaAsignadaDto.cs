using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    [Obsolete("Esta clase está obsoleta.")]
    public class TareaAsignadaDto
    {
        public int IdTareaAsignada { get; set; }
        public string IdUsuario { get; set; } = string.Empty; // id de el usuario al que se le asigna la tarea
        public string? TituloTareaAsignada { get; set; }
        public string? DescripcionTareaAsignada { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaLimiteTareaAsignada { get; set; } = DateTime.UtcNow;
        public int AulaId { get; set; }
        public int? AgendaId { get; set; }
    }
}
