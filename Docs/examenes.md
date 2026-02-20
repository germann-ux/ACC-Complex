# Sistema de examenes y desbloqueos

## Pieza clave en la UI
- **BtnCompletarSubtema.razor** (`ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/BtnCompletarSubtema.razor`)
  - Muestra un boton “Completar” por subtema.
  - Al cargarse, consulta `ProgresoUsuarioClient.ObtenerEstadoSubtema` para pintar estado inicial.
  - Al hacer click: marca el subtema como completado (`MarcarSubtemaComoCompletadoAsync`) y, si hay usuario autenticado, consulta si el examen de SubModulo ya quedo habilitado (`ExamenSubModuloHabilitadoAsync`). Mensaje varia segun resultado.

- **ExamenesMain.razor** (`ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Examenes/ExamenesMain.razor`)
  - Recibe listas de `ExamenSubModuloDto` y `ExamenModuloDto`.
  - Renderiza tarjetas `Examen` para cada elemento y decide la ruta destino: `/examen/ExamenSubModulo/{id}` o `/examen/ExamenModulo/{id}`.

- **Examen.razor** (`ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Examenes/Examen.razor`)
  - Parámetros: `Tipo` (`ExamenTipo`), `RefId` (SubModuloId | ModuloId | ExamenId), nombre, descripcion.
  - En `OnInitializedAsync` pregunta a `ProgresoUsuarioClient.ExamenHabilitadoAsync(userId, Tipo, RefId)`.
  - Si está habilitado, activa el botón “¡Enfrentar el Desafío!” y propaga `RefId` con `OnRealizarExamen`; si no, muestra estado bloqueado.

## Cliente que orquesta desbloqueos y estado
- **ProgresoUsuarioClient** (`ACC.WebApp/ACC.WebApp.Client/Services/ProgresoUsuarioClient.cs`)
  - Guarda progreso y marca subtemas completos.
  - Exponer lectura de desbloqueo genérico `ExamenHabilitadoAsync(userId, tipo, refId)` y atajos por tipo.
  - Usa endpoint `api/ProgresoUsuario/examen-habilitado/{userId}/{tipo}/{refId}`.

- **ExamenesServiceClient** (`ACC.WebApp/ACC.WebApp.Client/Services/ExamenesServiceClient.cs`)
  - Solo trae catálogos de exámenes (SubMódulo, Módulo) y los intentos; no decide desbloqueos.

## Backend: cómo se determina si un examen está habilitado
- **ProgresoUsuarioController** (`src/ACC.API/Controllers/ProgresoUsuarioController.cs`)
  - Endpoint GET `examen-habilitado/{userId}/{tipo}/{refId}` devuelve `{ ExamenHabilitado: bool }`.
  - Delegado a `IProgresoUsuarioService.ExamenHabilitadoAsync`.

- **ProgresoUsuarioService** (`src/ACC.API/Services/ProgresoUsuarioService.cs`)
  - Al marcar un subtema completo llama `IPrerrequisitosService.EvaluarDesbloqueosPorProgresoAsync` (reactivo).
  - La consulta `ExamenHabilitadoAsync` también delega en `IPrerrequisitosService`.

- **PrerrequisitosService** (`src/ACC.API/Services/PrerrequisitosService.cs`)
  - Regla A (progreso): Si el usuario completó **todos los subtemas** de un SubModulo → habilita examen de SubModulo (`ExamenTipo.SubModulo`) con un upsert en `ExamenesHabilitados`.
  - Regla B (aprobación): Si el usuario aprobó **todos los exámenes de SubModulo** de un Módulo → habilita examen de Módulo (`ExamenTipo.Modulo`).
  - Compatibilidad: `EvaluarDesbloqueoSubmoduloAsync` recalcula habilitación de un submódulo puntual.
  - Persistencia: tabla `ExamenesHabilitados` guarda `UsuarioId`, `Tipo`, `RefId`, `Habilitado`, `FechaHabilitacion`, `ReglaFuente`.
  - Lectura: `EstaHabilitadoAsync` solo revisa esa tabla (consulta rápida, sin recalcular).

## Flujo de desbloqueo paso a paso
1) El usuario marca un subtema como completado desde `BtnCompletarSubtema`.
2) Backend (`ProgresoUsuarioService`) persiste el progreso y llama a `EvaluarDesbloqueosPorProgresoAsync`.
3) `PrerrequisitosService` verifica si ya se completaron todos los subtemas del submódulo; si sí, hace upsert para habilitar el examen de ese submódulo.
4) En la UI, `Examen.razor` consulta `ExamenHabilitadoAsync`; si está en `ExamenesHabilitados`, muestra botón activo.
5) Cuando el usuario aprueba un examen de submódulo, otro flujo (no detallado aquí) llama `EvaluarDesbloqueosPorAprobacionAsync`; si todos los submódulos del módulo están aprobados, se habilita el examen de módulo.

## Notas de mantenimiento
- Cualquier nueva regla de desbloqueo debe terminar en `PrerrequisitosService.UpsertHabilitacionAsync` para que la UI siga funcionando sin cambios.
- Si se agrega un nuevo `ExamenTipo`, extender: enum `ExamenTipo`, DTO `ExamenRef`, endpoints del controller, validación de rutas en `ExamenesMain` y lógica en `ProgresoUsuarioClient`.
- `Examen.razor` asume que `RefId` > 0 y usuario autenticado; si se soportan exámenes públicos, ajustar la guardia y el endpoint.
