# ACC GUIA TECNICA - Reload

Estado: outline maestro
Objetivo: definir la estructura final del documento tecnico integral de ACC
Alcance: conservar todo lo importante del documento antiguo y ampliar con la arquitectura, flujos y sistemas actuales de ACC-Complex

## 0. Criterio editorial del documento final

Este documento final debe:

- mantener la columna vertebral del PDF antiguo
- corregir informacion obsoleta o incompleta
- documentar el estado real del codigo actual
- dejar claros los componentes implementados, los planeados y los legacy
- separar con precision lo vigente, lo heredado y lo pendiente

## 1. Estructura maestra propuesta

### 1. Portada y control documental

Debe incluir:

- titulo oficial: `ACC GUIA TECNICA - Reload`
- version del documento
- estado del documento
- fecha de actualizacion
- autores
- asesores
- repositorio o ruta fuente
- nota de sustitucion respecto al PDF/DOCX antiguo

### 2. Indice general

Debe listar todas las secciones principales y anexos.

### 3. Introduccion

Hereda del documento antiguo, pero ampliada.

Debe incluir:

- que es ACC
- para quien esta pensado
- que problema resuelve
- que partes del sistema cubre esta guia
- alcance tecnico del documento

### 4. Concepto, proposito y vision del producto

Nuevo bloque formal.

Debe incluir:

- proposito pedagogico de ACC
- vision general del sistema
- experiencia de aprendizaje esperada
- rol de Charp
- relacion con Taxonomia de Bloom

### 5. Estado actual del proyecto

Nuevo bloque formal.

Debe incluir:

- que partes estan operativas
- que partes estan en evolucion
- que partes siguen en roadmap
- aclaracion sobre `ACC.MultiPlataform`
- aclaracion sobre `ACC.Compiler` y el path legacy `src/API_CompilerACC`

### 6. Software usado

Hereda del documento antiguo, pero corregido.

Debe incluir:

- .NET SDK
- Visual Studio / VS Code
- SQL Server
- Docker Desktop
- Redis
- Postman
- Git / GitHub
- Aspire
- Entity Framework Core
- AutoMapper
- xUnit / Moq
- OpenTelemetry
- Blazored.LocalStorage
- CodeMirror
- Chatbase

### 7. Hardware usado

Hereda del documento antiguo.

Debe incluir:

- equipos de desarrollo usados historicamente
- nota de que esta seccion es referencial y no normativa

### 8. Requerimientos tecnicos para desarrollo

Hereda del documento antiguo y debe ampliarse.

Debe incluir:

- requisitos minimos y recomendados
- prerequisitos de sistema
- SDKs y herramientas necesarias
- requerimientos para contenedores y bases de datos
- secretos, cadenas de conexion y configuracion local

### 9. Requerimientos para usuario final

Hereda del documento antiguo.

Debe incluir:

- navegadores soportados
- conectividad
- resoluciones objetivo
- soporte responsive
- observaciones para movil, tablet y escritorio

### 10. Arquitectura general del proyecto

Hereda del documento antiguo, pero debe reescribirse.

Debe incluir:

- mapa general de ACC-Complex
- relacion entre `ACC.WebApp`, `ACC.API`, `ACC.Compiler`, `ACC.Data`, `ACC.Shared`, `ACC.ExternalClients`, `ACC.ServiceDefaults`, `ACC.AppHost`
- posicion de `ACC.WebApp.Client`
- posicion planeada de `ACC.MultiPlataform`

### 11. Patron de arquitectura

Hereda del documento antiguo y se profundiza.

Debe incluir:

- enfoque modular distribuido
- separacion de responsabilidades
- contratos compartidos
- uso de DTOs e interfaces
- service defaults compartidos
- decisiones de acoplamiento bajo

### 12. Decisiones arquitectonicas clave

Nuevo bloque.

Debe incluir:

- por que separar autenticacion del dominio academico
- por que usar Aspire como topologia local
- por que mantener un servicio dedicado para compilacion
- por que usar SQL Server doble: identidad y academica
- por que `OrdenSecciones` controla narrativa de lecciones
- por que los examenes se separan del contrato principal de leccion
- decisiones sobre Redis, OpenTelemetry y Chatbase

