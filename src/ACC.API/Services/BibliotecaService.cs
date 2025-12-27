using ACC.API.Interfaces;
using ACC.Data;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace ACC.API.Services; 
public class BibliotecaService : IBibliotecaService
{
    private readonly ACCDbContext _contexto;
    private readonly IMapper _mapper;

    public BibliotecaService(ACCDbContext contexto, IMapper mapper)
    {
        _contexto = contexto;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<CapituloDto>>> ObtenerCapitulosAsync()
    {
        try
        {
            var capitulos = await _contexto.Capitulos
                .AsNoTracking()
                .Include(c => c.Contenidos)
                .Include(c => c.CapituloTags!)
                    .ThenInclude(ct => ct.Tag)
                .OrderBy(c => c.IdCapitulo)
                .ToListAsync();

            var dto = _mapper.Map<List<CapituloDto>>(capitulos);

            return ServiceResult<List<CapituloDto>>.Ok(dto);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<CapituloDto>>.Error(ex);
        }
    }

    public async Task<ServiceResult<CapituloDto>> ObtenerCapituloPorIdAsync(int idCapitulo)
    {
        try
        {
            var capitulo = await _contexto.Capitulos
                .AsNoTracking()
                .Include(c => c.Contenidos)
                .Include(c => c.CapituloTags!)
                    .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.IdCapitulo == idCapitulo);

            if (capitulo is null)
                return ServiceResult<CapituloDto>.NotFound("Capítulo no encontrado.");

            var dto = _mapper.Map<CapituloDto>(capitulo);

            return ServiceResult<CapituloDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            return ServiceResult<CapituloDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerContenidosRecomendadosRandomAsync(int count, int? maxIdContenido)
    {
        try
        {
            // evita inputs absurdos
            count = Math.Clamp(count, 1, 20);

            var q = _contexto.ContenidoCapitulos
                .AsNoTracking()
                .AsQueryable();

            if (maxIdContenido.HasValue)
                q = q.Where(x => x.IdContenido <= maxIdContenido.Value);

            // Si tienes un campo "Publicado/Activo/EsVisible" aquí es el lugar perfecto para filtrar.

            var contenidos = await q
                .OrderBy(_ => Guid.NewGuid()) // random en SQL (NEWID)
                .Take(count)
                .ToListAsync();

            var dto = _mapper.Map<List<ContenidoCapituloDto>>(contenidos);
            return ServiceResult<List<ContenidoCapituloDto>>.Ok(dto);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<ContenidoCapituloDto>>.Error(ex);
        }
    }
}
