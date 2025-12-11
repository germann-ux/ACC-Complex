using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    /// <summary>
    /// Entidad que representa un intento de examen realizado por un usuario
    /// </summary>
    public class ExamenIntento
    {
        public int Id { get; set; }
        /// relaciones:
        public string IdUsuario { get; set; } = default!;
        public Usuario Usuario { get; set; } = default!; 

        public int? ExamenSubModuloId { get; set; }
        public ExamenSubModulo? ExamenSubModulo { get; set; } = default!;

        public int? ExamenModuloId { get; set; }
        public ExamenModulo? ExamenModulo { get; set; } = default!; 

        public int? ExamenId { get; set; }
        public Examen Examen { get; set; } = default!;

        /// Datos del intento:
        public bool Aprobado { get; set; }
        public DateTimeOffset FechaIntento { get; set; }
        public double Calificacion { get; set; }
        public int NumeroIntento { get; set; }
    }
}
