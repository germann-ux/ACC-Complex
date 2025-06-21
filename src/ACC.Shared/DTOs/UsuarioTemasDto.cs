using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class UsuarioTemasDto
    {
        public string Id_Usuario { get; set; }
        public int Id_Tema { get; set; }
        public bool EsFavorito { get; set; }
        public DateTime? UltimaVisita { get; set; }
        public int Progreso { get; set; }
    }
}
