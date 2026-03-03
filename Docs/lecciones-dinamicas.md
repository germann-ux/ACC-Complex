# RDL y lecciones dinamicas

## Idea general

- `LeccionView.razor` obtiene `LeccionDto` a traves de `NavegacionContenidoClient`, que consume `NavegacionContenidoController` en `ACC.API`.
- AutoMapper mantiene paridad entre `Leccion` y `LeccionDto`.
- `RDL.razor` es el renderizador final y decide que mostrar segun `OrdenSecciones`.

## Cambio respecto al modelo viejo

La documentacion anterior describia una leccion centrada en un solo bloque HTML y en banderas como `TieneEvaluacion` o `IdEvaluacion`. Ese modelo ya no representa el contrato vigente del producto.

Hoy la leccion se construye con secciones separadas y ordenables:

- `Teoria`
- `Ejemplo`
- `Practica`
- `CharpTip`
- `CharpDialog`
- `NivelBloom`
- `TieneActividad` + `UrlActividad`
- `TieneCompilador`
- `TieneVideo` + `VideoId`
- `OrdenSecciones`

Si se revisan migraciones o comentarios viejos, todavia pueden verse rastros del contrato anterior. La fuente real de verdad para el producto actual es:

- `src/ACC.Data/Entities/Leccion.cs`
- `src/ACC.Shared/DTOs/LeccionDto.cs`
- `src/ACC.Shared/Utils/SeccionesContenido.cs`
- `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Lecciones/RDL.razor`

## Contrato actual de datos

- Entidad: `Leccion.cs` guarda HTML para `Teoria`, `Ejemplo` y `Practica`, junto con metadata y flags para recursos opcionales.
- DTO: `LeccionDto.cs` expone el contrato al cliente.
- Orden: `OrdenSecciones : List<string>` define la narrativa exacta de la leccion.
- Literales: `SeccionesContenido` es la fuente unica de verdad para los tokens aceptados por frontend y backend.

## Tabla de secciones

| Token en `OrdenSecciones` | Origen de datos en `LeccionDto` | Componente renderizado | Condicion |
| --- | --- | --- | --- |
| `charpDialog` | `CharpDialog` | `CharpDialog.razor` | Se muestra si el token existe. |
| `charpTip` | `CharpTip` | `CharpTip.razor` | Se muestra si el token existe. |
| `teoria` | `Teoria` | `MarkupString` en `RDL.razor` | Se omite si el campo esta vacio. |
| `ejemplo` | `Ejemplo` | `MarkupString` en `RDL.razor` | Se omite si el campo esta vacio. |
| `practica` | `Practica` | `MarkupString` en `RDL.razor` | Se omite si el campo esta vacio. |
| `actividad` | `UrlActividad` + `TieneActividad` | `ModalActividades.razor` | Requiere flag activo y URL. |
| `compilador` | `TieneCompilador` | `CompiladorACC.razor` | Requiere flag activo. |
| `video` | `VideoId` + `TieneVideo` | `LiteYouTube.razor` | Requiere `VideoId` con valor. |

## Reglas de render y diseno

- El flujo pedagogico esta controlado por datos, no por una plantilla fija.
- `Teoria`, `Ejemplo` y `Practica` se inyectan con `MarkupString`; el backend asume responsabilidad sobre validacion o sanitizacion del HTML persistido.
- `ModalActividades.razor` se instancia una sola vez y los botones solo cambian la URL activa.
- `NivelBloom` se muestra en la cabecera de la leccion como contexto pedagogico.
- Un token desconocido dispara `InfoComponent`, lo cual ayuda a detectar carga incorrecta de datos.

## Implicaciones funcionales

- El sistema de examenes ya no debe documentarse como una simple seccion embebida en leccion.
- La evaluacion real hoy corre por un flujo independiente de examenes y desbloqueos.
- La vieja guia de insercion SQL ya no alcanza para representar lecciones actuales si no considera los nuevos campos y tokens.

## Notas para mantenimiento

- Antes de agregar una nueva seccion, extender `TipoSeccionContenido`, `SeccionesContenido`, AutoMapper y `RDL.razor`.
- Mantener los tokens exactamente como los define `SeccionesContenido`.
- `CompiladorACC` publica el codigo a `ServiceRoots.ACC_COMPILER_Url`; si cambia la ruta del servicio `ACC.Compiler`, hay que actualizar ese valor en `ACC.Shared/Core/ServiceRoots.cs`.
