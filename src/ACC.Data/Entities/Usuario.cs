using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

namespace ACC.Data.Entities
{
    public class Usuario
    {
        // Propiedades básicas
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = null!;   // el mismo Id que AspNetUsers.Id

        public string Nombre { get; set; } // Nombre del usuario
        public string Email { get; set; } // Correo electrónico
        public decimal ProgresoGeneral { get; set; } = 0m; // Progreso general del usuario

        // Relaciones uno a muchos
        public ICollection<HistorialCalificaciones> HistorialCalificaciones { get; set; } = [];
        public ICollection<AulaEstudiante> AulasEstudiantes { get; set; } = [];
        public ICollection<Notificacion> Notificaciones { get; set; } = [];
        public ICollection<Auditoria> Auditorias { get; set; } = [];

        // Relaciones uno a uno
        public Agenda Agenda { get; set; } = new();// Relación 1:1 con Agenda

        // Relaciones uno a muchos específicas
        public ICollection<Aula> AulasDocente { get; set; } = []; // Aulas donde el usuario es docente

        /// intentos en examenes:
        public ICollection<ExamenIntento> Intentos { get; set; } = []; 
    }
}
