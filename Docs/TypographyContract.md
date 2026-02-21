# TypographyContract

# ACC --- Contrato Tipográfico Oficial

Versión: 1.0\
Estado: Activo\
Aplicación: Aprendiendo C# con Charp (ACC)

------------------------------------------------------------------------

## 0. Objetivo

Establecer un sistema tipográfico coherente, escalable y formal para ACC
que garantice:

-   Identidad visual consistente en toda la aplicación.
-   Jerarquía clara entre títulos, contenido y UI.
-   Lectura cómoda en lecciones y bloques extensos.
-   Representación técnica limpia y profesional en código.
-   Eliminación de dispersión tipográfica por componente.
-   Capacidad de crecimiento sin degradación visual.

Este contrato define:

-   Familias tipográficas oficiales.
-   Roles tipográficos.
-   Escala y tokens.
-   Reglas obligatorias de uso.
-   Mapeo por zonas de la aplicación.

------------------------------------------------------------------------

# 1. Familias Tipográficas Oficiales

## 1.1 UI / Lectura (Body)

Fuente oficial: Nunito\
Token: --font-body

Uso permitido:

-   UI general (labels, inputs, botones, navegación, tabs, formularios).
-   Contenido educativo (párrafos, listas, explicaciones).
-   Texto secundario.
-   Mensajes de chat (usuario y Charp).
-   Metadatos.

Uso prohibido:

-   Títulos principales de página.
-   Títulos de sección o tarjeta (salvo casos definidos explícitamente).

------------------------------------------------------------------------

## 1.2 Encabezados / Jerarquía (Headings)

Fuente oficial: Sora\
Token: --font-heading

Uso permitido:

-   Títulos de página.
-   Títulos de sección.
-   Títulos de tarjetas.
-   Nombres de módulos o entidades importantes.
-   Encabezados dentro de paneles y bloques destacados.

Uso prohibido:

-   Párrafos largos.
-   Inputs.
-   Labels de formulario.
-   Texto continuo de lectura.

------------------------------------------------------------------------

## 1.3 Código / Terminal

Fuente oficial: JetBrains Mono\
Token: --font-code

Uso permitido:

-   Bloques de código.
-   Inline code.
-   Consolas, logs y terminal simulada.
-   Snippets educativos.

Uso prohibido:

-   Texto general.
-   Encabezados.
-   UI convencional.

------------------------------------------------------------------------

# 2. Regla de Oro (Anti-Dispersión)

1.  Ningún componente define tipografías manualmente.
2.  Todo componente debe consumir:
    -   Roles tipográficos (.ty-\*), o
    -   Tokens oficiales.
3.  Está prohibido declarar font-family directamente en componentes
    individuales.
4.  Cualquier excepción debe registrarse en DesignDebt.md.

------------------------------------------------------------------------

# 3. Tokens Oficiales

## 3.1 Tokens de Fuente

--font-body: "Nunito", system-ui, -apple-system, Segoe UI, Roboto,
Arial, sans-serif; --font-heading: "Sora", system-ui, -apple-system,
Segoe UI, Roboto, Arial, sans-serif; --font-code: "JetBrains Mono",
ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, monospace;

------------------------------------------------------------------------

## 3.2 Escala Tipográfica

--text-xs: 0.75rem; --text-sm: 0.875rem; --text-md: 1rem; --text-lg:
1.125rem; --text-xl: 1.25rem; --text-2xl: 1.5rem; --text-3xl: 1.875rem;
--text-4xl: 2.25rem;

Contrato:

-   UI estándar: sm o md.
-   Lectura larga: md.
-   Encabezados: lg en adelante.
-   No se permiten tamaños arbitrarios.

------------------------------------------------------------------------

## 3.3 Pesos Oficiales

--w-regular: 400; --w-medium: 500; --w-semibold: 600; --w-bold: 700;
--w-extrabold: 800;

Contrato:

-   Body: 400--700.
-   Headings: 600--800.
-   Code: 400--600.
-   Evitar 800 fuera de encabezados principales.

------------------------------------------------------------------------

## 3.4 Line-Height Oficial

--lh-tight: 1.12; --lh-snug: 1.25; --lh-normal: 1.5; --lh-relaxed: 1.72;

Contrato:

-   Lectura educativa: relaxed.
-   UI normal: normal.
-   Encabezados grandes: tight.
-   Subtítulos: snug.

------------------------------------------------------------------------

## 3.5 Letter-Spacing Oficial

--ls-tight: -0.01em; --ls-normal: 0; --ls-wide: 0.03em;

Contrato:

-   Labels y badges: wide + uppercase.
-   Headings grandes: tight.
-   Body: normal.

------------------------------------------------------------------------

# 4. Roles Tipográficos Oficiales

## 4.1 Jerarquía Principal (Headings)

### .ty-page-title

-   font: --font-heading
-   size: --text-3xl o --text-4xl
-   weight: --w-extrabold
-   line-height: --lh-tight
-   letter-spacing: --ls-tight

### .ty-section-title

-   font: --font-heading
-   size: --text-xl
-   weight: --w-bold
-   line-height: --lh-snug
-   letter-spacing: --ls-tight

### .ty-card-title

-   font: --font-heading
-   size: --text-lg
-   weight: --w-bold
-   line-height: --lh-snug
-   letter-spacing: --ls-tight

------------------------------------------------------------------------

## 4.2 Texto UI / Lectura

### .ty-body

-   font: --font-body
-   size: --text-md
-   weight: --w-regular
-   line-height: --lh-normal

### .ty-paragraph

-   font: --font-body
-   size: --text-md
-   weight: --w-regular
-   line-height: --lh-relaxed

Uso obligatorio en lecciones.

### .ty-muted

-   font: --font-body
-   size: --text-sm o --text-md
-   color secundario

------------------------------------------------------------------------

## 4.3 Labels y Meta

### .ty-label

-   font: --font-body
-   size: --text-xs
-   weight: --w-semibold
-   letter-spacing: --ls-wide
-   text-transform: uppercase

------------------------------------------------------------------------

## 4.4 Código

### .ty-code-inline

-   font: --font-code
-   size: 0.95em
-   padding ligero
-   fondo diferenciado
-   borde sutil

### .ty-code-block

-   font: --font-code
-   size: --text-sm
-   line-height: 1.55
-   padding interno
-   overflow-x auto

### .ty-terminal

-   font: --font-code
-   size: --text-sm
-   estilo tipo consola/log

------------------------------------------------------------------------

## 4.5 Voz de Charp

Charp no introduce fuente nueva.

### .ty-charp-title

-   font: --font-body
-   weight: --w-extrabold
-   letter-spacing: -0.005em
-   size: --text-md o --text-lg

### .ty-charp-body

-   font: --font-body
-   line-height: --lh-relaxed
-   color secundario

------------------------------------------------------------------------

# 5. Reglas Obligatorias de Implementación

1.  Solo :root define fuentes.
2.  Los componentes consumen roles o tokens.
3.  Headings siempre usan Sora.
4.  Texto largo siempre usa Nunito.
5.  Código siempre usa JetBrains Mono.
6.  No se permite declarar font-family dentro de componentes.
7.  Cualquier excepción debe documentarse.

------------------------------------------------------------------------
# 6. Filosofía del Sistema

Nunito → accesibilidad y lectura cómoda.\
Sora → jerarquía y visión moderna.\
JetBrains Mono → precisión técnica e ingeniería.

Este contrato convierte la tipografía en infraestructura visual, no en
decoración.

------------------------------------------------------------------------

Fin del documento.