### 13. Topologia de servicios e infraestructura

Nuevo bloque formal.

Debe incluir:

- AppHost
- SQL Identity
- SQL Academic
- Redis
- URLs y relaciones de arranque
- health checks
- observabilidad base

### 14. Capas, proyectos y responsabilidades

Debe incluir una tabla por proyecto con:

- nombre
- capa
- responsabilidad
- dependencias principales
- tipo de componente
- estado: activo, planeado o legacy

### 15. Modelo de datos y persistencia

Nuevo bloque formal.

Debe incluir:

- rol de `ACC.Data`
- `ACCDbContext`
- `ApplicationDbContext`
- separacion de base de datos de identidad y academica
- entidades nucleares del dominio
- migraciones
- notas sobre consistencia y sincronizacion de usuario

### 16. Autenticacion, identidad y sincronizacion de usuario

Nuevo bloque formal.

Debe incluir:

- ASP.NET Identity en `ACC.WebApp`
- roles
- flujo de registro
- confirmacion de cuenta
- `UsuarioSyncService`
- endpoint `Usuario/sincronizar`
- consumo autenticado de `ACC.API` por JWT

### 17. Navegacion academica y contratos compartidos

Nuevo bloque, alimentado por `navegacion-guia.md`.

Debe incluir:

- jerarquia `Modulo -> SubModulo -> Tema -> SubTema -> Leccion`
- `INodoJerarquico`
- `NodoJerarquicoDto`
- `ServiceResult<T>`
- `NavegacionContenidoService`
- rutas API de navegacion
- cliente Blazor y flujo de UI

### 18. Sistema de lecciones dinamicas

Esta es una seccion central del documento final.

Debe incluir:

- evolucion respecto al modelo antiguo
- `Leccion`
- `LeccionDto`
- `OrdenSecciones`
- `SeccionesContenido`
- `TipoSeccionContenido`
- `RDL.razor`
- `LeccionView.razor`
- tokens validos
- reglas de render
- manejo de errores de seccion desconocida

### 19. Manual de estilos para creacion de lecciones

Hereda del documento antiguo, pero debe modernizarse.

Debe incluir:

- estructura visual recomendada
- reglas de consistencia visual
- relacion con `TypographyContract.md`
- uso de tipografias oficiales
- uso de bloques HTML y componentes de apoyo
- que ya no aplica del manual viejo

### 20. Manual tecnico-pedagogico para creacion de lecciones

Hereda del documento antiguo y debe crecer.

Debe incluir:

- secuencia didactica recomendada
- relacion con Bloom
- rol de CharpTip y CharpDialog
- teoria, ejemplo, practica, actividad, compilador, video
- nivel de complejidad por leccion
- criterios de claridad, progresividad y carga cognitiva

### 21. Manual tecnico de insercion y carga de lecciones

Hereda la intencion del documento antiguo, pero debe reemplazar su contenido obsoleto.

Debe incluir:

- por que el antiguo modelo SQL ya no basta
- contrato actual de carga de lecciones
- campos actuales obligatorios y opcionales
- ejemplos de `OrdenSecciones`
- reglas para `Teoria`, `Ejemplo`, `Practica`
- reglas para `CharpTip`, `CharpDialog`, `VideoId`, `UrlActividad`
- recomendaciones para sanitizacion del HTML

### 22. Sistema de examenes, progreso y desbloqueos

Nuevo bloque formal.

Debe incluir:

- progreso por usuario
- completar subtemas
- habilitacion de examen de submodulo
- habilitacion de examen de modulo
- `ProgresoUsuarioClient`
- `ProgresoUsuarioService`
- `PrerrequisitosService`
- `ExamenesHabilitados`
- componentes UI de examenes

### 23. Compilador en linea

Debe incluir:

- nombre oficial actual `ACC.Compiler`
- aclaracion del path actual `src/API_CompilerACC`
- `CompiladorACC.razor`
- `CompileController`
- `RoslynCompileService`
- CodeMirror
- flujo request/response
- limites reales del servicio actual
- relacion actual con Redis y AppHost

