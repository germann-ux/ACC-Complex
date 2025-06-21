using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface INodoJerarquico
    {
        int Id { get; }
        string Nombre { get; }
        string Descripcion { get; }
        int? IdPadre { get; } // null si es raíz (ej. Módulo)
        TipoNodoJerarquico Tipo { get; }
    }
}
