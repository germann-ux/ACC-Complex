using Microsoft.EntityFrameworkCore;
using ACC.Data.Entities;

namespace ACC.Data
{
    /// <summary>
    /// DbContext principal para la base de datos académica de ACC.
    /// Gestiona módulos, lecciones, progreso, aulas, exámenes y tareas.
    /// </summary>
    /// <param name="options">Opciones de configuración de EF Core.</param>
    public class ACCDbContext(DbContextOptions<ACCDbContext> options) : DbContext(options)
    {
        // ============================================================
        // DbSets: Catálogo / Usuarios
        // ============================================================

        /// <summary>Usuarios registrados en ACC (no Identity, sino datos académicos).</summary>
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        /// <summary>Contenido HTML/JSON de capítulos.</summary>
        public DbSet<ContenidoCapitulo> ContenidoCapitulos => Set<ContenidoCapitulo>();

        /// <summary>Módulos principales del curso.</summary>
        public DbSet<Modulo> Modulos => Set<Modulo>();

        /// <summary>Submódulos asociados a un módulo.</summary>
        public DbSet<SubModulo> SubModulos => Set<SubModulo>();

        /// <summary>Temas de un submódulo.</summary>
        public DbSet<Tema> Temas => Set<Tema>();

        /// <summary>Subtemas de un tema.</summary>
        public DbSet<SubTema> SubTemas => Set<SubTema>();

        /// <summary>Capítulos teóricos/prácticos de un tema o submódulo.</summary>
        public DbSet<Capitulo> Capitulos => Set<Capitulo>();

        /// <summary>Lecciones atómicas dentro de un subtema.</summary>
        public DbSet<Leccion> Lecciones => Set<Leccion>();

        /// <summary>Tags reutilizables para etiquetar módulos, temas, capítulos, etc.</summary>
        public DbSet<Tag> Tags => Set<Tag>();

        public DbSet<ModuloTags> ModuloTags => Set<ModuloTags>();
        public DbSet<TemaTags> TemaTags => Set<TemaTags>();
        public DbSet<CapituloTags> CapituloTags => Set<CapituloTags>();

        // ============================================================
        // DbSets: Progreso y relación Usuario–Contenido
        // ============================================================

        public DbSet<UsuarioModulos> UsuarioModulos => Set<UsuarioModulos>();
        public DbSet<UsuarioSubModulos> UsuarioSubModulos => Set<UsuarioSubModulos>();
        public DbSet<UsuarioTemas> UsuarioTemas => Set<UsuarioTemas>();
        public DbSet<UsuarioSubTemas> UsuarioSubTemas => Set<UsuarioSubTemas>();
        public DbSet<HistorialCalificaciones> HistorialCalificaciones => Set<HistorialCalificaciones>();
        public DbSet<ProgresoUsuario> ProgresoUsuarios => Set<ProgresoUsuario>();

        // ============================================================
        // DbSets: Aulas, Agenda y comunicación
        // ============================================================

        public DbSet<Aula> Aulas => Set<Aula>();
        public DbSet<AulaEstudiante> AulaEstudiantes => Set<AulaEstudiante>();
        public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
        public DbSet<Auditoria> Auditorias => Set<Auditoria>();

        public DbSet<Agenda> Agendas => Set<Agenda>();

        [Obsolete("Esta propiedad está obsoleta. Use TareaAsignaciones y TareasPersonales en su lugar.")]
        public DbSet<TareaAsignada> TareasAsignadas => Set<TareaAsignada>();

        public DbSet<TareaPersonal> TareasPersonales => Set<TareaPersonal>();

        public DbSet<Aviso> Avisos => Set<Aviso>();
        public DbSet<Tip> Tips => Set<Tip>();

        public DbSet<InvitacionAula> InvitacionesAula => Set<InvitacionAula>();
        public DbSet<Anuncio> Anuncios => Set<Anuncio>();

        public DbSet<Evaluacion> Evaluaciones => Set<Evaluacion>();
        public DbSet<EvaluacionResultado> EvaluacionResultados => Set<EvaluacionResultado>();
        public DbSet<TareaAsignacion> TareaAsignaciones => Set<TareaAsignacion>();
        public DbSet<Tarea> Tareas => Set<Tarea>();

        // ============================================================
        // DbSets: Exámenes y evaluaciones
        // ============================================================

