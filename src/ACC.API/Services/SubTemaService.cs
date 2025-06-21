using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class SubTemaService : ISubTemaService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _Mapper; 
        public SubTemaService(ACCDbContext context, IMapper mapper)
        {
            _Mapper = mapper;
            _context = context;
        }

        public async Task<SubTemaDto> CreateSubTemaAsync(SubTemaDto subTema)
        {
            var TemaEntity = _Mapper.Map<SubTema>(subTema);
            _context.SubTemas.Add(TemaEntity);
            await _context.SaveChangesAsync();
            return subTema;
        }

        public async Task<SubTemaDto> GetSubTemaByIdAsync(int subTemaId)
        {
            var SubtemaEntity = await _context.SubTemas
                .Include(st => st.Tema)
                .Include(st => st.UsuarioSubTemas)
                .FirstOrDefaultAsync(st => st.Id_SubTema == subTemaId);

            var SubtemaDto = _Mapper.Map<SubTemaDto>(SubtemaEntity);
            return SubtemaDto;
        }

        public async Task<IEnumerable<SubTemaDto>> GetAllSubTemasAsync()
        {
            var listasubtemas = await _context.SubTemas
                .Include(st => st.Tema)
                .Include(st => st.UsuarioSubTemas)
                .ToListAsync();

            var listasubtemasDto = _Mapper.Map<IEnumerable<SubTemaDto>>(listasubtemas);

            return listasubtemasDto;
        }

        public async Task<SubTemaDto> UpdateSubTemaAsync(SubTemaDto subTema)
        {
            var existingSubTema = _Mapper.Map<SubTema>(subTema);
            _context.SubTemas.Update(existingSubTema);
            await _context.SaveChangesAsync();
            return subTema;
        }

        public async Task<bool> DeleteSubTemaAsync(int subTemaId)
        {
            var subTema = await _context.SubTemas.FindAsync(subTemaId);
            if (subTema == null)
            {
                return false;
            }

            _context.SubTemas.Remove(subTema);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SubTemaDto>> SearchSubTemasByNameAsync(string nombreSubTema)
        {
            var listaSubtemasEntity = await _context.SubTemas
                .Include(st => st.Tema)
                .Include(st => st.UsuarioSubTemas)
                .Where(st => st.NombreSubTema.Contains(nombreSubTema))
                .ToListAsync();

            var listaSubtemasDto = _Mapper.Map<IEnumerable<SubTemaDto>>(listaSubtemasEntity);
            return listaSubtemasDto;
        }

        public async Task<IEnumerable<SubTemaDto>> GetSubTemasByTemaAsync(int temaId)
        {
            var listaSubtemasEntity = await _context.SubTemas
                .Include(st => st.UsuarioSubTemas)
                .Where(st => st.Id_Tema == temaId)
                .ToListAsync();

            var listaSubtemasDto = _Mapper.Map<IEnumerable<SubTemaDto>>(listaSubtemasEntity);
            return listaSubtemasDto;
        }
    }
}







