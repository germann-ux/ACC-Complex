using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace ACC.Data.Entities
{
    public class Usuario
    {
        // Propiedades básicas
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Identificador único
        public string Nombre { get; set; } // Nombre del usuario
        public string Email { get; set; } // Correo electrónico
        public decimal ProgresoGeneral { get; set; } // Progreso general del usuario

        // Relaciones uno a muchos
        public ICollection<HistorialCalificaciones> HistorialCalificaciones { get; set; } = new List<HistorialCalificaciones>();
        public ICollection<AulaEstudiante> AulasEstudiantes { get; set; } = new List<AulaEstudiante>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public ICollection<Auditoria> Auditorias { get; set; } = new List<Auditoria>();

        // Relaciones uno a uno
        public Agenda Agenda { get; set; } // Relación 1:1 con Agenda

        // Relaciones uno a muchos específicas
        public ICollection<Aula> AulasDocente { get; set; } = new List<Aula>(); // Aulas donde el usuario es docente
    }
}
