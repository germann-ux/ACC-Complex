using AutoMapper;
using ACC.Shared.DTOs;
using ACC.Data.Entities;//LOL

namespace ACC.API.Extensions
{
    /// <summary>
    /// Clase de perfil de mapeo de AutoMapper para convertir entre entidades y DTOs.
    /// </summary>
    public class ACCmappingProfile : Profile
    {
        /// <summary>
        /// Constructor que configura los mapeos entre entidades y DTOs.
        /// </summary>
        public ACCmappingProfile()
        {
            // mapeo entre contenido y contenidoDto
            CreateMap<ContenidoCapitulo, ContenidoCapituloDto>().ReverseMap();

            // Mapeo entre Aula y AulaDto
            CreateMap<Aula, AulaDto>().ReverseMap();

            // Mapeo entre Aviso y AvisoDto
            CreateMap<Aviso, AvisoDto>().ReverseMap();

            // Mapeo entre Modulo y ModuloDto
            CreateMap<Modulo, ModuloDto>().ReverseMap();

            // Mapeo entre Notificacion y NotificacionDto
            CreateMap<Notificacion, NotificacionDto>().ReverseMap();

            // Mapeo entre TareaAsignada y TareaAsignadaDto
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
            CreateMap<Capitulo, CapituloDto>().ReverseMap();

            // Mapeo entre UsuarioSubTemas y UsuarioSubTemasDto
            CreateMap<UsuarioSubTemas, UsuarioSubTemasDto>().ReverseMap();

            // Mapeo entre UsuarioSubModulos y UsuarioSubModulosDto
            CreateMap<UsuarioSubModulos, UsuarioSubModulosDto>().ReverseMap();

            // Mapeo entre UsuarioTemas y UsuarioTemasDto
            CreateMap<UsuarioTemas, UsuarioTemasDto>().ReverseMap();

            // Mapeo entre CapituloTags y CapituloTagsDto
            CreateMap<CapituloTags, CapituloTagsDto>().ReverseMap();

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

            // Mapeo entre UsuarioModulos y UsuarioModulosDto
            CreateMap<UsuarioModulos, UsuarioModulosDto>().ReverseMap();
        }
    }
}


