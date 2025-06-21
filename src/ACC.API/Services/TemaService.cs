using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class TemaService : ITemaService
    {
        private readonly ACCDbContext _context;

        public TemaService(ACCDbContext context)
        {
            _context = context;
        }

        public Task<TemaDto> CreateTemaAsync(TemaDto tema)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTemaAsync(int temaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TemaDto>> GetAllTemasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TemaDto> GetTemaByIdAsync(int temaId)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioTemasDto> GetTemaProgresAsync(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TemaDto>> GetTemasBySubModuloAsync(int subModuloId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TemaDto>> GetTemasPorSubModuloAsync(int idSubModulo)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioTemasDto> GetUltimaVisitaTemaAsync(string idUsuario, int idTema)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TemaDto>> SearchTemasByNameAsync(string nombreTema)
        {
            throw new NotImplementedException();
        }

        public Task<TemaDto> UpdateTemaAsync(TemaDto tema)
        {
            throw new NotImplementedException();
        }
    }
}


