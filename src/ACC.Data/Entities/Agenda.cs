using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Representa la agenda de el usuario, con sus tareas 
    public class Agenda
    {
        [Key]
        public int Id_Agenda { get; set; }

        [Required]
        public string? IdUsuario { get; set; }

        [JsonIgnore]
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        [JsonIgnore]
        public ICollection<TareaAsignada>? TareasAsignadas { get; set; }

        [JsonIgnore]
        public ICollection<TareaPersonal>? TareasPersonales { get; set; }
    }
}
