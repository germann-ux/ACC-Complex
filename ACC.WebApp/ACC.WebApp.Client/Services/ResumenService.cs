using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.WebApp.Client.Interfaces;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services; 

public sealed class ResumenService(HttpClient http) : IResumenService
{
    private const string Root = ServiceRoots.ACC_API_Url;

    public async Task<ApplicationUserDto?> GetUsuarioAsync(string userId, CancellationToken ct)
        => (await http.GetFromJsonAsync<ServiceResult<ApplicationUserDto>>($"{Root}Usuario/usuario/{userId}", ct))?.Data;

    public async Task<(TareasPendientesResumenDto? resumen, List<TareaAlumnoListadoDto> asignadas, List<TareaPersonalDto> personales)> GetTareasAsync(string userId, CancellationToken ct)
    {
        var resumenTask = http.GetFromJsonAsync<ServiceResult<TareasPendientesResumenDto>>($"{Root}TareasAlumno/resumen/{userId}", ct);
        var asignadasTask = http.GetFromJsonAsync<ServiceResult<List<TareaAlumnoListadoDto>>>($"{Root}TareasAlumno/listado/{userId}", ct);
        var personalesTask = http.GetFromJsonAsync<ServiceResult<List<TareaPersonalDto>>>($"{Root}Tarea/personal/lista/{userId}", ct);

        await Task.WhenAll(resumenTask!, asignadasTask!, personalesTask!);

        var resumen = resumenTask?.Result?.Data;
        var asignadas = asignadasTask?.Result?.Data ?? [];
        var personales = personalesTask?.Result?.Data ?? [];
        return (resumen, asignadas, personales);
    }

    public async Task<TipDto?> GetTipAsync(CancellationToken ct)
        => await http.GetFromJsonAsync<TipDto>($"{Root}Tips/random", ct);

    public async Task<int?> GetUltimoTemaIdAsync(string userId, CancellationToken ct)
        => await http.GetFromJsonAsync<int?>($"{Root}ProgresoUsuario/ultimo/{userId}", ct);

    public async Task<List<NotaTiempoDto>> GetNotasTiempoAsync(string userId, CancellationToken ct)
        => await http.GetFromJsonAsync<List<NotaTiempoDto>>($"{Root}Evaluaciones/serie/{userId}", ct) ?? [];
}
