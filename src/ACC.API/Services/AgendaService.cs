using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ACC.Data.Entities;
using ACC.Data;
using ACC.Shared.Interfaces;
using ACC.Shared.DTOs;

namespace ACC.API.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly ACCDbContext _context;
        private readonly ILogger<AgendaService> _logger;

        public AgendaService(ACCDbContext context, ILogger<AgendaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<AgendaDto> CreateAgendaAsync(AgendaDto agenda, string IdUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAgendaAsync(int agendaId)
        {
            throw new NotImplementedException();
        }

        public Task<AgendaDto> GetAgendaByUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

