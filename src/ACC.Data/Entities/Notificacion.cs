using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(500)]
        public string Mensaje { get; set; }

        [Required]
        public DateTime FechaEnvio { get; set; }

        public bool Leido { get; set; } = false;

        // Relación con Aula
        public int? AulaId { get; set; }

        [ForeignKey("AulaId")]
        public Aula Aula { get; set; }

        // Relación con usuario
        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}

