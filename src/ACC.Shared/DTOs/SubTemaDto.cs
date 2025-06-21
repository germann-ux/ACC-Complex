using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class SubTemaDto
    {
        public int Id_SubTema { get; set; }
        public string NombreSubTema { get; set; }
        public string DescripcionSubTema { get; set; }
        public int Id_Tema { get; set; }
    }
}
