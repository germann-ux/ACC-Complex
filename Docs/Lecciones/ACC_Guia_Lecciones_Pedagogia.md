# Guía de Lecciones (ACC) — Pedagogía
**Aprendiendo C# con Charp (ACC)**

Este documento explica **cómo diseñar lecciones que enseñen bien** dentro del marco de ACC:  
Taxonomía de Bloom + secciones disponibles + estilo de acompañamiento de Charp.

> Alcance  
> - Aquí hablamos de **aprendizaje** (objetivos, flujo, práctica, progresión).  
> - El contrato **técnico** (JSON, enums, flags) vive en la guía de definiciones técnicas.  
> - El contrato **visual** (tokens/CSS) vive en la guía de estilos.

---

## 1) Propósito de una lección en ACC

Una lección en ACC es la **unidad mínima de aprendizaje**: una pieza pequeña, enfocada y accionable.

Una buena lección:
- enseña **una idea central**,
- guía al estudiante hacia una **acción observable**,

ACC no busca “texto bonito”; busca **progreso real**.

---

## 2) Principios pedagógicos base (reglas del juego)

Estas reglas evitan que ACC se convierta en un blog gigante con títulos bonitos.

1. **Un solo objetivo central por lección**  
   Si tienes 2–3 objetivos, divide la lección.

2. **Cognición incremental**  
   Primero comprender, luego aplicar, luego analizar/evaluar/crear (cuando toque).

3. **Carga mental baja**  
   Evita saltos mágicos, define términos, usa ejemplos cortos.

4. **El estudiante hace algo**  
   Leer no es suficiente: debe responder, predecir, comparar, ejecutar, corregir.

5. **Feedback inmediato o guiado**  
   Usa `charpTip` y `charpDialog` como andamios (scaffolding), no como relleno.

6. **Consistencia**  
   Forma similar de explicar = menos fricción = más aprendizaje.

---

## 3) Bloom en ACC (cómo usarlo en serio)

La Taxonomía de Bloom sirve para etiquetar el tipo de pensamiento que la lección provoca.

En ACC, **Bloom debe coincidir con lo que el estudiante hace**, no con lo que el autor dice.

### 3.1 Qué significa cada nivel (conducta observable)

- **Recordar**: identificar, listar, reconocer, definir.
- **Comprender**: explicar con tus palabras, resumir, interpretar, dar un ejemplo.
- **Aplicar**: usar el concepto para resolver una tarea, ejecutar, implementar.
- **Analizar**: comparar, descomponer, encontrar causas, detectar errores.
- **Evaluar**: justificar decisiones, criticar con criterios, elegir la mejor opción.
- **Crear**: construir algo nuevo, combinar ideas, diseñar una solución.

### 3.2 Cómo elegir el nivel Bloom

El nivel Bloom se elige por:
- el **verbo** del objetivo (“explicar”, “usar”, “diagnosticar”, “justificar”…),
- y la **evidencia**: qué entregable o acción prueba que aprendió.

### 3.3 Errores típicos

- Marcar **Aplicar** cuando solo hay definiciones.
- Marcar **Crear** cuando solo hay un ejercicio rutinario.
- Marcar **Analizar** sin preguntas que exijan “por qué” o comparación.

---

# 4 Correspondencia entre Nivel Bloom y Secciones en ACC

> Esta tabla describe qué tipos de secciones encajan mejor con cada nivel cognitivo de la Taxonomía de Bloom,
> respetando la semántica real de los bloques del sistema ACC.

