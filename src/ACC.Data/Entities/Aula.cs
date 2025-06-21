using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class Aula
    {
        [Key]
        public int Id { get; set; } // Identificador único

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } // Nombre descriptivo del aula

        [MaxLength(500)]
        public string Descripcion { get; set; } // Descripción opcional

        [Required]
        public int ModuloId { get; set; } // Relación con el módulo

        [Required]
        public string DocenteId { get; set; } // Relación con el docente

        // Relaciones
        [ForeignKey("ModuloId")]
        public Modulo Modulo { get; set; } // Relación 1:N con Modulo

        [ForeignKey("DocenteId")]
        public  Usuario Docente { get; set; } // Relación con el docente (ApplicationUser)

        public ICollection<AulaEstudiante> AulaEstudiantes { get; set; } = new List<AulaEstudiante>(); // Relación N:M con estudiantes

        public ICollection<TareaAsignada> Tareas { get; set; } = new List<TareaAsignada>(); // Relación 1:N con Tareas

        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>(); // Relación 1:N con Notificaciones

        public ICollection<Aviso>? Avisos { get; set; } = new List<Aviso>(); // Relación 1:N con Avisos
    }
}
