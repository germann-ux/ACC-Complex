namespace ACC.WebApp.Client.Services;

public sealed class AgendaRealtimeNotifier
{
    public event Action? Changed;

    public void NotifyChanged()
    {
        Changed?.Invoke();
    }
}
