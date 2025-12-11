using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    /// <summary>
    /// CapituloDto ---> Representa un capítulo dentro de la estructura de contenidos.
    /// </summary>
    public class CapituloDto
    {
        /// <summary>
        /// Id del capítulo.
        /// </summary>
        public int IdCapitulo { get; set; }

        /// <summary>
        /// Nombre o título del capítulo.
        /// </summary>
        public string NombreCapitulo { get; set; }

        public string HtmlBody { get; set; } = string.Empty;

        /// <summary>
        /// Descripción general del capítulo.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Id del módulo al que pertenece el capítulo.
        /// </summary>
        public int ModuloId { get; set; }

        /// <summary>
        /// Id del submódulo al que pertenece el capítulo.
        /// </summary>
        public int SubmoduloId { get; set; }

        /// <summary>
        /// Id del tema al que pertenece el capítulo.
        /// </summary>
        public int TemaId { get; set; }

        /// <summary>
        /// Id de la lección asociada, si aplica.
        /// </summary>
        public int? LeccionId { get; set; }
    }
}