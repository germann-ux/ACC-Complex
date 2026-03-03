# ACC GUIA TECNICA - Reload

Subtitulo: Recompilacion tecnica integral del ecosistema ACC  
Codigo documental: ACC-GT-RLD-2026-001  
Version del documento: 2.0-rc1  
Estado: Borrador integral en revision editorial  
Fecha de actualizacion: 2026-03-02  
Proyecto: ACC-Complex  
Producto: Aprendiendo C# con Charp (ACC)
Repositorio fuente: `ACC-Complex`

Autores:

- German Uriel Evangelista Martinez
- Aldo Juan Figueroa Espinoza

Asesores:

- Francisco Javier Tafolla Granados
- Jose Manuel Gonzalez Zaragoza

Fuente base:

- Repositorio `ACC-Complex`
- Documento historico `Docs/GUIA TECNICA - ACC.pdf`
- Documentacion tematica actual en `Docs/`

Nota editorial:
Este documento sustituye gradualmente a la guia tecnica historica en PDF y DOCX. Su objetivo es conservar la estructura y cobertura del documento anterior, corrigiendo informacion obsoleta e incorporando el estado real del sistema actual.

## Control documental

| Campo | Valor |
| --- | --- |
| Nombre oficial | `ACC GUIA TECNICA - Reload` |
| Tipo de documento | Guia tecnica integral |
| Estado actual | Borrador integral listo para revision editorial |
| Version | `2.0-rc1` |
| Fecha de corte tecnico | `2026-03-02` |
| Fuente principal de verdad | repositorio `ACC-Complex` |
| Sustituye a | `Docs/GUIA TECNICA - ACC.pdf` y `Docs/GUIA TECNICA - ACC.docx` |
| Formatos objetivo | Markdown, DOCX, PDF |

## Nota de version

La presente version consolida por primera vez en un solo documento:

- arquitectura y topologia vigentes
- capas, responsabilidades y decisiones de diseno
- persistencia, identidad y sincronizacion
- navegacion academica
- sistema actual de lecciones dinamicas y `RDL`
- lineamientos tecnicos, visuales y pedagogicos para contenido
- examenes, progreso, desbloqueos, compilador y Charp
- operacion local, pruebas, roadmap y anexos

## Tabla de contenido

