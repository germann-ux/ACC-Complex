using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class CapituloTags
    {
        [Required]
        public int Id_Capitulo { get; set; }

        [ForeignKey("Id_Capitulo")]
        public Capitulo? Capitulo { get; set; }

        [Required]
        public int Id_Tag { get; set; }

        [ForeignKey("Id_Tag")]
        public Tag? Tag { get; set; }
    }
}
