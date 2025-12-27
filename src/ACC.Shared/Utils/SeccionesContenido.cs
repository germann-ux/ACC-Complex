using ACC.Shared.Enums;

namespace ACC.Shared.Utils;

public static class SeccionesContenido
{
    public const string Teoria = "teoria";
    public const string Practica = "practica";
    public const string Ejemplo = "ejemplo";
    public const string Actividad = "actividad";
    public const string Compilador = "compilador";
    public const string Evaluacion = "evaluacion";
    public const string CharpTip = "charpTip";
    public const string CharpDialog = "charpDialog";

    public static readonly IReadOnlyDictionary<TipoSeccionContenido, string> 
        Map =
        new Dictionary<TipoSeccionContenido, string>
        {
            { TipoSeccionContenido.Teoria, Teoria },
            { TipoSeccionContenido.Practica, Practica },
            { TipoSeccionContenido.Ejemplo, Ejemplo },
            { TipoSeccionContenido.Actividad, Actividad },
            { TipoSeccionContenido.Compilador, Compilador },
            { TipoSeccionContenido.Evaluacion, Evaluacion },
            { TipoSeccionContenido.CharpTip, CharpTip },
            { TipoSeccionContenido.CharpDialog, CharpDialog }
        };
}
