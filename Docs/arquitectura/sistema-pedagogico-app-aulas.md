# ACC - Sistema Pedagogico APP y Aulas

## Estado del documento

Documento de diseno logico preliminar.

Este documento formaliza la relacion entre la Guia de Estudios de ACC, la seccion de Aulas y el Sistema Pedagogico APP. No define todavia el diseno fisico definitivo de base de datos, migraciones, endpoints ni componentes concretos de UI.

## Proposito

El Sistema Pedagogico APP es una capacidad de autoria pedagogica asistida que permite generar lecciones interactivas a partir de parametros definidos por un docente.

Su objetivo es transformar una intencion docente en contenido educativo estructurado, editable y renderizable dentro de ACC, reduciendo el tiempo de preparacion sin sustituir el criterio pedagogico humano.

APP no es un LMS ni una herramienta de administracion escolar. Su funcion principal es producir contenido didactico utilizable, no gestionar tareas institucionales, archivos, calificaciones formales o reportes administrativos.

## Separacion de contenido

ACC distingue dos familias de lecciones:

1. Lecciones de la Guia de Estudios.
2. Lecciones generadas mediante APP.

Ambas familias pueden compartir la infraestructura tecnica de renderizado basada en bloques interactivos, pero no tienen el mismo origen, control ni responsabilidad pedagogica.

### Lecciones de la Guia de Estudios

Las lecciones de la Guia de Estudios son contenido oficial de ACC.

Representan el modelo pedagogico base de la plataforma y forman parte de una progresion curricular curada. Su estructura, secuencia y objetivos responden a una planeacion centralizada.

Caracteristicas:

- Son contenido oficial.
- Tienen control de calidad centralizado.
- Pertenecen a la progresion principal de la Guia.
- No deben ser modificadas por docentes mediante APP.
- Sirven como base comun para todos los usuarios.

### Lecciones generadas mediante APP

Las lecciones APP son contenido contextual generado a partir de parametros proporcionados por un docente.

Su finalidad es complementar la experiencia educativa en un aula especifica, adaptandose a necesidades concretas del grupo, nivel de profundidad, recursos disponibles o estilo de explicacion del docente.

Caracteristicas:

- Son contenido contextual.
- Son editables por el docente responsable.
- No sustituyen la Guia oficial.
- Pueden asociarse a un aula, grupo o contexto docente.
- Comparten el renderizador por bloques de ACC.
- Su calidad pedagogica final es responsabilidad del docente que las crea, revisa y publica.

## Principio rector

APP no modifica contenido oficial de la Guia de Estudios.

El contenido generado por APP debe vivir como material complementario, normalmente dentro de Aulas, y debe poder renderizarse con la misma experiencia visual que una leccion oficial sin confundirse con ella.

## Relacion logica entre Guia, Aulas y APP

El modelo conceptual de ACC queda organizado en tres capas:

1. Guia oficial.
2. Aulas.
3. Sistema Pedagogico APP.

### Guia oficial

La Guia oficial contiene la ruta pedagogica base de ACC.

Su funcion es ofrecer una progresion estable, curada y comun. La navegacion principal de la Guia debe priorizar las lecciones oficiales y no mezclar automaticamente contenido generado por docentes.

### Aulas

Aulas es el espacio de interaccion pedagogica contextual.

Un aula agrupa a un docente y sus estudiantes, permitiendo comunicacion, seguimiento y asignacion de contenido generado o seleccionado para ese grupo.

En este contexto, APP opera como herramienta de creacion de lecciones de aula.

### APP

APP es el motor de autoria pedagogica.

Recibe parametros docentes, genera una propuesta de leccion estructurada en bloques, permite revision y edicion, y finalmente produce contenido listo para publicar dentro del aula.

APP no es una seccion aislada de consumo para estudiantes. Su lugar natural es dentro del flujo docente de Aulas.

## Vista docente de Aulas