| Nivel Bloom | Secciones que suelen encajar | Orden típico recomendado |
|-------------|------------------------------|---------------------------|
| **Recordar** | `charpDialog` (introducción muy corta), `teoria` mínima, `charpTip` (recordatorio), `practica` micro (opcional) | `["charpDialog","teoria","charpTip"]` o `["charpDialog","teoria","practica","charpTip"]` |
| **Comprender** | `charpDialog`, `teoria`, `ejemplo`, `practica` explicativa, `charpTip` | `["charpDialog","teoria","ejemplo","practica","charpTip"]` |
| **Aplicar** | `charpDialog` (brief), `teoria` mínima, `ejemplo`, `compilador`, `practica` fuerte, `actividad` (si aplica) | `["charpDialog","teoria","ejemplo","compilador","practica"]` o `["charpDialog","teoria","ejemplo","practica","actividad"]` |
| **Analizar** | `charpDialog` (planteamiento), `teoria` (criterios), `ejemplo` comparativo, `practica` diagnóstico, `charpTip` | `["charpDialog","teoria","ejemplo","practica","charpTip"]` |
| **Evaluar** | `charpDialog` (criterios), `teoria` (estándar), `ejemplo` (caso), `practica` (revisión), `charpTip` | `["charpDialog","teoria","ejemplo","practica","charpTip"]` |
| **Crear** | `charpDialog` (brief), `teoria` (mínimos), `compilador`, `practica` (mini-proyecto), `charpTip` | `["charpDialog","teoria","compilador","practica","charpTip"]` |

## 4.1 Reglas semánticas importantes

- `charpDialog` introduce, guía y contextualiza. Puede funcionar como teoría mínima o encuadre cognitivo.
- `teoria` documenta el contenido conceptual de forma explícita.
- `charpTip` es un refuerzo puntual, advertencia o heurística. No sustituye teoría.
- `ejemplo` concreta la teoría mediante casos.
- `practica` consolida mediante acción.
- `actividad` representa una experiencia externa o interactiva (URL).
- `compilador` habilita experimentación directa con código.

## 4.2 Reglas de coherencia del sistema

- No incluir `actividad` en `OrdenSecciones` si:
  - `TieneActividad = 0`  
  - `UrlActividad = ''`

- No incluir `compilador` si:
  - `TieneCompilador = 0`

- Evitar colocar `compilador` antes de `teoria` o `ejemplo`.

- Evitar usar tokens inexistentes en el renderizador (ej: `evaluacion`).

> Bloom define el tipo de pensamiento.  
> Las secciones definen la experiencia pedagógica.  
> No son la misma cosa.

---

## 5) Plantillas pedagógicas (recetas)

Las plantillas ayudan a producir lecciones consistentes, sin improvisar.

### 5.1 Plantilla A — Lección rápida (5–8 min)

Flujo recomendado:
- `charpDialog` (contexto + objetivo)
- `teoria` (1 idea)
- `ejemplo` (1)
- `practica` (1 microtarea)
- `charpTip` (recordatorio clave)

Ideal para: Recordar / Comprender.

### 5.2 Plantilla B — Lección práctica (Aplicar)

Flujo recomendado:
- `charpDialog` (meta + “vas a hacer X”)
- `ejemplo` (2 mini)
- `practica` (tarea)
- `compilador` (ejecuta)

Ideal para: Aplicar.

### 5.3 Plantilla C — Lección de análisis

Flujo recomendado:
- `charpDialog` (problema real)
- `ejemplo` (contraste: correcto vs error típico)
- `practica` (diagnóstico: “qué pasa y por qué”)

Ideal para: Analizar / Evaluar.

---

## 6) Cómo escribir cada sección (pedagógicamente)

En ACC, cada sección tiene una intención clara.  
Esta sección define **qué debe producir** cada una.

### 6.1 `teoria`
Intención:
- definir + explicar + delimitar.

Buenas prácticas:
- máximo 1 concepto central,
- evitar teoría enciclopédica,
- usar analogías solo si aclaran (no si decoran).

Anti-patrón:
- ensayo largo sin acción.

### 6.2 `ejemplo`
Intención:
- “aterrizar” la teoría.

Buenas prácticas:
- mínimo 1 ejemplo si se introduce algo nuevo,
- ideal 2: uno correcto, uno con error típico (si aplica),
- ejemplos cortos: lo suficiente para iluminar, no para abrumar.

Anti-patrón:
- ejemplo gigante que se vuelve otra lección.

### 6.3 `practica`
Intención:
- provocar acción observable (evidencia de aprendizaje).
- funjir de puente para la actividad externa(si aplica). 
Buenas prácticas:
- objetivo explícito,
- criterio de éxito (qué se considera correcto),
- dificultad realista respecto a la teoría presentada.

Anti-patrón:
- práctica imposible sin andamios.

