using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class Tema : INodoJerarquico
    {
        [Key]
        public int Id_Tema { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreTema { get; set; }

        [MaxLength(500)]
        public string DescripcionTema { get; set; }

        public DateTime? UltimaVisita { get; set; } // Última visita al tema

        public ICollection<Capitulo> Capitulos { get; set; } = new List<Capitulo>();

        // Relación con su SubModulo padre
        [Required]
        public int Id_SubModulo { get; set; }

        [ForeignKey("Id_SubModulo")]
        public SubModulo SubModulo { get; set; }

        // Relación 1:N con SubTemas
        public ICollection<SubTema> SubTemas { get; set; } = new List<SubTema>();

        // Relación N:M con Tags
        public ICollection<TemaTags> TemaTags { get; set; } = new List<TemaTags>();

        // Relación N:M con ApplicationUser
        public ICollection<UsuarioTemas> UsuarioTemas { get; set; } = new List<UsuarioTemas>();

        /*--------------------- implementacion de la interfaz ---------------------*/
        int INodoJerarquico.Id => Id_Tema; // su Id del tema

        string INodoJerarquico.Nombre => NombreTema; // el nombre del tema

        string INodoJerarquico.Descripcion => DescripcionTema; // la descripcion del tema

        int? INodoJerarquico.IdPadre => Id_SubModulo; // El Id del SubModulo padre

        TipoNodoJerarquico INodoJerarquico.Tipo => TipoNodoJerarquico.Tema; // el tipo de nodo, que es tema
    }
}