- [1. Introduccion](#1-introduccion)
- [2. Alcance de la guia](#2-alcance-de-la-guia)
- [3. Concepto, proposito y vision del producto](#3-concepto-proposito-y-vision-del-producto)
- [4. Vision general del sistema](#4-vision-general-del-sistema)
- [5. Estado actual del proyecto](#5-estado-actual-del-proyecto)
- [6. Software usado](#6-software-usado)
- [7. Hardware usado](#7-hardware-usado)
- [8. Requerimientos tecnicos para desarrollo](#8-requerimientos-tecnicos-para-desarrollo)
- [9. Requerimientos para usuario final](#9-requerimientos-para-usuario-final)
- [10. Arquitectura general del proyecto](#10-arquitectura-general-del-proyecto)
- [11. Patron de arquitectura](#11-patron-de-arquitectura)
- [12. Decisiones arquitectonicas clave](#12-decisiones-arquitectonicas-clave)
- [13. Topologia de servicios e infraestructura](#13-topologia-de-servicios-e-infraestructura)
- [14. Capas, proyectos y responsabilidades](#14-capas-proyectos-y-responsabilidades)
- [15. Modelo de datos y persistencia](#15-modelo-de-datos-y-persistencia)
- [16. Autenticacion, identidad y sincronizacion de usuario](#16-autenticacion-identidad-y-sincronizacion-de-usuario)
- [17. Navegacion academica y contratos compartidos](#17-navegacion-academica-y-contratos-compartidos)
- [18. Sistema de lecciones dinamicas](#18-sistema-de-lecciones-dinamicas)
- [19. Manual de estilos para creacion de lecciones](#19-manual-de-estilos-para-creacion-de-lecciones)
- [20. Manual tecnico-pedagogico para creacion de lecciones](#20-manual-tecnico-pedagogico-para-creacion-de-lecciones)
- [21. Manual tecnico de insercion y carga de lecciones](#21-manual-tecnico-de-insercion-y-carga-de-lecciones)
- [22. Sistema de examenes, progreso y desbloqueos](#22-sistema-de-examenes-progreso-y-desbloqueos)
- [23. Compilador en linea](#23-compilador-en-linea)
- [24. Charp e integraciones externas](#24-charp-e-integraciones-externas)
- [25. Observabilidad, resiliencia y operacion](#25-observabilidad-resiliencia-y-operacion)
- [26. Herramientas usadas y flujo operativo del equipo](#26-herramientas-usadas-y-flujo-operativo-del-equipo)
- [27. Metodologia de desarrollo y planeacion](#27-metodologia-de-desarrollo-y-planeacion)
- [28. Pruebas, calidad y validacion](#28-pruebas-calidad-y-validacion)
- [29. Puesta en marcha local](#29-puesta-en-marcha-local)
- [30. Roadmap, deuda tecnica y pendientes](#30-roadmap-deuda-tecnica-y-pendientes)
- [31. Anexos](#31-anexos)

---

## 1. Introduccion

Aprendiendo C# con Charp (ACC) es una plataforma educativa orientada a la ensenanza progresiva del lenguaje C#. Su diseno combina contenido academico estructurado, practica interactiva, seguimiento del progreso del estudiante, examenes con desbloqueo por reglas, apoyo pedagogico asistido por Charp y un servicio de compilacion en linea para reforzar el aprendizaje aplicado.

La solucion tecnica que da vida al producto se encuentra organizada dentro del repositorio `ACC-Complex`, que integra autenticacion, dominio academico, cliente web, servicios compartidos, persistencia, orquestacion local e integraciones externas. A diferencia del documento tecnico anterior, esta guia no se limita a describir la intencion inicial del sistema, sino que documenta su estado implementado, sus decisiones arquitectonicas y los cambios acumulados en su evolucion.

El objetivo central de esta guia es servir como referencia tecnica integral para desarrollo, mantenimiento, escalamiento, incorporacion de nuevos colaboradores y produccion de contenido academico dentro de ACC. Por ello, el documento no solo cubre arquitectura y tecnologias, sino tambien los sistemas pedagogicos y editoriales que distinguen a la plataforma, especialmente el modelo actual de lecciones dinamicas y su renderizador.

## 2. Alcance de la guia

Esta guia tecnica cubre los siguientes dominios del sistema:

- definicion general del producto y su proposito
- estado actual del proyecto y sus componentes
- stack tecnologico y requerimientos tecnicos
- arquitectura general, capas y decisiones de diseno
- persistencia, identidad y sincronizacion de usuario
- navegacion academica y contratos compartidos
- sistema de lecciones dinamicas y renderizado
- lineamientos tecnicos y pedagogicos para creacion de contenido
- examenes, progreso, desbloqueos y compilador en linea
- observabilidad, operacion local, pruebas y roadmap

Quedan fuera del alcance de esta primera version temas de despliegue productivo endurecido, Kubernetes, politicas formales de seguridad empresarial y manuales de usuario final detallados. Esos temas pueden aparecer en documentos complementarios o anexos posteriores.

## 3. Concepto, proposito y vision del producto

ACC nace para resolver un problema recurrente en la ensenanza de programacion: muchos estudiantes se enfrentan a recursos fragmentados, progresiones poco claras y poca retroalimentacion cuando intentan aprender C# por su cuenta o en etapas iniciales de formacion. El sistema busca reducir esa friccion mediante una experiencia guiada, modular y progresiva.

Desde el punto de vista pedagogico, ACC se concibe como un entorno de aprendizaje que no se limita a presentar teoria. Cada unidad de contenido aspira a conectar explicacion conceptual, ejemplo, practica, actividad y validacion, de forma que el estudiante no solo lea, sino que aplique lo aprendido dentro de una misma experiencia. Esta orientacion se alinea con la Taxonomia de Bloom, utilizada como referencia para graduar la dificultad y el tipo de actividad cognitiva esperada en cada leccion.

La vision de ACC es consolidarse como una plataforma educativa integral para C#, capaz de unir tres dimensiones que a menudo aparecen separadas:

- claridad conceptual para principiantes e intermedios
- practica interactiva con retroalimentacion inmediata
- acompanamiento pedagogico contextual a traves de Charp

Dentro de esa vision, Charp no debe entenderse como un sustituto del razonamiento del estudiante, sino como una capa de apoyo que orienta, contextualiza y sugiere caminos de solucion sin convertir la experiencia en simple entrega de respuestas.

## 4. Vision general del sistema

ACC funciona como un ecosistema compuesto por varios subsistemas coordinados:

- un entorno de identidad y acceso en `ACC.WebApp`
- un dominio academico centralizado en `ACC.API`
- un cliente web actual en `ACC.WebApp.Client`
- una capa de datos y contratos compartidos (`ACC.Data`, `ACC.Shared`)
- un compilador en linea documentado como `ACC.Compiler`
- una topologia local reproducible mediante `ACC.AppHost`
- integraciones externas que alimentan a Charp

Sobre esa base, el sistema ofrece una experiencia de aprendizaje centrada en una jerarquia academica clara: modulo, submodulo, tema, subtema y leccion. A partir de esa estructura, el usuario puede navegar contenidos, consumir lecciones dinamicas configuradas por datos, registrar progreso, habilitar examenes y practicar codigo en un compilador dedicado.

La experiencia actual esta implementada principalmente para web. Al mismo tiempo, `ACC.MultiPlataform` se mantiene como parte del roadmap del producto para expandir la experiencia a una app MAUI Blazor para escritorio y movil, aunque hoy no forme parte de la solucion activa.

## 5. Estado actual del proyecto

ACC se encuentra en desarrollo activo. La solucion actual ya integra varios de sus componentes nucleares y cuenta con una base funcional suficiente para documentar arquitectura, flujos y responsabilidades de manera precisa.

### 5.1 Componentes activos

Hoy forman parte del nucleo tecnico activo del proyecto:

- `ACC.WebApp`
- `ACC.WebApp.Client`
- `ACC.API`
- `ACC.Data`
- `ACC.Shared`
- `ACC.ExternalClients`
- `ACC.ServiceDefaults`
- `ACC.AppHost`
- `ACC.Compiler`
- `ACC.Tests`

### 5.2 Componentes planeados o en evolucion

Las siguientes areas deben documentarse con estado explicito para evitar confusion:

- `ACC.MultiPlataform`: sigue siendo parte del plan del producto, aunque no aparezca actualmente en `ACC.sln`
- Redis: ya forma parte de la topologia de AppHost y de las referencias entre servicios, pero su explotacion funcional aun sigue creciendo
- compilador: el nombre documental correcto es `ACC.Compiler`, aunque su ubicacion fisica actual siga siendo `src/API_CompilerACC`

### 5.3 Diferencias frente al documento tecnico historico

La guia antigua sigue siendo util como punto de partida, pero ya no representa correctamente varios aspectos del sistema. Entre los cambios mas importantes se encuentran:

- el modelo de lecciones ya no gira alrededor de un unico bloque HTML
- las secciones de una leccion ahora se controlan con `OrdenSecciones`
- el renderizado final depende de `RDL.razor`
- los examenes ya no se describen correctamente como una simple bandera embebida en la leccion
- la topologia con Aspire, dos bases de datos y Redis ya requiere una documentacion mas precisa
- la nomenclatura y alcance del compilador cambiaron respecto a la documentacion anterior

### 5.4 Funcion de esta nueva guia

`ACC GUIA TECNICA - Reload` existe para convertirse en la fuente documental principal del proyecto. Su papel no es repetir el PDF antiguo con ligeras correcciones, sino recompilar todo el conocimiento tecnico vigente de ACC, ordenar sus sistemas por capas y responsabilidades, y dejar una base confiable para futuras versiones en Markdown, DOCX y PDF.

---

## 6. Software usado

El entorno tecnico de ACC combina herramientas del ecosistema .NET, infraestructura local basada en contenedores y utilidades orientadas a desarrollo, pruebas y observabilidad. En esta seccion se distinguen dos clases de informacion: la fotografia historica del proyecto y el stack actualmente verificable en el repositorio.

### 6.1 Base de runtime y framework

La solucion activa trabaja sobre `net8.0` en sus proyectos principales. El archivo `global.json` fija como referencia el SDK `8.0.123` con politica `rollForward: latestFeature`, lo que deja claro que la base actual del entorno es .NET 8.

Componentes base verificados en el repositorio:

- .NET SDK 8.0.123
- ASP.NET Core 8
- Blazor Web App / Blazor WebAssembly
- ASP.NET Identity
- Entity Framework Core 8.x y 9.x segun proyecto

### 6.2 Herramientas y librerias principales del sistema

La solucion actual utiliza o declara de forma verificable las siguientes herramientas y paquetes:

| Herramienta o tecnologia | Version observada | Funcion principal |
| --- | --- | --- |
| .NET SDK | 8.0.123 | Compilacion y ejecucion de la solucion |
| Visual Studio 2022 / VS Code | Entorno externo | Desarrollo y depuracion |
| SQL Server | 2022 | Persistencia de identidad y dominio academico |
| Docker Desktop | Variable | Soporte para contenedores de infraestructura |
| Redis | 7.x en topologia | Cache distribuido y soporte infraestructural |
| .NET Aspire | 9.2.0 / 9.1.0 | Orquestacion local de servicios y recursos |
| OpenTelemetry | 1.9.0 a 1.11.2 | Telemetria y observabilidad |
| AutoMapper | 12.0.1 / 14.0.0 | Mapeo entre entidades y DTOs |
| xUnit | 2.5.3 | Pruebas unitarias |
| Moq | 4.20.72 | Simulacion de dependencias |
| coverlet.collector | 6.0.0 | Cobertura de pruebas |
| Swashbuckle.AspNetCore | 6.6.2 | Swagger y OpenAPI |
| Roslyn (`Microsoft.CodeAnalysis.CSharp`) | 4.13.0 | Compilacion dinamica de C# |
| Blazored.LocalStorage | 4.5.0 | Persistencia ligera del lado cliente |
| CodeMirror | Integrado via assets locales | Editor del compilador en linea |
| Postman | Uso operativo | Validacion manual de endpoints |
| Git / GitHub | Uso operativo | Control de versiones y colaboracion |
| Chatbase | Integracion externa | Base de la experiencia actual de Charp |

### 6.3 Herramientas por area

#### Desarrollo backend

- ASP.NET Core
- Entity Framework Core
- ASP.NET Identity
- AutoMapper
- Swagger / OpenAPI

#### Desarrollo frontend

- Blazor Web App
- Blazor WebAssembly
- Blazored.LocalStorage
- CodeMirror
- Bootstrap

#### Infraestructura y operacion local

- .NET Aspire
- SQL Server
- Redis
- Docker Desktop
- OpenTelemetry

#### Calidad y pruebas

- xUnit
- Moq
- coverlet
- Postman

### 6.4 Nota sobre herramientas historicas y obsolescencia documental

El documento tecnico antiguo mencionaba herramientas como Aspire en estado `Preview`, Entity Framework Core 8.0 y Serilog como parte del stack principal. Hoy la situacion debe matizarse:

- Aspire ya no debe documentarse como preview dentro de ACC
- Entity Framework Core aparece mezclado entre 8.x y 9.x segun el proyecto
- OpenTelemetry esta verificado directamente en `ACC.ServiceDefaults`
- Serilog no forma parte visible del stack principal actual del repositorio y no debe seguirse presentando como pieza central sin evidencia en codigo

## 7. Hardware usado

Esta seccion conserva la informacion historica del proyecto como referencia contextual, no como requerimiento normativo. Su valor documental consiste en mostrar el tipo de equipo con el que ACC fue desarrollado y validado en sus primeras etapas.

### 7.1 Equipos historicos de desarrollo

| Integrante | Dispositivo | Especificaciones principales |
| --- | --- | --- |
| German | ASUS TUF Gaming F17 | Intel Core i5-11400H, 32 GB RAM DDR4, SSD NVMe 1 TB, RTX 2050 4 GB, Windows 11 Home, pantalla FHD 17.3" 144 Hz |
| Aldo | MacBook Air M1 (2020) | Apple M1, 8 GB RAM unificada, SSD 256 GB, macOS Big Sur actualizable, pantalla Retina 13.3" |

### 7.2 Interpretacion tecnica

Estos equipos confirman que ACC puede desarrollarse de forma razonable en hardware de gama media o media-alta, siempre que se cuente con:

- suficiente memoria para correr IDE, servicios .NET y contenedores
- almacenamiento SSD para restauraciones, builds y bases de datos locales
- capacidad de ejecutar Docker Desktop y SQL Server sin degradacion excesiva

### 7.3 Nota editorial

La presente guia no adopta estos equipos como estandar obligatorio. Se incluyen por trazabilidad historica y para contextualizar el entorno real donde fue concebido el proyecto.

## 8. Requerimientos tecnicos para desarrollo

Para recrear, compilar y mantener ACC en un entorno local o profesional se requiere un entorno compatible con .NET 8, SQL Server, Docker y la topologia definida por `ACC.AppHost`.

### 8.1 Requerimientos de hardware

| Componente | Minimo recomendado | Recomendado para trabajo fluido |
| --- | --- | --- |
| CPU | Intel Core i5 / AMD Ryzen 5 | Intel Core i7 / Ryzen 7 o superior |
| RAM | 8 GB | 16 GB o mas |
| Almacenamiento | SSD con al menos 20 GB libres | SSD NVMe con 50 GB o mas |
| Pantalla | 1366x768 | Full HD 1920x1080 o superior |

### 8.2 Requerimientos de software

El entorno base para desarrollo debe contar con:

- .NET SDK 8.0
- Visual Studio 2022 o VS Code con soporte .NET
- Docker Desktop activo
- acceso a SQL Server local o levantado desde Aspire
- Git
- navegador moderno para validar la experiencia web

Herramientas complementarias recomendadas:

- Postman para pruebas manuales
- SQL Server Management Studio o herramienta equivalente
- terminal PowerShell

### 8.3 Requerimientos del repositorio y configuracion local

Para trabajar con ACC se debe poder:

- restaurar la solucion `ACC.sln`
- ejecutar migraciones sobre `ACCDbContext`
- ejecutar migraciones sobre `ApplicationDbContext`
- iniciar `ACC.AppHost`
- disponer de cadenas de conexion validas para identidad y dominio academico
- configurar secretos o `appsettings.Development.json` donde corresponda

### 8.4 Dependencias de infraestructura local

La topologia actual del proyecto presupone la disponibilidad de:

- una base de datos `ACC_Identity`
- una base de datos `ACC_Academic`
- un recurso Redis
- los proyectos `ACC.WebApp`, `ACC.API` y `ACC.Compiler`

En desarrollo local, esa topologia puede levantarse desde `ACC.AppHost`, lo cual simplifica el arranque conjunto de recursos y servicios.

### 8.5 Requisitos operativos recomendados

Para evitar problemas durante desarrollo se recomienda:

- mantener Docker Desktop funcionando antes de arrancar AppHost
- verificar disponibilidad de puertos locales para SQL y servicios web
- usar navegadores actualizados
- validar compatibilidad al actualizar paquetes mayores de EF Core, Aspire o ASP.NET Core

### 8.6 Nota de compatibilidad

La solucion combina paquetes 8.x y 9.x en distintas capas. Esto obliga a tratar actualizaciones de dependencias con cuidado y a validar compilacion, migraciones y arranque integral despues de cualquier cambio mayor de version.

## 9. Requerimientos para usuario final

ACC esta pensado para ejecutarse desde navegador web sin requerir una instalacion adicional del lado del usuario final. La experiencia actual se orienta principalmente a escritorio y navegacion web responsive.

### 9.1 Navegadores compatibles

Se consideran compatibles los navegadores modernos con soporte para:

- JavaScript
- cookies
- HTTPS
- capacidades de render web modernas

Referencias recomendadas:

- Google Chrome
- Microsoft Edge
- Firefox
- Safari

### 9.2 Requisitos funcionales del navegador

Para usar ACC correctamente el usuario final debe contar con:

- navegador actualizado
- cookies habilitadas
- soporte para contenido web interactivo
- conexion estable a Internet

El documento historico mencionaba WebAssembly como requisito central. Eso sigue siendo relevante para partes del ecosistema Blazor, aunque la experiencia actual debe describirse mas ampliamente como una aplicacion web moderna con componentes interactivos.

### 9.3 Resolucion y dispositivos

| Dispositivo | Estado esperado | Observaciones |
| --- | --- | --- |
| Computadora de escritorio o portatil | Compatible | Mejor experiencia general y mayor comodidad de trabajo |
| Tablet | Compatible | Experiencia adaptada con limitaciones ergonomicas menores segun el flujo |
| Smartphone | Compatible de forma responsive | Navegacion posible, pero con mas restricciones de espacio para contenido complejo y practica |

Resoluciones orientativas:

- minima funcional: 1024x768
- recomendada: 1366x768 o superior

### 9.4 Conectividad

Se recomienda:

- conexion estable a Internet
- ancho de banda suficiente para cargar la experiencia web, recursos integrados y servicios externos

Como referencia historica, el documento anterior sugeria al menos 5 Mbps. Esa cifra puede seguir tratandose como referencia minima razonable para evitar lentitud perceptible en el uso del sistema.

### 9.5 Consideraciones de seguridad y compatibilidad

Para una experiencia estable se recomienda:

- usar HTTPS siempre que el entorno lo permita
- mantener el navegador actualizado
- evitar dispositivos muy antiguos con poca memoria o CPU limitada
- considerar que ciertas experiencias, como el compilador y la navegacion de contenido extenso, resultan mas comodas en escritorio

### 9.6 Nota sobre multiplataforma

Aunque el roadmap contempla `ACC.MultiPlataform`, la experiencia principal documentada hoy para usuario final sigue siendo la experiencia web de ACC.

## 10. Arquitectura general del proyecto

ACC-Complex es una solucion modular compuesta por servicios, bibliotecas compartidas, capa de datos, cliente web y una topologia local orquestada. Su arquitectura busca separar con claridad la experiencia de usuario, la identidad, el dominio academico, la compilacion de codigo y los concerns transversales de infraestructura.

En la solucion actual, el sistema se organiza alrededor de los siguientes bloques:

- `ACC.WebApp`: host web principal, identidad y shell de acceso
- `ACC.WebApp.Client`: cliente web con componentes de experiencia interactiva
- `ACC.API`: dominio academico y servicios principales de negocio
- `ACC.Compiler`: servicio especializado para compilacion de C#
- `ACC.Data`: persistencia del dominio academico
- `ACC.Shared`: contratos, DTOs, enums e interfaces compartidas
- `ACC.ExternalClients`: integraciones hacia servicios externos
- `ACC.ServiceDefaults`: observabilidad, service discovery, health checks y resiliencia
- `ACC.AppHost`: topologia local y orquestacion con Aspire
- `ACC.Tests`: pruebas automatizadas

En terminos practicos, la experiencia del usuario comienza en la superficie web hospedada por `ACC.WebApp`. Desde ahi, el cliente consume informacion academica proveniente de `ACC.API`, obtiene lecciones y progreso, y activa sistemas complementarios como el compilador, los examenes y el soporte de Charp. Todo ello ocurre sobre una infraestructura local reproducible definida por `ACC.AppHost`.

### 10.1 Estructura actual de la solucion

La solucion `ACC.sln` actualmente incluye diez proyectos principales:

- `ACC.Shared`
- `ACC.Data`
- `ACC.API`
- `ACC.ExternalClients`
- `ACC.Tests`
- `ACC.AppHost`
- `ACC.WebApp`
- `ACC.WebApp.Client`
- `ACC.ServiceDefaults`
- `ACC.Compiler`

Adicionalmente, debe dejarse explicitado que `ACC.MultiPlataform` sigue perteneciendo al roadmap del producto, aunque no aparezca hoy dentro de la solucion activa.

### 10.2 Vista conceptual del sistema

Desde una vista macro, la arquitectura puede leerse asi:

1. `ACC.WebApp` gestiona identidad, registro, roles, shell web y parte del flujo de acceso.
2. `ACC.WebApp.Client` entrega la experiencia de navegacion, guia, contenido, agenda, examenes y compilador.
3. `ACC.API` concentra el dominio academico: modulos, submodulos, temas, subtemas, lecciones, progreso, aulas, tareas, notificaciones y examenes.
4. `ACC.Compiler` procesa solicitudes de compilacion de codigo C# usando Roslyn.
5. `ACC.Data` conecta el dominio academico con SQL Server.
6. `ACC.Shared` mantiene el lenguaje comun entre capas y proyectos.
7. `ACC.AppHost` levanta bases de datos, Redis y servicios, facilitando desarrollo local coherente.

### 10.3 Diferencia entre arquitectura de producto y estado de implementacion

Es importante distinguir entre lo que ACC quiere ser como producto y lo que hoy existe como implementacion concreta:

- como producto, ACC contempla experiencia web y experiencia multiplataforma
- como implementacion actual, la experiencia principal es la web
- como nomenclatura de producto, el compilador ya debe llamarse `ACC.Compiler`
- como realidad del repositorio, dicho compilador vive aun en `src/API_CompilerACC`

## 11. Patron de arquitectura

La arquitectura de ACC responde a un enfoque modular distribuido con fuerte separacion de responsabilidades y un conjunto de contratos compartidos para desacoplar capas funcionales. No se trata de una Clean Architecture academica en estado puro, pero si de una composicion pragmatica que adopta varios de sus principios.

### 11.1 Principios aplicados

#### Separacion de responsabilidades

Cada proyecto tiene un alcance predominante:

- `ACC.WebApp` se enfoca en identidad, acceso y host web
- `ACC.API` concentra logica del dominio academico
- `ACC.Compiler` atiende compilacion y ejecucion de codigo
- `ACC.Data` persiste el dominio academico
- `ACC.Shared` evita duplicacion de contratos y tipos
- `ACC.ServiceDefaults` encapsula configuracion transversal repetible

#### Contratos compartidos

La comunicacion entre componentes evita depender de entidades concretas siempre que es posible. DTOs, interfaces, enums y resultados de servicio se concentran en `ACC.Shared`, lo cual permite que cliente, API y servicios hablen un lenguaje comun.

#### Composicion por servicios

Aunque ACC no esta desplegado como una malla de microservicios compleja, si organiza responsabilidades operativas por servicios desacoplados:

- servicio de identidad y host web
- servicio de dominio academico
- servicio de compilacion
- recursos de infraestructura desacoplados

#### Infraestructura transversal reutilizable

`ACC.ServiceDefaults` permite que varios servicios adopten el mismo baseline tecnico para:

- OpenTelemetry
- health checks
- service discovery
- resiliencia HTTP

### 11.2 Capas logicas

Puede leerse la solucion en cuatro capas logicas principales:

| Capa logica | Componentes principales | Funcion |
| --- | --- | --- |
| Presentacion | `ACC.WebApp`, `ACC.WebApp.Client`, futuro `ACC.MultiPlataform` | Experiencia de usuario, navegacion, UI, autenticacion visible y shell de acceso |
| Aplicacion | `ACC.API`, `ACC.Compiler` | Casos de uso, reglas de negocio, flujos academicos y compilacion |
| Infraestructura | `ACC.Data`, `ACC.ExternalClients`, `ACC.ServiceDefaults`, `ACC.AppHost` | Persistencia, integraciones externas, telemetria, topologia y recursos |
| Compartido | `ACC.Shared` | Contratos, DTOs, interfaces, enums y resultados |

### 11.3 Patron operativo resultante

En la practica, ACC opera como una solucion distribuida ligera: varios servicios con responsabilidades delimitadas, dos bases de datos separadas por concern, un recurso de cache incorporado a la topologia y una capa compartida que evita acoplamiento innecesario entre cliente y backend.

## 12. Decisiones arquitectonicas clave

La arquitectura actual de ACC no surge solo de una preferencia tecnologica, sino de varias decisiones de diseno que responden a necesidades concretas del producto.

### 12.1 Separar identidad del dominio academico

Se opto por no mezclar directamente la persistencia de identidad con la persistencia academica. Esto permite:

- mantener mas limpio el modelo academico
- aislar flujos de autenticacion y cuentas
- reducir el acoplamiento entre usuarios de Identity y entidades del dominio
- permitir una sincronizacion explicita hacia el perfil academico cuando corresponde

### 12.2 Mantener un servicio dedicado para compilacion

El compilador en linea se documento y estructuro como un servicio independiente porque su naturaleza es distinta al resto del dominio:

- ejecuta compilacion dinamica
- requiere un control de referencias y superficie de ejecucion
- su evolucion tecnica y de seguridad no deberia contaminar el resto de la API academica

### 12.3 Usar Aspire para orquestacion local

`ACC.AppHost` existe para eliminar friccion de arranque y documentar la topologia de forma ejecutable. Gracias a ello:

- SQL Server y Redis se describen como recursos del sistema
- los servicios conocen sus dependencias desde el arranque
- el entorno local se vuelve reproducible
- la observabilidad y los health checks quedan mejor integrados

### 12.4 Mantener dos bases de datos SQL

La existencia de `ACC_Identity` y `ACC_Academic` no es accidental. Esta separacion permite:

- delimitar claramente los bounded contexts operativos
- reducir mezcla entre tablas de cuentas y tablas academicas
- evolucionar el dominio academico sin depender de la estructura de Identity

### 12.5 Mover la narrativa de lecciones a datos

Una de las decisiones mas importantes del producto fue dejar atras el modelo de leccion basado en un solo HTML principal y mover la narrativa al arreglo `OrdenSecciones`. Esa decision permite:

- cambiar el orden y presencia de bloques sin redeploy del cliente
- introducir nuevas secciones de forma incremental
- hacer que el frontend renderice segun datos, no segun plantillas fijas

### 12.6 Separar examenes del contrato basico de leccion

El documento antiguo asociaba evaluacion directamente con leccion. La evolucion actual llevo a un sistema mas robusto donde examenes, progreso y desbloqueos siguen reglas propias. Esto evita:

- sobrecargar el modelo de leccion
- mezclar contenido con control de habilitacion
- perder trazabilidad sobre aprobacion y prerrequisitos

### 12.7 Incorporar Redis desde la topologia aunque su uso siga creciendo

Redis ya forma parte de la arquitectura declarada porque se considera una pieza de soporte para evolucion futura. Aunque su explotacion funcional actual no cubre todo lo que la arquitectura sugiere, incluirlo desde AppHost ayuda a:

- preparar cache distribuido sin redisenar la topologia
- mantener la solucion lista para escenarios de baja latencia
- ensayar una arquitectura con recursos transversales desde etapas tempranas

### 12.8 Centralizar preocupaciones transversales en ServiceDefaults

OpenTelemetry, health checks, service discovery y resiliencia HTTP fueron concentrados en `ACC.ServiceDefaults` para evitar configuraciones divergentes entre servicios.

## 13. Topologia de servicios e infraestructura

La topologia actual de ACC esta declarada en `src/ACC.AppHost/Program.cs` y representa el entorno canonico de desarrollo local.

### 13.1 Recursos definidos por AppHost

`ACC.AppHost` levanta:

- `acc-sql-identity` en puerto 1434
- `ACC_Identity` como base de datos de identidad
- `acc-sql-academic` en puerto 1435
- `ACC_Academic` como base de datos del dominio academico
- `acc-redis` como recurso Redis
- `acc-compiler` como servicio de compilacion
- `acc-api` como servicio academico
- `acc-blazor` como host web principal

### 13.2 Relaciones de dependencia

Las dependencias definidas de forma explicita son:

- `ACC.Compiler` referencia Redis
- `ACC.API` referencia la base academica y espera a que este disponible
- `ACC.WebApp` referencia la base de identidad, la base academica y Redis, y espera la disponibilidad de las bases

### 13.3 Observabilidad y salud del sistema

Los servicios que incorporan `ACC.ServiceDefaults` heredan:

- OpenTelemetry para logs, trazas y metricas
- health checks basicos
- endpoints `/health` y `/alive` en desarrollo
- service discovery
- configuracion de resiliencia para clientes HTTP

### 13.4 Servicios visibles en ejecucion

En ejecucion local, la topologia debe permitir:

- levantar el dashboard de Aspire
- exponer `ACC.WebApp`
- exponer `ACC.API`
- exponer `ACC.Compiler`
- verificar el estado de los recursos SQL y Redis

### 13.5 Nota sobre topologia futura

La topologia actual ya prepara el terreno para escalamiento posterior, pero no debe confundirse con un despliegue productivo endurecido. Hoy su funcion principal es reproducibilidad local, visibilidad del sistema y coordinacion de servicios durante desarrollo.

## 14. Capas, proyectos y responsabilidades

La siguiente tabla resume el rol de cada proyecto dentro de ACC, su capa predominante y su estado actual.

| Proyecto | Capa predominante | Responsabilidad principal | Dependencias relevantes | Estado |
| --- | --- | --- | --- | --- |
| `ACC.WebApp` | Presentacion / identidad | Host web principal, ASP.NET Identity, shell de acceso, roles, registro, sincronizacion y consumo autenticado de API | `ACC.ServiceDefaults`, `ACC.Shared`, `ACC.WebApp.Client`, SQL Identity, SQL Academic, Redis | Activo |
| `ACC.WebApp.Client` | Presentacion | Cliente web de contenido, guia, agenda, progreso, examenes, compilador y experiencia de usuario | `ACC.Shared` | Activo |
| `ACC.API` | Aplicacion | Dominio academico: modulos, submodulos, temas, lecciones, progreso, aulas, tareas, examenes, notificaciones, navegacion y sincronizacion de usuario academico | `ACC.ServiceDefaults`, `ACC.Data`, `ACC.Shared`, SQL Academic | Activo |
| `ACC.Compiler` | Aplicacion especializada | Compilacion y ejecucion controlada de codigo C# para practica interactiva | `ACC.ServiceDefaults`, Roslyn, Redis en topologia | Activo |
| `ACC.Data` | Infraestructura | Entidades del dominio academico, `ACCDbContext`, configuracion relacional y migraciones | `ACC.Shared`, SQL Academic | Activo |
| `ACC.Shared` | Compartido | DTOs, interfaces, enums, utilidades, rutas y resultados compartidos entre capas | Sin dependencia fuerte de dominio operativo | Activo |
| `ACC.ExternalClients` | Infraestructura | Integraciones y clientes hacia servicios externos relacionados con Charp y otras extensiones | `ACC.Shared` | Activo / evolutivo |
| `ACC.ServiceDefaults` | Infraestructura transversal | OpenTelemetry, health checks, service discovery y resiliencia para servicios | OpenTelemetry, Microsoft Extensions | Activo |
| `ACC.AppHost` | Infraestructura / orquestacion | Declaracion y arranque coordinado de servicios y recursos del sistema | `ACC.WebApp`, `ACC.API`, `ACC.Compiler`, SQL, Redis | Activo |
| `ACC.Tests` | Calidad | Pruebas unitarias y soporte de validacion automatizada | `ACC.API`, `ACC.Shared`, `ACC.Data`, `ACC.WebApp.Client` | Activo |
| `ACC.MultiPlataform` | Presentacion | Cliente MAUI Blazor planificado para escritorio y movil | Por definir en siguiente fase de implementacion | Planeado |

### 14.1 Lectura por agrupacion fisica

Dentro del repositorio, los proyectos se distribuyen principalmente en estas zonas:

- `src/`: backend, datos, compartidos, infraestructura y compilador
- `ACC.WebApp/`: host web y cliente web
- `tests/`: pruebas
- `Docs/`: documentacion tecnica y funcional

### 14.2 Responsabilidad de cada bloque en terminos de mantenimiento

Para mantenimiento cotidiano, la lectura practica del sistema puede resumirse asi:

- si cambia login, roles o registro, el foco suele estar en `ACC.WebApp`
- si cambia contenido academico, progreso, examenes o navegacion, el foco suele estar en `ACC.API` y `ACC.Data`
- si cambia el contrato de datos entre cliente y backend, el foco suele estar en `ACC.Shared`
- si cambia el comportamiento del compilador, el foco suele estar en `ACC.Compiler`
- si cambia la experiencia visual y de interaccion, el foco suele estar en `ACC.WebApp.Client`
- si cambia el baseline operativo, el foco suele estar en `ACC.ServiceDefaults` y `ACC.AppHost`

## 15. Modelo de datos y persistencia

La persistencia de ACC se divide en dos contextos claramente diferenciados: uno para identidad y otro para el dominio academico. Esta separacion responde a una decision arquitectonica explicita orientada a reducir acoplamiento entre autenticacion y reglas del negocio educativo.

### 15.1 Dos contextos, dos esquemas, dos responsabilidades

ACC utiliza dos contextos principales de Entity Framework Core:

- `ApplicationDbContext` para identidad
- `ACCDbContext` para el dominio academico

Cada uno trabaja sobre un esquema distinto:

- `acc_identity` para ASP.NET Identity
- `acc_academic` para el dominio de aprendizaje, progreso, aulas, tareas y examenes

Esta separacion evita mezclar tablas de cuentas con estructuras propias del producto educativo.

### 15.2 Contexto de identidad: `ApplicationDbContext`

`ApplicationDbContext` vive en `ACC.WebApp` y hereda de:

`IdentityDbContext<ApplicationUser, IdentityRole, string>`

Su responsabilidad es almacenar:

- usuarios de identidad
- roles
- claims
- logins externos si llegaran a incorporarse
- tokens de identidad y demas estructuras internas de ASP.NET Identity

El modelo actual de `ApplicationUser` es deliberadamente ligero: hoy extiende `IdentityUser` sin propiedades adicionales. Eso indica que el producto prefiere mantener el perfil academico de usuario fuera del modelo de identidad.

### 15.3 Contexto academico: `ACCDbContext`

`ACCDbContext` es el contexto central del dominio academico y se encuentra en `src/ACC.Data`. Su funcion es orquestar persistencia para:

- catalogo de contenido
- relaciones usuario-contenido
- progreso academico
- aulas y comunicacion
- tareas y agenda
- examenes, intentos, habilitaciones y aprobaciones

El contexto fija como esquema por defecto:

`acc_academic`

Esto da coherencia al dominio y facilita separar a nivel SQL la informacion academica del resto de la plataforma.

### 15.4 Familias principales de entidades

El modelo academico actual puede agruparse en las siguientes familias:

#### Catalogo y estructura del contenido

- `Modulo`
- `SubModulo`
- `Tema`
- `SubTema`
- `Leccion`
- `Capitulo`
- `ContenidoCapitulo`
- `Tag`
- `CapituloTag`

#### Usuario academico y progreso

- `Usuario`
- `UsuarioModulos`
- `UsuarioSubModulos`
- `UsuarioTemas`
- `UsuarioSubTemas`
- `ProgresoUsuario`
- `HistorialCalificaciones`

#### Aulas, comunicacion y agenda

- `Aula`
- `AulaEstudiante`
- `Anuncio`
- `Aviso`
- `Notificacion`
- `Auditoria`
- `Agenda`
- `TareaPersonal`
- `InvitacionAula`

#### Tareas y evaluaciones operativas

- `Tarea`
- `TareasAsignaciones`
- `Evaluacion`
- `EvaluacionResultado`

#### Examenes y control de habilitacion

- `Examen`
- `ExamenModulo`
- `ExamenSubModulo`
- `ExamenIntento`
- `ExamenHabilitado`
- `ExamenAprobatorio`

### 15.5 Relaciones nucleares del modelo

Entre las relaciones mas importantes del contexto se encuentran:

- `Modulo -> SubModulo`
- `SubModulo -> Tema`
- `Tema -> SubTema`
- `SubTema -> Leccion`
- `Tema / SubModulo / Modulo -> Capitulo` segun el caso
- `Usuario -> Agenda` como relacion uno a uno
- `Aula -> AulaEstudiantes`
- `Aula -> Anuncios`
- `Aula -> Tareas`
- `Aula -> Evaluaciones`
- `SubModulo -> ExamenSubModulo`
- `Usuario -> ExamenIntento`

Esto muestra que el dominio no es un simple CMS de lecciones: incluye estructura academica, gestion de usuarios, trabajo en aula, evaluacion y trazabilidad del aprendizaje.

### 15.6 Tipos, constraints e indices relevantes

`ACCDbContext` no solo declara DbSets, tambien concentra decisiones importantes de modelado:

- uso de `decimal(5,2)` para progreso y calificaciones
- uso de `datetimeoffset` para eventos relevantes de progreso e intentos
- claves compuestas en relaciones usuario-contenido
- indices unicos para prevenir duplicados en tags, invitaciones y asignaciones
- indices de rendimiento para progreso, examenes, tareas y notificaciones
- configuraciones de `DeleteBehavior` diferenciadas segun la semantica de cada relacion

Este punto es importante porque parte del valor del modelo no esta solo en las entidades, sino en las garantias e invariantes que se codifican dentro del `OnModelCreating`.

### 15.7 Migraciones y evolucion del modelo

La solucion conserva migraciones para ambos contextos:

- migraciones academicas en `src/ACC.Data/Migrations`
- migraciones de identidad en `ACC.WebApp/ACC.WebApp/Migrations`

Las migraciones historicas tambien dejan ver la evolucion del dominio, especialmente en el caso de lecciones, examenes y entidades de aula. Por eso, para entender el estado presente del sistema, las migraciones deben leerse como historial tecnico, pero la fuente de verdad actual sigue siendo la combinacion de:

- entidades vigentes
- configuracion de `OnModelCreating`
- contratos DTO activos

### 15.8 Rol de `Usuario` dentro del dominio academico

La entidad `Usuario` representa al usuario desde la perspectiva academica, no desde la perspectiva de Identity. Su clave `Id` no es autogenerada: reutiliza el mismo `Id` del usuario de Identity.

Eso permite:

- vincular identidad y perfil academico sin fusionar sus tablas
- mantener progreso, agenda, notificaciones e intentos de examen sobre el mismo identificador funcional
- sincronizar datos basicos como nombre y email desde el flujo de registro

En otras palabras, `Usuario` es la proyeccion academica del usuario autenticado.

## 16. Autenticacion, identidad y sincronizacion de usuario

ACC separa autenticacion y dominio academico, pero los conecta mediante un flujo de sincronizacion controlado. Esta seccion documenta como ocurre ese acoplamiento.

### 16.1 Base de autenticacion en `ACC.WebApp`

`ACC.WebApp` utiliza ASP.NET Identity con:

- `ApplicationUser`
- `IdentityRole`
- `ApplicationDbContext`

La configuracion actual exige:

- cuentas confirmadas para el inicio de sesion (`RequireConfirmedAccount = true`)
- almacenamiento en SQL Server
- creacion de roles base al iniciar la aplicacion

Los roles iniciales definidos por el sistema son:

- `Administrador`
- `Docente`
- `Estudiante`

### 16.2 Rol de `ACC.WebApp` en el acceso

`ACC.WebApp` no actua solo como host visual. Tambien se encarga de:

- registro de usuarios
- login y flujo de cuenta
- gestion de roles
- estado de autenticacion para la experiencia web
- consumo autenticado de `ACC.API`

En este sentido, `ACC.WebApp` es simultaneamente interfaz de acceso e infraestructura de identidad.

### 16.3 Consumo autenticado de `ACC.API`

Del lado de `ACC.API`, la autenticacion se procesa mediante `JwtBearer`. El servicio:

- registra autenticacion bearer
- valida issuer, audience, firma y expiracion cuando la configuracion esta presente
- expone Swagger con definicion de seguridad para `Bearer`

Esto significa que la API academica esta preparada para recibir identidad ya resuelta desde el ecosistema web de ACC.

### 16.4 Flujo de registro y sincronizacion

El flujo actual de alta de usuario puede resumirse asi:

1. el usuario se registra en `ACC.WebApp`
2. Identity crea el `ApplicationUser`
3. `UsuarioSyncService` mapea ese usuario a `ApplicationUserDto`
4. `ACC.WebApp` envia ese DTO a `ACC.API`
5. `UsuarioController` recibe la solicitud en `api/Usuario/sincronizar`
6. `UsuarioService` crea o actualiza la entidad `Usuario` en `acc_academic`

Este mecanismo evita duplicar manualmente datos de usuario en dos lados y formaliza la relacion entre identidad y perfil academico.

### 16.5 `UsuarioSyncService`

`UsuarioSyncService` vive en `ACC.WebApp` y cumple una funcion critica: traducir el resultado del alta en Identity al lenguaje del dominio academico. Para ello:

- usa AutoMapper
- crea un `ApplicationUserDto`
- utiliza un `HttpClient` configurado para `ACC.API`
- envia el DTO al endpoint de sincronizacion
- registra errores si la operacion falla

Desde el punto de vista arquitectonico, esta clase es el puente entre la frontera de identidad y el dominio academico.

### 16.6 `UsuarioController` y `UsuarioService`

En `ACC.API`, el endpoint de sincronizacion se expone en:

`POST api/Usuario/sincronizar`

El controlador delega en `IUsuarioService`, cuya implementacion actual:

- valida que exista `Id`
- busca al usuario academico por el mismo `Id` de Identity
- si no existe, crea un nuevo `Usuario`
- si ya existe, actualiza nombre, email y progreso general
- persiste cambios en `ACCDbContext`

Esto vuelve idempotente la sincronizacion basica del perfil.

### 16.7 Union entre identidad y dominio academico

La union entre ambos mundos se establece sobre el mismo identificador:

- `ApplicationUser.Id` en `acc_identity`
- `Usuario.Id` en `acc_academic`

No se trata de una foreign key directa entre contextos, sino de una alineacion funcional basada en el mismo valor de clave. Esa decision permite mantener autonomia de contextos sin perder la capacidad de correlacionar al usuario a traves del sistema.

### 16.8 Responsabilidades separadas

La division final de responsabilidades queda asi:

- `ApplicationUser`: autenticacion, credenciales, roles y estado de cuenta
- `Usuario`: presencia academica del usuario dentro del dominio
- `ACC.WebApp`: gestiona el ciclo de vida de cuenta y dispara sincronizacion
- `ACC.API`: conserva y sirve la proyeccion academica del usuario

### 16.9 Implicaciones de mantenimiento

Para mantenimiento, cualquier cambio en identidad o sincronizacion debe analizarse en ambos lados:

- si cambia el modelo de registro, revisar `ACC.WebApp`
- si cambia el contrato del usuario academico, revisar `ACC.Shared`, `UsuarioService` y `Usuario`
- si cambian claims o estrategia de autenticacion de API, revisar `ACC.API/Program.cs`

Esto es especialmente importante porque la consistencia entre identidad y dominio no depende de una sola tabla, sino de un flujo coordinado entre servicios.

## 17. Navegacion academica y contratos compartidos

La navegacion academica de ACC es el mecanismo que conecta la estructura curricular del sistema con la experiencia de exploracion del usuario dentro de la guia. No se trata solo de un menu jerarquico: esta capa define como se representan los nodos del contenido, como se consultan desde el dominio, como se transportan al cliente y como se convierten finalmente en vistas navegables y lecciones renderizadas.

Su importancia arquitectonica radica en que desacopla la jerarquia academica concreta de las pantallas que la consumen. Gracias a ello, `ACC.API` puede exponer un contrato uniforme para modulos, submodulos, temas, subtemas y lecciones, mientras que el cliente Blazor reutiliza una misma logica para recorrer distintos niveles del arbol.

### 17.1 Jerarquia academica de contenido

La estructura vigente de la guia sigue un arbol academico de cinco niveles:

- `Modulo`
- `SubModulo`
- `Tema`
- `SubTema`
- `Leccion`

Cada nivel encapsula una granularidad distinta del contenido:

- `Modulo` agrupa grandes dominios tematicos del curso
- `SubModulo` organiza subconjuntos tematicos dentro de un modulo
- `Tema` concentra una unidad de avance reconocible para el estudiante
- `SubTema` delimita fragmentos mas pequenos sobre los que puede medirse progreso
- `Leccion` representa la unidad final consumible por el renderizador dinamico

Esta jerarquia no es solo editorial. Tambien condiciona:

- el orden de navegacion dentro de la guia
- el calculo de progreso por subtema
- la construccion de rutas y breadcrumbs
- el punto exacto donde se carga una `LeccionDto` para ser renderizada por `RDL`

### 17.2 Contrato compartido para nodos jerarquicos

Para evitar que cada entidad academica exponga contratos distintos, ACC define una interfaz comun en `ACC.Shared`: `INodoJerarquico`. Esta interfaz estandariza cinco propiedades:

- `Id`
- `Nombre`
- `Descripcion`
- `IdPadre`
- `Tipo`

Con ese contrato, el sistema puede tratar nodos distintos de forma uniforme sin perder el contexto del tipo real al que pertenecen. La propiedad `Tipo`, basada en `TipoNodoJerarquico`, permite distinguir si el nodo actual es un modulo, submodulo, tema, subtema o leccion.

En el transporte hacia el cliente, el contrato concreto utilizado es `NodoJerarquicoDto`. Este DTO implementa `INodoJerarquico` y se convierte en la pieza comun para:

- listar modulos raiz
- obtener hijos de cualquier nodo
- recuperar el padre de un nodo
- construir rutas desde la raiz
- alimentar componentes reutilizables en Blazor

El resultado de estas operaciones no viaja como datos crudos. Se envuelve en `ServiceResult<T>`, un contrato transversal de `ACC.Shared.Core` que unifica:

- estado de exito o error
- mensaje legible
- codigo HTTP
- carga util tipada

Esta decision reduce ambiguedad entre cliente y servidor y mantiene un mismo patron para navegacion, progreso, examenes y otros servicios del dominio.

### 17.3 Implementacion del servicio de navegacion

El contrato de negocio para esta capacidad vive en `INavegacionContenidoService`. Su implementacion principal reside en `NavegacionContenidoService`, dentro de `ACC.API`, y opera directamente sobre `ACCDbContext`.

Las operaciones nucleares del servicio son:

- `ObtenerModulosAsync`: devuelve los nodos raiz de la guia
- `ObtenerHijosAsync`: resuelve hijos segun el `TipoNodoJerarquico` del nodo padre
- `ObtenerPadreAsync`: localiza el padre inmediato del nodo solicitado
- `ObtenerRutaDesdeRaizAsync`: reconstruye el camino ascendente para navegacion contextual
- `ObtenerLeccionAsync`: carga una `LeccionDto` completa lista para renderizado
- `RegistrarUltimaVisitaTemaAsync`: registra una marca temporal de ultima visita sobre `Tema`

La implementacion usa un `switch` por tipo de nodo para proyectar cada nivel al mismo DTO. Eso convierte al servicio en una especie de traductor entre el modelo relacional del dominio y una vista jerarquica estable para el cliente.

Desde el punto de vista de responsabilidad, esta capa:

- conoce la forma real de las relaciones entre entidades
- decide como se proyecta cada nivel a `NodoJerarquicoDto`
- encapsula validaciones basicas de IDs y tipos
- evita que el cliente tenga que entender detalles de tablas o claves foraneas

### 17.4 Superficie HTTP expuesta por la API

`NavegacionContenidoController` publica la navegacion como un conjunto de endpoints especializados bajo `api/NavegacionContenido`. Las rutas principales son:

- `GET modulos`
- `GET hijos/{tipo}/{id}`
- `GET padre/{tipo}/{id}`
- `GET ruta/{tipo}/{id}`
- `GET leccion/{leccionId}`
- `POST tema/{id}/registrar-ultima-visita`

Este diseno mantiene una API pequena, legible y orientada a la experiencia de navegacion. En lugar de exponer CRUDs independientes por cada entidad academica para la guia publica, se expone una superficie enfocada al recorrido del contenido.

Una implicacion importante es que el valor de `tipo` en la ruta depende del enum `TipoNodoJerarquico`. Por ello, cualquier cambio de nombres en el enum o en la estrategia de serializacion puede romper la navegacion cliente-servidor si no se actualiza de forma coordinada.

### 17.5 Consumo desde el cliente Blazor

El cliente web encapsula estas llamadas en `NavegacionContenidoClient`, que actua como adaptador HTTP del lado de `ACC.WebApp.Client`. Este cliente replica los metodos principales del servicio de API y devuelve igualmente `ServiceResult`, preservando el mismo contrato hasta la UI.

Este patron aporta tres ventajas:

- la UI trabaja con respuestas consistentes y no con `HttpResponseMessage` crudos
- el manejo de errores se centraliza
- los componentes pueden concentrarse en estados de carga, exito, vacio o falla

En la practica, el recorrido principal se distribuye entre varios componentes:

- `GuiaNavegador.razor`: punto de entrada de la experiencia de guia
- `GuiaMainComponent.razor`: lista hijos del nodo actual y resuelve la navegacion descendente
- `LeccionView.razor`: solicita una `LeccionDto` concreta
- `RDL.razor`: renderiza la leccion cuando ya se llego al ultimo nivel

### 17.6 Flujo funcional de navegacion

El flujo academico actual puede resumirse en la siguiente secuencia:

1. El usuario entra a `/Guia` y el cliente consulta modulos disponibles.
2. Al seleccionar un modulo, el sistema navega a `/contenido/Modulo/{id}`.
3. La misma pagina reutiliza `GuiaMainComponent` para pedir hijos del nodo actual y seguir descendiendo por `SubModulo`, `Tema` y `SubTema`.
4. Cuando el usuario selecciona una leccion, la navegacion cambia a `/leccion/{id}`.
5. `LeccionView` solicita `ObtenerLeccionAsync`.
6. Si la respuesta es exitosa, la `LeccionDto` se entrega a `RDL`, que construye la experiencia final de lectura y practica.

Este flujo confirma una de las decisiones mas importantes del sistema: la guia no depende de paginas duras por cada contenido, sino de una navegacion basada en datos y un render final tambien guiado por datos.

### 17.7 Relacion con progreso y contexto del usuario

La navegacion academica no opera aislada. En `GuiaMainComponent`, cuando el usuario autenticado recorre subtemas, el cliente consulta y registra informacion de progreso para reflejar estados completados y mantener continuidad en la experiencia.

Esto significa que la navegacion cumple un doble papel:

- mover al usuario entre nodos del contenido
- servir como punto de integracion con sistemas de progreso y seguimiento

Tambien existe la capacidad de registrar `UltimaVisita` sobre `Tema`, lo cual deja abierta la puerta para futuras experiencias de recomendacion, reanudacion o analitica academica mas precisa.

### 17.8 Implicaciones arquitectonicas y de mantenimiento

La capa de navegacion introduce reglas que deben cuidarse cada vez que evoluciona el dominio:

- si se agrega un nuevo nivel jerarquico, debe extenderse `TipoNodoJerarquico`, `INodoJerarquico`, las entidades, el servicio, el controlador y los componentes cliente
- si cambia `LeccionDto`, debe revisarse el mapeo en API y el punto donde `LeccionView` entrega datos a `RDL`
- si se modifican nombres de tipos usados en rutas, debe validarse la compatibilidad con `NavegacionContenidoClient`
- si se cambian relaciones entre `Modulo`, `SubModulo`, `Tema`, `SubTema` o `Leccion`, debe revalidarse la construccion de rutas ascendentes y descendentes

En otras palabras, la navegacion academica es una capa transversal. Aunque a simple vista parezca una funcionalidad de interfaz, en realidad concentra acuerdos de dominio, contratos compartidos, convenciones de transporte y decisiones de experiencia de usuario.

### 17.9 Resumen tecnico de responsabilidades

Dentro de ACC, la responsabilidad de esta capacidad queda distribuida asi:

- `ACC.Data`: modela la jerarquia academica real en entidades y relaciones
- `ACC.Shared`: define enums, interfaces y contratos de transporte reutilizables
- `ACC.API`: resuelve consultas de navegacion y expone endpoints especializados
- `ACC.WebApp.Client`: consume los contratos, presenta nodos y dispara navegacion
- `RDL`: recibe el resultado final de esa cadena cuando el nodo seleccionado es una leccion

Este reparto de responsabilidades mantiene la navegacion como una capacidad cohesiva, reutilizable y suficientemente desacoplada para evolucionar junto con el sistema de lecciones dinamicas documentado en la siguiente tanda.

## 18. Sistema de lecciones dinamicas

El sistema de lecciones dinamicas es uno de los cambios mas importantes respecto a la concepcion original de ACC. En lugar de depender de una plantilla fija o de un unico bloque de contenido monolitico, la leccion actual se compone por secciones desacopladas, opcionales y ordenables, cuyo montaje final depende de datos persistidos y del renderizador `RDL`.

Esta capacidad convierte a la leccion en una unidad academica configurable. La narrativa, los recursos auxiliares y la experiencia de practica ya no estan codificados pagina por pagina, sino descritos por el contrato de datos de `Leccion` y ejecutados en cliente en tiempo de render.

### 18.1 Evolucion del modelo

El documento tecnico historico describia una leccion mucho mas simple, centrada en un contenido HTML general y en banderas como `TieneEvaluacion` o `IdEvaluacion`. Ese enfoque ya no representa correctamente el producto actual.

En la implementacion vigente, los rastros del modelo anterior siguen apareciendo en comentarios y migraciones antiguas, pero la fuente real de verdad es otra. Hoy la leccion se define con:

- bloques textuales diferenciados como `Teoria`, `Ejemplo` y `Practica`
- recursos de apoyo como `CharpTip`, `CharpDialog` y `VideoId`
- banderas funcionales como `TieneActividad`, `TieneCompilador` y `TieneVideo`
- un arreglo `OrdenSecciones` que controla la secuencia exacta de render

Esto significa que la narrativa de la leccion ya no esta implita en el HTML ni fija en la UI. La narrativa se almacena como parte del dato.

### 18.2 Entidad academica `Leccion`

La entidad `Leccion`, ubicada en `ACC.Data`, representa la unidad atomica de aprendizaje consumida al final de la navegacion academica. Sus responsabilidades principales son:

- pertenecer a un `SubTema` mediante `SubtemaId`
- transportar el contenido textual de la leccion
- indicar que recursos interactivos estan activos
- definir el orden en que la UI debe ensamblar la experiencia

Los campos funcionalmente mas relevantes del modelo actual son:

- `IdLeccion`
- `TituloLeccion`
- `DescripcionLeccion`
- `SubtemaId`
- `Teoria`
- `Ejemplo`
- `Practica`
- `CharpTip`
- `CharpDialog`
- `NivelBloom`
- `TieneActividad`
- `UrlActividad`
- `TieneCompilador`
- `TieneVideo`
- `VideoId`
- `OrdenSecciones`

Ademas, `Leccion` implementa `INodoJerarquico`, lo que le permite integrarse de forma natural a la navegacion descrita en la tanda anterior. Aunque la leccion tambien puede relacionarse con `Capitulos`, su papel principal dentro de la guia sigue siendo el de nodo terminal del arbol `Modulo -> SubModulo -> Tema -> SubTema -> Leccion`.

### 18.3 DTO y transporte al cliente

El contrato que viaja al frontend es `LeccionDto`, mapeado desde `Leccion` mediante AutoMapper. Este DTO conserva la estructura necesaria para que el renderizador no tenga que reinterpretar el dominio ni consultar datos complementarios para decidir que mostrar.

En otras palabras:

- `ACC.Data` modela la leccion persistida
- `ACC.API` la recupera y la proyecta
- `ACC.Shared` define el DTO comun
- `ACC.WebApp.Client` la consume sin acoplarse a EF Core ni a la base de datos

La paridad entre entidad y DTO es especialmente importante aqui. Si una propiedad se agrega en `Leccion` pero no llega a `LeccionDto`, la experiencia cliente quedara incompleta. Si el DTO se amplia pero `RDL` no entiende el nuevo campo, la leccion quedara funcionalmente huerfana.

### 18.4 `OrdenSecciones` como motor narrativo

La propiedad mas significativa del sistema es `OrdenSecciones : List<string>`. Esta lista no es un detalle cosmetico; es el motor narrativo de cada leccion.

Su funcion es indicar al renderizador el orden exacto en que debe ensamblarse la experiencia. Gracias a ello, dos lecciones pueden compartir el mismo contrato de datos pero contar historias pedagogicas diferentes. Una puede abrir con `charpDialog`, pasar a `teoria`, continuar con `ejemplo` y terminar con `compilador`; otra puede iniciar con `video`, seguir con `charpTip`, pasar a `practica` y cerrar con `actividad`.

Esa decision tiene varias implicaciones arquitectonicas:

- la UI deja de ser duena de la secuencia pedagogica
- el contenido puede evolucionar sin crear componentes de pagina por leccion
- el orden y la activacion de recursos quedan versionados como datos
- la carga incorrecta de tokens se vuelve detectable de inmediato en tiempo de render

### 18.5 Catalogo de secciones soportadas

Los tokens validos de `OrdenSecciones` se centralizan en `SeccionesContenido`, que funciona como fuente unica de verdad para frontend y backend. Actualmente, las secciones soportadas son las siguientes:

| Token | Origen en `LeccionDto` | Comportamiento en `RDL` |
| --- | --- | --- |
| `charpDialog` | `CharpDialog` | Renderiza el componente `CharpDialog` |
| `charpTip` | `CharpTip` | Renderiza el componente `CharpTip` |
| `teoria` | `Teoria` | Inyecta HTML con `MarkupString` |
| `ejemplo` | `Ejemplo` | Inyecta HTML con `MarkupString` |
| `practica` | `Practica` | Inyecta HTML con `MarkupString` |
| `actividad` | `TieneActividad` + `UrlActividad` | Muestra boton y abre `ModalActividades` |
| `compilador` | `TieneCompilador` | Inserta `CompiladorACC` |
| `video` | `TieneVideo` + `VideoId` | Inserta `LiteYouTube` |

Adicionalmente, `TipoSeccionContenido` mantiene la enumeracion semantica de esas secciones. La combinacion `TipoSeccionContenido` + `SeccionesContenido` evita la dispersion de strings magicos y facilita futuras ampliaciones del sistema.

### 18.6 Funcion del renderizador `RDL`

`RDL.razor` es el renderizador dinamico de lecciones. Su responsabilidad no es cargar datos, sino ensamblar la experiencia final a partir de un `LeccionDto` ya resuelto.

Su comportamiento actual puede resumirse asi:

- renderiza una sola vez el `ModalActividades`
- muestra una cabecera con `NivelBloom`
- recorre `Leccion.OrdenSecciones`
- por cada token ejecuta un `switch`
- inyecta HTML o monta componentes segun corresponda
- si encuentra un token desconocido, muestra `InfoComponent`

Este diseno es deliberadamente estricto. En vez de ignorar silenciosamente un token invalido, el sistema hace visible el problema. Eso ayuda a detectar errores de carga, typo en `OrdenSecciones` o ampliaciones incompletas del catalogo de secciones.

### 18.7 Reglas de renderizado observadas en la implementacion

El render actual impone varias reglas tecnicas que deben quedar explicitas en la guia:

- `Teoria`, `Ejemplo` y `Practica` se insertan como HTML confiado mediante `MarkupString`
- `actividad` requiere `TieneActividad = true` y una `UrlActividad` no vacia
- `compilador` requiere `TieneCompilador = true`
- `video` depende en la practica de `VideoId`; la bandera `TieneVideo` existe en el contrato aunque el render actual valida principalmente el identificador
- `NivelBloom` se usa tanto como texto visible como clase CSS

La ultima regla es especialmente importante. El estilo visual del badge depende de clases como:

- `Recordar`
- `Comprender`
- `Aplicar`
- `Analizar`
- `Evaluar`
- `Crear`

Si se carga un valor arbitrario en `NivelBloom`, el contenido seguira renderizando, pero perdera alineacion semantica y visual con la taxonomia de Bloom ya contemplada por el sistema.

### 18.8 Flujo tecnico extremo a extremo

El recorrido de una leccion dinamica en ACC sigue esta cadena:

1. El usuario navega hasta un nodo `Leccion`.
2. `LeccionView.razor` solicita la leccion a traves de `NavegacionContenidoClient`.
3. El cliente llama al endpoint `api/NavegacionContenido/leccion/{id}`.
4. `NavegacionContenidoService` recupera la entidad `Leccion` desde `ACCDbContext`.
5. AutoMapper proyecta la entidad a `LeccionDto`.
6. El cliente recibe `ServiceResult<LeccionDto>`.
7. `LeccionView` entrega el DTO a `RDL`.
8. `RDL` ensambla la salida final segun `OrdenSecciones`.

Ese flujo confirma que la leccion dinamica no es una simple vista de frontend. Es una capacidad transversal que cruza persistencia, mapeo, contratos compartidos, navegacion y render.

### 18.9 Beneficios y limites del modelo actual

El modelo vigente aporta beneficios concretos:

- flexibilidad para variar la secuencia pedagogica sin duplicar paginas
- reutilizacion de componentes para actividad, video, compilador y apoyos Charp
- separacion clara entre obtencion de datos y renderizado
- facilidad para extender la experiencia por nuevas secciones

Tambien introduce limites que la documentacion debe reconocer:

- no existe aun un modulo administrativo consolidado para crear o editar lecciones desde la interfaz
- el HTML persistido debe producirse con disciplina editorial y tecnica
- el render depende fuertemente de la exactitud de `OrdenSecciones`
- la validacion semantica de contenido recae hoy mas en procesos del equipo que en reglas automatizadas fuertes

### 18.10 Implicaciones de mantenimiento

Cuando el modelo de lecciones cambia, hay varios puntos que deben revisarse de forma coordinada:

- entidad `Leccion`
- `LeccionDto`
- perfil de AutoMapper
- `SeccionesContenido`
- `TipoSeccionContenido`
- `RDL.razor`
- documentacion editorial y tecnica de carga

Si alguno de esos puntos se actualiza por separado, el sistema entra rapidamente en un estado inconsistente: campos invisibles, tokens huerfanos, secciones no renderizadas o estilos sin correspondencia.

## 19. Manual de estilos para creacion de lecciones

El manual de estilos para lecciones define como debe redactarse y estructurarse el contenido HTML que termina dentro de `Teoria`, `Ejemplo` y `Practica`. Su objetivo no es imponer una estetica separada del producto, sino asegurar que las lecciones aprovechen correctamente el contrato visual ya implementado en `AccStyles_lecciones.css` y el contrato tipografico oficial de ACC.

Como `RDL` inyecta estos bloques mediante `MarkupString`, la calidad visual no depende de un editor WYSIWYG ni de una capa intermedia de normalizacion. Depende directamente de como el equipo redacta y marca el contenido.

### 19.1 Principio general

Toda leccion debe escribirse como contenido semantico y legible antes que como maquetacion ad hoc. La prioridad es:

- claridad de lectura
- jerarquia visual estable
- consistencia con la UI de ACC
- compatibilidad con desktop y mobile

Esto implica evitar HTML decorativo innecesario, estilos inline y estructuras que intenten forzar una presentacion distinta a la soportada por el sistema.

### 19.2 Base tipografica obligatoria

Las lecciones deben respetar el contrato tipografico oficial de ACC:

- `Nunito` como fuente de lectura y cuerpo
- `Sora` para jerarquias de encabezado
- `JetBrains Mono` para codigo

En terminos operativos, esto significa que quien redacta contenido no debe intentar fijar `font-family` manualmente dentro del HTML. La tipografia ya esta resuelta por tokens globales y por `AccStyles_lecciones.css`.

Tambien deben respetarse estos principios:

- los parrafos largos deben favorecer lectura relajada
- los encabezados deben marcar jerarquia real, no solo peso visual
- el codigo debe vivir en `code` o `pre > code`
- labels o pseudo-badges deben evitarse dentro del HTML cuando ya existe un componente o patron visual para ello

### 19.3 Estructura recomendada para bloques HTML

Cuando una seccion textual lo requiera, se recomienda usar contenedores coherentes con el CSS existente:

- `div.leccion-teoria`
- `div.leccion-ejemplos`
- `div.leccion-practicas`

Estos wrappers ya tienen estilos definidos en `AccStyles_lecciones.css` y permiten que el contenido respire correctamente dentro de la superficie de la leccion. Aunque `RDL` puede renderizar HTML sin esos contenedores, su uso mejora consistencia visual y facilita que teoria, ejemplos y practica se perciban como bloques diferenciados.

### 19.4 Etiquetas HTML recomendadas

Las etiquetas que mejor se integran hoy con el contrato visual de ACC para lecciones son:

- `h3` para subtitulos dentro de una seccion
- `p` para explicaciones
- `ul` y `ol` para secuencias o enumeraciones
- `strong` para enfasis puntual
- `a` para enlaces complementarios
- `pre` y `code` para bloques o fragmentos de codigo
- `table`, `thead`, `tbody`, `tr`, `th`, `td` para comparativas o referencias tecnicas
- `hr` para separar momentos narrativos cuando realmente haga falta
- `img`, `figure` y `figcaption` cuando el recurso visual aporte claridad real

No se recomienda abusar de:

- `br` como sustituto de estructura
- encabezados profundos innecesarios
- tablas para maquetacion
- `span` con clases arbitrarias que no existan en el contrato visual

### 19.5 Clases visuales soportadas y uso recomendado

El CSS actual de lecciones soporta varias clases utiles para authoring controlado:

- `.highlight`: para remarcar una palabra o idea breve
- `.alert`: para bloques de informacion destacada
- `.alert-info`, `.alert-success`, `.alert-warning`, `.alert-error`: variantes semanticas de alerta
- `.alert-title`: titulo corto dentro de una alerta
- `.fomentador`: bloque de impulso o recomendacion de accion

Uso recomendado:

- `highlight` solo para enfasis puntual, no para parrafos enteros
- `alert-info` para aclaraciones o contexto
- `alert-success` para buenas practicas o confirmaciones
- `alert-warning` para advertencias pedagogicas o errores comunes
- `alert-error` para anti-patrones o fallos frecuentes
- `fomentador` para llamadas a practicar, explorar o continuar

Estas clases deben entenderse como herramientas de apoyo, no como sustituto de una buena redaccion base.

### 19.6 Reglas para codigo y fragmentos tecnicos

El contenido tecnico dentro de una leccion debe seguir estas reglas:

- usar `code` para expresiones cortas, tipos, metodos, palabras clave o rutas
- usar `pre > code` para ejemplos completos
- mantener indentacion limpia y consistente
- evitar capturas de pantalla de codigo cuando el codigo puede escribirse como texto real
- acompanar bloques de codigo con una explicacion breve antes o despues

El objetivo no es solo que el bloque "se vea bien", sino que pueda leerse, copiarse y relacionarse con el concepto que se explica.

### 19.7 Reglas para enlaces, imagenes y multimedia

Los enlaces deben usarse con moderacion y solo cuando:

- amplien el tema
- apunten a una referencia necesaria
- conduzcan a un recurso complementario legitimo

Las imagenes deben cumplir una funcion clara:

- explicar una estructura
- reforzar una comparacion
- apoyar una instruccion dificil de describir solo con texto

No deben usarse imagenes meramente decorativas. Si una leccion incorpora video, este no debe sustituir el contenido central de lectura; debe reforzarlo o resumirlo.

### 19.8 Reglas especificas para bloques Charp

`CharpTip` y `CharpDialog` no son texto libre indiferenciado. Visual y funcionalmente son componentes de acompanamiento.

Por ello:

- `CharpTip` debe ser breve, puntual y orientado a destrabar una idea
- `CharpDialog` puede ser mas expresivo, pero debe seguir siendo directo
- ninguno de los dos debe duplicar la teoria principal
- ninguno debe convertirse en muro de texto

En la practica, estos bloques funcionan mejor cuando:

- introducen una intuicion
- advierten un error comun
- conectan teoria con practica
- invitan a pensar antes de seguir

### 19.9 Regla de oro para estilo de leccion

Una leccion bien estilizada en ACC no es la que "se ve mas producida", sino la que logra:

- lectura fluida
- enfasis claro
- codigo entendible
- apoyos visuales con proposito
- continuidad con la interfaz general del producto

Si el HTML necesita demasiada decoracion para "funcionar", normalmente el problema esta en la estructura pedagogica o en la redaccion, no en la falta de mas estilos.

### 19.10 Checklist de calidad visual antes de publicar

Antes de dar por buena una leccion, debe revisarse al menos lo siguiente:

- los encabezados siguen una jerarquia consistente
- no hay estilos inline
- los bloques de codigo usan `pre` o `code` correctamente
- las tablas son legibles en ancho y proposito
- los links tienen sentido y no saturan la lectura
- las alertas y destacados se usan con moderacion
- el HTML no rompe el espaciado ni la legibilidad dentro de `.leccion-container`
- la lectura sigue siendo comoda en viewport reducido

Este checklist debe ejecutarse viendo la leccion renderizada, no solo leyendo el HTML fuente.

## 20. Manual tecnico-pedagogico para creacion de lecciones

El manual tecnico-pedagogico define como debe pensarse una leccion antes de cargarse al sistema. Su funcion es conectar tres dimensiones que en ACC no deben separarse:

- el objetivo de aprendizaje
- la secuencia pedagogica
- el contrato tecnico del modelo `Leccion`

Una leccion correcta en ACC no es solo un texto bien escrito ni solo un registro valido en base de datos. Debe ser una unidad de aprendizaje progresiva, evaluable en contexto y coherente con el renderizador dinamico.

### 20.1 Proposito pedagogico de una leccion

Cada leccion debe ayudar al estudiante a avanzar de manera concreta sobre una idea delimitada. Esto implica que una leccion no debe intentar resolver un modulo entero ni mezclar demasiados conceptos nuevos al mismo tiempo.

La unidad pedagogica recomendada es:

- un concepto central claramente delimitado
- uno o varios ejemplos cercanos
- una transicion hacia practica o aplicacion
- un cierre que deje accion o reflexion

### 20.2 Punto de partida: objetivo medible

Antes de escribir cualquier bloque, la leccion debe responder estas preguntas:

- que debe poder comprender o hacer el estudiante al terminar
- que error o confusion concreta ayuda a resolver
- en que punto de la progresion del curso se encuentra
- que prerrequisitos asume

Si esas preguntas no pueden responderse con claridad, la leccion todavia no esta lo bastante definida para cargarse.

### 20.3 Uso de la taxonomia de Bloom

ACC ya incorpora `NivelBloom` en el contrato de lecciones y en el render visual. Por eso, la taxonomia no debe tratarse como un adorno documental, sino como una decision de diseno instruccional.

Los niveles soportados visualmente hoy son:

- `Recordar`
- `Comprender`
- `Aplicar`
- `Analizar`
- `Evaluar`
- `Crear`

La seleccion del nivel debe reflejar la exigencia cognitiva principal de la leccion. No se trata de etiquetar por intuicion, sino de alinear:

- profundidad conceptual
- tipo de ejemplo
- tipo de practica
- expectativa de autonomia del estudiante

### 20.4 Secuencia pedagogica recomendada

A partir del modelo actual de ACC, la secuencia mas sana para una leccion suele seguir este patron:

1. abrir con contexto breve o una pregunta detonadora
2. presentar teoria minima suficiente
3. mostrar uno o varios ejemplos accionables
4. conducir a practica o actividad
5. reforzar con Charp, video o compilador solo si realmente aportan

No todas las lecciones deben usar todos los recursos. El valor del sistema dinamico esta precisamente en poder escoger solo los bloques que ayuden a ese objetivo.

### 20.5 Criterios para teoria, ejemplo y practica

`Teoria` debe:

- explicar una idea puntual
- introducir vocabulario tecnico de forma gradual
- evitar definiciones enciclopedicas sin aterrizaje

`Ejemplo` debe:

- traducir la teoria a un caso visible
- usar codigo pequeno y comprensible
- resaltar el por que del resultado, no solo el resultado

`Practica` debe:

- pedir al estudiante hacer algo concreto
- exigir recuperacion o aplicacion, no solo lectura
- dejar claro que se espera observar

Una leccion que solo tiene teoria rara vez aprovecha bien la propuesta de valor de ACC.

### 20.6 Criterios para `CharpTip` y `CharpDialog`

Desde el punto de vista pedagogico, Charp debe guiar, no sustituir el esfuerzo cognitivo del estudiante.

Por eso:

- `CharpTip` debe ofrecer una pista o intuicion, no la solucion completa
- `CharpDialog` puede contextualizar o acompanar, pero no debe resolver el ejercicio por el usuario
- ambos deben reforzar comprension o motivar exploracion, no repetir texto ya dicho

Una buena regla operativa es esta: si el bloque de Charp puede borrarse sin perder nada, probablemente sobra; si al leerlo el estudiante deja de pensar por si mismo, esta mal calibrado.

### 20.7 Criterios para actividad, video y compilador

Estos recursos deben agregarse solo cuando tengan una funcion didactica clara:

- `actividad`: cuando exista una practica externa realmente alineada con el objetivo
- `video`: cuando ayude a visualizar o resumir una explicacion compleja
- `compilador`: cuando el valor principal este en probar codigo, experimentar o verificar comportamiento

No deben activarse por relleno. Una leccion con demasiados recursos accesorios pierde foco y fatiga al estudiante.

### 20.8 Regla de progresividad

ACC busca una experiencia progresiva. Eso obliga a que cada leccion:

- parta de lo que el estudiante ya debio ver antes
- agregue una dificultad nueva y reconocible
- no asuma saltos cognitivos innecesarios
- prepare el terreno para la siguiente pieza del recorrido

Cuando una leccion exija demasiados prerrequisitos ocultos, el problema no se corrige agregando mas texto: debe dividirse o reubicarse dentro de la jerarquia academica.

### 20.9 Diseno de `OrdenSecciones` con sentido pedagogico

`OrdenSecciones` no debe armarse solo por disponibilidad de campos. Debe construirse como una secuencia instruccional.

Ejemplos sanos:

- `charpDialog -> teoria -> ejemplo -> practica`
- `teoria -> ejemplo -> compilador -> actividad`
- `video -> teoria -> charpTip -> practica`

Ejemplos pobres:

- abrir con demasiados recursos seguidos sin contexto
- mezclar video, actividad y compilador sin un hilo claro
- cerrar una leccion sin transicion hacia accion o verificacion

La pregunta correcta no es “que bloques tiene la leccion”, sino “en que orden ayudan mejor a aprender”.

### 20.10 Errores pedagogicos frecuentes

Al crear lecciones para ACC deben evitarse especialmente estos errores:

- explicar demasiado y practicar poco
- usar ejemplos demasiado grandes para el nivel del estudiante
- introducir varios conceptos nuevos a la vez
- convertir `CharpTip` o `CharpDialog` en respuestas completas
- usar `NivelBloom` sin relacion real con la exigencia cognitiva
- cargar recursos externos solo para hacer la leccion “mas completa”

Estos errores suelen volver mas pesada la experiencia sin mejorar aprendizaje real.

### 20.11 Criterios de aprobacion interna para una leccion

Antes de considerar lista una leccion, el equipo deberia poder afirmar que:

- el objetivo de aprendizaje esta claro
- el nivel de Bloom elegido tiene sentido
- la teoria es suficiente pero no excesiva
- el ejemplo aterriza el concepto
- la practica exige accion real
- `OrdenSecciones` refleja una secuencia pedagogica coherente
- Charp, video, actividad y compilador solo aparecen cuando agregan valor

Si una leccion cumple tecnicamente pero falla en esta revision, todavia no esta lista desde el punto de vista de producto educativo.

### 20.12 Relacion con el modelo tecnico

Este manual no sustituye al manual tecnico de carga; lo complementa. La relacion entre ambos es directa:

- este apartado define que tipo de experiencia debe construirse
- la seccion 21 define como cargarla al sistema sin romper contratos

En ACC, pedagogia y tecnica no son capas separadas. La calidad final de una leccion depende de que ambas avancen coordinadas.

## 21. Manual tecnico de insercion y carga de lecciones

La carga tecnica de lecciones en ACC debe entenderse a partir del sistema vigente, no del flujo historico descrito por la guia anterior. Hoy no existe un CRUD administrativo formal y cerrado para alta de lecciones dentro de la solucion activa; por ello, la insercion debe documentarse como un proceso tecnico controlado por el equipo, alineado al modelo `Leccion`, al mapeo hacia `LeccionDto` y a las reglas del renderizador `RDL`.

### 21.1 Objetivo de esta seccion

Esta seccion define como debe construirse una leccion para que:

- pueda persistirse correctamente en `ACCDbContext`
- se integre a la jerarquia academica mediante `SubtemaId`
- viaje intacta a `LeccionDto`
- renderice sin errores en `RDL`

La meta no es solo insertar un registro en la tabla `Lecciones`, sino cargar una unidad de contenido tecnicamente valida para toda la cadena de navegacion y render.

### 21.2 Prerrequisitos tecnicos

Antes de dar de alta una leccion, debe existir ya su contexto academico:

- `Modulo`
- `SubModulo`
- `Tema`
- `SubTema`

La relacion obligatoria de una leccion es con `SubTema`, mediante `SubtemaId`. Si ese nodo padre no existe o no esta alineado con la estructura esperada, la leccion podra persistirse incorrectamente o quedar inaccesible desde la navegacion.

Tambien deben estar disponibles y alineados:

- la migracion actual de `ACCDbContext`
- el contrato `LeccionDto`
- el perfil de AutoMapper
- el catalogo de tokens en `SeccionesContenido`

### 21.3 Campos minimos para una leccion valida

Para que una leccion pueda navegarse y renderizarse de forma aceptable, los campos minimos que deben definirse son:

- `TituloLeccion`
- `DescripcionLeccion`
- `SubtemaId`
- `OrdenSecciones`

En la practica, casi siempre tambien deberian definirse:

- al menos uno entre `Teoria`, `Ejemplo`, `Practica`, `CharpTip`, `CharpDialog`, `UrlActividad`, `VideoId`
- `NivelBloom`, para conservar consistencia pedagogica y visual

Una leccion con `OrdenSecciones` pero sin datos utiles en los campos asociados puede persistirse, pero producira una experiencia vacia o incompleta en el render.

### 21.4 Regla principal de integridad

La regla central de carga es simple: cada token declarado en `OrdenSecciones` debe tener respaldo real en el resto del objeto `Leccion`.

Ejemplos:

- si `OrdenSecciones` incluye `teoria`, `Teoria` no debe ir vacio
- si incluye `actividad`, deben existir `TieneActividad = true` y `UrlActividad`
- si incluye `compilador`, `TieneCompilador` debe estar activo
- si incluye `video`, debe existir `VideoId`
- si incluye `charpTip`, debe existir contenido en `CharpTip`

ACC no resuelve automaticamente esas incoherencias. Algunas secciones simplemente no se mostraran, y otras pueden terminar en mensaje de error si el token no pertenece al catalogo soportado.

### 21.5 Tokens permitidos en `OrdenSecciones`

Los valores tecnicamente validos hoy son:

- `charpDialog`
- `charpTip`
- `teoria`
- `ejemplo`
- `practica`
- `actividad`
- `compilador`
- `video`

Estos valores deben respetarse exactamente. No deben cambiarse por sinonimos, traducciones, mayusculas arbitrarias o variantes editoriales. `RDL` depende de esos literales y `SeccionesContenido` es la referencia oficial.

### 21.6 Secuencia recomendada de construccion

Para alta o carga de una leccion, el proceso recomendado es el siguiente:

1. Identificar el `SubTema` destino y confirmar su `Id`.
2. Definir `TituloLeccion`, `DescripcionLeccion` y `NivelBloom`.
3. Redactar los bloques base de contenido: `Teoria`, `Ejemplo`, `Practica`, `CharpTip` o `CharpDialog`, segun corresponda.
4. Determinar si la leccion incluye actividad externa, video o compilador.
5. Construir `OrdenSecciones` con los tokens en la secuencia pedagogica deseada.
6. Verificar que cada token tenga datos de respaldo.
7. Persistir la entidad en la base academica o mediante el proceso tecnico de carga usado por el equipo.
8. Validar la navegacion hasta `/leccion/{id}` y confirmar el render real.

Esta secuencia evita el error comun de cargar primero un registro minimo y dejar la coherencia pedagogica para despues.

### 21.7 Criterios tecnicos por tipo de seccion

Al cargar una leccion, deben considerarse las siguientes reglas operativas:

- `Teoria`, `Ejemplo` y `Practica` aceptan HTML y deben redactarse pensando en el CSS existente de lecciones
- `CharpTip` y `CharpDialog` deben contener texto listo para el componente que los presenta
- `UrlActividad` debe ser una URL funcional, porque se carga en iframe dentro de `ModalActividades`
- `VideoId` debe corresponder al identificador esperado por `LiteYouTube`
- `NivelBloom` debe usar los valores ya estilizados por `AccStyles_lecciones.css`

Como el HTML se renderiza con `MarkupString`, el sistema actual asume que el contenido ya fue validado antes de persistirse. Esto vuelve especialmente importante el control editorial y tecnico del material fuente.

### 21.8 Validacion posterior a la carga

Despues de insertar o actualizar una leccion, se recomienda validar al menos estos puntos:

- la leccion aparece como hija del `SubTema` correcto
- `/leccion/{id}` responde sin error
- `RDL` renderiza todas las secciones esperadas
- no aparece `InfoComponent` por token desconocido
- actividad, video y compilador se abren o montan correctamente
- el badge de `NivelBloom` usa una clase coherente con el valor cargado

La validacion debe hacerse sobre la UI real, no solo sobre la base de datos, porque varios errores de consistencia solo se manifiestan en tiempo de render.

### 21.9 Escenarios tipicos de error

Los problemas mas comunes al cargar lecciones son:

- `SubtemaId` incorrecto, que deja la leccion fuera de la ruta esperada
- token mal escrito en `OrdenSecciones`
- bloque textual vacio para una seccion declarada
- `UrlActividad` o `VideoId` sin valor util
- cambios en `Leccion` sin sincronizar con `LeccionDto`
- nueva seccion agregada en datos pero no soportada en `RDL`

Muchos de estos errores no impiden guardar la entidad, pero si degradan la experiencia del usuario o rompen parcialmente el renderizado.

### 21.10 Procedimiento cuando se agrega una nueva seccion

Si el equipo necesita incorporar una nueva clase de bloque para lecciones, el procedimiento tecnico minimo debe incluir:

1. agregar el nuevo concepto al modelo `Leccion`
2. extender `LeccionDto`
3. ampliar `TipoSeccionContenido`
4. registrar el token en `SeccionesContenido`
5. actualizar AutoMapper si la paridad deja de ser implicita
6. implementar el caso correspondiente en `RDL`
7. ajustar estilos o componentes auxiliares si aplica
8. actualizar esta guia tecnica y la guia editorial

Este punto es clave: en ACC, agregar una seccion no es un cambio solo de contenido. Es un cambio transversal de contrato, render y documentacion.

### 21.11 Estado actual del proceso de carga

El estado actual puede resumirse asi:

- el modelo de leccion ya esta bien definido a nivel de dominio
- el renderizador dinamico ya consume ese modelo de forma estable
- la navegacion ya integra lecciones como nodo terminal
- la creacion y mantenimiento de lecciones sigue dependiendo de procesos tecnicos del equipo, no de una consola administrativa madura

Por ello, esta seccion debe leerse como una guia operativa de control de calidad para carga de contenido, no como manual de uso de una herramienta de authoring ya terminada.

## 22. Sistema de examenes, progreso y desbloqueos

El sistema de examenes, progreso y desbloqueos en ACC ya no forma parte del contrato basico de `Leccion`. Hoy constituye un subsistema propio que cruza interfaz, servicios de dominio, persistencia de intentos y reglas de habilitacion. Esta separacion es una de las decisiones mas importantes de la evolucion del producto, porque evita mezclar contenido educativo con control de avance.

### 22.1 Componentes conceptuales del subsistema

Este subsistema combina tres responsabilidades distintas:

- registrar avance del estudiante por `SubTema`
- determinar cuando un examen queda habilitado
- registrar intentos y aprobaciones de examenes

Aunque estas capacidades se conectan entre si, no deben confundirse:

- `ProgresoUsuario` representa avance sobre contenido
- `ExamenHabilitado` representa permiso de acceso a un examen
- `ExamenIntento` representa un intento real ya ejecutado
- `ExamenAprobatorio` conserva el primer aprobado como hito trazable

### 22.2 Modelo de dominio relevante

Las entidades mas importantes de este sistema son:

- `ProgresoUsuario`
- `Examen`
- `ExamenSubModulo`
- `ExamenModulo`
- `ExamenIntento`
- `ExamenHabilitado`
- `ExamenAprobatorio`

El reparto actual responde a una logica clara:

- `ExamenSubModulo` se asocia al cierre de un submodulo
- `ExamenModulo` se asocia al cierre de un modulo
- `Examen` cubre examenes libres o genericos fuera de esa jerarquia
- `ExamenIntento` permite trazabilidad de desempeno y numero de intento
- `ExamenAprobatorio` evita recalcular constantemente el primer aprobado
- `ExamenHabilitado` materializa el desbloqueo para lectura rapida en UI

El enum `ExamenTipo` estandariza hoy tres categorias:

- `SubModulo`
- `Modulo`
- `Libre`

### 22.3 Progreso academico por subtema

El progreso academico del recorrido principal se registra en `ProgresoUsuario`. Cada fila vincula:

- `UsuarioId`
- `SubTemaId`
- `Fecha`
- `Completado`

Desde el punto de vista funcional, este modelo permite dos cosas distintas:

- guardar huella de ultima visita o avance reciente
- marcar un subtema como completado para disparar evaluacion de prerrequisitos

La primera se usa como continuidad de navegacion. La segunda es la que activa las reglas de desbloqueo de examenes.

### 22.4 Flujo reactivo de desbloqueo

La regla principal del sistema es reactiva. Cuando el estudiante marca un subtema como completado, `ProgresoUsuarioService` persiste el cambio y llama a `IPrerrequisitosService.EvaluarDesbloqueosPorProgresoAsync`.

Ese servicio aplica hoy dos reglas nucleares:

1. si el usuario completo todos los subtemas de un submodulo, se habilita su examen de submodulo
2. si el usuario aprobo todos los examenes de submodulo de un modulo, se habilita el examen de modulo

La habilitacion no se resuelve recalculando cada vez desde cero en la UI. Se persiste en `ExamenesHabilitados`, con datos como:

- `UsuarioId`
- `Tipo`
- `RefId`
- `Habilitado`
- `FechaHabilitacion`
- `ReglaFuente`

Eso convierte el desbloqueo en una lectura rapida y desacopla el front de la logica de negocio completa.

### 22.5 `PrerrequisitosService` como centro de reglas

`PrerrequisitosService` es la pieza mas importante del subsistema de habilitacion. Su responsabilidad no es renderizar, ni listar examenes, ni registrar intentos. Su responsabilidad es responder dos preguntas:

- este examen ya esta habilitado para este usuario
- despues de este evento, hay que desbloquear algo mas

Para ello:

- consulta completitud de submodulos a partir de `Temas`, `SubTemas` y `ProgresoUsuarios`
- consulta aprobaciones de submodulo a partir de `ExamenesIntentos`
- hace `upsert` en `ExamenesHabilitados`
- conserva `ReglaFuente` para trazabilidad basica

Esta centralizacion es importante porque evita que UI, controladores o clientes HTTP implementen reglas duplicadas.

### 22.6 Registro de intentos y aprobacion

El registro de intentos vive en `ExamenesService`. Esta capa no se limita a guardar un DTO: aplica validaciones de dominio relevantes.

Entre las reglas actuales estan:

- el intento debe referenciar exactamente un examen
- el examen referido debe existir
- el numero de intento se calcula en servidor
- la fecha se sella en servidor si no viene informada
- la aprobacion se decide en servidor segun `PuntajeAprobacion`

Si un intento resulta aprobado:

- se persiste el intento
- se registra el primer `ExamenAprobatorio` si no existia
- si el examen es de `SubModulo`, se dispara `EvaluarDesbloqueosPorAprobacionAsync`

Esto conecta formalmente el desempeno del estudiante con la apertura del siguiente nivel de evaluacion.

### 22.7 Superficie API del subsistema

La API expone este sistema en dos frentes principales.

`ProgresoUsuarioController` publica endpoints para:

- obtener ultimo subtema visitado
- guardar progreso
- marcar subtema como completado
- consultar si un subtema ya esta completado
- consultar si un examen esta habilitado

`ExamenesController` publica endpoints para:

- listar examenes de submodulo
- obtener un examen de submodulo
- listar examenes de modulo
- consultar intentos por usuario
- consultar ultimo intento de un examen
- registrar un intento nuevo

La separacion tiene sentido: `ProgresoUsuarioController` gobierna avance y habilitacion, mientras `ExamenesController` gobierna catalogos y ejecucion de intentos.

### 22.8 Consumo en cliente y experiencia de usuario

Del lado del cliente, las piezas mas relevantes son:

- `BtnCompletarSubtema.razor`
- `ProgresoUsuarioClient`
- `ExamenesServiceClient`
- `ExamenesMain.razor`
- `Examen.razor`
- `ExamenView.razor`
- `ExamenRender.razor`

El flujo principal actual es:

1. el usuario recorre la guia y llega a un `SubTema`
2. al pulsar completar, `BtnCompletarSubtema` llama `MarcarSubtemaComoCompletadoAsync`
3. el backend persiste progreso y reevalua desbloqueos
4. el cliente vuelve a consultar si el examen de submodulo ya esta habilitado
5. `Examen.razor` consulta `ExamenHabilitadoAsync` para decidir si el boton esta activo o bloqueado
6. `ExamenView.razor` carga el examen por `slug` y `refId`
7. `ExamenRender.razor` inyecta el HTML del examen para su presentacion

Este flujo confirma que la UI depende de estados ya resueltos por el backend, no de recalculos locales.

### 22.9 Diferencia entre catalogo de examen y desbloqueo

Una diferencia importante para la documentacion es esta:

- `ExamenesServiceClient` trae catalogos e intentos
- `ProgresoUsuarioClient` resuelve progreso y estado de habilitacion

Es decir, saber que un examen existe no implica que este disponible para el usuario. Esa separacion evita errores comunes de UX y de modelado.

### 22.10 Implicaciones de mantenimiento

Este subsistema exige cuidado cuando se modifica:

- si se agrega un nuevo `ExamenTipo`, deben actualizarse enums, DTOs, clientes, rutas y reglas
- si se agregan nuevas reglas de desbloqueo, deben terminar en `PrerrequisitosService`
- si se cambia la forma de aprobar examenes, debe revisarse `ExamenesService` y la creacion de `ExamenAprobatorio`
- si cambia la jerarquia academica, deben revalidarse las consultas de completitud por submodulo

La regla practica es simple: el front puede cambiar su presentacion, pero la fuente de verdad del desbloqueo debe seguir concentrada en backend.

## 23. Compilador en linea

El compilador en linea de ACC es el sistema que transforma la practica de codigo en retroalimentacion inmediata. Documentalmente debe llamarse `ACC.Compiler`, aunque su implementacion actual siga ubicada en `src/API_CompilerACC`.

### 23.1 Rol funcional dentro del producto

El compilador no es un extra visual. Su funcion pedagogica es permitir que el estudiante:

- pruebe fragmentos de C#
- observe salida estandar
- detecte errores de compilacion
- experimente con pequenas variantes de codigo durante la leccion

Por ello aparece tanto como pagina propia (`/Compilador`) como bloque embebible dentro de lecciones dinamicas mediante el token `compilador`.

### 23.2 Arquitectura actual del compilador

La solucion actual separa la experiencia de compilacion en dos partes:

- cliente `CompiladorACC.razor` dentro de `ACC.WebApp.Client`
- microservicio `ACC.Compiler` con `CompileController` y `RoslynCompileService`

La topologia local levantada por `ACC.AppHost` incluye al compilador como servicio dedicado y tambien le asigna Redis en infraestructura. Sin embargo, la logica de compilacion visible hoy sigue centrada en Roslyn y ejecucion en memoria, no en cache distribuido.

### 23.3 Flujo tecnico actual

El flujo principal implementado hoy es:

1. el usuario escribe codigo en `CompiladorACC.razor`
2. el componente obtiene el contenido actual del editor CodeMirror por JavaScript
3. se serializa `{ Code, Input }`
4. se publica por HTTP a `ServiceRoots.ACC_COMPILER_Url`
5. `CompileController` recibe la solicitud en `ACC.Compiler`
6. `RoslynCompileService` compila y ejecuta
7. el servicio devuelve salida o errores al cliente

El resultado vuelve al componente y se muestra directamente en la caja de salida.

### 23.4 Editor y experiencia cliente

`CompiladorACC.razor` implementa una experiencia relativamente autocontenida:

- usa CodeMirror como editor
- permite cambiar tema visual
- admite `stdin` para entrada del programa
- presenta la salida en un bloque `pre`
- enlaza a una guia de uso del compilador

Desde el punto de vista de producto, esto permite que el compilador funcione tanto como herramienta libre de practica como recurso incrustado dentro de una leccion.

### 23.5 `RoslynCompileService` y modelo de ejecucion

La implementacion actual de `RoslynCompileService` trabaja asi:

- parsea el codigo con `CSharpSyntaxTree`
- construye una compilacion de consola con Roslyn
- limita referencias a ensamblados base de `System.*`
- emite a `MemoryStream`
- carga el assembly en memoria
- redirige `Console.Out`, `Console.Error` y `Console.In`
- invoca el `Main`

Si la compilacion falla, devuelve un texto consolidado con diagnosticos. Si la ejecucion falla, devuelve la excepcion capturada como error de tiempo de ejecucion.

### 23.6 Alcance real y limites actuales

La documentacion debe ser precisa con el alcance actual:

- si existe aislamiento logico basico por ejecucion en memoria
- no hay evidencia en esta implementacion de sandbox endurecido a nivel contenedor por ejecucion individual
- el servicio no expone una capa compleja de colas, cache o rate limiting en el codigo visible
- Redis aparece en topologia, pero no como parte explicita de la logica principal del compilador

Tambien existe un `CompilerController` dentro de `ACC.API`, apoyado en `ACC.Shared.Core.CompileService`, que actua como puente HTTP hacia el compilador dedicado. Sin embargo, el cliente actual usa de forma directa `ServiceRoots.ACC_COMPILER_Url`, por lo que ese camino debe considerarse apoyo legacy o evolutivo, no flujo principal vigente del frontend.

### 23.7 Contratos y dependencias compartidas

Las piezas compartidas mas relevantes para este sistema son:

- `CompileRequest`
- `ServiceRoots.ACC_COMPILER_Url`
- `CompileService` en `ACC.Shared.Core`

Estas piezas mantienen coherencia entre cliente, API y servicio dedicado, aunque hoy convivan rutas y nombres heredados del periodo en que el compilador aun no estaba totalmente consolidado como `ACC.Compiler`.

### 23.8 Implicaciones de mantenimiento

Para mantenimiento, cualquier cambio importante en el compilador exige revisar al menos:

- `CompiladorACC.razor`
- scripts de CodeMirror y su inicializacion
- `ServiceRoots`
- `CompileController`
- `RoslynCompileService`
- `ACC.AppHost`

Si en el futuro se endurece el aislamiento, se agregan politicas de ejecucion o se incorpora cache real con Redis, esta seccion debera actualizarse para distinguir claramente entre topologia de infraestructura y comportamiento efectivo del servicio.

## 24. Charp e integraciones externas

Charp es la capa de acompanamiento inteligente de ACC. Su funcion no es reemplazar la progresion academica ni responder todo por el estudiante, sino ofrecer contexto, pistas y apoyo durante la experiencia de aprendizaje.

### 24.1 Formas actuales de presencia de Charp

Hoy Charp aparece en tres formas principales dentro del producto:

- como widget global embebido en la shell de `ACC.WebApp`
- como pagina dedicada en `/Charp-IA`
- como bloques contextuales dentro de lecciones mediante `CharpTip` y `CharpDialog`

Esta distribucion permite una combinacion util:

- acceso transversal desde cualquier parte del sistema
- espacio dedicado para interaccion mas abierta
- apoyo puntual y contextualizado dentro de la narrativa de la leccion

### 24.2 Integracion global con Chatbase

La integracion activa de chatbot externo se realiza hoy con Chatbase. El `MainLayout.razor` incluye el script `embed.min.js` con un `chatbotId` fijo, mientras `App.razor` define `window.embeddedChatbotConfig` con el mismo identificador y dominio.

Esto implica que la experiencia principal actual de Charp depende de un proveedor SaaS externo. En consecuencia:

- la logica conversacional no vive dentro de `ACC.API`
- el modelo de IA no es mantenido localmente por ACC
- la disponibilidad del asistente depende tambien del servicio externo

### 24.3 Pagina dedicada de Charp

Ademas del widget embebido, ACC ofrece una pagina dedicada en `MainCharp-IAComponent.razor`. Esta vista monta un `iframe` al chatbot de Chatbase y lo presenta dentro de una superficie propia.

Desde el punto de vista de producto, esta pagina sirve para:

- separar una sesion de consulta mas libre del flujo de la leccion
- ofrecer una experiencia focalizada en preguntas o apoyo general
- mantener coherencia visual con el resto del sistema sin implementar un cliente conversacional propio

### 24.4 Charp contextual dentro de lecciones

El valor pedagogico mas fino de Charp aparece dentro de las lecciones. Los campos `CharpTip` y `CharpDialog` del modelo `Leccion` permiten insertar acompanamiento directamente en `RDL`.

Esto da lugar a dos modalidades complementarias:

- `CharpTip`: pista breve o intuicion puntual
- `CharpDialog`: acompanamiento mas expresivo y visible dentro de la secuencia de la leccion

Ambos componentes aceptan HTML por `MarkupString`, por lo que el equipo puede afinar el tono y la estructura del mensaje de acuerdo con el objetivo de la leccion.

### 24.5 Estado real de `ACC.ExternalClients`

Aunque la arquitectura documenta `ACC.ExternalClients` como capa de integraciones, el estado actual del proyecto muestra que este proyecto aun opera mas como contenedor estructural que como libreria madura de clientes externos. Su `csproj` referencia `ACC.Shared` y define carpetas base, pero no expone aun una implementacion rica equivalente al peso funcional que hoy tiene Chatbase dentro de la experiencia.

Eso obliga a documentar con honestidad la situacion actual:

- la integracion externa principal existe y esta operativa
- su implementacion visible vive mayoritariamente en la capa web
- `ACC.ExternalClients` sigue siendo una pieza activa pero evolutiva

### 24.6 Implicaciones arquitectonicas

La integracion actual de Charp tiene varias implicaciones:

- reduce complejidad interna al no hospedar un sistema conversacional propio
- acelera entrega de valor para el producto
- desplaza parte de la dependencia tecnica a un proveedor externo
- exige cuidar como se mezcla la voz de Charp con la pedagogia de ACC

Tambien explica por que Charp debe documentarse mas como capacidad de integracion y experiencia educativa que como microservicio interno del backend academico.

### 24.7 Reglas de producto para el uso de Charp

Aunque la IA externa pueda responder libremente, la intencion de producto sigue siendo:

- orientar en lugar de resolver automaticamente
- reforzar progresividad
- contextualizar sin romper el flujo de aprendizaje
- acompanar practica y comprension, no sustituirlas

Estas reglas ya se reflejan en la documentacion pedagogica del proyecto y deben seguir guiando cualquier evolucion futura, ya sea con Chatbase u otro proveedor.

### 24.8 Mantenimiento y evolucion futura

Si esta area evoluciona, hay varios caminos naturales:

- mover mas integraciones reales a `ACC.ExternalClients`
- abstraer configuracion sensible del proveedor externo
- enriquecer la experiencia contextual de Charp en lecciones
- explorar clientes o proveedores alternos sin cambiar la intencion pedagogica

Mientras eso ocurre, la documentacion debe mantener una distincion clara entre:

- la experiencia activa actual basada en Chatbase
- la capa de integraciones planeada o en consolidacion dentro de `ACC.ExternalClients`

## 25. Observabilidad, resiliencia y operacion

La operacion tecnica de ACC descansa hoy en tres pilares: instrumentacion comun, health checks y topologia reproducible. Estos elementos no sustituyen una plataforma productiva endurecida, pero si ofrecen una base clara para diagnostico, arranque coordinado y evolucion futura del sistema.

### 25.1 Base comun de servicio

`ACC.ServiceDefaults` concentra la configuracion transversal que comparten los servicios principales. Su extension `AddServiceDefaults` activa:

- OpenTelemetry para logs, metricas y trazas
- health checks basicos
- service discovery
- resiliencia estandar para `HttpClient`

Esto reduce configuracion duplicada y da un comportamiento uniforme a `ACC.API`, `ACC.WebApp` y `ACC.Compiler`.

### 25.2 OpenTelemetry

La instrumentacion actual incluye:

- `AddAspNetCoreInstrumentation`
- `AddHttpClientInstrumentation`
- `AddRuntimeInstrumentation`
- `AddSource` con el nombre de aplicacion para trazas

Ademas, los logs incluyen mensajes formateados y scopes. Esto proporciona una capa minima pero funcional de observabilidad distribuida sobre solicitudes HTTP, dependencias salientes y comportamiento del runtime.

La exportacion OTLP solo se activa cuando existe `OTEL_EXPORTER_OTLP_ENDPOINT`. Esto implica que:

- la capacidad de exportar telemetria ya esta preparada
- su activacion efectiva depende de configuracion de entorno
- el repositorio no obliga por si mismo a un backend especifico de observabilidad

### 25.3 Health checks y endpoints de salud

Los servicios incorporan un check `self` etiquetado como `live`. A partir de eso, `MapDefaultEndpoints` expone:

- `/health`
- `/alive`

Estos endpoints solo se publican automaticamente en entorno de desarrollo. La decision es correcta para el estado actual del proyecto porque evita exponer chequeos en otros ambientes sin una politica de seguridad mas dura.

### 25.4 Resiliencia de red

La configuracion de `HttpClient` en `ACC.ServiceDefaults` activa por defecto:

- `AddStandardResilienceHandler`
- service discovery para clientes salientes

Esto le da a la solucion una base moderna para retries, timeouts y protecciones comunes sin que cada servicio tenga que reconfigurarlas desde cero.

Adicionalmente, tanto `ACC.API` como `ACC.WebApp` usan `EnableRetryOnFailure` en SQL Server, lo que mejora tolerancia a fallos transitorios de conexion en ambientes locales y distribuidos.

### 25.5 Operacion local con `ACC.AppHost`

La experiencia operativa principal del repo esta centrada en `ACC.AppHost`. Este proyecto declara y arranca:

- SQL Server para identidad
- SQL Server para dominio academico
- Redis
- `ACC.WebApp`
- `ACC.API`
- `ACC.Compiler`

Tambien define scripts de creacion para `ACC_Identity` y `ACC_Academic`, publica cadenas de conexion y explicita dependencias entre servicios con `WaitFor`.

Esto convierte al AppHost en la pieza central de operacion local y en la fotografia mas fiel de la topologia vigente.

### 25.6 Estado real de operacion

La situacion actual puede resumirse asi:

- hay observabilidad base suficiente para desarrollo y diagnostico tecnico
- existe topologia reproducible mediante Aspire
- hay salud, resiliencia y service discovery a nivel base
- no hay aun un documento separado de operacion productiva endurecida

Por ello, esta guia documenta una operacion local seria y mantenible, pero no debe confundirse con un runbook completo de produccion empresarial.

## 26. Herramientas usadas y flujo operativo del equipo

El trabajo tecnico de ACC combina herramientas de desarrollo, coordinacion, validacion y documentacion. Esta seccion resume el flujo operativo real del equipo a partir del repositorio y de la documentacion historica actualizada.

### 26.1 Herramientas principales

Las herramientas mas visibles en el flujo de trabajo han sido:

- .NET SDK 8
- Visual Studio 2022 y VS Code
- Docker Desktop
- SQL Server
- SQL Server Management Studio
- Postman
- Draw.io
- Git y GitHub
- Discord y Teams
- Miro para trabajo UI/UX

En el ambito tecnico del producto tambien forman parte del flujo:

- .NET Aspire para orquestacion local
- OpenTelemetry para telemetria
- CodeMirror para experiencia de compilacion
- Chatbase como integracion activa de Charp

### 26.2 Flujo operativo de desarrollo

El ciclo operativo real del equipo puede resumirse asi:

1. definir o refinar una necesidad funcional o tecnica
2. traducirla a cambios de dominio, API, cliente o infraestructura
3. validar localmente con AppHost o con ejecucion directa de proyectos
4. probar endpoints y flujos de UI
5. registrar decisiones y deuda tecnica en la documentacion o bitacora

Este flujo no depende de una sola herramienta CI/CD. Su fortaleza principal ha estado en la cercania entre codigo, pruebas manuales, demos frecuentes y documentacion viva.

### 26.3 Trazabilidad documental

ACC mantiene trazabilidad distribuida en:

- el propio repositorio
- los documentos de `Docs/`
- la bitacora historica del proyecto
- diagramas y capturas asociadas a iteraciones

En el estado actual, el presente documento pasa a ser la referencia tecnica principal para consolidar esa trazabilidad.

### 26.4 Estado del flujo operativo

La operacion del equipo ya es suficiente para sostener desarrollo activo, pero sigue habiendo margen claro para profesionalizar:

- automatizacion continua
- consolidacion de pruebas
- flujos de despliegue
- runbooks especializados

Estas brechas no invalidan el sistema actual, pero deben reconocerse como parte del estado real del proyecto.

## 27. Metodologia de desarrollo y planeacion

La evolucion de ACC no siguio un modelo puramente lineal. La documentacion de planeacion y desarrollo muestra un enfoque de investigacion-accion tecnologica sostenido por iteraciones cortas y validaciones frecuentes.

### 27.1 Enfoque metodologico

La construccion del prototipo se apoyo en dos bases complementarias:

- analisis de contenido y decisiones pedagogicas
- Scrum adaptado como metodologia de ejecucion tecnica

Esto responde a la naturaleza del proyecto: ACC no es solo software, sino software educativo. Por eso la metodologia no podia separar completamente decisiones de arquitectura de decisiones pedagogicas.

### 27.2 Scrum adaptado

La forma operativa descrita en la documentacion historica es consistente con un Scrum ligero:

- backlog vivo
- sprints cortos
- refinamiento semanal
- demos frecuentes
- revisiones con asesor

Esta eleccion fue especialmente razonable por:

- incertidumbre alta en UX
- cambios de alcance durante la maduracion del producto
- necesidad de ajustar Charp, contenido y navegacion con rapidez
- evolucion continua del modelo de lecciones

### 27.3 Planeacion por fases

La narrativa historica del proyecto permite ordenar el trabajo en grandes etapas:

- preparacion y encuadre del problema
- diseno conceptual y modelado del dominio
- construccion iterativa de capacidades nucleares
- validacion, optimizacion y cierre documental

El valor de esta lectura por fases no es cronologico solamente. Tambien ayuda a entender por que hoy conviven piezas muy maduras con otras aun evolutivas.

### 27.4 Definition of Done implicita

Aunque no aparezca como contrato formal en codigo, la documentacion refleja una Definition of Done pragmatica:

- componente o flujo funcional
- endpoint operativo cuando aplica
- validacion manual o automatizada minima
- evidencia documental suficiente para continuar

Esto explica el ritmo del proyecto y tambien algunas de sus deudas actuales: se priorizo primero valor funcional y aprendizaje de producto, dejando algunas areas de endurecimiento para iteraciones posteriores.

### 27.5 Relacion entre metodologia y arquitectura

La metodologia adoptada influyo directamente en varias decisiones tecnicas:

- modularizar servicios para iterar sin bloquear toda la solucion
- usar AppHost para reducir costo de arranque entre iteraciones
- externalizar Charp para acelerar entrega de valor
- mover la narrativa de lecciones a datos para permitir evolucion continua del contenido

La arquitectura de ACC, por tanto, no puede entenderse solo como decision tecnica. Tambien es una consecuencia de la forma en que el equipo trabajo.

## 28. Pruebas, calidad y validacion

La estrategia de calidad de ACC combina hoy pruebas automatizadas puntuales, validacion manual intensiva y chequeo funcional sobre la topologia local completa. Esto refleja un proyecto en evolucion: ya existe infraestructura de testing real, pero su cobertura aun es desigual entre subsistemas.

### 28.1 Proyecto de pruebas

La solucion incluye `tests/ACC.Tests/ACC.Tests.csproj` como proyecto de pruebas en `net8.0`. Sus dependencias principales son:

- xUnit
- Moq
- `Microsoft.EntityFrameworkCore.InMemory`
- `coverlet.collector`

Esto da una base adecuada para pruebas unitarias y de componentes de logica sin depender de infraestructura real.

### 28.2 Cobertura actualmente visible

En el estado actual del repositorio, hay al menos dos tipos de evidencia automatizada:

- pruebas activas sobre logica de resumen de evaluaciones
- pruebas comentadas o incompletas sobre navegacion de contenido

`ResumenEvaluacionesTests` valida reglas de clasificacion, limites de intentos y metricas sobre evaluaciones. Esto es una buena senal de que el equipo ya esta encapsulando reglas de UI/dominio en componentes testeables.

Por otro lado, `NavegacionContenidoServiceTests` existe pero actualmente esta comentado. Eso indica una brecha real entre la intencion de probar servicios de dominio y la estabilidad o mantenimiento efectivo de esas pruebas.

### 28.3 Validacion manual

La validacion manual sigue teniendo un peso importante en ACC. Los mecanismos mas usados segun el repo y la documentacion son:

- pruebas de endpoints con Postman
- validacion de UI en la WebApp
- recorridos funcionales de guia, examenes, compilador y aulas
- maquetas o prototipos HTML en `tests/UI/`

Este enfoque ha sido suficiente para iterar rapido, pero no reemplaza la necesidad de ampliar la cobertura automatizada en servicios criticos.

### 28.4 Calidad en tiempo de desarrollo

La calidad actual se apoya en varias capas ligeras:

- tipado fuerte en C#
- separacion de contratos compartidos
- validacion de dominio en servicios
- mapeos centralizados en AutoMapper
- restricciones e indices a nivel de EF Core
- health checks y arranque reproducible con AppHost

Estas medidas no son pruebas en si, pero si reducen errores estructurales y hacen mas visible el fallo cuando aparece.

### 28.5 Riesgos y brechas actuales

Las brechas mas claras hoy son:

- cobertura automatizada todavia limitada para servicios centrales
- coexistencia de pruebas activas y pruebas comentadas
- dependencia alta de validacion manual en flujos complejos
- ausencia visible de pipeline CI automatizado dentro del repo

Documentar esto es importante porque evita sobrevalorar la madurez actual del sistema de testing.

### 28.6 Recomendacion tecnica natural

El siguiente salto de calidad para ACC no requiere cambiar la arquitectura completa. Requiere consolidar la capa de pruebas sobre:

- navegacion academica
- prerrequisitos y desbloqueos
- registro de intentos de examen
- carga de lecciones y contratos de render
- clientes de resumen y agenda que ya concentran reglas no triviales

Ese trabajo endureceria de forma visible la mantenibilidad del producto.

## 29. Puesta en marcha local

La puesta en marcha local vigente de ACC debe seguir la topologia real del repositorio: dos bases SQL, `ACC.WebApp`, `ACC.API`, `ACC.Compiler` y Redis orquestados por `ACC.AppHost`.

### 29.1 Requisitos previos

Antes de arrancar la solucion localmente se requiere:

- .NET SDK `8.0.123` o compatible con `global.json`
- Docker Desktop activo
- capacidad de ejecutar contenedores SQL Server y Redis
- dependencias NuGet restaurables

### 29.2 Secuencia recomendada

La secuencia tecnica recomendada hoy es:

1. restaurar la solucion
2. aplicar migraciones de la base academica
3. aplicar migraciones de identidad
4. iniciar `ACC.AppHost`
5. validar URLs expuestas por Aspire

Comandos base:

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

### 29.3 Resultado esperado

Si la topologia arranca correctamente, deben quedar disponibles:

- `ACC.WebApp`
- `ACC.API`
- `ACC.Compiler`
- SQL Identity
- SQL Academic
- Redis

Ademas, en desarrollo los servicios exponen health checks y el dashboard de Aspire permite inspeccionar su estado.

### 29.4 Validaciones minimas tras el arranque

Despues del arranque se recomienda comprobar al menos:

- acceso al dashboard de Aspire
- disponibilidad de `ACC.WebApp`
- disponibilidad de Swagger en `ACC.API` y `ACC.Compiler` si aplica
- funcionamiento basico de login, guia, compilador y consulta de examenes

### 29.5 Arranque de pruebas

Para ejecutar las pruebas automatizadas actuales:

```pwsh
dotnet test tests/ACC.Tests/ACC.Tests.csproj
```

La cobertura actual no representa todo el sistema, pero este comando si permite verificar que la base de testing existente sigue sana.

## 30. Roadmap, deuda tecnica y pendientes

ACC ya tiene una base tecnica clara, pero el repositorio y la documentacion muestran varias areas en transicion. Esta seccion resume lo que sigue vigente como roadmap y lo que hoy debe leerse como deuda tecnica consciente.

### 30.1 Roadmap vigente

Los frentes de evolucion mas visibles son:

- `ACC.MultiPlataform` como cliente MAUI Blazor planeado
- consolidacion de `ACC.ExternalClients` como capa real de integraciones
- crecimiento funcional de Redis mas alla de su presencia en topologia
- fortalecimiento de evaluacion, resumen y experiencia de aula
- maduracion de authoring y mantenimiento de contenido academico

### 30.2 Deuda tecnica visible

La deuda tecnica mas evidente hoy incluye:

- rename incompleto entre `ACC.Compiler` y `src/API_CompilerACC`
- coexistencia de rutas y namespaces legacy en compilacion
- ausencia de un modulo administrativo maduro para authoring de lecciones
- cobertura de pruebas aun parcial
- pruebas comentadas en areas importantes
- integraciones externas activas fuera de una capa `ExternalClients` plenamente consolidada

### 30.3 Deuda documental ya mitigada

Una parte importante de la deuda documental ya quedo reducida con este documento. Antes existia una brecha fuerte entre:

- el PDF historico
- el estado real del codigo
- la documentacion dispersa en `Docs/`

`ACC GUIA TECNICA - Reload` existe precisamente para cerrar esa brecha y convertirse en la fuente principal de referencia tecnica.

### 30.4 Pendientes de siguiente nivel

Despues de esta recompilacion documental, los pendientes mas naturales son:

- regenerar DOCX y PDF desde la version viva en Markdown
- endurecer pruebas sobre subsistemas criticos
- documentar despliegue productivo y seguridad operativa
- consolidar flujos de contenido y evaluacion desde herramientas mas maduras

### 30.5 Lectura correcta del estado del proyecto

El estado actual de ACC no debe interpretarse como producto estatico ni como simple prototipo descartable. La lectura correcta es:

- arquitectura ya suficientemente seria
- varias capacidades nucleares funcionales
- subsistemas complementarios aun en consolidacion
- deuda tecnica conocida y razonablemente trazable

Esa combinacion es consistente con un proyecto vivo y con proyeccion de crecimiento.

## 31. Anexos

Los siguientes artefactos complementan esta guia tecnica y deben leerse como apoyo especializado o historico segun el caso.

### 31.1 Documentos tecnicos complementarios

- `Docs/GUIA TECNICA - ACC.md`: guia tecnica resumida y vigente como referencia rapida
- `Docs/arquitectura.md`: vista sintetica de arquitectura y componentes
- `Docs/lecciones-dinamicas.md`: resumen tecnico del sistema de lecciones y `RDL`
- `Docs/navegacion-guia.md`: apoyo especifico para navegacion academica
- `Docs/examenes.md`: resumen puntual del flujo de examenes y desbloqueos
- `Docs/TypographyContract.md`: contrato tipografico oficial
- `Docs/planeacion-desarrollo.md`: narrativa historica de planeacion y ejecucion

### 31.2 Artefactos historicos

Se conservan como referencia historica:

- `Docs/GUIA TECNICA - ACC.pdf`
- `Docs/GUIA TECNICA - ACC.docx`

Estos archivos no deben tratarse ya como fuente principal de verdad, pero si como evidencia del punto de partida del proyecto.

### 31.3 Material de apoyo visual y pruebas

Tambien existen artefactos de apoyo en:

- `tests/UI/`
- diagramas y capturas historicas asociadas al proyecto
- bitacora fisica del desarrollo original

### 31.4 Funcion de este documento

Con esta tanda se cierra la primera version integral de `ACC GUIA TECNICA - Reload`. A partir de aqui, el documento debe entenderse como:

- fuente principal de referencia tecnica
- base para futuras exportaciones a DOCX y PDF
- punto de alineacion entre arquitectura, contenido, operacion y evolucion del proyecto

## Cierre editorial

La presente version deja cerrada la recompilacion tecnica integral de ACC en formato Markdown mantenible. A partir de este punto, cualquier exportacion a DOCX o PDF debe tomar este archivo como fuente principal de maquetacion y control de cambios.

Para futuras revisiones editoriales, se recomienda conservar al inicio del documento:

- codigo documental
- version
- fecha de corte tecnico
- nota de version

Con ello, `ACC GUIA TECNICA - Reload` queda listo para pasar a una fase de maquetacion formal sin perder trazabilidad tecnica.
