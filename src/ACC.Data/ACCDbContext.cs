using Microsoft.EntityFrameworkCore;
using ACC.Data.Entities;

namespace ACC.Data
{
    public class ACCDbContext(DbContextOptions<ACCDbContext> options) : DbContext(options)
    {
        // ---------------------------
        // DbSets
        // ---------------------------

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<ContenidoCapitulo> ContenidoCapitulos => Set<ContenidoCapitulo>();
        public DbSet<Modulo> Modulos => Set<Modulo>();
        public DbSet<SubModulo> SubModulos => Set<SubModulo>();
        public DbSet<Tema> Temas => Set<Tema>();
        public DbSet<SubTema> SubTemas => Set<SubTema>();
        public DbSet<Capitulo> Capitulos => Set<Capitulo>();
        public DbSet<Leccion> Lecciones => Set<Leccion>();   // NUEVO
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<ModuloTags> ModuloTags => Set<ModuloTags>();
        public DbSet<TemaTags> TemaTags => Set<TemaTags>();
        public DbSet<CapituloTags> CapituloTags => Set<CapituloTags>();  // NUEVO
        public DbSet<UsuarioModulos> UsuarioModulos => Set<UsuarioModulos>();
        public DbSet<UsuarioSubModulos> UsuarioSubModulos => Set<UsuarioSubModulos>();
        public DbSet<UsuarioTemas> UsuarioTemas => Set<UsuarioTemas>();
        public DbSet<UsuarioSubTemas> UsuarioSubTemas => Set<UsuarioSubTemas>();
        public DbSet<HistorialCalificaciones> HistorialCalificaciones => Set<HistorialCalificaciones>();
        public DbSet<ProgresoUsuario> ProgresoUsuarios => Set<ProgresoUsuario>();
        public DbSet<Aula> Aulas => Set<Aula>();
        public DbSet<AulaEstudiante> AulaEstudiantes => Set<AulaEstudiante>();
        public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
        public DbSet<Auditoria> Auditorias => Set<Auditoria>();
        public DbSet<Agenda> Agendas => Set<Agenda>();
        public DbSet<TareaAsignada> TareasAsignadas => Set<TareaAsignada>();
        public DbSet<TareaPersonal> TareasPersonales => Set<TareaPersonal>();
        public DbSet<ExamenHabilitado> ExamenesHabilitados => Set<ExamenHabilitado>();
        public DbSet<Aviso> Avisos => Set<Aviso>();
        public DbSet<Tip> Tips => Set<Tip>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // --------- Esquema por defecto --------- //
            modelBuilder.HasDefaultSchema("acc_academic");
            // ========== Configuración General ==========

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

            // ========== Relaciones: Jerarquía ==========

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

            // ========== NUEVO: relación SubTema -> Lecciones (1:n) ==========

            modelBuilder.Entity<Leccion>()
                .HasKey(l => l.IdLeccion);

            modelBuilder.Entity<Leccion>()
                .HasOne(l => l.SubTema)
                .WithMany(st => st.Lecciones)
                .HasForeignKey(l => l.SubtemaId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========== NUEVO: relación Capitulo -> Leccion (opcional) ==========

            modelBuilder.Entity<Capitulo>()
                .HasKey(c => c.IdCapitulo);

            modelBuilder.Entity<Capitulo>()
                .HasOne(c => c.Leccion)
                .WithMany(l => l.Capitulos)
                .HasForeignKey(c => c.LeccionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========== Relaciones: Many-to-Many con Tags ==========

            modelBuilder.Entity<ModuloTags>()
                .HasKey(mt => new { mt.Id_Modulo, mt.Id_Tag });

            modelBuilder.Entity<ModuloTags>()
                .HasIndex(mt => new { mt.Id_Modulo, mt.Id_Tag })
                .IsUnique();

            modelBuilder.Entity<ModuloTags>()
                .HasOne(mt => mt.Modulo)
                .WithMany(m => m.ModuloTags)
                .HasForeignKey(mt => mt.Id_Modulo);

            modelBuilder.Entity<ModuloTags>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.ModuloTags)
                .HasForeignKey(mt => mt.Id_Tag);

            modelBuilder.Entity<TemaTags>()
                .HasKey(tt => new { tt.Id_Tema, tt.Id_Tag });

            modelBuilder.Entity<TemaTags>()
                .HasIndex(tt => new { tt.Id_Tema, tt.Id_Tag })
                .IsUnique();

            modelBuilder.Entity<TemaTags>()
                .HasOne(tt => tt.Tema)
                .WithMany(t => t.TemaTags)
                .HasForeignKey(tt => tt.Id_Tema);

            modelBuilder.Entity<TemaTags>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TemaTags)
                .HasForeignKey(tt => tt.Id_Tag);

            // ========== NUEVO: CapituloTags (M:N) ==========

            modelBuilder.Entity<CapituloTags>()
                .HasKey(ct => new { ct.Id_Capitulo, ct.Id_Tag });

