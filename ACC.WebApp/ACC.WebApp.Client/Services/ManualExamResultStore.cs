using ACC.Shared.Enums;
using ACC.WebApp.Client.Components.Pages.Guia.Components.Examenes.Manual;

namespace ACC.WebApp.Client.Services;

public sealed class ManualExamResultStore
{
    private ManualExamResultSnapshot? currentSnapshot;

    public void Set(ManualExamResultSnapshot snapshot)
    {
        currentSnapshot = snapshot;
    }

    public ManualExamResultSnapshot? Get(ExamenTipo tipo, int refId)
    {
        if (currentSnapshot is null)
            return null;

        return currentSnapshot.Tipo == tipo && currentSnapshot.RefId == refId
            ? currentSnapshot
            : null;
    }

    public void Clear(ExamenTipo tipo, int refId)
    {
        if (currentSnapshot is null)
            return;

        if (currentSnapshot.Tipo == tipo && currentSnapshot.RefId == refId)
        {
            currentSnapshot = null;
        }
    }
}
