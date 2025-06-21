using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class ExamenHabilitado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; } // Relación con AspNetUsers

        [Required]
        [ForeignKey("SubModulo")]
        public int Id_SubModulo { get; set; } // Relación con SubModulos

        public bool Habilitado { get; set; } = false; // Indica si el examen está habilitado

        public DateTime? FechaHabilitacion { get; set; } // Fecha en que se habilitó el examen

        // Relaciones de navegación
        public virtual SubModulo SubModulo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
