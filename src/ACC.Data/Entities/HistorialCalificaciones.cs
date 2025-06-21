using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Historial de calificaciones
    public class HistorialCalificaciones
    {
        [Key]
        public int Id_Historial { get; set; }

        [Required]
        public string Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; }

        public int? Id_Modulo { get; set; }

        [ForeignKey("Id_Modulo")]
        public Modulo Modulo { get; set; }

        public int? Id_SubModulo { get; set; }

        [ForeignKey("Id_SubModulo")]
        public SubModulo SubModulo { get; set; }

        [Range(0.00, 10.00)]
        public decimal Calificacion { get; set; }

        [Required]
        public DateTime FechaCalificacion { get; set; }
    }
}
