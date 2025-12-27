using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs;

/// <summary>
/// CapituloDto ---> Representa un capítulo dentro de la estructura de contenidos de la biblioteca.
/// </summary>
public class CapituloDto
{
    public int IdCapitulo { get; set; }

    public string TituloCapitulo { get; set; } = string.Empty;
    public string SubtituloCapitulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;

    public int? ModuloId { get; set; }
    public int? SubmoduloId { get; set; }
    public int? TemaId { get; set; }
    public int? LeccionId { get; set; }

    public List<TagDto> Tags { get; set; } = new();
    public List<ContenidoCapituloDto> Contenidos { get; set; } = new();
}
