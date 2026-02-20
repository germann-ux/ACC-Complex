# RDL y lecciones dinamicas

## Idea general
- `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Lecciones/LeccionView.razor` consume `NavegacionContenidoClient` para obtener `LeccionDto` desde `ACC.API` (controlador `src/ACC.API/Controllers/NavegacionContenidoController.cs` y servicio `src/ACC.API/Services/NavegacionContenidoService.cs`).
- El mapeo de EF Core a DTO se hace via AutoMapper en `src/ACC.API/Extensions/ACCmappingProfile.cs`, asegurando paridad entre `Leccion` y `LeccionDto`.
- El renderizado final se concentra en `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Lecciones/RDL.razor` (Renderizador Dinamico de Lecciones), eliminando plantillas fijas y permitiendo que la vista dependa del orden de secciones definido en datos.

## Contrato de datos de leccion
- Entidad: `src/ACC.Data/Entities/Leccion.cs` guarda el HTML de `Teoria`, `Ejemplo` y `Practica`, mas flags (`TieneActividad`, `TieneCompilador`, `TieneVideo`) y metadata (`NivelBloom`, `UrlActividad`, `VideoId`).
- DTO: `src/ACC.Shared/DTOs/LeccionDto.cs` expone el mismo contrato al cliente.
- Orden: `OrdenSecciones : List<string>` se persiste como arreglo json. Cada token debe coincidir con los literales en `src/ACC.Shared/Utils/SeccionesContenido.cs`; el diccionario `Map` alinea `TipoSeccionContenido` con el string que consume el cliente.

## Tabla de secciones
| Token en `OrdenSecciones` | Origen de datos en `LeccionDto` | Componente renderizado | Condicion de activacion |
| --- | --- | --- | --- |
| `charpDialog` | `CharpDialog` (string html) | `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Contenido/CharpDialog.razor` | Se renderiza siempre que el token exista; muestra overlay con retardo inicial. |
| `charpTip` | `CharpTip` (string html) | `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Contenido/CharpTip.razor` | Igual que arriba; animacion de entrada. |
| `teoria` | `Teoria` | Bloque `MarkupString` dentro de `RDL.razor` | Se omite si el campo es vacio. |
| `ejemplo` | `Ejemplo` | Bloque `MarkupString` dentro de `RDL.razor` | Se omite si el campo es vacio. |
| `practica` | `Practica` | Bloque `MarkupString` dentro de `RDL.razor` | Se omite si el campo es vacio. |
| `actividad` | `UrlActividad` + `TieneActividad` | Boton que abre `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Modals/ModalActividades.razor` | Requiere `TieneActividad == true` y `UrlActividad` no vacio. |
| `compilador` | `TieneCompilador` | `ACC.WebApp/ACC.WebApp.Client/Components/Pages/ACC-Compiler/CompiladorACC.razor` | Requiere `TieneCompilador == true`. |
| `video` | `VideoId` + `TieneVideo` | `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Lecciones/LiteYouTube.razor` | Requiere `VideoId` no vacio. |

## Decisiones de diseno clave
- **Orden controlado por datos**: el flujo pedagogico se decide en base a `OrdenSecciones`, permitiendo cambiar la narrativa de una leccion sin redeploy del cliente.
- **HTML seguro bajo responsabilidad del backend**: `RDL.razor` inyecta `Teoria`, `Ejemplo` y `Practica` usando `MarkupString`; se asume que el HTML almacenado en BD ya fue validado o sanitizado.
- **Flags para recursos pesados**: actividades externas (`ModalActividades`) y el compilador ACC se encienden solo si los flags vienen en `LeccionDto`, evitando iframes y CodeMirror cuando no se requieren.
- **Modal unico**: `ModalActividades.razor` se instancia una vez fuera del bucle; los botones solo actualizan la url y abren el modal, reduciendo recreacion de iframes.
- **Compatibilidad enum <-> string**: `SeccionesContenido.Map` es la fuente unica de verdad entre `TipoSeccionContenido` (backend) y los tokens de `OrdenSecciones` (frontend), lo que permite validar entradas antes de persistir.
- **Manejo de errores de configuracion**: si llega un token desconocido se muestra `InfoComponent` con mensaje de seccion no reconocida, ayudando a detectar datos mal cargados.
- **Contexto pedagogico visible**: el nivel de Bloom se muestra arriba de cada leccion (`nivel-bloom @Leccion.NivelBloom`) para que los estudiantes sepan la expectativa cognitiva.

## Notas para mantenimiento
- Antes de agregar una nueva seccion, incluir su literal en `SeccionesContenido`, extender `TipoSeccionContenido`, mapear en AutoMapper si requiere nuevos campos y anadir el caso en `RDL.razor`.
- Mantener los strings de `OrdenSecciones` en minusculas para evitar diferencias culturales al serializar (el render usa comparacion exacta).
- El `CompiladorACC` postea el codigo a `ServiceRoots.ACC_COMPILER_Url` despues de leer el contenido actualizado de CodeMirror via JS interop; si cambia la ruta del servicio, actualizar `ACC.Shared.Core/ServiceRoots.cs`.
