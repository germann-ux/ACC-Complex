using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using ACC.WebApp.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ACC.WebApp.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger _logger; 

        public ApplicationUserService(IApplicationUserService applicationUserService, ApplicationDbContext context, IMapper mapper, ILogger logger)
        {
            _applicationUserService = applicationUserService;
            _context = context;
            _Mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ServiceResult<ApplicationUserDto>>>GetAllUsersAsync()
        {
            var ListaUsuarios = await _context.Users.ToListAsync();
            var ListaUsuariosDTO = _Mapper.Map<IEnumerable<ServiceResult<ApplicationUserDto>>>(ListaUsuarios);
            return ListaUsuariosDTO;
        }

        public async Task<ServiceResult<ServiceResult<ApplicationUserDto>>>GetUserByIdAsync(string userId)
        {
            var Usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (Usuario == null)
            {
                return ServiceResult<ServiceResult<ApplicationUserDto>>.NotFound("Usuario no encontrado");
            }
            else
            {
                var UsuarioDTO = _Mapper.Map<ServiceResult<ApplicationUserDto>>(Usuario);
                return ServiceResult<ServiceResult<ApplicationUserDto>>.Ok(UsuarioDTO);
            }
        }

        public Task<ServiceResult<ApplicationUserDto>> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdByClaimsAsync(ClaimsPrincipal claims)
        {
            try
            {
                if (claims.Identity?.IsAuthenticated == true)
                {
                    var userId = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _logger.LogInformation("Usuario autenticado: ID = {UserId}", userId);
                    return await Task.FromResult(userId);
                }
                else
                {
                    _logger.LogWarning("GetUserIdByClaimsAsync: El usuario no está autenticado");
                    return await Task.FromResult<string>(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el ID del usuario");
                throw;
            }
        }

        public Task<ServiceResult<ServiceResult<ApplicationUserDto>>> RegistrarUsuarioAsync(ApplicationUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}