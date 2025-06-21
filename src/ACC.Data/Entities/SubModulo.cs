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
    public class SubModulo : INodoJerarquico
    {
        [Key]
        public int Id_SubModulo { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreSubModulo { get; set; }

        [Required]
        [MaxLength(500)]
        public string DescripcionSubModulo { get; set; }

        // Relación con su Modulo padre
        [Required]
        public int Id_Modulo { get; set; }

        // objeto de su padre modulo
        [ForeignKey("Id_Modulo")]
        public Modulo Modulo { get; set; }

        // Relación 1:N con Temas
        public ICollection<Tema> Temas { get; set; } = new List<Tema>();

        // Relación 1:N con Capitulos
        public ICollection<Capitulo> Capitulos { get; set; } = new List<Capitulo>();

        // Relación N:M con ApplicationUser
        public ICollection<UsuarioSubModulos> UsuarioSubModulos { get; set; } = new List<UsuarioSubModulos>();

        /*--------------------- implementacion de la interfaz ---------------------*/
        int INodoJerarquico.Id => Id_SubModulo; // su Id del submodulo
        
        string INodoJerarquico.Nombre => NombreSubModulo; // el nombre del submodulo

        string INodoJerarquico.Descripcion => DescripcionSubModulo; // la descripcion del submodulo

        int? INodoJerarquico.IdPadre => Id_Modulo; // El Id del Modulo padre

        TipoNodoJerarquico INodoJerarquico.Tipo => TipoNodoJerarquico.SubModulo; // el tipo de nodo, que es submodulo
    }
}
