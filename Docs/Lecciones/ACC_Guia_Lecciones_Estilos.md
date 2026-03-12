# Guía de Lecciones (ACC) — Estilos (UI/CSS)
**Aprendiendo C# con Charp (ACC)**

Este documento explica el **sistema de estilos visuales** que ACC aplica a las lecciones: paleta (tokens), superficies, tipografía, tablas, código, alertas, fomentador, iframes y el indicador de **Nivel Bloom**.

> Nota de alcance  
> - Aquí documentamos **cómo se ve** la lección (CSS).  
> - El contrato de **estructura y secciones** (JSON, enums, flags como `TieneVideo`) vive en la guía técnica.

---

## 1) Fuente de verdad: Tokens globales de ACC

ACC utiliza variables CSS (`--acc-*`) como **tokens** (colores, radios, sombras, transición).  
Esto garantiza consistencia entre Biblioteca, Lecciones y demás vistas, se listan para el conocimiento de como lucen, no para que modifiques o insertes de forma directa usando estilos inline en el html, eso seria incorrecto. Digamos que debes tomarlo como un contexto a tener en cuenta. 

### 1.1 Paleta base (oscuros)

| Token | Valor | Uso típico |
|------|------:|-----------|
| `--acc-bg-0` | `#0f0f0f` | fondo global más profundo |
| `--acc-bg-1` | `#121218` | fondo de página |
| `--acc-bg-2` | `#1e1e2a` | superficies principales |
| `--acc-bg-3` | `#282838` | superficies elevadas / bloques |
| `--acc-bg-hover` | `#32324a` | hover en superficies |

### 1.2 Bordes y texto

| Token | Valor | Uso típico |
|------|------:|-----------|
| `--acc-border` | `#3f3f5a` | bordes y separadores |
| `--acc-text` | `#f8fafc` | texto principal |
| `--acc-text-muted` | `#cbd5e1` | texto secundario |

### 1.3 Marca (morado ACC)

| Token | Valor |
|------|------:|
| `--acc-brand-600` | `#9926fe` |
| `--acc-brand-700` | `#7c3aed` |
| `--acc-brand-800` | `#3d1f5c` |
| `--acc-brand-900` | `#2a003f` |

### 1.4 Azul de apoyo

| Token | Valor |
|------|------:|
| `--acc-blue-600` | `#4f46e5` |
| `--acc-blue-700` | `#3730a3` |

### 1.5 Semánticos

| Token | Valor | Uso |
|------|------:|-----|
| `--acc-success-500` | `#22c55e` | éxito |
| `--acc-warning-500` | `#f59e0b` | advertencia |
| `--acc-danger-500` | `#ef4444` | error |
| `--acc-warning-accent` | `#ffcc00` | resaltado “highlight” |
| `--acc-warning-icon` | `#d0d122` | íconos auxiliares |

### 1.6 Dimensiones y motion

| Token | Valor | Uso |
|------|------:|-----|
| `--acc-radius` | `12px` | redondeo estándar |
| `--acc-radius-sm` | `8px` | redondeo compacto |
| `--acc-shadow` | `0 8px 24px rgba(0, 0, 0, .35)` | sombra de tarjetas |
| `--acc-transition` | `0.2s ease` | transiciones |
| `--acc-focus` | `0 0 0 3px color-mix(...)` | foco accesible |

---

## 2) Dependencias / compatibilidad

Estos estilos usan:

- `color-mix(in srgb, ...)`
- `aspect-ratio`

Requieren navegadores relativamente modernos. Si ACC apuntara a navegadores antiguos, habría que agregar **fallbacks**.

---

## 3) Estilo base de la lección

### 3.1 Contenedor principal: `.leccion-container`

**Objetivo:** superficie principal de la lección, centrada, legible y consistente con el tema.

- Fuente: `Nunito` (con fallback a system UI).
- `max-width: 1200px` (layout amplio).
- Fondo: `--acc-surface-2` (equivalente a `--acc-bg-2`).
- Borde: `--acc-border`.
- Sombra: `--acc-shadow`.
- Padding: `22px`.
- Radio: `--acc-radius-sm`.

> Nota: el autor **no** escribe este contenedor. Lo genera el renderizador.

---

## 4) Estilo de secciones internas (superficies)

El CSS actual estiliza superficies internas con clases HTML:
estas son wrapers del contenedor principal, y deben ser usadas para delimitar las secciones dependiendo de su intencion pedagogica. 
- `.leccion-teoria`
- `.leccion-ejemplos`
- `.leccion-practicas`

**Objetivo visual:** “cards” internas, con borde sutil y acento lateral morado.

