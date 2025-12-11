using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs;

public class AulaConfigUpdateDto
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public bool? CerrarAula { get; set; }
    public bool? ArchivarAula { get; set; }
    public int? SubModuloId { get; set; }
}