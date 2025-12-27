using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Utils; 

/*
 Recordar, Comprender, Aplicar, Analizar, Evaluar, Crear
 */
public static class NivelesBloom
{
    public const string Recordar = "Recordar";
    public const string Comprender = "Comprender";
    public const string Aplicar = "Aplicar";
    public const string Analizar = "Analizar";
    public const string Evaluar = "Evaluar";
    public const string Crear = "Crear";

    public static readonly IReadOnlyDictionary<Enums.TipoNivelesBloom, string> 
        Map =
        new Dictionary<Enums.TipoNivelesBloom, string>
        {
            { Enums.TipoNivelesBloom.Recordar, Recordar },
            { Enums.TipoNivelesBloom.Comprender, Comprender },
            { Enums.TipoNivelesBloom.Aplicar, Aplicar },
            { Enums.TipoNivelesBloom.Analizar, Analizar },
            { Enums.TipoNivelesBloom.Evaluar, Evaluar },
            { Enums.TipoNivelesBloom.Crear, Crear }
        };
}
