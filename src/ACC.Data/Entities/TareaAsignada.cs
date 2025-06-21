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
    public class TareaAsignada // entidad que sirve para las tareas en las aulas que se le asignen a los alumnos en las aulas
    {
        [Key] // pk de la tarea asignada
        public int IdTareaAsignada { get; set; }

        [Required] // titulo de la tarea asignada
        [MaxLength(100)]
        public string? TituloTareaAsignada { get; set; }

        [Required] // descripcion de la tarea asignada
        [MaxLength(500)]
        public string? DescripcionTareaAsignada { get; set; }

        [AllowNull] // para confirmar el estado de la tarea
        public bool? Completada { get; set; } = false;

        [AllowNull] // fecha limite para completar la tarea, si no se completa se marca como incompleta
        public DateTime? FechaLimiteTareaAsignada { get; set; }

        [Required]
        [ForeignKey("AulaId")]
        public int AulaId { get; set; } // fk de la aula a la que pertenece la tarea asignada

        [AllowNull]
        public Agenda? Agenda { get; set; } // relacion con la agenda, para que se pueda ver en la agenda de el usuario

        [AllowNull]
        public int? AgendaId { get; set; } // id de la agenda, para que se pueda ver en la agenda de el usuario
        
        [Required]
        public string? IdUsuario { get; set; } // id de el usuario al que se le asigna la tarea
    }
}
