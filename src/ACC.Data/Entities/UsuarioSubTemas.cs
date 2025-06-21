using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Relación N:M entre ApplicationUser y SubTema
    public class UsuarioSubTemas
    {
        [Required]
        public string Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public int Id_SubTema { get; set; }

        [ForeignKey("Id_SubTema")]
        public SubTema SubTema { get; set; }

        public bool EsFavorito { get; set; }

        public DateTime? UltimaVisita { get; set; }
    }
}