Para el docente, un aula funciona como entorno de gestion pedagogica del grupo.

Capacidades logicas:

- Visualizar estudiantes asociados al aula.
- Consultar senales de progreso, desempeno y actividad.
- Publicar, editar y eliminar avisos del aula.
- Crear lecciones de aula mediante APP.
- Editar lecciones generadas antes o despues de publicarlas.
- Previsualizar lecciones usando el mismo renderizador de bloques.
- Publicar contenido para los estudiantes del aula.

El docente no modifica la Guia oficial desde Aulas. Cuando necesita contenido contextual, genera o edita una leccion propia del aula.

## Vista estudiante de Aulas

Para el estudiante, el aula es el espacio donde recibe indicaciones y contenido asignado por el docente.

Capacidades logicas:

- Consultar avisos del aula.
- Ver lecciones publicadas por el docente.
- Consumir lecciones de aula con el mismo renderizador dinamico de bloques.
- Registrar progreso dentro de las lecciones cuando aplique.

El estudiante debe poder distinguir que una leccion pertenece al aula y no a la Guia oficial, aunque la experiencia visual de aprendizaje sea consistente.

## Flujo logico de generacion APP

El flujo docente recomendado es:

1. El docente entra a un aula.
2. Selecciona crear una nueva leccion con APP.
3. Define parametros minimos:
   - Tema.
   - Nivel de Bloom.
   - Objetivo de aprendizaje.
   - Recursos de apoyo opcionales.
   - Necesidad de compilador, si aplica.
   - Solucion modelo o respuesta ideal, si aplica.
4. APP genera un borrador estructurado en bloques.
5. El docente revisa la leccion en preview.
6. El docente edita, elimina, agrega o reordena bloques.
7. La leccion se guarda como borrador o se publica en el aula.
8. Los estudiantes del aula pueden consumir la leccion publicada.

## Parametros pedagogicos base

APP se guia por la Taxonomia de Bloom:

- Recordar.
- Comprender.
- Aplicar.
- Analizar.
- Evaluar.
- Crear.

El nivel seleccionado debe influir en:

- Complejidad del lenguaje.
- Profundidad conceptual.
- Tipo de ejemplo.
- Tipo de actividad.
- Preguntas o interacciones.
- Retroalimentacion esperada.
- Forma de evaluacion formativa.

## Salida logica esperada

Una leccion generada por APP debe producir una secuencia ordenada de bloques compatibles con el renderizador de ACC.

La salida puede incluir:

- Introduccion.
- Explicacion conceptual.
- Ejemplos guiados.
- Tips o dialogos de apoyo.
- Recursos multimedia.
- Diagramas.
- Actividades interactivas.
- Preguntas alineadas a Bloom.
- Compilador, si aplica.
- Retroalimentacion.
- Resumen final.

La generacion automatica debe producir contenido editable, no contenido definitivo e intocable.

## Infraestructura compartida

La Guia oficial y las lecciones APP pueden compartir:

- Renderizador dinamico de bloques.
- Modelo declarativo de configuracion JSON.
- Componentes de texto, multimedia, diagramas, tips, preguntas y compilador.
- Validacion de tipos de bloque.
- Preview de leccion.

La diferencia no esta en como se renderiza, sino en:

- Origen del contenido.
- Lugar donde aparece.
- Responsable de calidad.
- Reglas de edicion.
- Relacion con la progresion pedagogica oficial.

## Reglas de integridad logica

- Una leccion oficial de la Guia no debe ser reemplazada por APP.
- Una leccion APP no debe aparecer como parte obligatoria de la Guia oficial salvo decision administrativa futura.
- Un docente solo debe administrar lecciones APP dentro de sus aulas.
- Un estudiante solo debe ver lecciones APP publicadas en aulas a las que pertenece.
- Toda leccion generada debe pasar por revision o confirmacion docente antes de publicarse.
- El renderizador puede ser comun, pero el origen del contenido debe conservarse.