Características:
- Padding interno (18px).
- Fondo “mezclado” con negro para profundidad.
- Borde sutil + borde izquierdo acentuado.
- Separación vertical (`margin-bottom: 18px`).

> Importante (técnico):
> - El contenedor global `.leccion-container` lo genera el renderizador.
> - Los wrappers de sección (`.leccion-teoria`, `.leccion-ejemplos`, `.leccion-practicas`) viven dentro del HTML guardado en BD
>   y actúan como **delimitadores semánticos** de cada sección.
> - El renderizador respeta `OrdenSecciones` y coloca cada bloque, pero no necesita que el autor escriba layout global.

### 4.1 Legibilidad por ancho
Para teoría y práctica, se restringe el ancho a `max-width: 900px` para evitar líneas demasiado largas.

---

### Mapeo recomendado (sección lógica → wrapper HTML/CSS)

- `teoria`  → `<div class="leccion-teoria">...</div>`
- `ejemplo` → `<div class="leccion-ejemplos">...</div>`
- `practica` → `<div class="leccion-practicas">...</div>`

Decoradores internos (opcionales):
- `.alert`, `.alert-info`, `.alert-warning`, `.alert-success`, `.alert-error`
- `.fomentador` (bloque de empuje/refuerzo dentro del contenido)

## 5) Tipografía y lectura

### 5.1 Títulos `h3`
- Color: `--acc-brand-600` (morado).
- Peso: `800`.
- Espaciado: `letter-spacing: .2px`.

### 5.2 Párrafos `p`
- Color: `--acc-text-muted`.
- `line-height: 1.65` (respira).
- Separación: `margin-bottom: 12px`.

Además, el último párrafo de una sección no agrega margen extra.

---

## 6) Listas

- `ul` usa `list-style-type: disc`.
- Indentación controlada (`margin-left: 20px`).
- `li` en texto principal (`--acc-text`) para contraste.

---

## 7) Enlaces

Los enlaces dentro de lecciones:
- usan `--acc-brand-600`,
- subrayado “elegante” con `border-bottom` semitransparente,
- hover a `--acc-brand-700`.

Esto mantiene enlaces “visibles” sin verse como HTML crudo.

---

## 8) Tablas

Las tablas están diseñadas como superficie con borde:

- Fondo: `--acc-surface-2`
- Borde: `--acc-border`
- Encabezado: fondo mezclado con negro (`color-mix`), texto `--acc-text`.

Se recomienda usar tablas para información estructurada (operadores, comparaciones, etc.).

---

## 9) Código (inline vs bloque)

### 9.1 Inline code: `code`
- Fondo: mezcla de `--acc-bg-3` con negro.
- Borde sutil.
- Redondeo 6px.
- Tamaño ligeramente reducido (`.95em`).

### 9.2 Bloques: `pre` + `pre code`
- Fondo: `--acc-bg-3`.
- Borde y sombra interna.
- Overflow horizontal (`overflow-x: auto`) para evitar romper layout.
- El `code` interno se “limpia” para no duplicar estilos.

---

## 10) Botones

Los botones en la lección:
- fondo morado (`--acc-brand-600`),
- hover (`--acc-brand-700`),
- foco accesible (`--acc-focus`),
- sombra media para profundidad.

> Nota: en lecciones, los botones suelen ser parte de componentes del sistema (por ejemplo, expand/acciones UI).  
> Si el autor llegara a incluir botones en HTML (no es lo común), heredarán este estilo.

---

## 11) Imágenes

Las imágenes se muestran:
- responsivas (`max-width: 100%`),
- con radio `--acc-radius-sm`,
- con margen vertical sutil.

---

## 12) Resaltados: `.highlight`

Clase utilitaria para destacar texto:
- color: `--acc-warning-accent` (`#ffcc00`)
- peso: `800`

Uso típico: conceptos clave, advertencias suaves, “ojo con esto”.

---

## 13) Separadores `hr`

`hr` se redibuja como línea suave:
- altura 1px,
- color mezclado desde `--acc-border`.

---

## 14) Alertas

### 14.1 Base: `.alert`
Bloque con:
- fondo mezclado (texto 6% + superficie),
- borde y sombra,
- borde izquierdo como acento (por defecto morado).

### 14.2 Título opcional: `.alert-title`
- peso `900`,
- color principal.

### 14.3 Variantes semánticas
- `.alert-info` (azul)
- `.alert-success` (verde)
- `.alert-warning` (amarillo/naranja)
- `.alert-error` (rojo)

Cada variante:
- ajusta fondo (mezcla del color semántico),
- ajusta `border-left-color`.

---

## 15) Fomentador (bloque de biblioteca)

