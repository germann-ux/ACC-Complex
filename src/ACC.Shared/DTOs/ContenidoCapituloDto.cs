using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class ContenidoCapituloDto
    {
        public int IdContenido { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Subtitulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public string? Dificultad { get; set; }
        public string? Tags { get; set; }
        public string IconoBadge { get; set; } = string.Empty;
        public string EtiquetaNivel { get; set; } = string.Empty;
        public string HtmlBody { get; set; } = string.Empty;
        public DateTime FechaActualizacion { get; set; }
    }
}
