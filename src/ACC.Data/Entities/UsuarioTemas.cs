using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Relación N:M entre ApplicationUser y Tema
    public class UsuarioTemas
    {
        [Required]
        public string Id_Usuario { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public int Id_Tema { get; set; }

        [ForeignKey("Id_Tema")]
        public Tema Tema { get; set; }

        public bool EsFavorito { get; set; }

        public DateTime? UltimaVisita { get; set; }

        [Range(0, 100, ErrorMessage = "no se puede tener un progreso mayor al 100%")]
        public int Progreso { get; set; }
    }
}

