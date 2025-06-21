using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class AulaEstudiante
    {
        [Required]
        public int AulaId { get; set; } // Relación con Aula

        [Required]
        public string UsuarioId { get; set; } // Relación con Usuario

        // Relaciones
        [ForeignKey("AulaId")]
        public Aula Aula { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}