## Alcance inicial recomendado

El primer MVP logico deberia cubrir:

- Separacion explicita entre contenido oficial y contenido APP.
- Aulas con vista docente y vista estudiante.
- Muro simple de avisos.
- Lista de estudiantes del aula.
- Lecciones de aula generadas o editadas por docente.
- Preview y publicacion de lecciones de aula.
- Renderizado de lecciones de aula con bloques interactivos.

No es necesario que el primer MVP incluya:

- Calificacion institucional.
- Entrega de archivos.
- Reportes complejos.
- Analitica avanzada.
- Adaptacion automatica por rendimiento.
- Exportacion a LMS.

## Extensiones futuras compatibles

El modelo permite extenderse hacia:

- Generacion automatica de examenes.
- Rubricas automaticas.
- Calificacion asistida.
- Feedback personalizado por desempeno.
- Variaciones adaptativas de una leccion.
- Recomendaciones docentes basadas en progreso del grupo.
- Exportacion a PDF o formatos externos.

Estas extensiones deben mantenerse como capacidades complementarias, no como reemplazo de la Guia oficial ni como intento de convertir APP en un LMS completo.

## Gestion, curaduria e integracion de lecciones

ACC debe evolucionar hacia un sistema que no solo consume contenido educativo, sino que tambien permite producirlo, observarlo, evaluarlo y eventualmente integrarlo de forma controlada.

Este modelo mantiene la Guia oficial como nucleo pedagogico estable, pero reconoce que las lecciones generadas por docentes pueden convertirse en una fuente valiosa de mejora, experimentacion y expansion del contenido.

### Tipos de contenido gestionado

El sistema distingue dos categorias principales:

1. Contenido oficial de ACC.
2. Contenido generado por docentes.

El contenido oficial pertenece a la Guia de Estudios y responde a control de calidad centralizado.

El contenido generado por docentes pertenece normalmente al contexto de Aulas y APP. Puede ser observado, evaluado y reutilizado, pero no forma parte de la Guia oficial por defecto.

### Rol del administrador de ACC

El administrador de ACC tiene una funcion distinta a la del docente.

Mientras el docente crea contenido contextual para sus aulas, el administrador gestiona la integridad del ecosistema completo de contenido educativo.

Capacidades logicas del administrador:

- Gestionar la estructura oficial de la Guia.
- Crear o editar lecciones oficiales.
- Usar APP como apoyo para crear borradores oficiales.
- Revisar lecciones generadas por docentes.
- Inspeccionar bloques, recursos, interacciones y metadatos pedagogicos.
- Identificar autor, aula, contexto y fecha de creacion.
- Marcar lecciones como candidatas a revision.
- Aprobar, rechazar, archivar o duplicar contenido.
- Integrar contenido seleccionado a repositorios oficiales.

### Gestion de la Guia de Estudios

La Guia oficial puede ser gestionada por administradores en sus niveles estructurales:

- Modulos.
- Submodulos.
- Temas.
- Subtemas.
- Lecciones.
- Bloques de contenido.
- Evaluaciones.

Las acciones administrativas pueden incluir:

- Crear nuevas lecciones oficiales.
- Editar contenido existente.
- Reorganizar rutas de aprendizaje.
- Eliminar o archivar contenido.
- Generar borradores mediante APP.
- Convertir contenido revisado en contenido oficial.

El uso de APP en este contexto no implica publicacion automatica. APP puede reducir el costo inicial de produccion, pero la validacion pedagogica sigue siendo responsabilidad administrativa.

### Repositorio global de lecciones generadas

El administrador debe contar logicamente con acceso a un repositorio global de lecciones generadas dentro de ACC.

Este repositorio permite observar el contenido creado por docentes sin convertirlo en contenido publico ni oficial.

Informacion relevante:

