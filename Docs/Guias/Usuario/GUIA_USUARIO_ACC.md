# GUIA DE USUARIO - ACC

Manual base para usuarios de ACC. Este documento se construyo con las capturas de `Docs/Guias/Usuario` y con la estructura actual de la aplicacion.

## Alcance

Esta guia documenta las secciones visibles para el usuario autenticado:

- Resumen
- Agenda
- Guia
- Biblioteca
- Charp
- Cuenta

Las secciones `Docentes`, `Aulas` y `Administracion` dependen del rol del usuario y no se documentan aqui porque no hay capturas de apoyo en esta carpeta.

## Navegacion general

- Usa la barra lateral izquierda para cambiar de seccion.
- El icono de casa lleva a `Resumen`.
- El icono de calendario lleva a `Agenda`.
- El icono de brujula lleva a `Guia`.
- El icono de libro lleva a `Biblioteca`.
- El icono de chat lleva a `Charp`.
- El icono de usuario lleva a `Cuenta`.
- En casi todas las pantallas aparece un boton flotante azul con `+`. Ese boton abre un panel rapido para crear una tarea personal sin salir de la pagina actual.

![Creacion rapida de tarea](./AgendaCreacionBurbuja.png)

## 1. Resumen

La seccion `Resumen` concentra el estado general del usuario: avance, evaluaciones, tareas y recomendaciones.

![Vista general del resumen](./Resumen1parte.png)

### Como usarla

1. Entra a `Resumen` desde la barra lateral.
2. Revisa el boton `Continuar tu ultimo tema` para volver al punto mas reciente de tu avance en la guia.
3. Haz clic en la bombilla para abrir un tip rapido de C#.
4. Consulta la barra de `Progreso General` para ver el porcentaje completado del modulo actual.
5. Baja a `Evaluaciones` para revisar calificaciones, examenes pendientes, bloqueados y completados.
6. Revisa `Tareas Pendientes` para identificar entregas asignadas y tareas personales.
7. Usa `Esto podria interesarte` para abrir capitulos recomendados de la biblioteca.

![Detalle de evaluaciones](./Resumen2parte_Evaluaciones.png)

![Tareas e intereses](./Resumen3parte_TareasYIntereses.png)

![Tip rapido](./Tip.png)

### Que informacion muestra

- `Ultima calificacion`: la nota mas reciente.
- `Mejor calificacion`: tu mejor resultado registrado.
- `Peor calificacion`: el resultado mas bajo para detectar area de mejora.
- `Promedio`: promedio general de evaluaciones calificadas.
- `Pendientes`: examenes habilitados que aun no completas.
- `Bloqueados`: examenes que todavia no se habilitan.
- `Completados`: examenes ya cerrados con resultado final.

## 2. Agenda

La `Agenda` sirve para organizar tareas personales y revisar actividades asignadas.

![Vista principal de agenda](./Agenda.png)

### Como usarla

1. Entra a `Agenda` desde la barra lateral.
2. Usa `Nueva tarea` para registrar una tarea personal.
3. Completa `Titulo`, `Descripcion` y `Fecha limite`.
4. Guarda la tarea para que aparezca en la `Vista de trabajo`.
5. Cambia entre los filtros `Todo`, `Hoy`, `Proximas`, `Vencidas`, `Completadas`, `Asignadas`, `Personales` y `Recordatorios`.
6. Cambia la agrupacion entre `Tipo` y `Vencimiento` segun la forma en que quieras revisar tu carga de trabajo.
7. En tareas personales puedes usar `Completar`, `Reabrir`, `Manana`, `+7 dias` y `Eliminar`.
8. En tareas asignadas puedes usar `Ir a practica` para volver al contenido relacionado.

![Formulario de nueva tarea en agenda](./AgendaCrearTareaAsignada.png)

### Creacion rapida desde cualquier pagina

1. Haz clic en el boton flotante azul con `+`.
2. Captura el titulo y, si lo necesitas, una descripcion.
3. Activa o desactiva `Definir fecha limite`.
4. Pulsa `Guardar`.
5. Si prefieres mas detalle, usa `Abrir Agenda`.

![Panel rapido de nueva tarea](./AgendaCreacionBurbuja.png)

### Eliminacion y deshacer

- Cuando eliminas una tarea personal, ACC muestra un aviso temporal.
- Usa `Deshacer` si eliminaste la tarea por error.
- Si no la restauras a tiempo, la eliminacion se confirma.

![Aviso para deshacer eliminacion](./AgendaEliminacion.png)

## 3. Guia

La `Guia` es el recorrido principal de aprendizaje. Organiza el contenido por modulo, submodulo, tema y subtema.

![Entrada principal a la guia](./GuiaPrimeraVista.png)

### Flujo recomendado

1. Entra a `Guia`.
2. Elige un modulo y pulsa `Navegar`.
3. Dentro del modulo, abre un `SubModulo`.
4. Dentro del submodulo, abre un `Tema`.
5. Dentro del tema, revisa cada `SubTema`.
6. Marca cada subtema con `Completar` cuando termines el contenido.
7. Una vez cubiertos los requisitos, entra a las evaluaciones habilitadas.

![Navegacion por modulo](./GuiaModulo.png)

![Navegacion por submodulo](./GuiaSubModulo.png)

### Estado de avance

- Si un subtema aun no se completa, aparece el boton `Completar`.
- Si ya fue cerrado correctamente, se muestra el estado `Completado`.
- Usa los botones de flecha en la parte superior para volver o avanzar dentro del recorrido.

![Tema pendiente por completar](./GuiaTemaNoCompletado.png)

![Tema completado](./GuiaTemaCompletado.png)

### Evaluaciones

