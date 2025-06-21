using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class TareaPersonalDto
    {
        public int TareaPersonalId { get; set; }
        public string? TareaPersonalTitulo { get; set; }
        public string? TareaPersonalDescripcion { get; set; }
        public bool? Completada { get; set; }
        public DateTime? TareaPersonalFechaLimite { get; set; }
        public string? IdUsuario { get; set; }
        public int? IdAgenda { get; set; }
    }
}
