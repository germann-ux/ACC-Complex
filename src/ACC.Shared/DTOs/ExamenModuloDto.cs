using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class ExamenModuloDto
    {
        /// Propiedades del examen:
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public int NumeroPreguntas { get; set; }
        public int PuntajeAprobacion { get; set; }
        public string ContenidoHtml { get; set; } = default!;

        /// Relación N:1 con Módulo
        public int ModuloId { get; set; }
        public ModuloDto Modulo { get; set; } = null!;
        /// Relación 1:N con ExamenIntento
        public List<ExamenIntentoDto> Intentos { get; set; } = [];
    }
}
