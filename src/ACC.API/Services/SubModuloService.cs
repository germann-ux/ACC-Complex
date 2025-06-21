using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class SubModuloService : ISubModuloService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper; 

        public SubModuloService(ACCDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<SubModuloDto> CreateSubModuloAsync(SubModuloDto subModulo)
        {
            var subModuloEntity = _mapper.Map<SubModulo>(subModulo);
            _context.SubModulos.Add(subModuloEntity);
            await _context.SaveChangesAsync();
            return subModulo;
        }

        public async Task<SubModuloDto> GetSubModuloByIdAsync(int subModuloId)
        {
            var SubmoduloUbicado = await _context.SubModulos.FirstOrDefaultAsync(sm => sm.Id_SubModulo == subModuloId); 
            var submoduloUbicadoDto = _mapper.Map<SubModuloDto>(SubmoduloUbicado); // Mapeo de entidad a DTO
            return submoduloUbicadoDto;
        }

        public async Task<IEnumerable<SubModulo>> GetAllSubModulosAsync()
        {
            return await _context.SubModulos
                .Include(sm => sm.Modulo)
                .Include(sm => sm.Temas)
                .Include(sm => sm.UsuarioSubModulos)
                .ToListAsync();
        }

        public async Task<SubModuloDto> UpdateSubModuloAsync(SubModuloDto subModulo)
        {
            var existingSubModulo = await _context.SubModulos.FindAsync(subModulo.Id_SubModulo);
            _context.SubModulos.Update(existingSubModulo);
            await _context.SaveChangesAsync();
            return subModulo;
        }

        public async Task<bool> DeleteSubModuloAsync(int subModuloId)
        {
            var subModulo = await _context.SubModulos.FindAsync(subModuloId);
            if (subModulo == null)
            {
                return false;
            }

            _context.SubModulos.Remove(subModulo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SubModuloDto>> SearchSubModulosByNameAsync(string nombreSubModulo)
        {
            throw new NotImplementedException(); 
        }

        public async Task<List<SubModuloDto>> GetSubModulosPorModuloAsync(int idModulo)
        {
            return await _context.SubModulos
                .Where(sm => sm.Id_Modulo == idModulo)
                .Select(sm => new SubModuloDto
                {
                    Id_SubModulo = sm.Id_SubModulo,
                    NombreSubModulo = sm.NombreSubModulo,
                    Id_Modulo = sm.Id_Modulo
                }).ToListAsync();
        }

        Task<IEnumerable<SubModuloDto>> ISubModuloService.GetAllSubModulosAsync()
        {
            throw new NotImplementedException();
        }
    }
}