Clase: `.fomentador`

Objetivo:
- llamar a profundizar en Biblioteca,
- mantener el mismo estilo semántico que alertas pero con identidad propia.

Características:
- fondo mezclado como “panel”,
- borde izquierdo más neutro (derivado de `--acc-border`),
- tipografía y line-height pensados para texto “invita a leer”.

Enlaces dentro del fomentador:
- heredan estilo morado + hover.

---

## 16) Utilidades existentes (se conservan)

### 16.1 Botón “expand”
Clase: `.expand-btn`  
Solo define margen para mantener consistencia.

### 16.2 Iframe responsivo
Clase: `.responsive-iframe-no-modal`

- `aspect-ratio: 16/9`
- sin bordes
- ancho completo

Uso típico: actividades externas (renderizadas por sistema).

---

## 17) Indicador Bloom (UI)

### 17.1 Contenedor `.nivel-bloom`
- se alinea a la derecha (`justify-content: flex-end`).

### 17.2 “Badge” `.nivel-bloom p`
- forma de píldora
- borde 2px
- uppercase
- `letter-spacing: 1px`
- hover con elevación + brillo suave

### 17.3 Paleta por nivel Bloom

| Nivel | Texto | Borde | Fondo |
|------|-------|-------|------|
| `Recordar` | blanco | `#64748b` | `rgba(71, 85, 105, 0.68)` |
| `Comprender` | blanco | `#2563eb` | `rgba(29, 78, 216, 0.68)` |
| `Aplicar` | blanco | `#22c55e` | `rgba(22, 163, 74, 0.68)` |
| `Analizar` | negro | `#f59e0b` | `rgba(250, 204, 21, 0.68)` |
| `Evaluar` | blanco | `#e11d48` | `rgba(190, 18, 60, 0.68)` |
| `Crear` | blanco | `#8b5cf6` | `rgba(109, 40, 217, 0.68)` |

> Importante: el autor no renderiza el HTML del Bloom; solo declara `NivelBloom`.  
> El sistema genera el badge con la clase correspondiente.

---

## 18) CSS de referencia (fuente)

A continuación se incluye el bloque CSS actual como referencia oficial (si se actualiza en el repo, este documento debe versionarse):

