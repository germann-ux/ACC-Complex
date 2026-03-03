# Guia Tecnica - ACC

Estado: vigente

Esta guia sustituye conceptualmente a la version PDF/DOCX vieja. La fuente de verdad ahora es el codigo actual del repositorio junto con los documentos tematicos de `Docs/`.

## 1. Introduccion

ACC es una plataforma educativa para aprender C# de forma progresiva, aplicada y guiada. La solucion integra autenticacion, contenido academico, navegacion jerarquica, examenes con desbloqueo por reglas, Charp y un compilador en linea para retroalimentacion inmediata.

## 2. Estado actual

- La solucion activa se apoya en .NET 8, Blazor, ASP.NET Core, Entity Framework Core, SQL Server, Redis y Aspire.
- `ACC.MultiPlataform` sigue siendo parte del plan del producto, aunque hoy no aparezca dentro de `ACC.sln`.
- El compilador debe documentarse como `ACC.Compiler`.
- Fisicamente, el compilador sigue alojado en `src/API_CompilerACC`, por lo que aun existen namespaces y rutas legacy.

## 3. Componentes principales

| Capa | Componente | Responsabilidad |
| --- | --- | --- |
| Cliente actual | `ACC.WebApp.Client` | Cliente web Blazor que consume autenticacion, contenido, progreso y compilador. |
| Cliente planeado | `ACC.MultiPlataform` | Cliente MAUI Blazor para escritorio y movil. |
| Autenticacion | `ACC.WebApp` | Manejo de cuentas, roles, shell web y acceso autenticado a la API. |
| Dominio academico | `ACC.API` | Modulos, submodulos, temas, subtemas, lecciones, progreso, aulas, tareas, examenes y notificaciones. |
| Compilacion | `ACC.Compiler` | Compilacion y ejecucion controlada de codigo C#. |
| Datos | `ACC.Data` | Entidades, DbContext y migraciones. |
| Compartido | `ACC.Shared` | DTOs, interfaces, enums, rutas y resultados. |
| Integraciones | `ACC.ExternalClients` | Integraciones externas, especialmente alrededor de Charp. |
| Defaults | `ACC.ServiceDefaults` | OpenTelemetry, health checks, service discovery y resiliencia HTTP. |
| Orquestacion | `ACC.AppHost` | Topologia local con SQL Server, Redis y proyectos principales. |
| Pruebas | `ACC.Tests` | Pruebas unitarias y de soporte. |

## 4. Topologia actual

`ACC.AppHost` levanta y conecta:

- SQL Server para identidad (`ACC_Identity`, puerto 1434)
- SQL Server para dominio academico (`ACC_Academic`, puerto 1435)
- Redis (`acc-redis`)
- `ACC.WebApp`
- `ACC.API`
- `ACC.Compiler`

La topologia completa corre con `dotnet run --project src/ACC.AppHost/ACC.AppHost.csproj`.

## 5. Patron de arquitectura

La solucion sigue un enfoque modular distribuido:

- separacion entre autenticacion, dominio academico y compilacion
- contratos compartidos en `ACC.Shared`
- persistencia centralizada en `ACC.Data`
- configuracion transversal en `ACC.ServiceDefaults`
- orquestacion local reproducible con Aspire

Notas de precision:

- `ACC.API` valida JWT bearer.
- `ACC.WebApp` usa ASP.NET Identity.
- Redis ya esta declarado y referenciado en AppHost, pero su uso funcional aun sigue en expansion.

## 6. Navegacion academica

La jerarquia base es:

`Modulo -> SubModulo -> Tema -> SubTema -> Leccion`

Piezas clave:

- `INodoJerarquico`
- `NodoJerarquicoDto`
- `NavegacionContenidoService`
- `GuiaNavegador.razor`
- `GuiaMainComponent.razor`
- `LeccionView.razor`
- `RDL.razor`

Rutas principales:

- `/Guia`
- `/contenido/{Tipo}/{Id}`
- `/leccion/{id}`

## 7. Modelo actual de lecciones dinamicas

Esta area cambio de forma importante respecto a la guia anterior.

