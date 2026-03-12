# Guía de Lecciones (ACC) - Inserción SQL
**Aprendiendo C# con Charp (ACC)**

Este documento explica cómo **insertar y mantener** registros de lecciones en **SQL Server** de forma **coherente con el contrato de ACC** (renderizador + enums + flags).

> Importante  
> - Aquí mandan las **reglas lógicas de ACC**, no si una columna está `NULL/NOT NULL`.  
> - La BD es almacenamiento; el contrato de ACC es la verdad del negocio.

---

## 1) Propósito y alcance

Objetivo:
- Insertar lecciones nuevas y actualizar existentes sin romper el render.
- Mantener consistencia entre:
  - `OrdenSecciones` (flujo),
  - flags (`TieneActividad`, `TieneCompilador`),
  - contenido (`Teoria`, `Practica`, etc.),
  - `NivelBloom` (enum lógico).

No cubre:
- Migraciones / EF / UI / CSS.
- Diseño pedagógico (eso vive en la guía pedagógica).

---

## 2) Tabla objetivo y campos reales
Esquema: `ACC_Academic`
Tabla: `Lecciones`
campos:

- `IdLeccion` (int)
- `TituloLeccion` (nvarchar(100))
- `DescripcionLeccion` (nvarchar(500))
- `TieneActividad` (bit)
- `UrlActividad` (nvarchar(max))
- `TieneCompilador` (bit)
- `OrdenSecciones` (nvarchar(max))  <- JSON
- `SubtemaId` (int)
- `Teoria` (nvarchar(max))          <- HTML
- `Practica` (nvarchar(max))        <- HTML
- `Ejemplo` (nvarchar(max))         <- HTML
- `CharpTip` (nvarchar(500))        <- HTML (tip conciso)
- `CharpDialog` (nvarchar(max))     <- HTML (mensaje largo/puente)
- `NivelBloom` (nvarchar(20))       <- string de enum lógico
- `TieneVideo` (bit)
- `VideoId`(nvarchar(20))
---

## 3) Contrato de secciones (`OrdenSecciones`)

`OrdenSecciones` define el **flujo de renderizado** de la lección.

Formato:
- Debe ser un **JSON array de strings**.
- Debe ser JSON válido.
- **No se repiten** secciones.
- **Solo** se permiten valores del enum `SeccionesContenido`:

Secciones válidas:
- `video`
- `teoria`
- `practica`
- `ejemplo`
- `actividad`
- `compilador`
- `charpTip`
- `charpDialog`

Ejemplos válidos:
```json
["charpDialog","teoria","ejemplo","practica"]
```

Ejemplos inválidos:
```json
["teoria","teoria"]
```

```json
["Video"]
```

```json
["fomentador"]
```

> Nota  
> El autor **no escribe** HTML para el Bloom ni para la actividad externa o videos de youtube: el renderizador lo maneja según `NivelBloom`, `TieneActividad`, `UrlActividad` y `TieneVideo`, `VideoId`.  

## Nota clave: secciones lógicas vs delimitadores HTML

- `OrdenSecciones` define el **flujo del renderizador** (qué secciones existen y en qué orden).
- Las columnas (`Teoria`, `Practica`, `Ejemplo`, etc.) guardan **HTML interno**.

Importante:
El autor **NO** escribe el contenedor global del layout (`.leccion-container`)  
pero **SÍ** escribe el delimitador raíz de cada sección de contenido en su HTML, por ejemplo:

- `Teoria` -> `<div class="leccion-teoria">...</div>`
- `Ejemplo` -> `<div class="leccion-ejemplos">...</div>`
- `Practica` -> `<div class="leccion-practicas">...</div>`

Estos wrappers son **delimitadores semánticos**, no layout global.
---

## 4) Reglas cruzadas: secciones ↔ flags ↔ contenido

Estas reglas aseguran coherencia del registro.

### 4.1 Actividad (`actividad`)
Si `OrdenSecciones` contiene `"actividad"`:
- `TieneActividad = 1`
- `UrlActividad` debe contener una URL real (valor útil, no vacío).

Si **NO** contiene `"actividad"`:
- `TieneActividad = 0`
- `UrlActividad` debe quedar vacío/limpio (no amarrado a `NULL` vs `''`, pero debe quedar “sin dato útil”).

