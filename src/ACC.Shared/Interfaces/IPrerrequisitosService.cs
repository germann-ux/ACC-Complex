using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces; 

public readonly record struct ExamenRef(ExamenTipo Tipo, int RefId);

public interface IPrerrequisitosService
{
    //Task EvaluarDesbloqueosPorProgresoAsync(string userId, int subTemaId);
    //Task EvaluarDesbloqueosPorAprobacionAsync(string userId, int examenSubModuloId);

    //Task<bool> EstaHabilitadoAsync(string userId, ExamenRef examen);
    // Reactivo por PROGRESO: se usa al completar un subtema.
    Task EvaluarDesbloqueosPorProgresoAsync(string userId, int subTemaId);

    // Reactivo por APROBACIÓN: se usa tras aprobar un examen de submódulo.
    Task EvaluarDesbloqueosPorAprobacionAsync(string userId, int examenSubModuloId);

    // Lectura de estado.
    Task<bool> EstaHabilitadoAsync(string userId, ExamenRef examen);

    // Compat (invocación explícita por submódulo, sin subtema): re-calcula y habilita si corresponde.
    Task EvaluarDesbloqueoSubmoduloAsync(string userId, int subModuloId);
}
