# Navegacion de la Guia

## Jerarquia academica (nodos)
- Orden: Modulo -> SubModulo -> Tema -> SubTema -> Leccion.
- Cada entidad implementa `INodoJerarquico` (`src/ACC.Shared/Interfaces/INodoJerarquico.cs`), lo que estandariza `Id`, `Nombre`, `Descripcion`, `IdPadre` y `Tipo`.
- Implementaciones concretas: `src/ACC.Data/Entities/Modulo.cs`, `SubModulo.cs`, `Tema.cs`, `SubTema.cs`, `Leccion.cs`.
- El tipo se enumera en `src/ACC.Shared/Enums/TipoNodoJerarquico.cs`.

## Contrato compartido
- DTO unificado: `src/ACC.Shared/DTOs/NodoJerarquicoDto.cs` implementa la misma interfaz para transportar nodos al cliente.
- Los resultados de servicio viajan envueltos en `ServiceResult<T>` (`src/ACC.Shared/Core`), lo que aporta `Success`, `Message` y `StatusCode`.
- AutoMapper mantiene paridad entre entidades y DTOs en `src/ACC.API/Extensions/ACCmappingProfile.cs`.

## Backend: servicio de navegacion
- Interface: `src/ACC.Shared/Interfaces/INavegacionContenidoService.cs`.
- Implementacion: `src/ACC.API/Services/NavegacionContenidoService.cs`.
  - `ObtenerModulosAsync`: lista Modulos raiz.
  - `ObtenerHijosAsync(tipoPadre, idPadre)`: cambia por `TipoNodoJerarquico` y proyecta a `NodoJerarquicoDto` con LINQ/EF.
  - `ObtenerPadreAsync`: valida que el nodo tenga padre y lo proyecta.
  - `ObtenerRutaDesdeRaizAsync`: sube por padres hasta Modulo para construir breadcrumbs.
  - `ObtenerLeccionAsync`: retorna `LeccionDto` ya mapeado, usado por el renderizador RDL.
  - `RegistrarUltimaVisitaTemaAsync`: marca `Tema.UltimaVisita` con `DateTime.UtcNow`.
- API: `src/ACC.API/Controllers/NavegacionContenidoController.cs` publica rutas `api/NavegacionContenido/*`:
  - GET `modulos`, `hijos/{tipo}/{id}`, `padre/{tipo}/{id}`, `ruta/{tipo}/{id}`, `leccion/{leccionId}`.
  - POST `tema/{id}/registrar-ultima-visita`.

## Cliente Blazor: consumo y UI
- Cliente HTTP: `ACC.WebApp/ACC.WebApp.Client/Services/NavegacionContenidoClient.cs` expone metodos homonimos que llaman al API y devuelven `ServiceResult`.
- Pantalla raiz de la guia: `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/GuiaNavegador.razor`.
  - Al iniciar carga en paralelo modulos (Navegacion) y examenes (ExamenesService).
  - Muestra tarjetas de modulos y permite seleccionar uno.
  - Al seleccionar, inserta `GuiaMainComponent` con `Tipo=Modulo` e `Id` del modulo.
- Navegacion descendente: `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/GuiaMainComponent.razor`.
  - Lee `Tipo` e `Id` desde la ruta `/contenido/{Tipo}/{Id}`.
  - Valida el enum, llama `ObtenerHijosAsync` y lista los nodos hijos.
  - Para `SubTema`, consulta progreso en `ProgresoUsuarioClient` y muestra estado; al click navega:
    - Si `Tipo` destino es `Leccion` => `/leccion/{id}`.
    - Si no, reusa la misma pagina con el nuevo `Tipo`.
  - Registra progreso de SubTema al navegar cuando hay usuario autenticado.
- Render final de leccion:
  - `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Lecciones/LeccionView.razor` llama `ObtenerLeccionAsync` y pasa el DTO a `RDL.razor`.
  - `RDL.razor` pinta las secciones en el orden definido por `OrdenSecciones`.

## Flujo resumido end-to-end
1) `/Guia` carga modulos y examenes; usuario elige un Modulo.
2) `/contenido/Modulo/{id}` lista SubModulos; al elegir uno navega a `/contenido/SubModulo/{id}` y asi sucesivamente.
3) `/contenido/SubTema/{id}` muestra lecciones; clic abre `/leccion/{id}`.
4) `LeccionView` obtiene `LeccionDto`; `RDL` renderiza secciones (teoria, practica, actividad, compilador, video, CharpTip/Dialog).
5) Opcional: `RegistrarUltimaVisitaTemaAsync` actualiza la marca temporal cuando el cliente lo invoque.

## Notas de mantenimiento
- Al agregar un nuevo nivel de jerarquia, extender `TipoNodoJerarquico`, `INodoJerarquico`, las entidades y `NavegacionContenidoService` (hijos, padre, ruta), y reflejarlo en `GuiaNavegador`/`GuiaMainComponent`.
- Mantener nombres de ruta coherentes con el string del enum porque el cliente arma URLs con `Tipo.ToString()`.
- Si se amplian datos de `LeccionDto`, actualizar el perfil de AutoMapper y el renderizador RDL para evitar campos huérfanos.
