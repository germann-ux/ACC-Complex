using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class UsuarioModulos
    {
        [Required]
        public string Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public int Id_Modulo { get; set; }

        [ForeignKey("Id_Modulo")]
        public Modulo Modulo { get; set; }

        public bool EsCompletado { get; set; } // Indica si completó el módulo

        [Range(0.00, 10.00, ErrorMessage = "no se puede tener una calificacion mayor a 10.00")]
        public decimal? Calificacion { get; set; } // Calificación obtenida

        [Range(0, 100, ErrorMessage = "no se puede tener un progreso mayor al 100%")]
        public int Progreso { get; set; } // Progreso en el módulo
    }
}
