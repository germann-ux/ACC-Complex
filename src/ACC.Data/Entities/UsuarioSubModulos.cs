using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class UsuarioSubModulos
    {
        [Required]
        public string Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public int Id_SubModulo { get; set; }

        [ForeignKey("Id_SubModulo")]
        public SubModulo SubModulo { get; set; }

        public bool EsCompletado { get; set; }

        [Range(0.00, 10.00, ErrorMessage = "no se puede tener una calificacion mayor a 10.00")]
        public decimal? Calificacion { get; set; } // para la calificacion del submodulo / examen de ese submodulo

        [Range(0, 100, ErrorMessage = "no se puede tener un progreso mayor al 100%")]
        public int Progreso { get; set; }
    }
}