- Titulo de la leccion.
- Autor docente.
- Aula o contexto de origen.
- Fecha de creacion.
- Estado de publicacion.
- Nivel de Bloom.
- Estructura de bloques.
- Recursos vinculados.
- Interacciones incluidas.
- Senales de uso o desempeno, cuando existan.

El repositorio global funciona como herramienta de supervision, analisis y curaduria interna.

### Flujo de curaduria

El proceso de curaduria permite que una leccion generada por un docente pueda convertirse, despues de revision, en base para contenido oficial.

Flujo logico:

```txt
Leccion generada por docente
|
v
Revision por administrador
|
v
Evaluacion de calidad
|
v
Edicion o adaptacion
|
v
Aprobacion
|
v
Integracion a Guia o repositorio oficial
```

Acciones posibles durante el flujo:

- Revisar contenido.
- Editar bloques.
- Ajustar lenguaje, estructura o nivel cognitivo.
- Aprobar.
- Rechazar.
- Duplicar como base oficial.
- Marcar como candidata a integracion.
- Archivar.
- Retirar de revision.

La integracion debe ocurrir sobre una copia o version controlada, no como modificacion directa e irreversible del contenido docente original.

### Evaluacion interna de calidad

El sistema puede incorporar una evaluacion heuristica del contenido generado.

Esta evaluacion no sustituye la revision humana, pero puede ayudar a priorizar, orientar y detectar oportunidades de mejora.

Criterios posibles:

- Claridad del objetivo de aprendizaje.
- Alineacion con el nivel de Bloom.
- Coherencia estructural.
- Calidad de la explicacion.
- Pertinencia de ejemplos.
- Presencia y calidad de interacciones.
- Calidad de retroalimentacion.
- Uso adecuado del compilador, cuando aplique.
- Tasa de finalizacion.
- Errores frecuentes.
- Nivel de interaccion del estudiante.

### Objetivos de la evaluacion

La evaluacion cumple dos funciones.

Para el docente:

- Proveer retroalimentacion sobre sus lecciones.
- Sugerir mejoras pedagogicas.
- Indicar problemas de estructura o claridad.
- Promover mejores practicas de autoria.

Para la administracion:

- Priorizar contenido para revision.
- Identificar lecciones destacadas.
- Detectar patrones de calidad.
- Seleccionar material candidato a integracion oficial.

### Principios de control de calidad

- Ninguna leccion generada por docentes se integra automaticamente a la Guia oficial.
- Toda integracion requiere revision administrativa.
- El contenido oficial mantiene un estandar superior de validacion.
- El contenido generado es una fuente potencial, no un reemplazo directo.
- La curaduria debe preservar trazabilidad entre origen, autor, revision e integracion.
- El administrador puede usar APP como apoyo, pero no como sustituto del criterio editorial y pedagogico.

### Impacto esperado

Este modelo permite:

- Escalar la produccion de contenido.
- Aprovechar aportaciones docentes.
- Identificar lecciones efectivas en contextos reales.
- Mejorar continuamente el acervo educativo.
- Reducir costos de produccion manual.
- Convertir ACC en una plataforma pedagogica evolutiva.

ACC queda posicionado como un sistema que:

- Produce contenido.
- Evalua contenido.
- Aprende del uso del contenido.
- Integra conocimiento validado.
- Mantiene separacion entre contenido oficial y contenido contextual.

## Resumen ejecutivo

La Guia de Estudios de ACC conserva el contenido oficial, curado y progresivo de la plataforma.

Aulas proporciona el contexto real de interaccion entre docente y estudiantes.

APP funciona como herramienta de autoria pedagogica dentro de Aulas, generando lecciones complementarias estructuradas en bloques interactivos.

La arquitectura por bloques permite reutilizar el mismo renderizador para contenido oficial y contenido generado, pero la plataforma debe preservar siempre la distincion de origen, responsabilidad y proposito pedagogico.
