using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    public class ExamenHabilitado
    {
        public int Id { get; set; }

        // Clave de usuario
        public string UsuarioId { get; set; } = default!;

        // Referencia polimórfica al examen
        public ExamenTipo Tipo { get; set; }
        public int RefId { get; set; } // Id del examen según el tipo

        // Estado
        public bool Habilitado { get; set; }
        public DateTimeOffset FechaHabilitacion { get; set; }

        // Metadata opcional (útil para auditoría)
        public string? ReglaFuente { get; set; }
    }
}
