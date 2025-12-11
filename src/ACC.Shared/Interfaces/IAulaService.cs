using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces;
/// <summary>
/// Interfaz de servicio para la gestión de aulas.
/// </summary>
public interface IAulaService //TODO: me falta arreglar todo el sistema de los Anuncios con las tareas las aulas y etc con los usuarios docentes
{
    Task<ServiceResult<AulaConfigDto>> GetConfigAsync(
        int aulaId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<AulaConfigDto>> UpdateConfigAsync(
        int aulaId,
        AulaConfigUpdateDto updateDto,
        string currentUserId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<InvitacionGeneradaDto>> GenerarInvitacionAsync(
        int aulaId,
        string currentUserId,
        CancellationToken cancellationToken = default); 
}


