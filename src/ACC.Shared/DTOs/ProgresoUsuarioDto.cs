using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class ProgresoUsuarioDto
    {
        public int IdProgreso { get; set; }
        public string UsuarioId { get; set; }
        public int SubTemaId { get; set; }
        public DateTime Fecha { get; set; }
        public bool Completado { get; set; }
    }
}
