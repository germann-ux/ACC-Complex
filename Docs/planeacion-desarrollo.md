## Descripción de planeación y desarrollo del proyecto

La construcción del prototipo “Aprendiendo C# con Charp” (ACC) se concibió y ejecutó como un proceso continuo de investigación‑acción tecnológica de quince semanas efectivas. Desde el inicio se asumió que el valor educativo del sistema dependía de alinear decisiones técnicas con principios pedagógicos (Taxonomía de Bloom) y con la ruta metodológica de análisis de contenido propuesta por Bardin (2002). A continuación se narra, de manera secuencial y detallada, cómo se planificó, qué recursos se emplearon, qué procesos clave se implementaron y cómo se coordinó el equipo hasta el cierre documental.

### Temporalización narrativa

**Fase 0 – Preparación (Febrero, semana 4).**  
En la cuarta semana de febrero se realizó el encuadre inicial: se definió el problema (brecha en aprendizaje práctico de C# para principiantes), se redactaron objetivos generales y específicos y se bosquejó la estructura de módulos formativos. En paralelo se ejecutó un benchmarking de plataformas educativas, tomando notas en la bitácora manuscrita sobre patrones de navegación, uso de exámenes adaptativos y mecanismos de feedback inmediato. Con esa información se elaboró el primer documento de arquitectura, plasmado luego en `Docs/arquitectura.md`, que proponía una separación de servicios: una WebApp para identidad y front, una API académica y un microservicio aislado para compilación.

**Fase 1 – Diseño conceptual y análisis técnico (Febrero S4 a Marzo S2).**  
Durante dos semanas se consolidó el diseño de datos y la taxonomía de contenidos. Se definió la jerarquía Módulo → SubMódulo → Tema → SubTema → Lección/Capítulo y se modeló en `src/ACC.Data/ACCDbContext.cs`, fijando claves, relaciones en cascada y restricciones únicas para tags. Se eligió SQL Server como motor principal por compatibilidad con .NET 8 y facilidad de despliegue en contenedores. La identidad se aisló en el esquema `acc_identity` mediante `ApplicationDbContext`, mientras que el dominio académico quedó en `acc_academic`. Se establecieron las primeras historias de usuario del backlog: autenticación con roles, biblioteca navegable, aulas con avisos y un compilador seguro accesible desde la WebApp.

**Fase 2 – Construcción iterativa (Marzo S3 a Mayo S1).**  
Se adoptó Scrum adaptado con sprints semanales. Cada lunes se refinaba el backlog y cada viernes se hacía una demo interna registrada en la bitácora. Los incrementos más relevantes fueron:

- Sprint 1-2: Implementación de ASP.NET Identity en `ACC.WebApp/Program.cs` con creación automática de roles “Administrador”, “Docente” y “Estudiante”; configuración de `ApplicationDbContext` con schema `acc_identity` y activación de confirmación de cuenta. Se preparó la capa de servicios cliente (`ACC.WebApp.Client/Services`) para consumir la API.
- Sprint 3-4: Exposición de la Biblioteca y Temario a través de `ACC.API` (`BibliotecaController`, `ModuloController`, `TemaController`, etc.). Se consumieron desde Blazor WebAssembly usando `BibliotecaClientService`. Se agregaron índices de rendimiento e integridad en `ACCDbContext` (por ejemplo, unicidad de tags y combinación usuario‑examen en `ExamenIntento`).
- Sprint 5-6: Desarrollo de Agenda y tareas (`AgendaController`, `TareaController`, `TareasAlumnoController`), con persistencia de tareas personales y asignadas, y vínculos con aulas. Se afinó la UI de aulas para docentes y alumnos (`ACC.WebApp.Client/Components/Pages/Aulas/*`), controlando visibilidad por rol mediante `AuthorizeView` y comprobaciones en `AutoHideSidebar.razor`.
- Sprint 7: Integración de Charp IA mediante ChatBase. Se añadió el script de embed en `ACC.WebApp/Components/Layout/MainLayout.razor` y una página dedicada `Charp-IA/MainCharp-IAComponent.razor` con iframe al bot configurado. Se validó que el widget cargara sin bloquear el render de Blazor.
- Sprint 8: Construcción del compilador en línea. En el cliente, `ACC-Compiler/CompiladorACC.razor` envía el código a `ServiceRoots.ACC_COMPILER_Url`. En el backend especializado `API_CompilerACC`, `CompileController` llama a `RoslynCompileService`, que compila en memoria con referencias mínimas a `System.*` y retorna stdout, manteniendo aislamiento de disco y red.

Durante esta fase se mantuvo la maqueta MAUI Blazor para pruebas en dispositivos Windows y móviles, centrada en verificar responsividad y carga del iframe de ChatBase.

**Fase 3 – Validación y optimización (Mayo S2 a Mayo S4).**  
Se ejecutaron pruebas con usuarios simulados. Se corrigió la lógica de notificaciones y auditoría (`NotificacionController`, `Auditoria` en `ACCDbContext`) y se ajustaron defaults (`GETUTCDATE()` y `OnDelete` restrictivo) para evitar registros huérfanos. Se habilitó compresión Brotli/Gzip en `ACC.WebApp/Program.cs` para mejorar la carga del bundle WASM y se dejaron configurados reintentos SQL (`EnableRetryOnFailure`) tanto en WebApp como en API. Se preparó la incorporación de Redis como caché distribuido: el recurso está declarado en `src/ACC.AppHost/Program.cs` y referenciado por los servicios, listo para activarse en la siguiente iteración.

**Fase 4 – Cierre y documentación (Junio S1 y S2).**  
Se integraron evidencias de cada incremento: capturas de pantallas, diagramas de arquitectura, flujos de trabajo y fragmentos de código clave. Se redactaron los manuales de instalación, operación y usuario, y se completó la bitácora manuscrita en libreta Scribe, numerada y firmada por el asesor. El material final se preparó para el concurso local de prototipos. El Gantt del Anexo 1.0 se mantuvo como referencia visual de la temporalización.

### Recursos en acción

El equipo estuvo compuesto por dos roles principales. Germán Uriel Evangelista Martínez asumió la responsabilidad full‑stack: diseñó la arquitectura, configuró Docker y Aspire/AppHost (`src/ACC.AppHost/Program.cs`) para orquestar contenedores de SQL (identidad y académico), Redis, la API, la WebApp y el compilador; lideró la organización del repositorio y documentó decisiones técnicas. Aldo Juan Figueroa Espinoza se enfocó en UI/UX: prototipos en Miro, maquetación responsiva en Blazor con criterios de accesibilidad y coherencia visual, integración estética de Charp y apoyo gráfico para la bitácora y la documentación. El asesor docente acompañó cada revisión semanal, firmando la bitácora y validando los hitos.

Los recursos materiales incluyeron una laptop Windows 11 y otra macOS 15, ambas con Visual Studio 2022 o VS Code, Docker Desktop para contenedores de SQL Server y Redis, SQL Server Management Studio, Postman para pruebas HTTP, Draw.io para diagramas y Discord/Teams para coordinación. GitHub funcionó como sistema central de control de versiones, con ramas `feature/*` y revisiones cruzadas antes de integrar cambios a la rama principal.

### Procesos clave implementados (con trazabilidad a código)

La autenticación y autorización se basan en ASP.NET Identity. En `ACC.WebApp/Program.cs` se registran los servicios y, al iniciar, se crean los roles “Administrador”, “Docente” y “Estudiante” si no existen. El contexto `ApplicationDbContext` fija el schema `acc_identity`, separando la identidad del dominio académico. La UI de roles se gestiona con componentes de cuenta y un aviso contextual controlado por `RoleStateService`, que persiste el cierre de alertas en LocalStorage.

El dominio académico se modela en `src/ACC.Data/ACCDbContext.cs`: define las entidades para módulos, submódulos, temas, subtemas, lecciones, capítulos, tags, progreso, calificaciones, aulas, invitaciones, notificaciones, tareas y exámenes. Se configuran relaciones en cascada, índices compuestos (por ejemplo, unicidad de intentos de examen por usuario y evaluación) y tipos de columna adecuados (`decimal(5,2)` para calificaciones, `datetimeoffset` para fechas críticas). La API (`src/ACC.API`) expone controladores que orquestan estas entidades: `BibliotecaController` para contenidos, `AulaController` y `Anuncio` para comunicación en aulas, `TareaController` y `TareasAlumnoController` para gestión de tareas, `ProgresoUsuarioController` para seguimiento, y `ExamenesController` para evaluaciones habilitadas.

La capa cliente consume estos endpoints con servicios tipados (`ACC.WebApp.Client/Services/*`). Por ejemplo, `BibliotecaClientService` llama a `api/Biblioteca/capitulos` y `…/contenidos/recomendados`, mientras `ResumenService` y `ProgresoUsuarioClient` obtienen progreso y tips para la pantalla principal. Los enlaces de servicio se centralizan en `src/ACC.Shared/Core/ServiceRoots.cs`, garantizando consistencia de URLs para WebApp, API y compilador.

Charp IA se integra como chatbot externo mediante ChatBase. El script de embed vive en `ACC.WebApp/Components/Layout/MainLayout.razor`, y la página `Charp-IA/MainCharp-IAComponent.razor` aloja un iframe al bot. Esto permite responder dudas de C#, generar prácticas y ofrecer retroalimentación contextual sin mantener un modelo local.

El compilador en línea materializa la retroalimentación inmediata: el componente `ACC-Compiler/CompiladorACC.razor` usa JavaScript para editar código (CodeMirror) y publica el código al microservicio `API_CompilerACC`. `CompileController` recibe la petición, delega a `RoslynCompileService`, compila en memoria y devuelve la salida estándar al cliente. Al limitar referencias a ensamblados básicos de `System.*`, se mitiga el riesgo de accesos no deseados a recursos del host.

En rendimiento y resiliencia se habilitaron compresión Brotli/Gzip en `ACC.WebApp/Program.cs` para mejorar la entrega del bundle WASM, y reintentos de conexión SQL en WebApp y API. Redis está declarado en AppHost para cacheo de notificaciones y recomendaciones; aunque aún no se activa en producción, la infraestructura está lista para habilitarla con mínima fricción.

### Coordinación y supervisión

La gestión siguió Scrum adaptado: reuniones diarias breves para desbloqueos, planificación semanal para fijar metas y demos internas los viernes. El asesor docente participó en las revisiones, firmando los avances en la bitácora física. Las decisiones técnicas se registraron en el repositorio y en notas de la bitácora, asegurando trazabilidad de los cambios respecto a los objetivos pedagógicos. Las pruebas de cada incremento se documentaron con capturas y checklists en la bitácora y en los manuales que acompañan el cierre.

La comunicación pasó de Microsoft Teams a Discord para agilizar mensajes y compartir pantallas durante sprints. GitHub Actions no se configuró aún; el control de calidad se basó en revisiones cruzadas y pruebas manuales con Postman y el front en Blazor.

### Ruta metodológica según Bardin (2002)

Se aplicó análisis de contenido en tres etapas. En el preanálisis se revisaron fuentes de pedagogía, enseñanza de C#, IA educativa y UX en Blazor, clasificadas en la bitácora. En la codificación se agruparon hallazgos en cuatro categorías: (1) enseñanza de programación, (2) plataformas educativas, (3) diseño de interfaces en Blazor, (4) enfoques pedagógicos interactivos. En la fase interpretativa se tradujeron esos códigos en decisiones técnicas: integrar retroalimentación inmediata (compilador y tips), reforzar aprendizaje visual con componentes responsivos, y promover autonomía progresiva con seguimiento de progreso y exámenes habilitados. Cada decisión quedó reflejada en artefactos de código y en la arquitectura documentada.

### Metodología de desarrollo (Scrum adaptado) y justificación

Se eligió Scrum adaptado en lugar de cascada por la alta incertidumbre en UX y en la integración del chatbot externo. Los sprints cortos permitieron ajustar flujos de aula y prompts de Charp tras cada demo. La Definition of Done exigía al menos: componente UI funcional, endpoint API operativo y una prueba manual o automatizada básica. La deuda técnica se registró y se revisó cada dos sprints para evitar acumulación. El uso de Aspire/AppHost permitió levantar todo el entorno (WebApp, API, Compiler, SQL, Redis) con un solo comando, reproduciendo entornos de forma consistente y reduciendo tiempos de puesta en marcha en cada iteración.

### Cierre, trazabilidad y validez

Al finalizar, cada funcionalidad quedó respaldada por: (1) código fuente (componentes Blazor, controladores API, configuraciones de DbContext), (2) evidencia visual (capturas y diagramas), (3) registro en la bitácora manuscrita con firma del asesor y (4) manuales de instalación, operación y usuario. La combinación de documentación, bitácora y healthchecks provistos por `ACC.ServiceDefaults` ofrece trazabilidad completa desde los objetivos pedagógicos hasta la implementación técnica. El proyecto culminó preparado para escalar a Kubernetes gracias a la topología en contenedores definida en AppHost y a la separación clara de servicios.

### Nota sobre el cronograma

El gráfico de Gantt usado en seguimiento se mantiene como Anexo 1.0. Si se requiere su inclusión directa en este documento, agréguese la imagen o reprodúzcase como [diagrama x] sustituyendo el marcador.
