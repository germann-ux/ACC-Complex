using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;

namespace ACC.Data.Entities
{
    public class Modulo : INodoJerarquico
    {
        [Key]
        public int Id_Modulo { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreModulo { get; set; }

        [Required]
        [MaxLength(500)]
        public string DescripcionModulo { get; set; }

        public ICollection<Capitulo> Capitulos { get; set; }
        public ICollection<SubModulo> SubModulos { get; set; }
        public ICollection<ModuloTags> ModuloTags { get; set; }
        public ICollection<UsuarioModulos> UsuarioModulos { get; set; }
        public ICollection<Aula> Aulas { get; set; }

        // Implementación explícita de INodoJerarquico
        int INodoJerarquico.Id => Id_Modulo;
        string INodoJerarquico.Nombre => NombreModulo;
        string INodoJerarquico.Descripcion => DescripcionModulo;
        int? INodoJerarquico.IdPadre => null; // Módulo no tiene padre
        TipoNodoJerarquico INodoJerarquico.Tipo => TipoNodoJerarquico.Modulo;
    }

}
