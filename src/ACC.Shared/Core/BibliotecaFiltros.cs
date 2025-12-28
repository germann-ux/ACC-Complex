using ACC.Shared.Enums;

namespace ACC.Shared.Core; 

public sealed class BibliotecaFiltros
{
    public HashSet<TipoContenidoCapitulo> Tipos { get; } = new();
    public HashSet<NivelContenido> Niveles { get; } = new();
    public HashSet<DificultadContenido> Dificultades { get; } = new();

    public bool IsEmpty => Tipos.Count == 0 && Niveles.Count == 0 && Dificultades.Count == 0;

    public BibliotecaFiltros Clone()
    {
        var c = new BibliotecaFiltros();
        foreach (var t in Tipos) c.Tipos.Add(t);
        foreach (var n in Niveles) c.Niveles.Add(n);
        foreach (var d in Dificultades) c.Dificultades.Add(d);
        return c;
    }
}