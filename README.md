<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet" alt=".NET 8">
  <img src="https://img.shields.io/badge/Blazor-WebAssembly-512BD4?style=for-the-badge&logo=blazor" alt="Blazor WASM">
  <img src="https://img.shields.io/badge/MAUI-Blazor-512BD4?style=for-the-badge&logo=dotnet" alt="MAUI">
  <img src="https://img.shields.io/badge/Aspire-9.2-512BD4?style=for-the-badge&logo=dotnet" alt="Aspire">
</p>

<h1 align="center">🦈 Aprendiendo C# con Charp</h1>

<p align="center">
  <strong>Plataforma educativa interactiva para aprender C# de forma clara y progresiva.</strong>
</p>

<p align="center">
  <a href="#-características-principales">Características</a> •
  <a href="#-arquitectura">Arquitectura</a> •
  <a href="#-instalación">Instalación</a> •
  <a href="#-estructura-del-proyecto">Estructura</a> •
  <a href="#-tecnologías">Tecnologías</a> •
  <a href="#-contribución">Contribución</a>
</p>

<p align="center">
  <a href="https://deepwiki.com/germann-ux/ACC-Complex">
    <img src="https://deepwiki.com/badge.svg" alt="Ask DeepWiki">
  </a>
</p>

---

## 📖 Descripción

**Aprendiendo C# con Charp** es una plataforma educativa completa diseñada para enseñar C# de manera interactiva y personalizada. Cuenta con un asistente de IA educativo llamado **Charp** 🦈, que asiste a los estudiantes a través de un currículo estructurado basado en la **Taxonomía de Bloom**.

La plataforma soporta múltiples modalidades de aprendizaje: desde lecciones teóricas y lecciones dinámicas por secciones hasta compilación de código en tiempo real, exámenes con desbloqueo por progreso y aulas virtuales para colaboración entre estudiantes y docentes.

Documento técnico vivo: [`Docs/GUIA TECNICA - ACC.md`](Docs/GUIA%20TECNICA%20-%20ACC.md)

---
## Características principales

### Asistente educativo con IA
- Asistente **Charp** integrado a servicios externos de IA.
- Retroalimentación contextual durante prácticas y evaluaciones.
- Recomendaciones de estudio basadas en progreso y desempeño.

### Lecciones dinámicas basadas en bloques
- Renderizado por secciones con orden configurable mediante `OrdenSecciones`.
- Flujo pedagógico configurable con `charpDialog`, `charpTip`, `teoria`, `ejemplo`, `practica`, `actividad`, `compilador` y `video`.
- Cada lección puede exponer `NivelBloom`, actividad externa, compilador y apoyo audiovisual sin cambiar el cliente.

### Compilación y ejecución de C# en tiempo real
- Compilación con **Roslyn** para prácticas interactivas.
- Ejecución controlada en memoria desde un servicio dedicado.
- Retroalimentación inmediata con errores de compilación y salida estándar.

### Gestión académica
- Agenda académica y seguimiento de progreso.
- Aulas virtuales: gestión de grupos, estudiantes y contenido.
- Sistema de tareas, evaluaciones y reportes de desempeño.

### Autenticación y control de acceso
- **ASP.NET Identity** con roles (estudiante, docente, administrador).
- Autenticación con **ASP.NET Identity** y consumo autenticado de `ACC.API` mediante **JWT**.
- Autorización granular por políticas y permisos.

### Multiplataforma
- Web: **Blazor Web App** con cliente **WebAssembly**.
- App planificada: **ACC.MultiPlataform** con **MAUI Blazor** para escritorio y móvil.

### Observabilidad y resiliencia
- Telemetría distribuida con **OpenTelemetry**.
- Health checks y service discovery mediante `ACC.ServiceDefaults`.
- Políticas de resiliencia (timeouts, retries, circuit breaker) donde aplique.

---

## 🏗 Arquitectura

