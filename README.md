# ACC-Complex
# 🦈 Aprendiendo C# con Charp (ACC)

> Plataforma educativa interactiva para aprender C# de forma clara, progresiva y motivadora.  
> Desarrollado con .NET 8, Blazor, MAUI, Roslyn, Redis, Aspire y más.

---

## 🚀 Características Principales

- 🧠 IA educativa personalizada (**Charp**) integrada vía Chatbase.
- 📚 Lecciones dinámicas con bloques ordenados (HTML, actividad, compilador, evaluación).
- 🧪 Compilación de código C# en tiempo real (Roslyn).
- 📅 Agenda, progreso, aulas virtuales, tareas y evaluaciones.
- 🔐 Sistema de autenticación con roles (estudiante, docente, administrador).
- 🌐 Compatible con Web (Blazor WebAssembly), Móvil y Escritorio (MAUI Blazor).

---

## 🏗️ Arquitectura Modular (ACC-Complex)

ACC está dividido en varios proyectos distribuidos por capas y responsabilidades:

### 🧩 Backend

| Proyecto             | Función Principal                                      |
|----------------------|--------------------------------------------------------|
| `ACC.API`            | Backend de contenido educativo (lecciones, tareas...). |
| `API_CompilerACC`    | Compilador de código C# en tiempo real con Roslyn.     |
| `ACC.WebApp`         | Backend de autenticación y sistema de cuentas.         |
| `ACC.Data`           | Entidades, DbContext y configuración de base de datos. |
| `ACC.ExternalClients`| Integración con servicios externos (IA, APIs).         |
| `ACC.Shared`         | DTOs, enums, interfaces y tipos comunes.               |
| `ACC.ServiceDefaults`| Telemetría, resiliencia, descubrimiento de servicios.  |
| `ACC.AppHost`        | Orquestador general con Aspire.                        |

### 💻 Frontend

| Proyecto               | Función Principal                             |
|------------------------|-----------------------------------------------|
| `ACC.WebApp.Client`    | Aplicación SPA en Blazor WebAssembly.         |
| `ACC.MultiPlataform`   | App MAUI Blazor multiplataforma.              |

### 🧪 Pruebas

| Proyecto       | Propósito                         |
|----------------|-----------------------------------|
| `ACC.Tests`    | Pruebas unitarias con xUnit + Moq |

---

## 🛠️ Tecnologías Usadas

### ⚙️ Backend

- ASP.NET Core 8
- Entity Framework Core
- AutoMapper
- FluentValidation
- Roslyn (.NET Compiler Platform)
- Redis (cache)
- Serilog + OpenTelemetry
- Docker + Aspire
- xUnit + Moq

### 🎨 Frontend

- Blazor WebAssembly
- MAUI Blazor (App móvil y escritorio)
- Bootstrap (estilos)
- JSInterop (interacción JS–C#)

---

## ✍️ Estructura de Lecciones

Las lecciones en ACC siguen un flujo pedagógico basado en la **Taxonomía de Bloom**:

1. **Teoría**  
2. **Ejemplos** (buenos y malos)  
3. **Práctica guiada**  
4. **Actividad**  
5. **Evaluación automática**  
6. **Recursos adicionales (fomentadores)**

> Cada lección se representa como HTML enriquecido + componentes renderizados dinámicamente según un array `OrdenSecciones`.

---

## 📖 Estilo y Plantilla Visual

- Fondo oscuro con colores didácticos.
- Código resaltado en bloques scrollables.
- Alertas tipo `.alert-info`, `.alert-success`, `.alert-warning`, `.alert-error`.
- Estilo responsive y optimizado para múltiples dispositivos.

---

## 🧠 Metodología

ACC fue desarrollado usando **SCRUM**, con:

- Iteraciones semanales (sprints).
- Roles definidos: Product Owner, Scrum Master y Development Team.
- Evaluaciones constantes y enfoque en aprendizaje activo.
- Aplicación de principios de diseño educativo y progresividad.

---

## 👥 Autores y Créditos

**Desarrolladores**:  
- Germán Uriel Evangelista Martínez  
- Aldo Juan Figueroa Espinoza

**Asesores**:  
- Francisco Javier Tafolla Granados (Técnico)  
- José Manuel González Zaragoza (Metodológico)

---

## 📌 Estado del Proyecto

**🛠️ En desarrollo activo**  
Se aceptan sugerencias, mejoras o colaboración.  
¡Tu retroalimentación es bienvenida!

---

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/germann-ux/ACC-Complex)