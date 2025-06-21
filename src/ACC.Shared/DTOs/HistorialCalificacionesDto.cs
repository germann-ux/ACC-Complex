using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class HistorialCalificacionesDto
    {
        public int Id_Historial { get; set; }
        public string Id_Usuario { get; set; }
        public int? Id_Modulo { get; set; }
        public int? Id_SubModulo { get; set; }
        public decimal Calificacion { get; set; }
        public DateTime FechaCalificacion { get; set; }
    }
}
