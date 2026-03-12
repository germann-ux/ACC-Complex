# Guía de Lecciones (ACC) — Definiciones técnicas
**Aprendiendo C# con Charp (ACC)**

Esta sección define el **contrato técnico** de una lección: qué campos existen, qué piezas reconoce el sistema, qué escribe el autor y qué renderiza automáticamente ACC.

---

## 1) Qué es una lección (definición técnica)

En ACC, una **lección** es la unidad mínima de aprendizaje dentro de la jerarquía:

**Módulo → Submódulo → Tema → Subtema → Lección**

Técnicamente, una lección es un registro que:

- pertenece a un **Subtema** (relación obligatoria),
- define un **Nivel Bloom** (obligatorio),
- contiene un **orden de secciones** (flujo) basado en un conjunto cerrado (obligatorio),
- Incluye contenido HTML controlado **solo en secciones específicas**,
- puede habilitar componentes del sistema (video, actividad externa, compilador) mediante **flags/datos declarativos**.

---

## 2) Responsabilidades: Autor vs Sistema

### Autor (creador de lecciones)
El autor:
- define **qué piezas** aparecen (secciones),
- define el **orden** (flujo cognitivo),
- escribe **HTML de marcado** únicamente en secciones que lo permiten,
- declara valores válidos (por ejemplo, `NivelBloom`, `VideoId`, URLs).

### Sistema (renderizador ACC)
El sistema:
- envuelve el contenido dentro del layout oficial (incluye contenedor principal),
- renderiza indicadores visuales (por ejemplo, Bloom),
- renderiza componentes especiales (video, iframe de actividad, compilador),
- aplica estilos globales y consistencia visual,
- impone/valida el contrato (cuando aplique).

Reglas clave (capas del sistema)
>- El autor NO escribe el layout global (ej. .leccion-container) ni componentes automáticos del sistema (video, actividad, compilador).
>- El autor SÍ escribe el marcado interno de cada sección de contenido, incluyendo su delimitador raíz (wrapper) dentro del HTML guardado en BD.
>- Excepción: charpTip y charpDialog tienen prohibido tener wrapper o delimitador raíz; su HTML debe contener únicamente el contenido interno.

Los “decoradores” (ej. .alert, .fomentador) viven dentro del HTML de una sección, pero no son secciones del renderizador.
## 3) Enumeraciones oficiales (contrato de valores)

### 3.1 Niveles Bloom (string con regla tipo enum)
El campo `NivelBloom` es un `string`, pero solo se considera válido si coincide exactamente con uno de estos valores:

- `Recordar`
- `Comprender`
- `Aplicar`
- `Analizar`
- `Evaluar`
- `Crear`

Ejemplo de definición en código:

```csharp
public static class NivelesBloom
{
    public const string Recordar = "Recordar";
    public const string Comprender = "Comprender";
    public const string Aplicar = "Aplicar";
    public const string Analizar = "Analizar";
    public const string Evaluar = "Evaluar";
    public const string Crear = "Crear";
}
```

> Importante: el autor **no escribe HTML** del nivel Bloom.  
> Solo asigna el string válido; el renderizador genera el indicador visual.

---

### 3.2 Secciones de contenido (conjunto cerrado)
Las **únicas secciones válidas** que pueden aparecer en el orden de una lección son:

- `video`
- `teoria`
- `practica`
- `ejemplo`
- `actividad`
- `compilador`
- `charpTip`
- `charpDialog`

Ejemplo de definición en código:

```csharp
public static class SeccionesContenido
{
    public const string Video = "video";
    public const string Teoria = "teoria";
    public const string Practica = "practica";
    public const string Ejemplo = "ejemplo";
    public const string Actividad = "actividad";
    public const string Compilador = "compilador";
    public const string CharpTip = "charpTip";
    public const string CharpDialog = "charpDialog";
}
```

---

## 4) Estructura mínima de una lección (campos obligatorios)

Una lección válida debe contar con:

1. **Título** (obligatorio)  
2. **Descripción** (obligatorio)  
3. **Id de Subtema** (obligatorio)  
4. **NivelBloom** (obligatorio, usando el enum lógico)  
5. **OrdenSecciones** (obligatorio, usando `SeccionesContenido`)  

> Las secciones como `teoria`, `ejemplo`, `practica` **no son obligatorias** por sí mismas:  
> la lección puede existir sin ellas, siempre que su estructura mínima sea válida.

---

## 5) OrdenSecciones (flujo de renderizado)

`OrdenSecciones` es un arreglo (JSON) con strings que definen el orden exacto en el que el renderizador presenta cada pieza.

Ejemplo:

```json
["charpDialog", "video", "teoria", "ejemplo", "practica"]
```

### Reglas obligatorias
- Solo se permiten strings definidos en `SeccionesContenido`.
- Cada sección es **única**: no debe repetirse dentro del array.
- Si el array incluye una sección que requiere datos/flags, estos deben existir (ver sección 7).

### Nota importante
`OrdenSecciones` define **orden**, no contenido.  
El contenido (HTML o datos) vive en campos aparte.

### Tipos de secciones en `OrdenSecciones`

En ACC existen 2 tipos de secciones:

**A) Secciones de contenido (vienen de columnas HTML)**
Estas secciones consumen directamente el HTML guardado en la tabla:
- `teoria`  → columna `Teoria`
- `ejemplo` → columna `Ejemplo`
- `practica` → columna `Practica`
- `charpDialog` → columna `CharpDialog`
- `charpTip` → columna `CharpTip` (corto)