        public DbSet<Examen> Examenes => Set<Examen>();
        public DbSet<ExamenModulo> ExamenesModulos => Set<ExamenModulo>();
        public DbSet<ExamenSubModulo> ExamenesSubModulo => Set<ExamenSubModulo>();
        public DbSet<ExamenIntento> ExamenesIntentos => Set<ExamenIntento>();
        public DbSet<ExamenHabilitado> ExamenesHabilitados => Set<ExamenHabilitado>();
        public DbSet<ExamenAprobatorio> ExamenesAprobatorios => Set<ExamenAprobatorio>();

        // ============================================================
        // Configuración del modelo
        // ============================================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------------------------------------------
            // Esquema por defecto
            // --------------------------------------------------------
            modelBuilder.HasDefaultSchema("acc_academic");

            // --------------------------------------------------------
            // Claves primarias básicas
            // --------------------------------------------------------
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Tema>().HasKey(t => t.Id_Tema);
            modelBuilder.Entity<Modulo>().HasKey(m => m.Id_Modulo);
            modelBuilder.Entity<SubModulo>().HasKey(sm => sm.Id_SubModulo);
            modelBuilder.Entity<Tag>().HasKey(t => t.Id_Tag);

            modelBuilder.Entity<UsuarioModulos>().HasKey(um => new { um.Id_Usuario, um.Id_Modulo });
            modelBuilder.Entity<UsuarioSubModulos>().HasKey(usm => new { usm.Id_Usuario, usm.Id_SubModulo });
            modelBuilder.Entity<UsuarioTemas>().HasKey(ut => new { ut.Id_Usuario, ut.Id_Tema });
            modelBuilder.Entity<UsuarioSubTemas>().HasKey(ust => new { ust.Id_Usuario, ust.Id_SubTema });

            modelBuilder.Entity<Leccion>().HasKey(l => l.IdLeccion);
            modelBuilder.Entity<Capitulo>().HasKey(c => c.IdCapitulo);

            modelBuilder.Entity<ProgresoUsuario>().HasKey(p => p.IdProgreso);
            modelBuilder.Entity<HistorialCalificaciones>().HasKey(h => h.Id_Historial);

            modelBuilder.Entity<ExamenIntento>().HasKey(ei => ei.Id);
            modelBuilder.Entity<ExamenModulo>().HasKey(em => em.Id);
            modelBuilder.Entity<ExamenSubModulo>().HasKey(es => es.Id);
            modelBuilder.Entity<Examen>().HasKey(e => e.Id);
            modelBuilder.Entity<ExamenHabilitado>().HasKey(e => e.Id);

            modelBuilder.Entity<Aula>().HasKey(x => x.AulaId);
            modelBuilder.Entity<AulaEstudiante>().HasKey(x => x.AulaEstudianteId);
            modelBuilder.Entity<Notificacion>().HasKey(x => x.Id);
            modelBuilder.Entity<Auditoria>().HasKey(a => a.Id);

            modelBuilder.Entity<Agenda>().HasKey(a => a.Id_Agenda);
            modelBuilder.Entity<Aviso>().HasKey(a => a.IdAviso);

            modelBuilder.Entity<InvitacionAula>().HasKey(a => a.Id);
            modelBuilder.Entity<Evaluacion>().HasKey(x => x.Id);
            modelBuilder.Entity<EvaluacionResultado>().HasKey(a => a.Id);
            modelBuilder.Entity<TareaAsignacion>().HasKey(x => x.Id);
            modelBuilder.Entity<Tarea>().HasKey(x => x.TareaId);
            modelBuilder.Entity<Anuncio>().HasKey(x => x.AnuncioId);

            // --------------------------------------------------------
            // Tipos numéricos y fechas
            // --------------------------------------------------------

            modelBuilder.Entity<Usuario>()
                .Property(u => u.ProgresoGeneral)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<UsuarioModulos>()
                .Property(um => um.Calificacion)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<UsuarioSubModulos>()
                .Property(usm => usm.Calificacion)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<HistorialCalificaciones>()
                .Property(hc => hc.Calificacion)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<ProgresoUsuario>()
                .Property(p => p.Fecha)
                .HasColumnType("datetimeoffset");

            modelBuilder.Entity<ExamenIntento>()
                .Property(e => e.FechaIntento)
                .HasColumnType("datetimeoffset");

            modelBuilder.Entity<ExamenHabilitado>()
                .Property(eh => eh.FechaHabilitacion)
                .HasColumnType("datetimeoffset");

            // --------------------------------------------------------
            // Jerarquía de contenido: Módulo → SubMódulo → Tema → SubTema → Lección / Capítulo
            // --------------------------------------------------------

