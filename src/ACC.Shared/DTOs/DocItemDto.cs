using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class DocItemDto
    {
        public string Tipo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Subtitulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public string FechaActualizacion { get; set; } = string.Empty;
        public string Dificultad { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public string IconoBadge { get; set; } = "fas fa-book";
        public string EtiquetaNivel { get; set; } = "Básico";
    }
}