- Los examenes de submodulo se habilitan cuando completas los subtemas requeridos.
- Los examenes bloqueados se muestran con el estado `Examen Bloqueado`.
- Cuando un examen esta listo, aparece el boton `Enfrentar el Desafio`.

![Examenes bloqueados y habilitados](./GuiaExamenes.png)

## 4. Biblioteca

La `Biblioteca` agrupa documentacion, conceptos, ejemplos, ejercicios y proyectos en formato de capitulos.

![Vista principal de biblioteca](./BibliotecaPrimeraVista.png)

### Como usarla

1. Entra a `Biblioteca`.
2. Usa los filtros rapidos superiores para cambiar entre `Documentacion`, `Concepto`, `Ejemplo`, `Ejercicio` y `Proyecto`.
3. Revisa cada tarjeta para ver titulo, descripcion breve, tiempo estimado y fecha.
4. Pulsa `Ir al capitulo` para abrir el contenido completo.
5. Si necesitas una busqueda mas precisa, usa el panel lateral derecho.

![Filtros rapidos](./BibliotecaFiltrosRapidos.png)

### Filtros laterales

En el panel `Filtros` puedes refinar por:

- `Tipo de contenido`
- `Nivel`
- `Dificultad`

Esto permite encontrar recursos mas adecuados para tu objetivo actual.

![Filtros laterales](./BibliotecaFiltros.png)

## 5. Charp

`Charp` es el asistente conversacional de ACC. Sirve para resolver dudas y apoyar el estudio de C#.

![Vista principal de Charp](./CharpPrimeraVista.png)

### Como usarlo

1. Entra a `Charp` desde la barra lateral.
2. Escribe tu pregunta en el campo inferior `Preguntale algo a Charp...`.
3. Puedes usar el icono de microfono si esta disponible en tu navegador.
4. Envia la consulta con el boton del extremo derecho.
5. Espera la respuesta en el panel central del chat.

![Caja de entrada](./CharpEntradaDatosChat.png)

### Opciones del chat

En el menu de tres puntos puedes usar:

- `Start a new chat` para iniciar una conversacion nueva.
- `End chat` para cerrar la conversacion actual.
- `View recent chats` para volver a conversaciones recientes.

![Opciones del chat](./CharpOpcionesChat.png)

### Recomendaciones de uso

- Pide explicaciones de conceptos de C#.
- Solicita ejemplos cortos.
- Pide ayuda para entender errores.
- Usa preguntas concretas para obtener respuestas mas utiles.

## 6. Cuenta

La seccion `Cuenta` centraliza perfil, seguridad y configuraciones personales.

![Panel principal de cuenta](./CuentasPrimerVista.png)

### 6.1 Perfil

Usa `Perfil` para actualizar:

- Nombre de usuario
- Numero telefonico

Notas importantes:

- El nombre de usuario tiene un tiempo de espera de 7 dias entre cambios.
- Despues de guardar, la sesion se actualiza automaticamente.

### 6.2 Correo

Usa `Correo` para:

- Ver el correo actual
- Confirmar el correo si aun no esta validado
- Solicitar cambio a un nuevo correo

### 6.3 Cambiar contrasena

1. Entra a `Cambiar Contrasena`.
2. Escribe la contrasena actual.
3. Captura la nueva contrasena.
4. Confirma la nueva contrasena.
5. Pulsa `Cambiar contrasena`.

![Cambio de contrasena](./CuentasContrase%C3%B1a.png)

### 6.4 Autenticacion de dos factores

La pantalla `Autenticacion de Dos Factores` permite activar una capa extra de seguridad.

1. Entra a `Autenticacion de Dos Factores`.
2. Pulsa `Configurar aplicacion de autenticacion`.
3. Escanea el codigo QR o captura la clave manual.
4. Escribe el codigo de verificacion generado por la app.
5. Pulsa `Verificar`.

Si ya tenias una configuracion anterior, puedes usar `Restaurar aplicacion de autenticacion`.

![Pantalla principal de 2FA](./Cuentas2FA_PrimeraVista.png)

![Configuracion de 2FA](./Cuentas2FA_Config.png)

### 6.5 Datos personales

Usa `Datos Personales` para:

- Descargar la informacion personal asociada a tu cuenta
- Eliminar la cuenta de forma permanente

Precaucion:

- La opcion `Eliminar` no se puede deshacer.

![Datos personales](./CuentasDatosPersonales.png)

### 6.6 Asignar roles

Por defecto, el usuario opera como `Estudiante`.

Si necesitas otro rol:

1. Entra a `Asignar Roles`.
2. Elige `Docente` o `Administrador`.
3. Captura la clave de verificacion.
4. Pulsa `Verificar`.

Si no deseas cambiar, usa `Quedarme con el rol de Estudiante`.

![Asignar roles](./CuentasRoles.png)

### 6.7 Cerrar sesion

- Usa `Cerrar sesion` en el menu lateral de cuenta para salir de la aplicacion.
- Al cerrar sesion, volveras al flujo publico o de acceso segun la ruta actual.

![Cerrar sesion](./CuentasCerrarSesion.png)

## Buenas practicas de uso

- Revisa `Resumen` al iniciar sesion para identificar lo mas urgente.
- Usa la `Agenda` para convertir pendientes informales en tareas visibles.
- Marca subtemas en `Guia` solo cuando realmente hayas terminado el contenido.
- Consulta `Biblioteca` cuando necesites profundizar en un concepto.
- Usa `Charp` como apoyo, no como sustituto del estudio guiado.
- Activa `2FA` si tu cuenta contiene trabajo academico importante.

## Cierre

Esta version del manual cubre el flujo principal del estudiante dentro de ACC. Si mas adelante se agregan capturas de `Aulas`, `Docentes` o `Administracion`, conviene extender este mismo documento para dejar una guia completa por rol.