La solución sigue un enfoque de **Clean Architecture distribuida** con servicios orquestados mediante **.NET Aspire**.
```mermaid
%%{init: {
  "theme": "dark",
  "themeVariables": {
    "background": "#0a0a0f",
    "mainBkg": "#141420",
    "secondBkg": "#10101a",

    "primaryColor": "#141420",
    "primaryTextColor": "#e5e7eb",
    "primaryBorderColor": "#2a2a40",

    "lineColor": "#4cc9f0",

    "clusterBkg": "#0f0f1a",
    "clusterBorder": "#2a2a40",

    "titleColor": "#cbd5e1",
    "edgeLabelBackground": "#0a0a0f",
    "nodeTextColor": "#e5e7eb"
  }
}}%%

graph TB
    %% Clientes
    WASM["ACC.WebApp.Client<br/>Blazor Web"]
    MAUI["ACC.MultiPlataform<br/>MAUI Blazor (Planned)"]

    %% Orquestación
    AppHost["ACC.AppHost<br/>Orchestrator (Aspire)"]

    %% Servicios
    WebApp["ACC.WebApp<br/>Identity + Web Host"]
    API["ACC.API<br/>Academic Domain"]
    Compiler["ACC.Compiler<br/>Roslyn Compiler"]

    %% Compartidos
    Data["ACC.Data<br/>Data Layer"]
    SharedLib["ACC.Shared<br/>Shared Library"]
    Defaults["ServiceDefaults<br/>Configuration"]
    External["Chatbase / External AI"]

    %% Infraestructura
    SQL_Id[("SQL Server<br/>Identity DB")]
    SQL_Acad[("SQL Server<br/>Academic DB")]
    Redis[("Redis<br/>Cache")]

    %% Cliente -> Servicios
    WASM -->|HTTPS API| WebApp
    WASM -->|HTTPS API| API
    WASM -->|HTTPS API| Compiler

    MAUI -.->|planned| WebApp
    MAUI -.->|planned| API
    MAUI -.->|planned| Compiler

    %% Orquestación
    AppHost -.->|orchestrate| WebApp
    AppHost -.->|orchestrate| API
    AppHost -.->|orchestrate| Compiler
    AppHost -.->|provision| SQL_Id
    AppHost -.->|provision| SQL_Acad
    AppHost -.->|provision| Redis

    %% Servicios -> Compartidos
    WebApp --> SharedLib
    WebApp --> Defaults

    API --> SharedLib
    API --> Defaults

    Compiler --> Defaults

    %% Servicios -> Infra
    WebApp -->|Identity EF| SQL_Id
    API --> Data
    Data -->|EF Core| SQL_Acad

    %% Integraciones
    WebApp -.->|sync user profile| API
    WASM -.->|Charp / iframe| External

    %% Estilos ACC
    classDef client fill:#141420,stroke:#4cc9f0,stroke-width:1.5px,color:#e5e7eb
    classDef orchestration fill:#1a1a2e,stroke:#4cc9f0,stroke-width:2px,color:#e5e7eb
    classDef service fill:#121224,stroke:#3a3a5a,stroke-width:1.5px,color:#e5e7eb
    classDef shared fill:#0f0f1f,stroke:#2a2a40,stroke-width:1.2px,color:#e5e7eb
    classDef infra fill:#0b0b16,stroke:#2a2a40,stroke-width:1.2px,color:#e5e7eb

    class WASM,MAUI client
    class AppHost orchestration
    class WebApp,API,Compiler service
    class Data,SharedLib,Defaults,External shared
    class SQL_Id,SQL_Acad,Redis infra
```

### 📌 Descripción de Proyectos

| Capa | Proyecto | Descripción |
|------|----------|-------------|
| **Frontend** | `ACC.WebApp.Client` | Cliente web actual en Blazor |
| **Frontend** | `ACC.MultiPlataform` | Cliente MAUI Blazor planificado para móvil y escritorio |
| **Backend** | `ACC.WebApp` | Servicio de autenticación, registro y gestión de usuarios |
| **Backend** | `ACC.API` | API de contenido educativo (módulos, lecciones, tareas) |
| **Backend** | `ACC.Compiler` | Servicio de compilación C# en tiempo real, alojado actualmente en `src/API_CompilerACC` |
| **Datos** | `ACC.Data` | Entidades, DbContext, migraciones EF Core |
| **Compartido** | `ACC.Shared` | DTOs, interfaces, enums y tipos comunes |
| **Compartido** | `ACC.ExternalClients` | Clientes para APIs externas (IA de Charp) |
| **Infraestructura** | `ACC.ServiceDefaults` | Configuración de resiliencia, telemetría, health checks |
| **Orquestación** | `ACC.AppHost` | Host de Aspire - orquesta todos los servicios |
| **Pruebas** | `ACC.Tests` | Pruebas unitarias con xUnit + Moq |

---

## 🚀 Instalación

### Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (para Redis y SQL Server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recomendado) o VS Code
- [SQL Server](https://www.microsoft.com/sql-server) (opcional si usas Docker)

### Pasos de Instalación

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/germann-ux/ACC-Complex.git
   cd ACC-Complex
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Configurar variables de entorno**
   
   Configurar cadenas de conexión y valores necesarios para `ACC.WebApp` y `ACC.API`. Puedes usar `appsettings.Development.json` o User Secrets:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "tu_cadena_de_conexion"
   ```

4. **Ejecutar las migraciones**
   ```pwsh
   dotnet ef database update `
     --project src/ACC.Data `
     --startup-project src/ACC.API `
     --context ACCDbContext

   dotnet ef database update `
     --project ACC.WebApp/ACC.WebApp `
     --startup-project ACC.WebApp/ACC.WebApp `
     --context ApplicationDbContext
   ```

5. **Iniciar la aplicación con Aspire**
   ```pwsh
   dotnet run --project src/ACC.AppHost/ACC.AppHost.csproj
   ```

6. **Acceder a la aplicación**
   - Dashboard de Aspire: usar la URL publicada por `ACC.AppHost` al iniciar.
   - Aplicación Web: usar la URL de `ACC.WebApp` expuesta por Aspire.

---

## 📁 Estructura del Proyecto

```
ACC-Complex/
├── 📂 src/
│   ├── ACC.API/              # API de contenido educativo
│   ├── ACC.AppHost/          # Orquestador Aspire
│   ├── ACC.Data/             # Capa de datos (EF Core)
│   ├── ACC.ExternalClients/  # Clientes de APIs externas
│   ├── API_CompilerACC/      # Implementación actual de ACC.Compiler
│   ├── ACC.Shared/           # Código compartido
│   └── data/                 # Scripts y datos semilla
│
├── 📂 ACC.WebApp/
│   ├── ACC.WebApp/           # Backend de autenticación
│   └── ACC.WebApp.Client/    # Cliente Blazor WASM
│
├── 📂 ACC.ServiceDefaults/   # Configuración transversal
├── 📂 Docs/                  # Documentación técnica y funcional
│
├── 📂 tests/
│   └── ACC.Tests/            # Pruebas unitarias
│
├── 📄 ACC.sln                # Solución principal

└── 📄 README.md              # Este archivo
```

---

## 👨‍💻 Tecnologías

### Backend
| Tecnología | Versión | Uso |
|------------|---------|-----|
| .NET | 8.0 | Framework principal |
| ASP.NET Core | 8.0 | APIs REST |
| Entity Framework Core | 8.x / 9.x | Persistencia y migraciones según el proyecto |
| ASP.NET Identity | 8.0 | Autenticación y autorización |
| Roslyn | 4.13 | Compilación dinámica de C# |
| AutoMapper | 12.x / 14.x | Mapeo objeto-objeto |
| OpenTelemetry | 1.9+ | Telemetría distribuida |

### Frontend
| Tecnología | Versión | Uso |
|------------|---------|-----|
| Blazor Web App + WASM | 8.0 | Experiencia web actual |
| .NET MAUI Blazor | Planificado | Cliente multiplataforma futuro |
| Blazored.LocalStorage | 4.5 | Persistencia ligera del lado cliente |
| CodeMirror | Actual | Editor del compilador en línea |
| Bootstrap | 5.x | Framework CSS |

### Infraestructura
| Tecnología | Versión | Uso |
|------------|---------|-----|
| .NET Aspire | 9.2 | Orquestación de servicios |
| SQL Server | 2022 | Bases de datos (Identity + Académica) |
| Redis | 7.x | Recurso orquestado y listo para cache distribuido |
| Docker | Latest | Contenedorización |
| Chatbase | SaaS | Integración actual de Charp |

---

## 🔄 Flujo de Trabajo
```mermaid
%%{init: {
  "theme": "dark",
  "themeVariables": {
    "background": "#0a0a0f",
    "mainBkg": "#141420",
    "secondBkg": "#10101a",

    "actorBkg": "#141420",
    "actorBorder": "#4cc9f0",
    "actorTextColor": "#e5e7eb",
    "actorLineColor": "#4cc9f0",

    "signalColor": "#e5e7eb",
    "signalTextColor": "#e5e7eb",

    "labelBoxBkgColor": "#10101a",
    "labelBoxBorderColor": "#2a2a40",
    "labelTextColor": "#cbd5e1",

    "noteBorderColor": "#2a2a40",
    "noteBkgColor": "#0f0f1a",
    "noteTextColor": "#cbd5e1",

    "activationBorderColor": "#4cc9f0",
    "activationBkgColor": "#141420",

    "loopTextColor": "#cbd5e1",
    "sequenceNumberColor": "#7dd3fc",
    "lineColor": "#4cc9f0",
    "textColor": "#e5e7eb",
    "fontSize": "14px"
  }
}}%%

sequenceDiagram
    autonumber

    participant U as Usuario
    participant W as ACC.WebApp<br/>Identity + Web Host
    participant IdDB as Identity DB
    participant C as ACC.WebApp.Client<br/>UI
    participant A as ACC.API<br/>Academic API
    participant AcDB as Academic DB
    participant X as ACC.Compiler<br/>Compile API

    rect rgb(20,20,40)
    Note over U,AcDB: Registro y sincronización académica
    end

    U->>+W: Registro desde /Account/Register
    W->>+IdDB: Crear usuario Identity
    IdDB-->>-W: Usuario creado

    W->>+A: POST /api/Usuario/sincronizar
    A->>+AcDB: Crear perfil académico
    AcDB-->>-A: Perfil persistido
    A-->>-W: Sincronización completa
    W-->>-U: Registro finalizado


    rect rgb(18,18,34)
    Note over U,A: Navegación de contenido
    end

    U->>+C: Abrir guía
    C->>+A: GET /api/NavegacionContenido/modulos
    A->>+AcDB: Consultar módulos
    AcDB-->>-A: DTOs jerárquicos
    A-->>-C: Lista de módulos

    U->>+C: Abrir lección
    C->>+A: GET /api/NavegacionContenido/leccion/{id}
    A->>+AcDB: Consultar lección
    AcDB-->>-A: LeccionDto
    A-->>-C: Datos de lección + OrdenSecciones
    C-->>-U: Render RDL

    rect rgb(16,16,30)
    Note over U,X: Práctica con compilador
    end

    U->>+C: Ejecutar código C#
    C->>+X: POST /api/compile
    X-->>-C: stdout o errores de compilación
    C-->>-U: Retroalimentación inmediata
```
---

## 📖 Metodología de Lecciones

Las lecciones actuales se renderizan dinámicamente y siguen la **Taxonomía de Bloom** a través de `NivelBloom` y `OrdenSecciones`:

| Nivel | Componente | Descripción |
|-------|------------|-------------|
| 1️⃣ | **CharpDialog / CharpTip** | Contexto inicial, andamiaje y orientación breve |
| 2️⃣ | **Teoría** | Conceptos fundamentales explicados en HTML enriquecido |
| 3️⃣ | **Ejemplos** | Casos guiados y comparaciones buenas/malas |
| 4️⃣ | **Práctica** | Aplicación progresiva del concepto |
| 5️⃣ | **Actividad** | Recurso externo opcional cuando la lección lo requiere |
| 6️⃣ | **Compilador / Video** | Práctica interactiva y apoyo audiovisual según configuración |

Los exámenes ya no se modelan como una simple sección fija de la lección. Su habilitación depende del progreso del usuario y de reglas de desbloqueo en `PrerrequisitosService`.

---

## 🧪 Pruebas

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar con cobertura
dotnet test --collect:"XPlat Code Coverage"

# Ejecutar pruebas específicas
dotnet test --filter "FullyQualifiedName~ACC.Tests.NombreDelTest"
```

---

## 🤝 Contribución

¡Las contribuciones son bienvenidas! Por favor, sigue estos pasos:

1. **Fork** el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/NuevaCaracteristica`)
3. Realiza tus cambios y haz commit (`git commit -m 'Agregar nueva característica'`)
4. Push a la rama (`git push origin feature/NuevaCaracteristica`)
5. Abre un **Pull Request**

---

## 👥 Equipo

<table>
  <tr>
    <td align="center">
      <strong>Desarrolladores</strong><br/>
      Germán Uriel Evangelista Martínez<br/>
      Aldo Juan Figueroa Espinoza
    </td>
    <td align="center">
      <strong>Asesores</strong><br/>
      Francisco Javier Tafolla Granados (Técnico)<br/>
      José Manuel González Zaragoza (Metodológico)
    </td>
  </tr>
</table>

---

## 📄 Propiedad Intelectual

Este proyecto es propiedad intelectual exclusiva. Todos los derechos están reservados.
La licencia MIT ya no está vigente para este proyecto.

---

## 📌 Estado del Proyecto

<p align="center">
  <img src="https://img.shields.io/badge/Estado-En%20Desarrollo%20Activo-brightgreen?style=for-the-badge" alt="Estado">
  <img src="https://img.shields.io/badge/Versión-1.0.0--beta-blue?style=for-the-badge" alt="Versión">
</p>

**🛠 En desarrollo activo** - Se aceptan sugerencias, mejoras y colaboración.

> ¡Tu retroalimentación es bienvenida! Abre un [issue](https://github.com/germann-ux/ACC-Complex/issues) o contáctanos directamente.

---

<p align="center">
  <strong>Hecho con ❤️ para la comunidad educativa de C#</strong>
</p>

<p align="center">
  <a href="https://deepwiki.com/germann-ux/ACC-Complex">
    <img src="https://deepwiki.com/badge.svg" alt="Ask DeepWiki">
  </a>
</p>
