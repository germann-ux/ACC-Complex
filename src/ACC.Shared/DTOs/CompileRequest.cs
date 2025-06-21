using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class CompileRequest
    {
        public string Code { get; set; } // codigo fuente
        public string Input { get; set; } // cualquier entrada del usuario...
    }
}