            modelBuilder.Entity<Modulo>()
                .HasMany(m => m.SubModulos)
                .WithOne(sm => sm.Modulo)
                .HasForeignKey(sm => sm.Id_Modulo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubModulo>()
                .HasMany(sm => sm.Temas)
                .WithOne(t => t.SubModulo)
                .HasForeignKey(t => t.Id_SubModulo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tema>()
                .HasMany(t => t.SubTemas)
                .WithOne(st => st.Tema)
                .HasForeignKey(st => st.Id_Tema)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tema>()
                .HasMany(t => t.Capitulos)
                .WithOne(c => c.Tema)
                .HasForeignKey(c => c.TemaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubModulo>()
                .HasMany(sm => sm.Capitulos)
                .WithOne(c => c.SubModulo)
                .HasForeignKey(c => c.SubmoduloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Modulo>()
                .HasMany(m => m.Capitulos)
                .WithOne(c => c.Modulo)
                .HasForeignKey(c => c.ModuloId)
                .OnDelete(DeleteBehavior.Restrict);

            // SubTema -> Lecciones (1:n)
            modelBuilder.Entity<Leccion>()
                .HasOne(l => l.SubTema)
                .WithMany(st => st.Lecciones)
                .HasForeignKey(l => l.SubtemaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Capítulo -> Lección (opcional, 1:n desde Lección)
            modelBuilder.Entity<Capitulo>()
                .HasOne(c => c.Leccion)
                .WithMany(l => l.Capitulos)
                .HasForeignKey(c => c.LeccionId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------------------------------------
            // Many-to-Many: Tags
            // --------------------------------------------------------

            modelBuilder.Entity<ModuloTags>(e =>
            {
                e.HasKey(mt => new { mt.Id_Modulo, mt.Id_Tag });
                e.HasIndex(mt => new { mt.Id_Modulo, mt.Id_Tag }).IsUnique();

                e.HasOne(mt => mt.Modulo)
                 .WithMany(m => m.ModuloTags)
                 .HasForeignKey(mt => mt.Id_Modulo);

                e.HasOne(mt => mt.Tag)
                 .WithMany(t => t.ModuloTags)
                 .HasForeignKey(mt => mt.Id_Tag);
            });

            modelBuilder.Entity<TemaTags>(e =>
            {
                e.HasKey(tt => new { tt.Id_Tema, tt.Id_Tag });
                e.HasIndex(tt => new { tt.Id_Tema, tt.Id_Tag }).IsUnique();

                e.HasOne(tt => tt.Tema)
                 .WithMany(t => t.TemaTags)
                 .HasForeignKey(tt => tt.Id_Tema);

                e.HasOne(tt => tt.Tag)
                 .WithMany(t => t.TemaTags)
                 .HasForeignKey(tt => tt.Id_Tag);
            });

            // Mapeo explícito para CapituloTags
            modelBuilder.Entity<CapituloTags>(e =>
            {
                e.ToTable("CapituloTags", "acc_academic");
                e.HasKey(x => new { x.Id_Capitulo, x.Id_Tag });

                e.Property(x => x.Id_Capitulo).HasColumnName("Id_Capitulo");
                e.Property(x => x.Id_Tag).HasColumnName("Id_Tag");

                e.HasOne(x => x.Capitulo)
                 .WithMany(c => c.CapituloTags)
                 .HasForeignKey(x => x.Id_Capitulo)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Tag)
                 .WithMany(t => t.CapituloTags)
                 .HasForeignKey(x => x.Id_Tag)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(x => x.Id_Tag);
            });

            // --------------------------------------------------------
            // Progreso e historial de calificaciones
            // --------------------------------------------------------

            modelBuilder.Entity<ProgresoUsuario>()
                .HasOne(p => p.SubTema)
                .WithMany()
                .HasForeignKey(p => p.SubTemaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistorialCalificaciones>()
                .HasOne(h => h.Usuario)
                .WithMany(u => u.HistorialCalificaciones)
                .HasForeignKey(h => h.Id_Usuario);

            modelBuilder.Entity<HistorialCalificaciones>()
                .HasOne(h => h.Modulo)
                .WithMany()
                .HasForeignKey(h => h.Id_Modulo);

            modelBuilder.Entity<HistorialCalificaciones>()
                .HasOne(h => h.SubModulo)
                .WithMany()
                .HasForeignKey(h => h.Id_SubModulo);

            // --------------------------------------------------------
            // Exámenes e intentos
            // --------------------------------------------------------

            modelBuilder.Entity<ExamenIntento>()
                .HasOne(x => x.Usuario)
                .WithMany(u => u.Intentos)
                .HasForeignKey(x => x.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamenIntento>()
                .HasOne(x => x.ExamenSubModulo)
                .WithMany(esm => esm.Intentos)
                .HasForeignKey(x => x.ExamenSubModuloId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamenIntento>()
                .HasOne(x => x.ExamenModulo)
                .WithMany(em => em.Intentos)
                .HasForeignKey(x => x.ExamenModuloId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamenIntento>()
                .HasOne(x => x.Examen)
                .WithMany(e => e.Intentos)
                .HasForeignKey(x => x.ExamenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índices únicos por (usuario, examenX, numeroIntento)
            modelBuilder.Entity<ExamenIntento>()
                .HasIndex(x => new { x.IdUsuario, x.ExamenSubModuloId, x.NumeroIntento })
                .IsUnique();

            modelBuilder.Entity<ExamenIntento>()
                .HasIndex(x => new { x.IdUsuario, x.ExamenModuloId, x.NumeroIntento })
                .IsUnique();

            modelBuilder.Entity<ExamenIntento>()
                .HasIndex(x => new { x.IdUsuario, x.ExamenId, x.NumeroIntento })
                .IsUnique();

            // Enlace ExamenSubModulo -> SubModulo
            modelBuilder.Entity<ExamenSubModulo>()
                .HasOne(e => e.SubModulo)
                .WithMany(sm => sm.Examenes)
                .HasForeignKey(e => e.SubModuloId)
                .OnDelete(DeleteBehavior.Cascade);

            // Exámenes habilitados (polimórfico)
            modelBuilder.Entity<ExamenHabilitado>()
                .HasIndex(e => new { e.UsuarioId, e.Tipo, e.RefId })
                .IsUnique();

            modelBuilder.Entity<ExamenAprobatorio>(b =>
            {
                b.HasIndex(x => new { x.UsuarioId, x.Tipo, x.ExamenId }).IsUnique();
                b.HasIndex(x => x.ExamenIntentoId).IsUnique();
                b.Property(x => x.RowVersion).IsRowVersion();
            });

            // --------------------------------------------------------
            // Agenda y tareas personales
            // --------------------------------------------------------

            modelBuilder.Entity<Agenda>()
                .HasOne(a => a.Usuario)
                .WithOne(u => u.Agenda)
                .HasForeignKey<Agenda>(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Agenda>()
                .HasMany(a => a.TareasAsignadas)
                .WithOne(t => t.Agenda)
                .HasForeignKey(t => t.AgendaId);

            modelBuilder.Entity<Agenda>()
                .HasMany(a => a.TareasPersonales)
                .WithOne(t => t.Agenda)
                .HasForeignKey(t => t.IdAgenda);

            modelBuilder.Entity<Aviso>()
                .Property(a => a.FechaAviso)
                .HasDefaultValueSql("GETDATE()");

            // --------------------------------------------------------
            // Aulas, anuncios, notificaciones y auditoría
            // --------------------------------------------------------

            modelBuilder.Entity<Aula>(entity =>
            {
                entity.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Descripcion).HasMaxLength(500);

                entity.Property(x => x.CerrarAula).HasDefaultValue(false);
                entity.Property(x => x.ArchivarAula).HasDefaultValue(false);

                entity.Property(x => x.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(x => x.FechaActualizacion).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(x => x.DocenteId).IsRequired().HasMaxLength(64);
            });

            modelBuilder.Entity<AulaEstudiante>(entity =>
            {
                entity.Property(x => x.UsuarioId).IsRequired().HasMaxLength(64);
                entity.Property(x => x.FechaInscripcion).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Aula)
                      .WithMany(a => a.AulaEstudiantes)
                      .HasForeignKey(x => x.AulaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.Property(x => x.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Cuerpo).IsRequired();
                entity.Property(x => x.Fecha).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(x => x.DocenteId).IsRequired().HasMaxLength(64);
                entity.Property(x => x.AulaId).IsRequired();

                entity.HasOne(x => x.Aula)
                      .WithMany(a => a.Anuncios)
                      .HasForeignKey(x => x.AulaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.Property(x => x.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Mensaje).IsRequired().HasMaxLength(500);
                entity.Property(x => x.UsuarioId).IsRequired().HasMaxLength(64);
                entity.Property(x => x.FechaEnvio).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(x => x.Leido).HasDefaultValue(false);

                // Relación con Usuario (no borrar si se elimina el usuario)
                entity.HasOne(n => n.Usuario)
                      .WithMany(u => u.Notificaciones)
                      .HasForeignKey(n => n.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación opcional con Aula (si se elimina Aula, dejar la notificación "huérfana")
                entity.HasOne(x => x.Aula)
                      .WithMany(a => a.Notificaciones)
                      .HasForeignKey(x => x.AulaId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Auditoria>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Auditorias)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------------------------------------
            // Invitaciones, evaluaciones y tareas de aula
            // --------------------------------------------------------

            modelBuilder.Entity<InvitacionAula>(entity =>
            {
                entity.Property(a => a.Token).IsRequired().HasMaxLength(64);
                entity.Property(a => a.Activa).HasDefaultValue(true);
                entity.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(a => a.Aula)
                      .WithMany(a => a.Invitaciones)
                      .HasForeignKey(a => a.AulaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.Property(x => x.Titulo).IsRequired().HasMaxLength(160);
                entity.Property(x => x.Fecha).IsRequired();
                entity.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Aula)
                      .WithMany(au => au.Evaluaciones)
                      .HasForeignKey(x => x.AulaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EvaluacionResultado>(entity =>
            {
                entity.Property(ev => ev.UsuarioId).IsRequired().HasMaxLength(64);
                entity.Property(ev => ev.Calificacion).IsRequired();
                entity.Property(ev => ev.FechaCalificacion).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Evaluacion)
                      .WithMany(e => e.Resultados)
                      .HasForeignKey(x => x.EvaluacionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.Property(x => x.Titulo).IsRequired().HasMaxLength(160);
                entity.Property(x => x.FechaLimite).IsRequired();
                entity.Property(x => x.Scope).IsRequired().HasConversion<int>();
                entity.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(x => x.Aula)
                      .WithMany(a => a.Tareas)
                      .HasForeignKey(x => x.AulaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TareaAsignacion>(entity =>
            {
                entity.Property(x => x.UsuarioId).IsRequired().HasMaxLength(64);
                entity.Property(x => x.Estado).IsRequired().HasConversion<int>();

                entity.HasOne(x => x.Tarea)
                      .WithMany(t => t.Asignaciones)
                      .HasForeignKey(x => x.TareaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // --------------------------------------------------------
            // Índices de rendimiento
            // --------------------------------------------------------

            modelBuilder.Entity<AulaEstudiante>()
                .HasIndex(x => new { x.AulaId, x.UsuarioId })
                .IsUnique();

            modelBuilder.Entity<Aula>()
                .HasIndex(x => x.DocenteId);

            modelBuilder.Entity<Aula>()
                .HasIndex(x => new { x.ModuloId, x.SubModuloId });

            modelBuilder.Entity<Anuncio>()
                .HasIndex(x => new { x.AulaId, x.Fecha });

            modelBuilder.Entity<Notificacion>()
                .HasIndex(x => x.UsuarioId);

            modelBuilder.Entity<Notificacion>()
                .HasIndex(x => new { x.AulaId, x.FechaEnvio });

            modelBuilder.Entity<TareaAsignacion>()
                .HasIndex(x => x.TareaId);

            modelBuilder.Entity<TareaAsignacion>()
                .HasIndex(x => new { x.TareaId, x.UsuarioId })
                .IsUnique();

            modelBuilder.Entity<Tarea>()
                .HasIndex(x => new { x.AulaId, x.FechaLimite });

            modelBuilder.Entity<EvaluacionResultado>()
                .HasIndex(x => x.EvaluacionId);

            modelBuilder.Entity<EvaluacionResultado>()
                .HasIndex(x => new { x.EvaluacionId, x.UsuarioId })
                .IsUnique();

            modelBuilder.Entity<Evaluacion>()
                .HasIndex(x => new { x.AulaId, x.Fecha });

            modelBuilder.Entity<InvitacionAula>()
                .HasIndex(inv => inv.Token)
                .IsUnique();

            modelBuilder.Entity<InvitacionAula>()
                .HasIndex(inv => new { inv.AulaId, inv.Activa });

            modelBuilder.Entity<ProgresoUsuario>()
                .HasIndex(p => new { p.UsuarioId, p.SubTemaId, p.Completado });

            modelBuilder.Entity<Tema>()
                .HasIndex(t => t.Id_SubModulo);

            modelBuilder.Entity<SubTema>()
                .HasIndex(st => st.Id_Tema);

            modelBuilder.Entity<ExamenSubModulo>()
                .HasIndex(e => e.SubModuloId);

            modelBuilder.Entity<ExamenIntento>()
                .HasIndex(i => new { i.IdUsuario, i.ExamenSubModuloId, i.Aprobado });
        }
    }
}