```css
/* -----------------------------
   Lecciones (superficies, tablas, code, alerts)
   + mejoras estéticas ACC
   ----------------------------- */

.leccion-container {
    font-family: 'Nunito', system-ui, -apple-system, Segoe UI, Roboto, Arial, sans-serif;
    max-width: 1200px;
    background-color: var(--acc-surface-2);
    border-radius: var(--acc-radius-sm);
    padding: 22px;
    margin: 0 auto;
    border: 1px solid var(--acc-border);
    box-shadow: var(--acc-shadow);
}

/* Secciones */
.leccion-container .leccion-teoria,
.leccion-container .leccion-ejemplos,
.leccion-container .leccion-practicas {
    margin-bottom: 18px;
    padding: 18px 18px 16px;
    border-radius: var(--acc-radius);
    background: color-mix(in srgb, var(--acc-surface-2) 82%, black);
    border: 1px solid color-mix(in srgb, var(--acc-border) 75%, transparent);
    border-left: 3px solid color-mix(in srgb, var(--acc-brand-700) 70%, var(--acc-border));
    align-content: center;
}

/* Legibilidad (reduce línea muy larga) */
.leccion-container .leccion-teoria,
.leccion-container .leccion-practicas {
    max-width: 900px;
}

/* Títulos y texto */
.leccion-container h3 {
    color: var(--acc-brand-600);
    font-weight: 800;
    margin: 0 0 10px;
    letter-spacing: .2px;
}

.leccion-container p {
    color: var(--acc-text-muted);
    font-size: 1rem;
    margin: 0 0 12px;
    line-height: 1.65;
}

.leccion-container .leccion-teoria p:last-child,
.leccion-container .leccion-ejemplos p:last-child,
.leccion-container .leccion-practicas p:last-child {
    margin-bottom: 0;
}

/* Listas */
.leccion-container ul {
    margin: 0 0 12px 20px;
    padding: 0;
    list-style-type: disc;
}

.leccion-container li {
    margin-bottom: 8px;
    font-size: 1rem;
    color: var(--acc-text);
}

/* Links (faltaban en tu base) */
.leccion-container a {
    color: var(--acc-brand-600);
    font-weight: 700;
    text-decoration: none;
    border-bottom: 1px solid color-mix(in srgb, var(--acc-brand-600) 60%, transparent);
    transition: color var(--acc-transition), border-bottom-color var(--acc-transition);
}

.leccion-container a:hover {
    color: var(--acc-brand-700);
    border-bottom-color: var(--acc-brand-700);
}

/* Tablas */
.leccion-container table {
    width: 100%;
    border-collapse: collapse;
    margin: 18px 0;
    background-color: var(--acc-surface-2);
    border: 1px solid var(--acc-border);
    color: var(--acc-text);
}

.leccion-container table th,
.leccion-container table td {
    border: 1px solid var(--acc-border);
    padding: 10px;
    text-align: left;
}

.leccion-container table thead th {
    background: color-mix(in srgb, var(--acc-surface-2) 85%, black);
    color: var(--acc-text);
    font-weight: 800;
}

/* --- CÓDIGO: FIX INLINE vs BLOQUE --- */
/* Inline code */
.leccion-container code {
    display: inline;
    padding: .15rem .35rem;
    border-radius: 6px;
    background: color-mix(in srgb, var(--acc-bg-3) 85%, black);
    border: 1px solid color-mix(in srgb, var(--acc-border) 70%, transparent);
    color: var(--acc-text);
    font-size: .95em;
}

/* Code block */
.leccion-container pre {
    background-color: var(--acc-bg-3);
    color: var(--acc-text);
    border-radius: var(--acc-radius-sm);
    padding: 14px;
    font-size: 0.95rem;
    display: block;
    overflow-x: auto;
    margin: 12px 0;
    box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.15);
    border: 1px solid color-mix(in srgb, var(--acc-border) 65%, transparent);
}

.leccion-container pre code {
    display: block;
    padding: 0;
    border: none;
    background: transparent;
    font-size: inherit;
}

/* Botones */
.leccion-container button {
    padding: .6rem 1rem;
    background-color: var(--acc-brand-600);
    color: #fff;
    border: none;
    border-radius: var(--acc-radius-sm);
    cursor: pointer;
    font-size: 1rem;
    transition: var(--acc-transition);
    box-shadow: 0 6px 18px rgba(0, 0, 0, .35);
}

.leccion-container button:hover {
    background-color: var(--acc-brand-700);
}

.leccion-container button:focus-visible {
    outline: none;
    box-shadow: var(--acc-focus);
}

/* Imágenes */
.leccion-container img {
    max-width: 100%;
    height: auto;
    display: block;
    margin: 10px 0;
    border-radius: var(--acc-radius-sm);
}

/* Destacados */
.leccion-container .highlight {
    color: var(--acc-warning-accent);
    font-weight: 800;
}

/* Separador suave (nuevo) */
.leccion-container hr {
    border: none;
    height: 1px;
    margin: 14px 0;
    background: color-mix(in srgb, var(--acc-border) 60%, transparent);
}

/* Alertas */
.leccion-container .alert {
    padding: 15px;
    background-color: color-mix(in srgb, var(--acc-text) 6%, var(--acc-surface-2));
    color: var(--acc-text);
    border-left: 3px solid var(--acc-brand-700);
    border-radius: var(--acc-radius);
    margin-bottom: 18px;
    box-shadow: var(--acc-shadow);
    border: 1px solid color-mix(in srgb, var(--acc-border) 70%, transparent);
}

/* Título opcional para alertas (nuevo) */
.leccion-container .alert-title {
    font-weight: 900;
    margin: 0 0 6px;
    color: var(--acc-text);
}

.leccion-container .alert-info {
    background-color: color-mix(in srgb, var(--acc-blue-600) 15%, var(--acc-surface-2));
    border-left-color: var(--acc-blue-600);
}

.leccion-container .alert-success {
    background-color: color-mix(in srgb, var(--acc-success-500) 15%, var(--acc-surface-2));
    border-left-color: var(--acc-success-500);
}

.leccion-container .alert-warning {
    background-color: color-mix(in srgb, var(--acc-warning-500) 15%, var(--acc-surface-2));
    border-left-color: var(--acc-warning-500);
}

.leccion-container .alert-error {
    background-color: color-mix(in srgb, var(--acc-danger-500) 15%, var(--acc-surface-2));
    border-left-color: var(--acc-danger-500);
}

/* Fomentador */
.fomentador,
.leccion-container .fomentador {
    padding: 15px;
    background-color: color-mix(in srgb, var(--acc-text) 6%, var(--acc-surface-2));
    color: var(--acc-text);
    border-left: 3px solid color-mix(in srgb, var(--acc-border) 90%, black);
    border-radius: var(--acc-radius);
    margin: 18px 0;
    font-size: 1rem;
    line-height: 1.6;
    box-shadow: var(--acc-shadow);
    border: 1px solid color-mix(in srgb, var(--acc-border) 70%, transparent);
}

.leccion-container .fomentador a {
    color: var(--acc-brand-600);
    text-decoration: none;
    font-weight: 800;
    transition: color var(--acc-transition);
}

.leccion-container .fomentador a:hover {
    color: var(--acc-brand-700);
}

/* BOTON EQUISDE (se conserva) */
.expand-btn {
    margin: 1rem 0 1rem;
}

/* Iframe responsivo (se conserva) */
.responsive-iframe-no-modal {
    width: 100%;
    aspect-ratio: 16 / 9;
    border: none;
}

.leccion-container .nivel-bloom {
    display: flex;
    justify-content: flex-end; 
}

.leccion-container .nivel-bloom p {
    display: inline-block;
    padding: 0.4rem 1rem;
    border-radius: 50px;
    margin-bottom: 1rem;
    font-weight: 400;
    letter-spacing: 1px;
    text-transform: uppercase;
    font-size: 0.85rem;
    border: 2px solid;
}

.leccion-container .nivel-bloom p {
    box-shadow: 0 0 0 0 transparent;
    transition: transform 0.18s ease, box-shadow 0.18s ease, filter 0.18s ease;
    will-change: transform, box-shadow;
}

.leccion-container .nivel-bloom p:hover {
    transform: translateY(-2px);
    box-shadow: 0 6px 14px rgba(0, 0, 0, 0.12), 0 2px 4px rgba(0, 0, 0, 0.06);
    filter: brightness(1.06) saturate(1.05);
}

/* RECORDAR */
.nivel-bloom.Recordar p {
    color: #ffffff;
    border-color: #64748b;
    background-color: rgba(71, 85, 105, 0.68);
}

/* COMPRENDER */
.nivel-bloom.Comprender p {
    color: #ffffff;
    border-color: #2563eb;
    background-color: rgba(29, 78, 216, 0.68);
}

/* APLICAR */
.nivel-bloom.Aplicar p {
    color: #ffffff;
    border-color: #22c55e;
    background-color: rgba(22, 163, 74, 0.68);
}

/* ANALIZAR */
.nivel-bloom.Analizar p {
    color: #000000;
    border-color: #f59e0b;
    background-color: rgba(250, 204, 21, 0.68);
}

/* EVALUAR */
.nivel-bloom.Evaluar p {
    color: #ffffff;
    border-color: #e11d48;
    background-color: rgba(190, 18, 60, 0.68);
}

/* CREAR */
.nivel-bloom.Crear p {
    color: #ffffff;
    border-color: #8b5cf6;
    background-color: rgba(109, 40, 217, 0.68);
}
```

