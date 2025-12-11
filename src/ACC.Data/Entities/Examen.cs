using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities; 

/// <summary>
/// Examen generico para que los docentes y administradores puedan crear examenes fuera del contexto de modulos o submodulos
/// </summary>
public class Examen
{
    /// Propiedades del examen:
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Descripcion {  get; set; } = default!;
    public int NumeroPreguntas { get; set; }
    public int PuntajeAprobacion { get; set; }
    public string ContenidoHtml { get; set; } = default!;

    /// Relaciones:
    public ICollection<ExamenIntento> Intentos { get; set; } = []; 
}
