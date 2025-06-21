using ACC.API.Interfaces;
using ACC.Data;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class BibliotecaService : IBibliotecaService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;

        public BibliotecaService(ACCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerContenidosAsync()
        {
            var contenidos = await _context.ContenidoCapitulos.ToListAsync();
            //return _mapper.Map<List<ContenidoCapituloDto>>(contenidos);
            var contenidosDto = _mapper.Map<List<ContenidoCapituloDto>>(contenidos);
            if (contenidosDto.Count <= 0)
            {
                return ServiceResult<List<ContenidoCapituloDto>>.Fail("No se encontraron contenidos disponibles.", HttpStatusCodes.NotFound);
            }
            else
            {
                return ServiceResult<List<ContenidoCapituloDto>>.Ok(contenidosDto, "Contenidos obtenidos exitosamente.");
            }
        }

        public async Task<ServiceResult<ContenidoCapituloDto>> ObtenerCapituloAsync(int Id)
        {
            var Capitulo = await _context.ContenidoCapitulos.FirstOrDefaultAsync(cap => cap.IdContenido == Id);
            if (Capitulo.Titulo == null || Capitulo.Subtitulo == null || Capitulo.HtmlBody == null)
            {
                return ServiceResult<ContenidoCapituloDto>.Fail("El capitulo no cumple con los estandares de contenido", HttpStatusCodes.BadRequest); 
            }
            else
            {
                var CapituloDto = _mapper.Map<ContenidoCapituloDto>(Capitulo); 
                return ServiceResult<ContenidoCapituloDto>.Ok(CapituloDto, "operacion realizada con exito"); 
            }
        }
    }
}
