using ACC.Data;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services 
{
    public class NavegacionContenidoService : INavegacionContenidoService
    {
        private readonly ACCDbContext _Context;
        private readonly IMapper _mapper;

        public NavegacionContenidoService(ACCDbContext db, IMapper mapper)
        {
            _Context = db;
            _mapper = mapper;
        }

        public async Task<List<NodoJerarquicoDto>> ObtenerHijosAsync(int idPadre, TipoNodoJerarquico tipoPadre)
        {
            return tipoPadre switch
            {
                TipoNodoJerarquico.Modulo => await _Context.SubModulos
                    .Where(s => s.Id_Modulo == idPadre)
                    .Select(s => new NodoJerarquicoDto(s.Id_SubModulo, s.NombreSubModulo, s.Id_Modulo, s.DescripcionSubModulo, TipoNodoJerarquico.SubModulo))
                    .ToListAsync(),

                TipoNodoJerarquico.SubModulo => await _Context.Temas
                    .Where(t => t.Id_SubModulo == idPadre)
                    .Select(t => new NodoJerarquicoDto(t.Id_Tema, t.NombreTema, t.Id_SubModulo, t.DescripcionTema, TipoNodoJerarquico.Tema))
                    .ToListAsync(),

                TipoNodoJerarquico.Tema => await _Context.SubTemas
                    .Where(st => st.Id_Tema == idPadre)
                    .Select(st => new NodoJerarquicoDto(st.Id_SubTema, st.NombreSubTema, st.Id_Tema, st.DescripcionSubTema, TipoNodoJerarquico.SubTema))
                    .ToListAsync(),

                TipoNodoJerarquico.SubTema => await _Context.Lecciones
                    .Where(l => l.SubtemaId == idPadre)
                    .Select(l => new NodoJerarquicoDto(l.IdLeccion, l.TituloLeccion, l.SubtemaId, l.DescripcionLeccion, TipoNodoJerarquico.Leccion))
                    .ToListAsync(),

                _ => []
            };
        }

        public async Task<NodoJerarquicoDto?> ObtenerPadreAsync(int id, TipoNodoJerarquico tipoActual)
        {
            return tipoActual switch
            {
                TipoNodoJerarquico.SubModulo => await _Context.SubModulos
                    .Where(s => s.Id_SubModulo == id)
                    .Select(s => new NodoJerarquicoDto(s.Modulo.Id_Modulo, s.Modulo.NombreModulo, null, s.DescripcionSubModulo, TipoNodoJerarquico.Modulo))
                    .FirstOrDefaultAsync(),

                TipoNodoJerarquico.Tema => await _Context.Temas
                    .Where(t => t.Id_Tema == id)
                    .Select(t => new NodoJerarquicoDto(t.SubModulo.Id_SubModulo, t.SubModulo.NombreSubModulo, t.SubModulo.Id_Modulo, t.DescripcionTema, TipoNodoJerarquico.SubModulo))
                    .FirstOrDefaultAsync(),

                TipoNodoJerarquico.SubTema => await _Context.SubTemas
                    .Where(st => st.Id_SubTema == id)
                    .Select(st => new NodoJerarquicoDto(st.Tema.Id_Tema, st.Tema.NombreTema, st.Tema.Id_SubModulo, st.DescripcionSubTema, TipoNodoJerarquico.Tema))
                    .FirstOrDefaultAsync(),

                TipoNodoJerarquico.Leccion => await _Context.Lecciones
                    .Where(l => l.IdLeccion == id)
                    .Select(l => new NodoJerarquicoDto(l.SubTema.Id_SubTema, l.SubTema.NombreSubTema, l.SubTema.Id_Tema, l.DescripcionLeccion, TipoNodoJerarquico.SubTema))
                    .FirstOrDefaultAsync(),

                _ => null
            };
        }

        public async Task<List<NodoJerarquicoDto>> ObtenerModulosAsync()
        {
            var modulos = await _Context.Modulos
                .Select(m => new NodoJerarquicoDto(m.Id_Modulo, m.NombreModulo, null, m.DescripcionModulo, TipoNodoJerarquico.Modulo))
                .ToListAsync();
            return modulos;
        }

        public async Task<LeccionDto> ObtenerLeccionAsync(int IdLeccion)
        {
            var leccion = await _Context.Lecciones.FirstOrDefaultAsync(le => le.IdLeccion == IdLeccion) ?? throw new Exception("Lección no encontrada");
            var lecciondto = _mapper.Map<LeccionDto>(leccion);
            return lecciondto;
        }
        public async Task<List<NodoJerarquicoDto>> ObtenerRutaDesdeRaizAsync(int id, TipoNodoJerarquico tipoActual)
        {
            var ruta = new List<NodoJerarquicoDto>();

            while (tipoActual != TipoNodoJerarquico.Modulo)
            {
                var padre = await ObtenerPadreAsync(id, tipoActual);
                if (padre == null) break;

                ruta.Insert(0, padre); // Insertamos al inicio para mantener el orden Módulo → ...
                id = padre.Id;
                tipoActual = padre.Tipo;
            }

            return ruta;
        }

        public async Task<bool> RegistrarUltimaVisitaTemaAsync(int idTema)
        {
            var tema = await _Context.Temas.FindAsync(idTema);
            if (tema == null) return false;

            tema.UltimaVisita = DateTime.UtcNow;
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