---

## 19) Tokens globales (referencia completa)

```css
:root {
    /* Paleta base (oscuros) */
    --acc-bg-0: #0f0f0f;
    --acc-bg-1: #121218;
    --acc-bg-2: #1e1e2a;
    --acc-bg-3: #282838;
    --acc-bg-hover: #32324a;

    /* Bordes / separadores */
    --acc-border: #3f3f5a;

    /* Texto */
    --acc-text: #f8fafc;
    --acc-text-muted: #cbd5e1;

    /* Marca */
    --acc-brand-600: #9926fe;
    --acc-brand-700: #7c3aed;
    --acc-brand-800: #3d1f5c;
    --acc-brand-900: #2a003f;

    /* Azul de apoyo */
    --acc-blue-600: #4f46e5;
    --acc-blue-700: #3730a3;

    /* Gradiente */
    --acc-grad-start: var(--acc-bg-0);
    --acc-grad-mid: #1c0b33;
    --acc-grad-end: var(--acc-brand-900);

    /* Dimensiones y motion */
    --acc-radius: 12px;
    --acc-radius-sm: 8px;
    --acc-shadow: 0 8px 24px rgba(0, 0, 0, .35);
    --acc-transition: 0.2s ease;

    /* Focus accesible */
    --acc-focus: 0 0 0 3px color-mix(in srgb, var(--acc-brand-600) 55%, white);

    /* Superficies */
    --acc-surface-2: #1e1e2a;
    --acc-surface-3: #282838;
    --acc-surface-hover: #32324a;

    /* Bordes/texto auxiliares */
    --acc-border-strong: #929292;

    /* Semánticos */
    --acc-success-500: #22c55e;
    --acc-warning-500: #f59e0b;
    --acc-danger-500: #ef4444;

    /* Acentos puntuales */
    --acc-warning-accent: #ffcc00;
    --acc-warning-icon: #d0d122;

    /* Error boundary */
    --acc-danger-surface: color-mix(in srgb, #b32121 85%, black);
}
```