### 4.2 Compilador (`compilador`)
Si `OrdenSecciones` contiene `"compilador"`:
- `TieneCompilador = 1`

Si **NO** contiene `"compilador"`:
- `TieneCompilador = 0`

### 4.3 Secciones de contenido (HTML)
Si la sección existe en `OrdenSecciones`, el campo correspondiente debe traer contenido HTML útil:

- `"teoria"` -> `Teoria`
- `"practica"` -> `Practica`
- `"ejemplo"` -> `Ejemplo`
- `"charpTip"` -> `CharpTip`
- `"charpDialog"` -> `CharpDialog`

> Regla de HTML  
> Estos campos aceptan **solo marcado** (sin scripts). Evita `script`, `onClick`, etc.

### 4.4 Video (`video`)
Se contempla `video`.
- `TieneVideo` (bit)
- `VideoId` (nvarchar)

Entonces aplica:

Si `OrdenSecciones` contiene `"video"`:
- `TieneVideo = 1`
- `VideoId` debe ser **solo el ID de YouTube**, no la URL.

Si **NO** contiene `"video"`:
- `TieneVideo = 0`
- `VideoId` limpio.

---

## 5) `NivelBloom` (regla ACC)

`NivelBloom` es un string que debe coincidir con el enum lógico:

Valores válidos:
- `Recordar`
- `Comprender`
- `Aplicar`
- `Analizar`
- `Evaluar`
- `Crear`

Ejemplo:
```text
NivelBloom = 'Comprender'
```

---
## 6) Plantillas de UPDATE (mantenimiento seguro)

### 6.1 Cambiar el orden de secciones
```sql
UPDATE ACC_Academic.Lecciones
SET OrdenSecciones = N'["charpDialog","teoria","ejemplo","practica"]'
WHERE IdLeccion = 12;
```

### 6.2 Activar actividad (sincronía JSON ↔ flag ↔ URL)
```sql
UPDATE ACC_Academic.Lecciones
SET
    TieneActividad = 1,
    UrlActividad   = N'https://tusitio.com/actividad/nueva',
    OrdenSecciones = N'["teoria","actividad"]'
WHERE IdLeccion = 12;
```

### 6.3 Desactivar actividad (limpieza lógica)
```sql
UPDATE ACC_Academic.Lecciones
SET
    TieneActividad = 0,
    UrlActividad   = N'',
    OrdenSecciones = N'["teoria","ejemplo","practica"]'
WHERE IdLeccion = 12;
```

### 6.4 Activar compilador
```sql
UPDATE ACC_Academic.Lecciones
SET
    TieneCompilador = 1,
    OrdenSecciones  = N'["teoria","ejemplo","compilador","practica"]'
WHERE IdLeccion = 12;
```

### 6.5 Desactivar compilador
```sql
UPDATE ACC_Academic.Lecciones
SET
    TieneCompilador = 0,
    OrdenSecciones  = N'["teoria","ejemplo","practica"]'
WHERE IdLeccion = 12;
```

### 6.6 Corregir Bloom
```sql
UPDATE ACC_Academic.Lecciones
SET NivelBloom = N'Analizar'
WHERE IdLeccion = 12;
```

---

## 7) Validación post-inserción (sanity checks)

### 7.1 Bloom inválido o vacío
```sql
SELECT IdLeccion, TituloLeccion, NivelBloom
FROM ACC_Academic.Lecciones
WHERE NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
   OR NivelBloom IS NULL
   OR LTRIM(RTRIM(NivelBloom)) = N'';
```

### 7.2 Actividad incoherente: aparece en JSON pero flag apagado
```sql
SELECT IdLeccion, TituloLeccion, TieneActividad, OrdenSecciones
FROM ACC_Academic.Lecciones
WHERE TieneActividad = 0
  AND OrdenSecciones LIKE N'%"actividad"%';
```

### 7.3 Actividad incoherente: flag encendido pero no existe en JSON
```sql
SELECT IdLeccion, TituloLeccion, TieneActividad, OrdenSecciones
FROM ACC_Academic.Lecciones
WHERE TieneActividad = 1
  AND OrdenSecciones NOT LIKE N'%"actividad"%';
```

