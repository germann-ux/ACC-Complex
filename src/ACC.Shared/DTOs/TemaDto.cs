using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class TemaDto
    {
        public int Id_Tema { get; set; }
        public string NombreTema { get; set; }
        public string DescripcionTema { get; set; }
        public DateTime? UltimaVisita { get; set; }
        public int Id_SubModulo { get; set; }
    }
}
