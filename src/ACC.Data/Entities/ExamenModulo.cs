using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities;

/// <summary>
/// Examen asociado a un modulo, se desbloquea al completar los examenes de sus submodulos hijos
/// </summary>
public class ExamenModulo
{
    /// Propiedades del examen:
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public int NumeroPreguntas { get; set; }
    public int PuntajeAprobacion { get; set; }
    public string ContenidoHtml { get; set; } = default!;

    /// Relaciones:
    public int ModuloId { get; set; }
    public Modulo Modulo { get; set; } = null!;
    public ICollection<ExamenIntento> Intentos { get; set; } = [];
}
