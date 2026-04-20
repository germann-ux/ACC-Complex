using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    /// <summary>
    /// DTO que representa un intento de examen realizado por un usuario
    /// </summary>
    public class ExamenIntentoDto
    {
        public int Id { get; set; }
        /// relaciones:
        public string IdUsuario { get; set; } = default!;
        public UsuarioDto? Usuario { get; set; }

        public int? ExamenSubModuloId { get; set; }
        public ExamenSubModuloDto? ExamenSubModulo { get; set; }

        public int? ExamenModuloId { get; set; }
        public ExamenModuloDto? ExamenModulo { get; set; }

        public int? ExamenId { get; set; }
        public ExamenDto? Examen { get; set; }

        /// Datos del intento:
        public bool Aprobado { get; set; }
        public DateTimeOffset FechaIntento { get; set; }
        public double Calificacion { get; set; }
        public int NumeroAciertos { get; set; }
        public int TotalPreguntas { get; set; }
        public int? TiempoSegundos { get; set; }
        public double PorcentajeObtenido { get; set; }
        public int NumeroIntento { get; set; }
    }
}
