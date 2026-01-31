using System.Linq;
using AutoMapper;
using ACC.Shared.DTOs;
using ACC.Data.Entities;
using ACC.Shared.Enums; //LOL

namespace ACC.API.Extensions; 

/// <summary>
/// Clase de perfil de mapeo de AutoMapper para convertir entre entidades y DTOs.
/// </summary>
public class ACCmappingProfile : Profile
{
    public ACCmappingProfile()
    {
        // =========================
        // MAPEOS EXISTENTES
        // =========================

        // mapeo entre contenido y contenidoDto
        CreateMap<ContenidoCapitulo, ContenidoCapituloDto>().ReverseMap();

        // Mapeo entre Aula y AulaDto
        CreateMap<Aula, AulaDto>().ReverseMap();

        // Mapeo entre Aviso y AvisoDto (LEGACY)
        // Nota: cuando migres completamente a Anuncio/AnuncioDto
        // puedes eliminar este mapeo y la entidad Aviso.
        CreateMap<Aviso, AvisoDto>().ReverseMap();

        // Mapeo entre Modulo y ModuloDto
        CreateMap<Modulo, ModuloDto>().ReverseMap();

        // Mapeo entre Notificacion y NotificacionDto
        CreateMap<Notificacion, NotificacionDto>().ReverseMap();

        // Mapeo entre TareaAsignada y TareaAsignadaDto (LEGACY de tareas)
        CreateMap<TareaAsignada, TareaAsignadaDto>().ReverseMap();

        // Mapeo entre TareaPersonal y TareaPersonalDto
        CreateMap<TareaPersonal, TareaPersonalDto>().ReverseMap();

        // Mapeo entre ApplicationUser y ApplicationUserDto
        CreateMap<ApplicationUserDto, Usuario>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<Usuario, ApplicationUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        // Mapeo entre AulaEstudiante y AulaEstudianteDto
        CreateMap<AulaEstudiante, AulaEstudianteDto>().ReverseMap();

        // Mapeo entre Agenda y AgendaDto
        CreateMap<Agenda, AgendaDto>().ReverseMap();

        // Mapeo entre HistorialCalificaciones y HistorialCalificacionesDto
        CreateMap<HistorialCalificaciones, HistorialCalificacionesDto>().ReverseMap();

        // Mapeo entre Auditoria y AuditoriaDto
        CreateMap<Auditoria, AuditoriaDto>().ReverseMap();

        // Mapeo entre SubModulo y SubModuloDto
        CreateMap<SubModulo, SubModuloDto>().ReverseMap();

        // Mapeo entre SubTema y SubTemaDto
        CreateMap<SubTema, SubTemaDto>().ReverseMap();

        // Mapeo entre Leccion y LeccionDto
        CreateMap<Leccion, LeccionDto>().ReverseMap();

        // Mapeo entre Tag y TagDto
        CreateMap<Tag, TagDto>().ReverseMap();

        // Mapeo entre Tema y TemaDto
        CreateMap<Tema, TemaDto>().ReverseMap();

        // Mapeo entre Capitulo y CapituloDto
        CreateMap<Capitulo, CapituloDto>()
            .ForMember(d => d.Tags, opt => opt.MapFrom
            (s => s.CapituloTags != null ? s.CapituloTags.Select(ct => ct.Tag) : Enumerable.Empty<Tag?>()))
            .ForMember(d => d.Contenidos, opt => opt.MapFrom(s => s.Contenidos));


        // Mapeo entre UsuarioSubTemas y UsuarioSubTemasDto
        CreateMap<UsuarioSubTemas, UsuarioSubTemasDto>().ReverseMap();

        // Mapeo entre UsuarioSubModulos y UsuarioSubModulosDto
        CreateMap<UsuarioSubModulos, UsuarioSubModulosDto>().ReverseMap();

        // Mapeo entre UsuarioTemas y UsuarioTemasDto
        CreateMap<UsuarioTemas, UsuarioTemasDto>().ReverseMap();

        // Mapeo entre CapituloTags y CapituloTagsDto
        CreateMap<CapituloTag, CapituloTagsDto>().ReverseMap();

        // Mapeo entre ExamenHabilitado y ExamenHabilitadoDto
        CreateMap<ExamenHabilitado, ExamenHabilitadoDto>().ReverseMap();

        // Mapeo entre ModuloTags y ModuloTagsDto
        CreateMap<ModuloTags, ModuloTagsDto>().ReverseMap();

        // Mapeo entre ProgresoUsuario y ProgresoUsuarioDto
        CreateMap<ProgresoUsuario, ProgresoUsuarioDto>().ReverseMap();

        // Mapeo entre TemaTags y TemaTagsDto
        CreateMap<TemaTags, TemaTagsDto>().ReverseMap();

        // Mapeo entre Tip y TipDto
        CreateMap<Tip, TipDto>().ReverseMap();

        /// NUEVO:
        CreateMap<UsuarioModulos, UsuarioModulosDto>().ReverseMap();

        CreateMap<ExamenModulo, ExamenModuloDto>().ReverseMap();

        CreateMap<ExamenSubModulo, ExamenSubModuloDto>().ReverseMap();

        CreateMap<ExamenIntento, ExamenIntentoDto>().ReverseMap();

        CreateMap<Examen, ExamenDto>().ReverseMap();


        // =========================
        // NUEVO: AULAS DOCENTE (Dashboard)
        // =========================

        // -------- Configuración del Aula --------
        CreateMap<Aula, AulaConfigDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AulaId))
            .ForMember(d => d.SubModuloId, opt => opt.MapFrom(s => s.SubModuloId))
            // Nombre del submódulo (string) lo llenas tú con JOIN o servicio
            .ForMember(d => d.Submodulo, opt => opt.Ignore());

        // Patch de configuración
        CreateMap<AulaConfigUpdateDto, Aula>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, value) => value != null));

        // -------- Invitaciones de Aula --------
        CreateMap<InvitacionAula, InvitacionGeneradaDto>()
            // El LinkInvitacion se arma en el controller a partir de la Token
            .ForMember(d => d.LinkInvitacion, opt => opt.Ignore());

        // -------- Lista de Estudiantes del Aula --------
        // Proyección auxiliar → DTO para la tabla de ListaEstudiantes
        CreateMap<EstudianteListadoProjection, EstudianteListadoDto>();

        // =========================
        // NUEVO: ANUNCIOS (muro del aula)
        // =========================

        // Entity -> DTO
        CreateMap<Anuncio, AnuncioDto>();

        // Create DTO -> Entity
        CreateMap<AnuncioCreateDto, Anuncio>()
            .ForMember(d => d.AulaId, opt => opt.Ignore())
            .ForMember(d => d.AulaId, opt => opt.Ignore())  // viene de la ruta
            .ForMember(d => d.Fecha, opt => opt.Ignore()); // lo pone el backend/DB

        // Update DTO -> Entity
        CreateMap<AnuncioUpdateDto, Anuncio>()
            .ForMember(d => d.AnuncioId, opt => opt.Ignore())
            .ForMember(d => d.AulaId, opt => opt.Ignore())
            .ForMember(d => d.Fecha, opt => opt.Ignore());

        // =========================
        // NUEVO: TAREAS (aula + asignaciones)
        // =========================

        // Tarea (aula-level) -> Listado DTO
        CreateMap<Tarea, TareaListadoDto>()
            .ForMember(d => d.Scope, opt => opt.MapFrom(s => s.Scope))
            // EstadoGlobal se calcula en el servicio si lo necesitas
            .ForMember(d => d.EstadoGlobal, opt => opt.Ignore());

        // Create DTO -> Tarea
        CreateMap<TareaCreateDto, Tarea>()
            .ForMember(d => d.TareaId, opt => opt.Ignore())
            .ForMember(d => d.AulaId, opt => opt.Ignore())   // route param
            .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            .ForMember(d => d.Scope, opt => opt.MapFrom(s => s.Scope));

        // Update de asignación por alumno
        CreateMap<TareaAsignacionUpdateDto, TareasAsignaciones>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.TareaId, opt => opt.Ignore())
            .ForMember(d => d.UsuarioId, opt => opt.Ignore())
            .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.Estado));

        // =========================
        // NUEVO: EVALUACIONES
        // =========================

        // Evaluacion -> Listado DTO (Promedio desde Resultados; TotalAlumnos se rellena)
        CreateMap<Evaluacion, EvaluacionListadoDto>()
            .ForMember(d => d.TotalAlumnos, opt => opt.Ignore())
            .ForMember(d => d.Promedio, opt => opt.MapFrom(s =>
                s.Resultados != null && s.Resultados.Any()
                    ? Math.Round(s.Resultados.Average(r => r.Calificacion), 2)
                    : 0.0));

        // Create DTO -> Evaluacion
        CreateMap<EvaluacionCreateDto, Evaluacion>()
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ForMember(d => d.AulaId, opt => opt.Ignore())   // route param
            .ForMember(d => d.CreatedAt, opt => opt.Ignore());
    }
}