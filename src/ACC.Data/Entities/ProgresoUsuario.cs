using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class ProgresoUsuario
    {
        [Key]
        public int IdProgreso { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [Required]
        public int SubTemaId { get; set; }

        [Required]
        public DateTimeOffset Fecha { get; set; }

        public bool Completado { get; set; } = false;

        // Relación con SubTema
        [ForeignKey("SubTemaId")]
        public SubTema? SubTema { get; set; }
    }
}
