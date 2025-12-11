using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class AnuncioCreateDto
    {
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
    }
}
