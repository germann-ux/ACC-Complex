using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Representa un Tag para categorizar Modulos o Temas
    public class Tag
    {
        [Key]
        public int Id_Tag { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreTag { get; set; }

        [MaxLength(500)]
        public string DescripcionTag { get; set; }

        // Relación N:M con Modulo
        public ICollection<ModuloTags>? ModuloTags { get; set; }

        // Relación N:M con Tema
        public ICollection<TemaTags>? TemaTags { get; set; }

        // Relación N:M con Capitulo
        public ICollection<CapituloTags>? CapituloTags { get; set; }
    }
}
