using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public sealed class NotaTiempoDto
    {
        public DateTime Fecha { get; set; }
        public decimal Nota { get; set; } // 0..100
        public string? Submodulo { get; set; }
    }
}