### 7.4 Actividad sin URL útil
```sql
SELECT IdLeccion, TituloLeccion, UrlActividad
FROM ACC_Academic.Lecciones
WHERE TieneActividad = 1
  AND (UrlActividad IS NULL OR LTRIM(RTRIM(UrlActividad)) = N'');
```

### 7.5 Compilador incoherente: aparece en JSON pero flag apagado
```sql
SELECT IdLeccion, TituloLeccion, TieneCompilador, OrdenSecciones
FROM ACC_Academic.Lecciones
WHERE TieneCompilador = 0
  AND OrdenSecciones LIKE N'%"compilador"%';
```

### 7.6 Compilador incoherente: flag encendido pero no existe en JSON
```sql
SELECT IdLeccion, TituloLeccion, TieneCompilador, OrdenSecciones
FROM ACC_Academic.Lecciones
WHERE TieneCompilador = 1
  AND OrdenSecciones NOT LIKE N'%"compilador"%';
```

### 7.7 Secciones desconocidas en JSON (SQL Server 2016+)
> Este check detecta strings dentro del JSON que no pertenecen al enum.
```sql
SELECT L.IdLeccion, L.TituloLeccion, J.[value] AS SeccionEncontrada
FROM ACC_Academic.Lecciones L
CROSS APPLY OPENJSON(L.OrdenSecciones) J
WHERE J.[value] NOT IN
(
    N'video', N'teoria', N'practica', N'ejemplo',
    N'actividad', N'compilador',
    N'charpTip', N'charpDialog'
);
```

---

## 8 Plantilla oficial (ACC_Academic)
INSERT:
```sql
USE [ACC_Academic]
GO

INSERT INTO [ACC_Academic].[Lecciones]
(
  [TituloLeccion],
  [DescripcionLeccion],
  [TieneActividad],
  [UrlActividad],
  [TieneCompilador],
  [OrdenSecciones],
  [SubtemaId],
  [Teoria],
  [Practica],
  [Ejemplo],
  [CharpTip],
  [CharpDialog],
  [NivelBloom],
  [VideoId],
  [TieneVideo]
)
VALUES
(
  <...>
)
GO
```

---

UPDATE:
```sql
USE [ACC_Academic]
GO

UPDATE [ACC_Academic].[Lecciones]
SET
  [TituloLeccion] = <...>,
  [DescripcionLeccion] = <...>,
  [TieneActividad] = <...>,
  [UrlActividad] = <...>,
  [TieneCompilador] = <...>,
  [OrdenSecciones] = <...>,
  [SubtemaId] = <...>,
  [Teoria] = <...>,
  [Practica] = <...>,
  [Ejemplo] = <...>,
  [CharpTip] = <...>,
  [CharpDialog] = <...>,
  [NivelBloom] = <...>,
  [VideoId] = <...>,
  [TieneVideo] = <...>
WHERE <condición>
GO
```

