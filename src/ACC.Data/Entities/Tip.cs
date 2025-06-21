using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class Tip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Contenido { get; set; }
    }
}
