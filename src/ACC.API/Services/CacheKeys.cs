using ACC.Shared.Enums;

namespace ACC.API.Services;

internal static class CacheKeys
{
    public static string NavigationModulos() => "nav:modulos:v1";

    public static string NavigationHijos(TipoNodoJerarquico tipoPadre, int idPadre) => $"nav:hijos:{tipoPadre}:{idPadre}:v1";

    public static string NavigationPadre(TipoNodoJerarquico tipoActual, int id) => $"nav:padre:{tipoActual}:{id}:v1";

    public static string NavigationRuta(TipoNodoJerarquico tipoActual, int id) => $"nav:ruta:{tipoActual}:{id}:v1";

    public static string NavigationLeccion(int idLeccion) => $"nav:leccion:{idLeccion}:v1";

    public static string NavigationSubTemaLecciones(int subTemaId) => $"nav:hijos:{TipoNodoJerarquico.SubTema}:{subTemaId}:v1";

    public static string PrerequisitoEstado(string userId, ExamenTipo tipo, int refId) => $"prerreq:enabled:{userId}:{tipo}:{refId}:v1";

    public static string LeccionPublicada(int idLeccion) => $"leccion:publicada:{idLeccion}:v1";
}

