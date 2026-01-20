using ACC.Data;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Core;
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

        public async Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerHijosAsync(int idPadre, TipoNodoJerarquico tipoPadre)
        {
            try
            {
                if (idPadre <= 0)
                    return ServiceResult<List<NodoJerarquicoDto>>.Fail("El ID debe ser mayor a 0.");

                List<NodoJerarquicoDto>? hijos = tipoPadre switch
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

                    _ => null
                };

                if (hijos is null)
                    return ServiceResult<List<NodoJerarquicoDto>>.Fail("Tipo de nodo no valido.");

                if (hijos.Count == 0)
                    return ServiceResult<List<NodoJerarquicoDto>>.NotFound("No se encontraron hijos para el nodo especificado.");

                return ServiceResult<List<NodoJerarquicoDto>>.Ok(hijos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<NodoJerarquicoDto>>.Error(ex);
            }
        }

        public async Task<ServiceResult<NodoJerarquicoDto>> ObtenerPadreAsync(int id, TipoNodoJerarquico tipoActual)
        {
            try
            {
                if (id <= 0)
                    return ServiceResult<NodoJerarquicoDto>.Fail("El ID debe ser mayor a 0.");

                if (tipoActual == TipoNodoJerarquico.Modulo)
                    return ServiceResult<NodoJerarquicoDto>.NotFound("No se encontro el nodo padre.");

                if (!TienePadre(tipoActual))
                    return ServiceResult<NodoJerarquicoDto>.Fail("Tipo de nodo no valido.");

                var padre = await ObtenerPadreNodoAsync(id, tipoActual);
                if (padre is null)
                    return ServiceResult<NodoJerarquicoDto>.NotFound("No se encontro el nodo padre.");

                return ServiceResult<NodoJerarquicoDto>.Ok(padre);
            }
            catch (Exception ex)
            {
                return ServiceResult<NodoJerarquicoDto>.Error(ex);
            }
        }

        public async Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerModulosAsync()
        {
            try
            {
                var modulos = await _Context.Modulos
                    .Select(m => new NodoJerarquicoDto(m.Id_Modulo, m.NombreModulo, null, m.DescripcionModulo, TipoNodoJerarquico.Modulo))
                    .ToListAsync();

                if (modulos.Count == 0)
                    return ServiceResult<List<NodoJerarquicoDto>>.NotFound("No se encontraron modulos.");

                return ServiceResult<List<NodoJerarquicoDto>>.Ok(modulos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<NodoJerarquicoDto>>.Error(ex);
            }
        }

        public async Task<ServiceResult<LeccionDto>> ObtenerLeccionAsync(int idLeccion)
        {
            try
            {
                if (idLeccion <= 0)
                    return ServiceResult<LeccionDto>.Fail("El ID de la lección debe ser mayor a 0.");

                var leccion = await _Context.Lecciones.FirstOrDefaultAsync(le => le.IdLeccion == idLeccion);
                if (leccion is null)
                    return ServiceResult<LeccionDto>.NotFound("No se encontró la lección especificada.");

                var lecciondto = _mapper.Map<LeccionDto>(leccion);
                return ServiceResult<LeccionDto>.Ok(lecciondto);
            }
            catch (Exception ex)
            {
                return ServiceResult<LeccionDto>.Error(ex);
            }
        }
        public async Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerRutaDesdeRaizAsync(int id, TipoNodoJerarquico tipoActual)
        {
            try
            {
                if (id <= 0)
                    return ServiceResult<List<NodoJerarquicoDto>>.Fail("El ID debe ser mayor a 0.");

                if (tipoActual == TipoNodoJerarquico.Modulo)
                    return ServiceResult<List<NodoJerarquicoDto>>.NotFound("No se encontro una ruta desde la raiz para el nodo especificado.");

                if (!TienePadre(tipoActual))
                    return ServiceResult<List<NodoJerarquicoDto>>.Fail("Tipo de nodo no valido.");

                var ruta = new List<NodoJerarquicoDto>();

                while (tipoActual != TipoNodoJerarquico.Modulo)
                {
                    var padre = await ObtenerPadreNodoAsync(id, tipoActual);
                    if (padre == null) break;

                    ruta.Insert(0, padre);
                    id = padre.Id;
                    tipoActual = padre.Tipo;
                }

                if (ruta.Count == 0)
                    return ServiceResult<List<NodoJerarquicoDto>>.NotFound("No se encontro una ruta desde la raiz para el nodo especificado.");

                return ServiceResult<List<NodoJerarquicoDto>>.Ok(ruta);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<NodoJerarquicoDto>>.Error(ex);
            }
        }

        public async Task<ServiceResult> RegistrarUltimaVisitaTemaAsync(int idTema)
        {
            try
            {
                if (idTema <= 0)
                    return ServiceResult.Fail("El ID debe ser mayor a 0.");

                var tema = await _Context.Temas.FindAsync(idTema);
                if (tema == null)
                    return ServiceResult.NotFound("Tema no encontrado.");

                tema.UltimaVisita = DateTime.UtcNow;
                await _Context.SaveChangesAsync();
                return ServiceResult.Ok("Fecha de ultima visita actualizada.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(ex);
            }
        }

        private static bool TienePadre(TipoNodoJerarquico tipoActual)
        {
            return tipoActual is TipoNodoJerarquico.SubModulo
                or TipoNodoJerarquico.Tema
                or TipoNodoJerarquico.SubTema
                or TipoNodoJerarquico.Leccion;
        }

        private async Task<NodoJerarquicoDto?> ObtenerPadreNodoAsync(int id, TipoNodoJerarquico tipoActual)
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
    }
}
