using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class TareaPersonal // entidad que sirve para que los alumnos puedan ponerse tareas personales a cumplir
    {
        [Key]
        public int TareaPersonalId { get; set; } // id de la tarea personal

        [Required]
        [MaxLength(100)]
        public string? TareaPersonalTitulo { get; set; } // titulo de la tarea personal

        [Required]
        [MaxLength(500)]
        public string? TareaPersonalDescripcion { get; set; } // descripcion de la tarea personal

        [AllowNull]
        public bool? Completada { get; set; } // para confirmar el estado de la tarea

        [AllowNull]
        public DateTime? TareaPersonalFechaLimite { get; set; } // fecha limite para completar la tarea, si no se completa se marca como incompleta

        [Required]
        public string? IdUsuario { get; set; } // id del usuario que creo la tarea personal

        // Relación 1:N con Agenda
        [ForeignKey("IdAgenda")]
        public Agenda? Agenda { get; set; } // relacion con la agenda, para que se pueda ver en la agenda de el usuario

        public int? IdAgenda { get; set; } // id de la agenda, para que se pueda ver en la agenda de el usuario
    }
}
