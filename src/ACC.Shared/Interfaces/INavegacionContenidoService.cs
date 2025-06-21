using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface INavegacionContenidoService
    {
        Task<List<NodoJerarquicoDto>> ObtenerHijosAsync(int idPadre, TipoNodoJerarquico tipoPadre);
        Task<List<NodoJerarquicoDto>> ObtenerModulosAsync(); 
        Task<NodoJerarquicoDto?> ObtenerPadreAsync(int id, TipoNodoJerarquico tipoActual);
        Task<List<NodoJerarquicoDto>> ObtenerRutaDesdeRaizAsync(int id, TipoNodoJerarquico tipoActual);
        Task<LeccionDto> ObtenerLeccionAsync(int idLeccion);
        Task<bool> RegistrarUltimaVisitaTemaAsync(int idTema); 
    }
}
