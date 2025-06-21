using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Relación N:M entre Modulo y Tag
    public class ModuloTags
    {
        [Required]
        public int Id_Modulo { get; set; }

        [ForeignKey("Id_Modulo")]
        public Modulo Modulo { get; set; }

        [Required]
        public int Id_Tag { get; set; }

        [ForeignKey("Id_Tag")]
        public Tag Tag { get; set; }
    }
}
