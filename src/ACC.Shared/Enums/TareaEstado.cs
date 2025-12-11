using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Enums; 

public enum TareaEstado
{
    NoIniciada = 0, 
    EnProgreso = 1,
    Completada = 2,
}

public enum TareaEstadoEntrega
{
    NoEntregada = 0,
    Entregada = 1,
    EntregadaTarde = 2,
    Calificada = 3,
    DevueltaParaCorreccion = 4,
}