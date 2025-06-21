using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
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
    public class SubTema : INodoJerarquico
    {
    
        [Key]
        public int Id_SubTema { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreSubTema { get; set; }

        [Required]
        [MaxLength(500)]
        public string DescripcionSubTema { get; set; }

        // Relación con su Tema padre
        [Required]
        public int Id_Tema { get; set; }

        [ForeignKey("Id_Tema")]
        public Tema Tema { get; set; }

        // Relación 1:N con sus lecciones hijas
        public ICollection<Leccion> Lecciones { get; set; } = new List<Leccion>();

        // Relación N:M con ApplicationUser
        public ICollection<UsuarioSubTemas> UsuarioSubTemas { get; set; } = new List<UsuarioSubTemas>();

        /*--------------------- implementacion de la interfaz INodoJerarquico ---------------------*/

        int INodoJerarquico.Id => Id_SubTema; // su Id del subtema

        string INodoJerarquico.Nombre => NombreSubTema; // el nombre del subtema

        string INodoJerarquico.Descripcion => DescripcionSubTema; // la descripcion del subtema

        int? INodoJerarquico.IdPadre => Id_Tema; // El Id del Tema padre

        TipoNodoJerarquico INodoJerarquico.Tipo => TipoNodoJerarquico.SubTema; // el tipo de nodo, que es subtema
    }
}
