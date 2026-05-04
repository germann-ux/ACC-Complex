using ACC.Shared.Enums;

namespace ACC.Shared.Utils;

public static class BloquesLeccionCatalogo
{
    public static readonly IReadOnlyDictionary<TipoBloqueLeccion, string> Nombres =
        new Dictionary<TipoBloqueLeccion, string>
        {
            [TipoBloqueLeccion.TextoHtml] = "Texto enriquecido",
            [TipoBloqueLeccion.Imagen] = "Imagen",
            [TipoBloqueLeccion.Video] = "Video",
            [TipoBloqueLeccion.Mermaid] = "Diagrama Mermaid",
            [TipoBloqueLeccion.CharpTip] = "CharpTip",
            [TipoBloqueLeccion.CharpDialog] = "CharpDialog",
            [TipoBloqueLeccion.ActividadExterna] = "Actividad externa",
            [TipoBloqueLeccion.Compilador] = "Compilador",
            [TipoBloqueLeccion.OpcionMultiple] = "Opcion multiple",
            [TipoBloqueLeccion.VerdaderoFalso] = "Verdadero/Falso",
            [TipoBloqueLeccion.RespuestaCorta] = "Respuesta corta",
            [TipoBloqueLeccion.Checklist] = "Checklist"
        };

    public static readonly IReadOnlyDictionary<TipoBloqueLeccion, string> ConfiguracionInicial =
        new Dictionary<TipoBloqueLeccion, string>
        {
            [TipoBloqueLeccion.TextoHtml] = "{\"html\":\"<p>Nuevo contenido</p>\"}",
            [TipoBloqueLeccion.Imagen] = "{\"url\":\"\",\"alt\":\"\",\"caption\":\"\"}",
            [TipoBloqueLeccion.Video] = "{\"videoId\":\"\",\"proveedor\":\"youtube\"}",
            [TipoBloqueLeccion.Mermaid] = "{\"codigo\":\"flowchart TD\\nA[Inicio] --> B[Fin]\",\"descripcion\":\"\"}",
            [TipoBloqueLeccion.CharpTip] = "{\"texto\":\"\"}",
            [TipoBloqueLeccion.CharpDialog] = "{\"texto\":\"\"}",
            [TipoBloqueLeccion.ActividadExterna] = "{\"url\":\"\",\"textoBoton\":\"Ver actividad interactiva\"}",
            [TipoBloqueLeccion.Compilador] = "{\"lenguaje\":\"csharp\",\"codigoInicial\":\"\",\"instrucciones\":\"\"}",
            [TipoBloqueLeccion.OpcionMultiple] = "{\"pregunta\":\"\",\"opciones\":[\"Opcion 1\",\"Opcion 2\"],\"respuestaCorrecta\":\"Opcion 1\",\"retroalimentacion\":\"\"}",
            [TipoBloqueLeccion.VerdaderoFalso] = "{\"afirmacion\":\"\",\"respuestaCorrecta\":true,\"retroalimentacion\":\"\"}",
            [TipoBloqueLeccion.RespuestaCorta] = "{\"pregunta\":\"\",\"respuestaEsperada\":\"\",\"esAutoevaluacion\":true}",
            [TipoBloqueLeccion.Checklist] = "{\"items\":[\"Elemento 1\"]}"
        };
}
