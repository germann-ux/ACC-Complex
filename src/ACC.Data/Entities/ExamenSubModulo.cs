using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

/// <summary>
/// Examen asociado a un submodulo, se desbloquea al marcar las actividades del submodulo como completadas
/// </summary>
public class ExamenSubModulo
{
    /// Propiedades del Examen:
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public int NumeroPreguntas { get; set; }
    public int PuntajeAprobacion { get; set; }
    public string ContenidoHtml { get; set; } = default!;

    /// Relaciones:
    public int SubModuloId { get; set; }
    public SubModulo SubModulo { get; set; } = null!;
    public ICollection<ExamenIntento> Intentos { get; set; } = []; 
}