            modelBuilder.Entity<CapituloTags>()
                .HasOne(ct => ct.Capitulo)
                .WithMany(c => c.CapituloTags)
                .HasForeignKey(ct => ct.Id_Capitulo);

            modelBuilder.Entity<CapituloTags>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.CapituloTags)
                .HasForeignKey(ct => ct.Id_Tag);

            // ========== Relaciones: Progreso, Historial, Exámenes ==========

            modelBuilder.Entity<ProgresoUsuario>()
                .HasOne(p => p.SubTema)
                .WithMany()
                .HasForeignKey(p => p.SubTemaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistorialCalificaciones>()
                .HasKey(h => h.Id_Historial);

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

            modelBuilder.Entity<ExamenHabilitado>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<ExamenHabilitado>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamenHabilitado>()
                .HasOne(e => e.SubModulo)
                .WithMany()
                .HasForeignKey(e => e.Id_SubModulo)
                .OnDelete(DeleteBehavior.Restrict);

            // ========== Relaciones: Aulas y Notificaciones ==========

            modelBuilder.Entity<Aula>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Modulo)
                .WithMany(m => m.Aulas)
                .HasForeignKey(a => a.ModuloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Docente)
                .WithMany(u => u.AulasDocente)
                .HasForeignKey(a => a.DocenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aula>()
                .HasMany(a => a.AulaEstudiantes)
                .WithOne(ae => ae.Aula)
                .HasForeignKey(ae => ae.AulaId);

            modelBuilder.Entity<Aula>()
                .HasMany(a => a.Notificaciones)
                .WithOne(n => n.Aula)
                .HasForeignKey(n => n.AulaId);

            modelBuilder.Entity<Aula>()
                .HasMany(a => a.Avisos)
                .WithOne(av => av.Aula)
                .HasForeignKey(av => av.AulaId);

            modelBuilder.Entity<AulaEstudiante>()
                .HasKey(ae => new { ae.AulaId, ae.UsuarioId });

            modelBuilder.Entity<AulaEstudiante>()
                .HasOne(ae => ae.Usuario)
                .WithMany(u => u.AulasEstudiantes)
                .HasForeignKey(ae => ae.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notificacion>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Notificaciones)
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notificacion>()
                .Property(n => n.Leido)
                .HasDefaultValue(false);

            // ========== Auditoría ==========

            modelBuilder.Entity<Auditoria>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Auditoria>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Auditorias)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========== Agenda (1:1 con Usuario) y Tareas ==========

            modelBuilder.Entity<Agenda>()
                .HasKey(a => a.Id_Agenda);

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

            // ========== Aviso ========== 

            modelBuilder.Entity<Aviso>()
                .HasKey(a => a.IdAviso);

            modelBuilder.Entity<Aviso>()
                .Property(a => a.FechaAviso)
                .HasDefaultValueSql("GETDATE()");

            // ========== Llaves primarias ========== // 

            modelBuilder.Entity<Tema>().HasKey(t => t.Id_Tema);
            modelBuilder.Entity<Modulo>().HasKey(m => m.Id_Modulo);
            modelBuilder.Entity<SubModulo>().HasKey(sm => sm.Id_SubModulo);
            modelBuilder.Entity<Leccion>().HasKey(l => l.IdLeccion);
            modelBuilder.Entity<HistorialCalificaciones>().HasKey(h => h.Id_Historial);
            modelBuilder.Entity<ModuloTags>().HasKey(mt => new { mt.Id_Modulo, mt.Id_Tag });
            modelBuilder.Entity<TemaTags>().HasKey(tt => new { tt.Id_Tema, tt.Id_Tag });
            modelBuilder.Entity<CapituloTags>().HasKey(ct => new { ct.Id_Capitulo, ct.Id_Tag });
            modelBuilder.Entity<Capitulo>().HasKey(c => c.IdCapitulo);
            modelBuilder.Entity<SubTema>().HasKey(st => st.Id_SubTema);
            modelBuilder.Entity<Tag>().HasKey(t => t.Id_Tag);
            modelBuilder.Entity<ProgresoUsuario>().HasKey(p => p.IdProgreso);
            modelBuilder.Entity<ExamenHabilitado>().HasKey(e => e.Id);
            modelBuilder.Entity<TareaAsignada>().HasKey(ta => ta.IdTareaAsignada);
            modelBuilder.Entity<TareaPersonal>().HasKey(tp => tp.TareaPersonalId);
            modelBuilder.Entity<Tip>().HasKey(t => t.Id);
            modelBuilder.Entity<UsuarioModulos>().HasKey(um => new { um.Id_Usuario, um.Id_Modulo });
            modelBuilder.Entity<UsuarioSubModulos>().HasKey(usm => new { usm.Id_Usuario, usm.Id_SubModulo });
            modelBuilder.Entity<UsuarioTemas>().HasKey(ut => new { ut.Id_Usuario, ut.Id_Tema });
            modelBuilder.Entity<UsuarioSubTemas>().HasKey(ust => new { ust.Id_Usuario, ust.Id_SubTema });
            modelBuilder.Entity<Aula>().HasKey(a => a.Id);
        }
    }
}