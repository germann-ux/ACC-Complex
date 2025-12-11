# 🦈 ACC-Complex – Aprendiendo C# con Charp  

> Plataforma educativa interactiva para aprender C# de forma clara, progresiva y motivadora.  
> Desarrollado con **.NET 8, Blazor, MAUI, Roslyn, Redis, Aspire** y un ecosistema modular distribuido.

---

## 🚀 Características Principales

- 🤖 IA educativa personalizada (*Charp*) integrada con servicios externos.  
- 📚 Lecciones dinámicas basadas en bloques ordenados con *OrdenSecciones*.  
- 🧪 Compilación de código C# en tiempo real con **Roslyn**.  
- 📅 Agenda, progreso, aulas virtuales, tareas y evaluaciones.  
- 🔐 Autenticación con **Identity + roles** (estudiante, docente, administrador).  
- 🌐 Soporte multiplataforma: **Web (Blazor WASM), Escritorio y Móvil (MAUI Blazor)**.  
- 📊 Telemetría y resiliencia con **OpenTelemetry, Serilog y Aspire**.  

---

## 🏗 Arquitectura del Proyecto

La solución ACC-Complex sigue un enfoque **Clean Architecture distribuida**, con separación clara de capas y servicios orquestados con **Aspire**.  

### 📌 Backend

| Proyecto | Función Principal |
|---|---|
| **ACC.API** | Gestión del contenido educativo (módulos, lecciones, tareas, aulas). |
| **API_CompilerACC** | Servicio para compilación en tiempo real de código C# (Roslyn). |
| **ACC.WebApp** | Backend de autenticación, registro, login, roles y tokens. |
| **ACC.Data** | Entidades, DbContext, migraciones y configuración de base de datos. |
| **ACC.ExternalClients** | Integración con servicios externos (ej. IA de Charp). |
| **ACC.ServiceDefaults** | Descubrimiento de servicios, resiliencia, health checks, OpenTelemetry. |
| **ACC.Shared** | DTOs, enums, interfaces y tipos comunes (ej. `ServiceResult<T>`). |
| **ACC.AppHost** | Orquestador general con Aspire (levanta y conecta todos los servicios). |

### 🎨 Frontend

| Proyecto | Función |
|---|---|
| **ACC.WebApp.Client** | SPA en Blazor WebAssembly. Cliente principal web. |
| **ACC.MultiPlataform** | App MAUI Blazor para móvil, tablet y escritorio. |

### 🧪 Pruebas

| Proyecto | Propósito |
|---|---|
| **ACC.Tests** | Pruebas unitarias (xUnit + Moq) sobre servicios, controladores y validaciones. |

---

## 🌳 Estructura de la Solución

```plaintext
ACC-Complex (11 proyectos)
├─ src/
│  ├─ ACC.API              # Backend de contenido educativo
│  ├─ ACC.AppHost          # Orquestador Aspire
│  ├─ ACC.Data             # Entidades, DbContext, migraciones
│  ├─ ACC.ExternalClients  # Integración con APIs externas
│  ├─ ACC.ServiceDefaults  # Resiliencia, health checks, telemetría
│  ├─ ACC.Shared           # DTOs, interfaces, tipos comunes
│  └─ API_CompilerACC      # Compilación C# en tiempo real (Roslyn)
│
├─ tests/
│  └─ ACC.Tests            # Pruebas unitarias (xUnit + Moq)
│
├─ ACC.MultiPlataform      # App MAUI Blazor (móvil y escritorio)
├─ ACC.WebApp              # Backend de autenticación y cuentas
└─ ACC.WebApp.Client       # SPA Blazor WebAssembly
```

---

## 🔄 Flujo General

1. **Autenticación (ACC.WebApp).** El cliente (Blazor WASM o MAUI) envía credenciales → se valida con Identity → se asignan roles → se devuelve un token.  
2. **Carga de contenido (ACC.API).** El cliente usa el token para solicitar lecciones, módulos, evaluaciones; ACC.API consulta base de datos vía **ACC.Data** y responde con DTOs de **ACC.Shared**.  
3. **Compilación de código (API_CompilerACC).** El cliente envía código → Roslyn compila → devuelve salida o errores.  
4. **Servicios transversales (ACC.ServiceDefaults).** Resiliencia, descubrimiento, métricas y logs distribuidos para todos los servicios.  
5. **App nativa (ACC.MultiPlataform).** Reutiliza los mismos servicios para autenticación y contenido, orientado a movilidad.  

---

## ⚙ Tecnologías Clave

- **Backend:** ASP.NET Core 8, EF Core, AutoMapper, FluentValidation, Roslyn, Redis, Serilog, OpenTelemetry, Docker, Aspire.  
- **Frontend:** Blazor WebAssembly, MAUI Blazor, Bootstrap, JSInterop.  
- **Pruebas:** xUnit + Moq.  
- **DevOps:** GitHub, Docker Desktop, SQL Server, SSMS, Postman.  

---

## 📖 Lecciones y Metodología

- Basadas en la **Taxonomía de Bloom**: teoría → ejemplos → práctica → actividad → evaluación → fomentadores.  
- Estilo visual consistente con bloques didácticos y alertas (`.alert-info`, `.alert-warning`, etc.).  
- Contenido modular, ordenado dinámicamente con `OrdenSecciones`.  

---

## 👥 Autores y Créditos

- **Desarrolladores:** Germán Uriel Evangelista Martínez, Aldo Juan Figueroa Espinoza  
- **Asesores:** Francisco Javier Tafolla Granados (Técnico), José Manuel González Zaragoza (Metodológico)  

---

## 📌 Estado del Proyecto

**🛠 En desarrollo activo.**  
Se aceptan sugerencias, mejoras o colaboración.  
¡Tu retroalimentación es bienvenida!  

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/germann-ux/ACC-Complex)
