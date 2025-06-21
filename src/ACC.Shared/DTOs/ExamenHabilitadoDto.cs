using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class ExamenHabilitadoDto
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int Id_SubModulo { get; set; }
        public bool Habilitado { get; set; }
        public DateTime? FechaHabilitacion { get; set; }
    }
}
