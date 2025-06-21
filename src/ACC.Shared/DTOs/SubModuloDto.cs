using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class SubModuloDto
    {
        public int Id_SubModulo { get; set; }
        public string NombreSubModulo { get; set; }
        public string DescripcionSubModulo { get; set; }
        public int Id_Modulo { get; set; }
    }
}
