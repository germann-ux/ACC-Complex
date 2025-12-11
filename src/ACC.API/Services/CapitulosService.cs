using System.Security.Cryptography;
using ACC.API.Interfaces;
using ACC.Data;
using ACC.Shared.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class CapitulosService : ICapitulosService
    {
        private readonly IMapper _mapper;
        private readonly ACCDbContext _context;

        public CapitulosService(ACCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContenidoCapituloDto>> CapitulosRecomendados(List<int> seleccionados)
        {
            var capitulos = await _context.ContenidoCapitulos
                .Where(c => seleccionados.Contains(c.IdContenido))
                .ToListAsync();

            return _mapper.Map<List<ContenidoCapituloDto>>(capitulos);
        }

        // selección aleatoria en servidor
        public async Task<List<ContenidoCapituloDto>> CapitulosRecomendadosRandom(int maxIdIncluido, int count)
        {
            if (maxIdIncluido < 1 || count < 1) return [];

            // Trae solo IDs válidos en el rango [1..max]
            var idsDisponibles = await _context.ContenidoCapitulos
                .Where(c => c.IdContenido >= 1 && c.IdContenido <= maxIdIncluido)
                .Select(c => c.IdContenido)
                .ToListAsync();

            if (idsDisponibles.Count == 0) return [];

            // Si piden más de los que hay, se reduce al total
            count = Math.Min(count, idsDisponibles.Count);

            var seleccion = MuestreoSinReemplazo(idsDisponibles, count);

            var capitulos = await _context.ContenidoCapitulos
                .Where(c => seleccion.Contains(c.IdContenido))
                .ToListAsync();

            return _mapper.Map<List<ContenidoCapituloDto>>(capitulos);
        }

        private static List<int> MuestreoSinReemplazo(List<int> universo, int count)
        {
            // Fisher–Yates parcial con RNG criptográfico
            var arr = universo.ToArray();
            using var rng = RandomNumberGenerator.Create();
            Span<byte> buf = stackalloc byte[4];

            for (int i = 0; i < count; i++)
            {
                rng.GetBytes(buf);
                int r = Math.Abs(BitConverter.ToInt32(buf)) % (arr.Length - i) + i;
                (arr[i], arr[r]) = (arr[r], arr[i]);
            }
            return arr.AsSpan(0, count).ToArray().ToList();
        }
    }
}
