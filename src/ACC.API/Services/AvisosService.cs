using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class AvisosService : IAvisosService
    {
        private readonly ACCDbContext context_;
        private readonly IMapper _Mapper;    

        public AvisosService(ACCDbContext context, IMapper mapper)
        {
            context_ = context;
            _Mapper = mapper;
        }

        // este metodo esta generando problemas //
        public async Task<List<AvisoDto>> GetAvisosAsync(int aulaId)
        { // usando el nuevo mapeador
            var avisos = await context_.Avisos.Where(a => a.AulaId == aulaId).ToListAsync();
            var avisosDto = _Mapper.Map<List<AvisoDto>>(avisos);
            return avisosDto;
        }
        // --------------------------------------- //

        public async Task<AvisoDto> GetAvisoAsync(int id)
        {
            var aviso = await context_.Avisos.FindAsync(id);
            var avisoDto = _Mapper.Map<AvisoDto>(aviso);
            return avisoDto;
        }

        public async Task<AvisoDto> CreateAvisoAsync(AvisoDto aviso)
        {
            var avisoEntity = _Mapper.Map<Aviso>(aviso);
            context_.Avisos.Add(avisoEntity);
            await context_.SaveChangesAsync();
            return aviso;
        }

        public async Task<AvisoDto> UpdateAvisoAsync(AvisoDto aviso)
        {
            var avisoEntity = _Mapper.Map<Aviso>(aviso);
            var existingAviso = await context_.Avisos.FindAsync(avisoEntity.IdAviso);
            if (existingAviso != null)
            {
                existingAviso.TituloAviso = aviso.TituloAviso;
                existingAviso.ContenidoAviso = aviso.ContenidoAviso;
                existingAviso.FechaAviso = aviso.FechaAviso;
                existingAviso.DocenteId = aviso.DocenteId;
                existingAviso.AulaId = aviso.AulaId;

                await context_.SaveChangesAsync();
            }
            var avisoDto = _Mapper.Map<AvisoDto>(existingAviso);
            return avisoDto;
        }

        public async Task DeleteAvisoAsync(int id)
        {
            var aviso = await context_.Avisos.FindAsync(id);
            if (aviso != null)
            {
                context_.Avisos.Remove(aviso);
                await context_.SaveChangesAsync();
            }
        }
    }
}
