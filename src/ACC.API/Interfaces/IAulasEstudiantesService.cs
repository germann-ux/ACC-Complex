using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.API.Interfaces;

/// <summary>
/// Interfaz de servicio para la gestión de estudiantes en aulas.
/// </summary>
public interface IAulasEstudiantesService
{
    Task<ServiceResult<IReadOnlyList<EstudianteListadoDto>>> GetEstudiantesAsync(
        int aulaId,
        string currentUserId,
        bool esAdministrador = false,
        CancellationToken cancellationToken = default);
}