## EJEMPLOS REALES DE INSERTS:
Ejemplo 1:
```sql
INSERT INTO [ACC_Academic].[Lecciones]
(
    [IdLeccion],
    [TituloLeccion],
    [DescripcionLeccion],
    [TieneActividad],
    [UrlActividad],
    [TieneCompilador],
    [OrdenSecciones],
    [SubtemaId],
    [Teoria],
    [Practica],
    [Ejemplo],
    [CharpTip],
    [CharpDialog],
    [NivelBloom],
    [VideoId],
    [TieneVideo]
)
VALUES
(
    6,

    N'Ventajas de un buen diseño vs consecuencias de uno malo',

    N'Compara los efectos reales de un buen diseño frente a los problemas que aparecen cuando el diseño es deficiente.',

    1,

    N'https://view.genially.com/6974589aad9cbd35338abe05/interactive-content-verdadero-o-falso-diseno-de-software',

    0,

    N'["charpDialog","teoria","ejemplo","practica","actividad"]',

    5,

    N'
<div class="leccion-teoria">
    <h3>El diseño sí tiene consecuencias reales</h3>

    <p>Un buen diseño no se nota cuando todo va bien. Se nota cuando el sistema crece, cambia o se pone bajo presión.</p>

    <div class="alert alert-info">
        <p class="alert-title">Idea central</p>
        <p>El diseño es como la <strong>cimentación</strong>: si está mal desde el inicio, todo lo que construyas encima corre riesgo.</p>
    </div>

    <p>En software, al igual que en una construcción, los errores de diseño no siempre aparecen de inmediato,
    pero <strong>se acumulan</strong>.</p>

    <div class="alert alert-warning">
        <p class="alert-title">Advertencia</p>
        <p>Un diseño deficiente puede sostener un sistema pequeño, pero empieza a fallar cuando el sistema crece.</p>
    </div>
</div>
',

    N'
<div class="leccion-practicas">
    <h3>Actividad: buenas y malas prácticas de software</h3>

    <p>
        En software, muchas veces el problema no aparece al inicio, sino cuando el sistema crece:
        llegan más cambios, más módulos, más requisitos… y si la base está mal, todo empieza a crujir.
    </p>

    <p>
        Esta actividad te ayudará a reconocer señales típicas de <strong>malas prácticas</strong>
        (fragilidad, desorden, dificultad para mantener) y también de <strong>buenas prácticas</strong>
        (claridad, estabilidad, facilidad para evolucionar).
    </p>

    <ul>
        <li>¿Los cambios pequeños rompen cosas que no deberían?</li>
        <li>¿Cada mejora cuesta más esfuerzo que la anterior?</li>
        <li>¿El sistema se siente “pesado” de modificar aunque sea algo simple?</li>
    </ul>

    <div class="alert alert-info">
        <p class="alert-title">Pista</p>
        <p>
            Cuando un sistema se vuelve frágil con el tiempo, normalmente no es por el lenguaje,
            es por decisiones de diseño y hábitos de desarrollo que fueron acumulando problemas.
        </p>
    </div>

    <p>Haz clic en el botón a continuación para hacer la actividad.</p>
</div>
',

    N'
<div class="leccion-ejemplos">
    <h3>Ejemplo: una casa mal cimentada</h3>

    <p>Imagina una casa construida sobre un terreno inestable.</p>

    <div class="alert alert-error">
        <p class="alert-title">Mala cimentación</p>
        <p>
            La casa parece estable al inicio, pero al agregar un segundo piso comienzan las grietas.
            Con el tiempo, la estructura se ladea o colapsa.
        </p>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Buena cimentación</p>
        <p>
            La casa fue pensada desde el inicio para soportar más peso.
            Al agregar otro piso, la estructura se mantiene firme.
        </p>
    </div>

    <p>
        En software ocurre lo mismo: un mal diseño puede funcionar al principio,
        pero al agregar nuevas funciones, el sistema se vuelve frágil.
    </p>
</div>
',

    NULL,

    N'
<p>No todos los errores vienen del código.</p>
<p>Muchos vienen de haber construido sobre una base que no podía soportar el crecimiento.</p>
',

    N'Comprender',

    NULL,

    0
);
```

