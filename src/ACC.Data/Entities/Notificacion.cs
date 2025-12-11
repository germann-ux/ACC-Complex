using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class Notificacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public DateTime FechaEnvio { get; set; } // UTC
        public bool Leido { get; set; } = false;

        public int? AulaId { get; set; }
        public string UsuarioId { get; set; } = null!;

        public Aula? Aula { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}

