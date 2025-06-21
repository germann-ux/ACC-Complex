using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class UsuarioModulosDto
    {
        public string Id_Usuario { get; set; }
        public int Id_Modulo { get; set; }
        public bool EsCompletado { get; set; }
        public decimal? Calificacion { get; set; }
        public int Progreso { get; set; }
    }
}
