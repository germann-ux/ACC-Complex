namespace ACC.Shared.DTOs;

public sealed class LeccionAdminDto
{
    public int IdLeccion { get; set; }

    public int SubtemaId { get; set; }

    public string TituloLeccion { get; set; } = string.Empty;

    public string DescripcionLeccion { get; set; } = string.Empty;

    public string Teoria { get; set; } = string.Empty;

    public string Practica { get; set; } = string.Empty;

    public string Ejemplo { get; set; } = string.Empty;

    public string? CharpTip { get; set; }

    public string? CharpDialog { get; set; }

    public string? NivelBloom { get; set; }

    public bool TieneActividad { get; set; }

    public string? UrlActividad { get; set; }

    public bool TieneCompilador { get; set; }

    public bool TieneVideo { get; set; }

    public string? VideoId { get; set; }

    public List<string> OrdenSecciones { get; set; } = [];
}