La documentacion vieja describia una leccion basada en un solo bloque HTML y banderas como `TieneEvaluacion` o `IdEvaluacion`. Ese modelo ya no representa el contrato vigente del producto.

La fuente real de verdad para la leccion actual es:

- `src/ACC.Data/Entities/Leccion.cs`
- `src/ACC.Shared/DTOs/LeccionDto.cs`
- `src/ACC.Shared/Utils/SeccionesContenido.cs`
- `ACC.WebApp/ACC.WebApp.Client/Components/Pages/Guia/Components/Lecciones/RDL.razor`

Campos relevantes del modelo actual:

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

Tokens validos en `OrdenSecciones`:

- `charpDialog`
- `charpTip`
- `teoria`
- `ejemplo`
- `practica`
- `actividad`
- `compilador`
- `video`

Reglas de render:

- `RDL.razor` recorre `OrdenSecciones` y decide que mostrar.
- `Teoria`, `Ejemplo` y `Practica` se inyectan como `MarkupString`.
- `actividad` requiere `TieneActividad == true` y `UrlActividad`.
- `compilador` requiere `TieneCompilador == true`.
- `video` se muestra cuando `VideoId` tiene valor.
- Un token desconocido dispara `InfoComponent`.

Implicacion importante: la vieja guia de insercion SQL ya no basta para describir una leccion actual si no considera estos campos y tokens.

## 8. Examenes y desbloqueos

Los examenes ya no deben documentarse como una simple bandera embebida en la leccion.

Reglas actuales:

- completar todos los subtemas de un submodulo habilita su examen de submodulo
- aprobar todos los examenes de submodulo de un modulo habilita el examen de modulo

Piezas principales:

- `BtnCompletarSubtema.razor`
- `ExamenesMain.razor`
- `Examen.razor`
- `ProgresoUsuarioClient`
- `ProgresoUsuarioService`
- `PrerrequisitosService`
- `ExamenesHabilitados`

## 9. Compilador en linea

El nombre documental correcto es `ACC.Compiler`.

Flujo actual:

1. `CompiladorACC.razor` toma el codigo del editor.
2. Publica la peticion a `ServiceRoots.ACC_COMPILER_Url`.
3. `CompileController` recibe el request.
4. `RoslynCompileService` compila en memoria con referencias limitadas.
5. El servicio devuelve errores de compilacion o salida de ejecucion.

Precision: AppHost referencia Redis para el compilador, pero la implementacion actual del servicio sigue centrada en compilacion en memoria. La documentacion no debe sobreafirmar un uso de Redis que todavia no aparece en la logica del servicio.

## 10. Charp y pedagogia

ACC usa hoy una integracion de Chatbase para Charp:

- embed global en `MainLayout.razor`
- pagina dedicada `Charp-IA/MainCharp-IAComponent.razor`
- soporte contextual dentro de lecciones con `CharpTip` y `CharpDialog`

El enfoque pedagogico sigue centrado en progresividad, practica deliberada y Taxonomia de Bloom.

## 11. Contrato visual

La guia vieja de estilos para lecciones tambien quedo parcialmente superada. El documento vigente para tipografia y jerarquia visual es `Docs/TypographyContract.md`.

Tipografias oficiales:

- `Nunito` para lectura y UI
- `Sora` para encabezados
- `JetBrains Mono` para codigo

## 12. Puesta en marcha local

```pwsh
dotnet restore ACC.sln

dotnet ef database update `
  --project src/ACC.Data `
  --startup-project src/ACC.API `
  --context ACCDbContext

dotnet ef database update `
  --project ACC.WebApp/ACC.WebApp `
  --startup-project ACC.WebApp/ACC.WebApp `
  --context ApplicationDbContext

dotnet run --project src/ACC.AppHost/ACC.AppHost.csproj
```

## 13. Roadmap y pendientes

- `ACC.MultiPlataform` sigue vigente como parte del plan.
- El rename tecnico total de `src/API_CompilerACC` a una estructura completamente alineada con `ACC.Compiler` sigue pendiente.
- Redis ya forma parte de la topologia, pero su explotacion funcional aun esta en evolucion.
- La guia binaria vieja debe considerarse historica hasta que se regenere a partir de esta version mantenible.