Ejemplo 2:
```sql
INSERT INTO [ACC_Academic].[Lecciones]
(
    [IdLeccion],
    [TituloLeccion],
    [DescripcionLeccion],
    [TieneActividad],
    [UrlActividad],
    [TieneCompilador],
    [OrdenSecciones],
    [SubtemaId],
    [Teoria],
    [Practica],
    [Ejemplo],
    [CharpTip],
    [CharpDialog],
    [NivelBloom],
    [VideoId],
    [TieneVideo]
)
VALUES
(
    11,

    N'Primer vistazo: programa "Hola Mundo"',

    N'Ejecuta tu primer programa en C#, entiende qué significa ejecutar, qué es la salida (output) y cómo modificar código para experimentar.',

    0,

    N'',

    1,

    N'["charpDialog","teoria","ejemplo","compilador","practica","charpTip"]',

    1005,

    N'
<div class="leccion-teoria">
    <h3>Tu primer programa: ejecuta y observa</h3>

    <p>Este es el momento clásico: ver un programa correr por primera vez. No es magia… pero sí se siente como magia la primera vez.</p>

    <div class="alert alert-info">
        <p class="alert-title">Antes de tocar nada</p>
        <p>En el compilador ya hay un código por defecto. Tu primer objetivo es <strong>ejecutarlo</strong> y ver la <strong>salida</strong>.</p>
    </div>

    <hr />

    <p>Dos conceptos nuevos:</p>
    <ul>
        <li><strong>Ejecutar:</strong> correr el programa para que haga lo que le pediste.</li>
        <li><strong>Salida (output):</strong> lo que el programa muestra hacia afuera (por ejemplo, texto en pantalla).</li>
    </ul>

    <div class="alert alert-warning">
        <p class="alert-title">Regla simple</p>
        <p>Si cambias el texto dentro de <code>Console.WriteLine(...)</code>, cambiará lo que se muestra en la salida.</p>
    </div>

    <div class="fomentador">
        <p><strong>Consejo:</strong> no intentes memorizar todo hoy. Solo entiende el flujo:
        <strong>ver código → ejecutar → ver salida → cambiar → repetir</strong>.</p>
    </div>
</div>
',

    N'
<div class="leccion-ejemplos">
    <h3>El código por defecto (léelo con calma)</h3>

    <p>Este es el código que ya viene listo en el compilador:</p>

    <pre><code>
using System;
class Program {
    static void Main(string[] args) {
        Console.WriteLine("Hola desde ACC!");
    }
}
    </code></pre>

    <div class="alert alert-info">
        <p class="alert-title">¿Qué significa cada parte?</p>
        <ul>
            <li><code>using System;</code> permite usar herramientas básicas como <code>Console</code>.</li>
            <li><code>class Program</code> es un contenedor para tu programa (por ahora, piénsalo como “la caja”).</li>
            <li><code>Main</code> es el punto donde empieza la ejecución.</li>
            <li><code>Console.WriteLine(...)</code> imprime texto en pantalla: eso es <strong>output</strong>.</li>
        </ul>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Objetivo</p>
        <p>Hoy no necesitas dominarlo todo. Solo identifica
        <strong>dónde empieza</strong> el programa (<code>Main</code>) y
        <strong>dónde se imprime</strong> la salida (<code>WriteLine</code>).</p>
    </div>
</div>
',

    N'
<div class="leccion-practicas">
    <h3>Práctica: experimenta (sin miedo)</h3>

    <p>Haz estos cambios <strong>uno por uno</strong>. Ejecuta cada vez y observa la salida.</p>

    <div class="alert alert-info">
        <p class="alert-title">Paso 1</p>
        <p>Ejecuta el código tal como está. Asegúrate de ver el texto <code>Hola desde ACC!</code> en la salida.</p>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Paso 2</p>
        <p>Cambia el mensaje por tu nombre, por ejemplo:
        <code>"Hola, soy Germán"</code>. Ejecuta y observa.</p>
    </div>

    <div class="alert alert-warning">
        <p class="alert-title">Paso 3</p>
        <p>Agrega una segunda línea de salida debajo de la primera, con otro <code>Console.WriteLine(...)</code>.</p>
        <p>Ejemplo:</p>
        <pre><code>
Console.WriteLine("Linea 1");
Console.WriteLine("Linea 2");
        </code></pre>
    </div>

    <div class="alert alert-info">
        <p class="alert-title">Paso 4 (primer vistazo a input/output)</p>
        <p>El <strong>input</strong> es información que entra al programa. En consola, puede venir de <code>Console.ReadLine()</code>.</p>
        <p>Prueba esto: pide un nombre y luego muéstralo.</p>
        <pre><code>
Console.WriteLine("¿Cómo te llamas?");
string nombre = Console.ReadLine();
Console.WriteLine("Hola, " + nombre);
        </code></pre>
    </div>

    <div class="alert alert-success">
        <p class="alert-title">Si algo falla</p>
        <p>No pasa nada. Lee el error, revisa comillas <code>"</code>,
        paréntesis <code>( )</code> y punto y coma <code>;</code>.
        La práctica es detectar y corregir.</p>
    </div>
</div>
',

    N'<p>Tip: programa como científico: cambia <strong>una cosa</strong>, ejecuta, observa el resultado. Así aprendes más rápido que “tocando todo”.</p>',

    N'
<p>Hoy no vienes a memorizar C#. Vienes a verlo vivo.</p>
<p>Ejecuta el programa, observa la salida y empieza a experimentar: así se aprende de verdad.</p>
',

    N'Comprender',

    NULL,

    0
);
```

## 9) Regla de oro

> Si cambias `OrdenSecciones`, revisa y sincroniza flags y contenido.  
> El flujo manda: la lección se define por lo que **se renderiza**.

Con esta guía, la inserción SQL queda repetible, auditable y consistente con ACC.
