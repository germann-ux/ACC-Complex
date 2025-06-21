using Microsoft.EntityFrameworkCore;
using ACC.Data.Entities;
using ACC.Data;
using ACC.Shared.Interfaces;
using ACC.Shared.DTOs;
using AutoMapper;

namespace ACC.API.Services
{
    public class TipService : ITipService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;

        public TipService(ACCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipDto> ObtenerTipAleatorioAsync()
        {
            var count = await _context.Tips.CountAsync();
            if (count == 0) return new TipDto { Contenido = "No hay tips disponibles aún." };

            var random = new Random();
            int skip = random.Next(0, count);
            var tip = await _context.Tips.Skip(skip).FirstOrDefaultAsync();

            return _mapper.Map<TipDto>(tip);
        }
    }
}
