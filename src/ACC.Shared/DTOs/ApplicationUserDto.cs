using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class ApplicationUserDto
    {
        public string Id { get; set; } = string.Empty; // clave primaria 
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal ProgresoGeneral { get; set; } = 0; // progreso general del usuario
        public int ModulosCompletados { get; set; } = 0; // modulos completados por el usuario
    }
}
