using ACC.Shared.DTOs;

namespace ACC.WebApp.Client.Components.Pages.Guia.Components;

public sealed class GuiaTreeNodeViewModel
{
    public NodoJerarquicoDto Node { get; set; } = default!;
    public List<GuiaTreeNodeViewModel> Children { get; set; } = new();
    public bool IsRoot { get; set; }
    public bool IsActive { get; set; }
    public bool IsExpanded { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsInActivePath { get; set; }
}
