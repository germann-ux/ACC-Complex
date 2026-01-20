using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface INavegacionContenidoService
    {
        Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerHijosAsync(int idPadre, TipoNodoJerarquico tipoPadre);
        Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerModulosAsync();
        Task<ServiceResult<NodoJerarquicoDto>> ObtenerPadreAsync(int id, TipoNodoJerarquico tipoActual);
        Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerRutaDesdeRaizAsync(int id, TipoNodoJerarquico tipoActual);
        Task<ServiceResult<LeccionDto>> ObtenerLeccionAsync(int idLeccion);
        Task<ServiceResult> RegistrarUltimaVisitaTemaAsync(int idTema);
    }
}
