using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class ExamenSubModuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public int NumeroPreguntas { get; set; }
        public int PuntajeAprobacion { get; set; }
        public string ContenidoHtml { get; set; } = string.Empty;

        // Relación N:1 con SubModulo
        public int SubModuloId { get; set; }
        public SubModuloDto SubModulo { get; set; } = null!;

        // Relación 1:N con ExamenIntento
        public List<ExamenIntentoDto> Intentos { get; set; } = [];
    }
}
