using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs; 

public class AulaConfigDto
{
    public int Id { get; set; }                 // map de Aula.AulaId
    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }
    public bool CerrarAula { get; set; }
    public bool ArchivarAula { get; set; }

    // Visor/compat: si usas catálogo, puedes exponer ambos:
    public int? SubModuloId { get; set; }       // entrada/salida si editas por id
    public string? Submodulo { get; set; }      // salida: nombre legible
}
