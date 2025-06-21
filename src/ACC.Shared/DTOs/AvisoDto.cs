using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class AvisoDto
    {
        public int IdAviso { get; set; }
        public string TituloAviso { get; set; }
        public string ContenidoAviso { get; set; }
        public DateTime FechaAviso { get; set; }
        public string DocenteId { get; set; }
        public int AulaId { get; set; }
    }
}