### 24. Charp e integraciones externas

Nuevo bloque formal.

Debe incluir:

- Chatbase como integracion actual
- embed global
- pagina dedicada de Charp
- Charp dentro de lecciones
- rol pedagogico
- limites de la asistencia

### 25. Observabilidad, resiliencia y operacion

Nuevo bloque.

Debe incluir:

- `ACC.ServiceDefaults`
- OpenTelemetry
- health checks
- service discovery
- reintentos HTTP
- compresion
- estado actual de Redis

### 26. Herramientas usadas y flujo operativo del equipo

Puede fusionar parte del bloque antiguo de herramientas con una mirada mas actual.

Debe incluir:

- herramientas de desarrollo
- herramientas de pruebas
- herramientas de diagramacion y coordinacion
- observaciones sobre Git y ramas

### 27. Metodologia de desarrollo y planeacion

Hereda del documento antiguo, pero ampliada con `planeacion-desarrollo.md`.

Debe incluir:

- Scrum adaptado
- temporalizacion narrativa
- backlog y demos
- decision metodologica
- relacion con Bardin
- cierre documental y trazabilidad

### 28. Pruebas, calidad y validacion

Nuevo bloque formal.

Debe incluir:

- `ACC.Tests`
- xUnit / Moq
- validaciones manuales
- Swagger / Postman
- riesgos conocidos
- huecos actuales de testing

### 29. Puesta en marcha local

Debe incluir:

- restauracion
- migraciones academicas
- migraciones de identidad
- arranque con `ACC.AppHost`
- verificacion basica de servicios

### 30. Roadmap, deuda tecnica y pendientes

Debe incluir:

- `ACC.MultiPlataform`
- rename total de `ACC.Compiler`
- evolucion del uso de Redis
- endurecimiento de seguridad del compilador
- futuras mejoras del sistema de lecciones y examenes

### 31. Anexos

Debe incluir:

- diagramas
- tablas de tokens
- mapas de rutas
- ejemplos de payloads
- glosario
- referencias cruzadas a archivos clave del repo

## 2. Mapeo directo contra el documento antiguo

Para no perder cobertura, el documento final debe absorber explicitamente estas secciones del PDF viejo:

- `Introduccion` -> secciones 3 y 4
- `Software usado` -> seccion 6
- `Hardware usado` -> seccion 7
- `Requerimientos tecnicos para desarrolladores` -> seccion 8
- `Requerimientos para usuario final` -> seccion 9
- `Arquitectura del proyecto` -> seccion 10
- `Patron de arquitectura` -> seccion 11
- `Herramientas usadas` -> secciones 6 y 26
- `Metodologia usada` -> seccion 27
- `Manual de estilos para lecciones` -> seccion 19
- `Manual tecnico-pedagogico para lecciones` -> seccion 20
- `Manual de insercion de lecciones` -> seccion 21

## 3. Bloques nuevos obligatorios que el documento antiguo no cubria bien

El documento final debe agregar de forma obligatoria:

- estado real del proyecto
- decisiones arquitectonicas
- topologia con AppHost
- autenticacion y sincronizacion de usuario
- navegacion academica
- sistema de lecciones dinamicas actual
- renderizador RDL
- sistema de examenes y desbloqueos
- compilador actualizado como `ACC.Compiler`
- Charp y Chatbase
- observabilidad y resiliencia
- roadmap y deuda tecnica

## 4. Orden de redaccion recomendado

Para construir el documento final sin retrabajo:

1. Portada, introduccion, concepto y estado
2. software, hardware y requerimientos
3. arquitectura, capas, topologia y decisiones
4. persistencia, autenticacion y navegacion
5. lecciones dinamicas, manuales y renderizador
6. examenes, progreso, compilador y Charp
7. operacion, metodologia, pruebas, puesta en marcha y roadmap

## 5. Resultado esperado

Al terminar, `ACC GUIA TECNICA - Reload` debe ser:

- mas completo que el PDF antiguo
- fiel al codigo actual
- util para mantenimiento y onboarding tecnico
- util para produccion de lecciones
- util como base para futuras versiones DOCX/PDF
