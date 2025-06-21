using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    // Relación N:M entre Tema y Tag
    public class TemaTags
    {
        [Required]
        public int Id_Tema { get; set; }

        [ForeignKey("Id_Tema")]
        public Tema Tema { get; set; }

        [Required]
        public int Id_Tag { get; set; }

        [ForeignKey("Id_Tag")]
        public Tag Tag { get; set; }
    }
}