> Recomendación: el HTML de estas columnas **sí debe venir delimitado** con su wrapper raíz:
> - `<div class="leccion-teoria">...</div>`
> - `<div class="leccion-ejemplos">...</div>`
> - `<div class="leccion-practicas">...</div>`

**B) Secciones del sistema (no requieren HTML del autor)**
Estas secciones no viven como HTML del autor, sino que se renderizan por flags/campos:
- `compilador` → depende de `TieneCompilador = 1`
- `actividad` → depende de `TieneActividad = 1` y `UrlActividad` válido
- `video` → depende de `TieneVideo = 1` y `VideoId`

✅ Importante: una sección puede aparecer en `OrdenSecciones` aunque no exista una columna HTML asociada, siempre que tenga su flag/campo que la soporte.

### Nota: `fomentador` no es sección del renderizador

`fomentador` **no forma parte** de `OrdenSecciones`.  
Es un **bloque decorador HTML** que el autor puede incluir dentro de `teoria`, `ejemplo` o `practica`:
la ruta sigue la siguiente estructura: Capitulo/Contenido/numero de capitulo
```html
<div class="fomentador">
  <p>¿Te gusta el tema?, profundiza mas sobre el en la biblioteca, haciendo clic <a href="Capitulo/Contenido/4">aqui</a></p>
</div>
```
---

## 6) Qué secciones aceptan HTML y cuáles no

| Sección | ¿Autor escribe HTML? | Qué hace el sistema |
|--------|------------------------|---------------------|
| `teoria` | ✅ Sí | Renderiza contenido dentro del layout |
| `ejemplo` | ✅ Sí | Renderiza contenido dentro del layout |
| `practica` | ✅ Sí | Renderiza contenido dentro del layout |
| `charpTip` | ✅ Sí (corto/puntual) | Renderiza bloque tipo tip |
| `charpDialog` | ✅ Sí (más extenso) | Renderiza diálogo contextual |
| `video` | ❌ No | Inyecta reproductor YouTube |
| `actividad` | ❌ No | Inyecta iframe de actividad |
| `compilador` | ❌ No | Renderiza compilador del sistema |

---

## 7) Campos declarativos para componentes del sistema

### 7.1 Video (YouTube)
Se agregan campos:

- `TieneVideo` (bool): indica si la lección tiene video.
- `VideoId` (string): **solo el ID del video de YouTube**, no la URL.

Ejemplo válido de `VideoId`:
- `dQw4w9WgXcQ`

Ejemplo inválido:
- `https://www.youtube.com/watch?v=dQw4w9WgXcQ`

#### Reglas
- Si `OrdenSecciones` contiene `"video"`, entonces:
  - `TieneVideo` debe ser `true`
  - `VideoId` debe ser válido (ID puro)
- El autor **no escribe HTML** para el video.

---

### 7.2 Actividad externa
La actividad externa se declara por datos, no por HTML.

#### Reglas
- Si `OrdenSecciones` contiene `"actividad"`, entonces:
  - debe existir la marca de “tiene actividad”
  - debe existir la **URL** de la actividad
- El autor **no escribe `<iframe>`** ni HTML de actividad; el sistema lo genera.
- Considere usar para videos de youtube el componente **video** y para actividades externas, como por ejemplo actividades de educaplay, utilice **actividad**. 
---

### 7.3 Compilador
- Si `OrdenSecciones` contiene `"compilador"`, el sistema renderiza el componente oficial del compilador.
- El autor no define HTML.
- El compilador tiene las siguientes secciones:
- area de edicion de codigo
- entradas stdin
- salida del codigo
---

## 8) HTML controlado (reglas de marcado)

Las secciones que aceptan HTML (`teoria`, `ejemplo`, `practica`, `charpTip`, `charpDialog`) deben usar **lenguaje de marcado html y clases css de los estilos oficiales de ACC**.
>- Es importante recalcar que charpTip y charpDialog no pueden llevar ningun wrapper, solo html muy simple, como decoradores de texto como strong o clases decoradoras como .alert o .highlight.

### Permitido (ejemplos típicos)
- `p`, `h3`
- `ul`, `ol`, `li`
- `strong`, `em`
- `code`, `pre`
- `table`, `thead`, `tbody`, `tr`, `th`, `td`
- `a`, `img`, `hr`

### Prohibido
- `script`
- estilos inline (`style=""`)
- iframes manuales
- JS embebido o comportamiento dinámico incrustado

---

## 9) Ejemplos estructurales (conceptuales)

### 9.1 Lección mínima (sin video, sin actividad)
Orden:

```json
["charpDialog", "teoria", "practica"]
```

Campos declarativos:
- `NivelBloom = "Comprender"`
- `TieneVideo = false`

Contenido (HTML) solo en:
- `charpDialog`
- `teoria`
- `practica`

---

### 9.2 Lección con video y actividad externa
Orden:

```json
["charpDialog", "video", "teoria", "actividad", "practica"]
```

Campos declarativos:
- `TieneVideo = true`
- `VideoId = "dQw4w9WgXcQ"`
- `TieneActividad = true`
- `UrlActividad = "https://..."`

---

### Estado de este documento
Este archivo define el **contrato técnico** para que cualquier persona pueda crear lecciones válidas en ACC sin depender de conocimiento interno del renderizador.
