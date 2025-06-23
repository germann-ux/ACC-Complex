# Vista de Docentes - Sección de Aulas

Esta guía resume la funcionalidad esperada para la sección **Aulas** cuando un docente accede a la plataforma. Cada subsección describe brevemente la utilidad pedagógica y sugiere elementos visuales o componentes que favorecen una interfaz intuitiva.

## 1. Muro de Anuncios

- **Objetivo**: Permitir al docente informar a sus estudiantes sobre novedades o recordatorios.
- **Funciones clave**:
  - Publicar, editar y eliminar anuncios.
  - Mostrar fecha, título y cuerpo del anuncio.
  - Botón **“+ Crear nuevo anuncio”** para iniciar el formulario de publicación.
- **Sugerencias visuales**:
  - Lista cronológica de avisos con tarjeta o `card` por anuncio.
  - Íconos de edición y eliminación en cada tarjeta.
  - Área de texto desplegable para el nuevo anuncio.

## 2. Lista de Estudiantes

- **Objetivo**: Brindar al docente una vista rápida del progreso de su grupo.
- **Datos mostrados**:
  - Nombre del alumno y correo electrónico.
  - Progreso general expresado en porcentaje.
  - Última lección vista.
  - Enlace o botón para **Ver Detalles** del alumno.
- **Sugerencias visuales**:
  - Tabla responsiva con filas alternadas.
  - Barras de progreso o `progress bar` para representar el avance.
  - Icono de lupa en el botón de detalle.

## 3. Vista Detallada de Tareas del Aula

- **Objetivo**: Gestionar las tareas asignadas en el aula.
- **Datos mostrados**:
  - Título y descripción de cada tarea.
  - Estado actual (en progreso, completada, no iniciada).
- **Acciones**:
  - **Agregar nueva tarea** mediante un botón o diálogo emergente.
- **Sugerencias visuales**:
  - Lista con etiquetas de color para los diferentes estados.
  - Sección de filtros para buscar tareas por estado.

## 4. Vista Detallada de Evaluaciones del Aula

- **Objetivo**: Visualizar y organizar las evaluaciones aplicadas.
- **Datos mostrados**:
  - Resultados de exámenes por estudiante o por actividad.
- **Acciones**:
  - Configurar o reprogramar evaluaciones existentes.
- **Sugerencias visuales**:
  - Gráficas de barras para promedios generales.
  - Botones para editar fecha y parámetros de cada evaluación.

## 5. Configuración del Aula

- **Objetivo**: Centralizar las opciones administrativas del aula.
- **Elementos**:
  - Título del aula y descripción.
  - Submódulo asignado o materia correspondiente.
  - Botón para **Generar link de invitación**.
  - Opción para **Cerrar** o **Archivar** el aula.
- **Sugerencias visuales**:
  - Panel tipo formulario con campos editables.
  - Mensajes de confirmación antes de cerrar o archivar.

---

Esta estructura busca que la vista del docente sea fácil de navegar y proporcione la información pedagógica relevante de forma clara. Los componentes descritos se pueden reutilizar dentro de la implementación Blazor existente para consolidar una experiencia coherente.
