using ACC.Shared.DTOs;
using ACC.WebApp.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;

namespace ACC.WebApp.Services
{
    public class UsuarioSyncService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioSyncService> _logger; 

        public UsuarioSyncService(
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory httpClientFactory,
            IMapper mapper,
            ILogger<UsuarioSyncService> logger)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SincronizarUsuarioAsync(ApplicationUser usuario)
        {
            // Mapear el ApplicationUser al DTO
            var dto = _mapper.Map<ApplicationUserDto>(usuario);

            var httpClient = _httpClientFactory.CreateClient("ACC.API");
            
            Console.WriteLine(httpClient.BaseAddress);

            try
            {
                var response = await httpClient.PostAsJsonAsync("https://localhost:7059/api/Usuario/sincronizar", dto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al sincronizar el usuario {UserId} con la API", usuario.Id);
            }
        }
    }
}
