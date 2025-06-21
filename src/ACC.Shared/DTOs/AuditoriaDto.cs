using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class AuditoriaDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string TipoAccion { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaAccion { get; set; }
    }
}
