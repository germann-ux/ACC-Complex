using ACC.Shared.DTOs;
using ACC.WebApp.Data;
using AutoMapper;

namespace ACC.WebApp.Services
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Mapeo Identity -> DTO
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            //// Mapeo Entity -> DTO
            //CreateMap<Usuario, ApplicationUserDto>();

            //// Mapeo DTO -> Entity (para guardar o sincronizar)
            //CreateMap<ApplicationUserDto, Usuario>();
        }
    }

}