### 6.4 `charpTip`
Intención:
- tip conciso, una sola idea, un recordatorio quirúrgico.

Buenas prácticas:
- 1 bala, 1 impacto,
- advertencia o aclaración puntual.

Anti-patrón:
- meter teoría completa aquí.

### 6.5 `charpDialog`
Intención:
- humano y contextual: bienvenida, puente, repaso, motivación breve.

Buenas prácticas:
- conectar con lo anterior: “Ahora que ya conoces…”
- declarar meta: “Hoy vamos a…”
- cerrar con dirección: “Vas a practicar…”
- mantenerlo en un párrafo o dos.

Anti-patrón:
- sermón largo que distrae.

### 6.6 `video`
Intención:
- reforzar visualmente o con demostración.

Buenas prácticas:
- usarlo cuando aporta algo que el texto no explica bien (ej. demostración),
- mantenerlo alineado con práctica.

Anti-patrón:
- video como sustituto de práctica.

### 6.7 `actividad`
Intención:
- interacción externa utilizando actividades echas a la medida usando herramientas como genially.

Buenas prácticas:
- usarla si añade interactividad real,
- no meter actividades por “relleno”.

Anti-patrón:
- Colocarla sin un objetivo claro.

### 6.8 `compilador`
Intención:
- permitir “probar” y validar hipótesis.

Buenas prácticas:
- usarlo cuando la lección exige ejecutar, modificar y observar.

Anti-patrón:
- incluirlo si no se usará.

### 6.9 `fomentador` (bloque Biblioteca)
Intención:
- llevar al estudiante a profundizar en la Biblioteca.

Aclaracion: 
- No es un bloque logico que pertenezca a las secciones, es un decorador que puede formar parte de el bloque html, se recomienda en las secciones de teoria y ejemplo al final, para fomentar la profundizacion del tema en la biblioteca. 

Buenas prácticas:
- incluirlo cuando el tema es denso o propenso a olvidarse,
- enlazar a un capítulo que realmente amplíe el tema.

Ejemplo de ruta:
- `Capitulo/Contenido/4`

Anti-patrón:
- enlaces irrelevantes o “por compromiso”.

La implementacion se detalla en la guia de estilos y insercion. 

---

## 7) Reglas de calidad (checklist antes de publicar)

Checklist corto base:

- [ ] ¿Hay un objetivo central en 1 línea?
- [ ] ¿El Bloom coincide con lo que el estudiante hace?
- [ ] ¿La práctica exige acción, no solo lectura?
- [ ] ¿Hay al menos 1 ejemplo si la lección introduce algo nuevo?
- [ ] ¿CharpTip/CharpDialog aportan valor y no relleno?
- [ ] ¿La dificultad es coherente con el nivel y el módulo?
- [ ] ¿Hay fomentador cuando realmente ayuda a profundizar?

---

## 8) Anti-patrones comunes (lecciones que salen “mal”)

- Mucha teoría, cero acción.
- Práctica imposible (sin andamios).
- Ejemplos enormes (matan atención).
- Se asumió conocimiento previo no enseñado.
- Bloom inflado (etiqueta que no coincide con la evidencia).
- CharpDialog como novela.

---

## 9) Progresión y dificultad (micro-currículo)

En un subtema, la progresión típica funciona así:

1) base conceptual (Comprender)  
2) práctica guiada (Aplicar)  
3) variaciones (Aplicar/Analizar)  

Además, conviene reciclar errores típicos en lecciones futuras (repetición espaciada).

---

## 10) Estilo de voz de ACC (cómo habla el contenido)

- claro, directo, técnico,
- humor ligero permitido (sin infantilizar),
- corregir sin humillar.

Frases útiles:
- Inicio: “Hoy vamos a…”
- Puente: “Ahora que ya sabes…”
- Refuerzo: “Considera lo siguiente…”

---

## 11) Principio final

> El autor diseña el recorrido cognitivo.  
> El estudiante demuestra aprendizaje con acción.  
> El sistema vuelve la experiencia consistente y medible.

Si esta guía se respeta, ACC produce lecciones coherentes, progresivas y realmente enseñables.
