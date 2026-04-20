-- Inserción de contenidos del capítulo de biblioteca (propuesta final, no ejecutada automáticamente)
-- Capítulo objetivo: Fundamentos de C#

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @CapituloTitulo NVARCHAR(100) = N'Fundamentos de C#';
    DECLARE @CapituloId INT;

    SELECT TOP (1) @CapituloId = c.IdCapitulo
    FROM acc_academic.Capitulos c
    WHERE c.TituloCapitulo = @CapituloTitulo;

    IF @CapituloId IS NULL
        THROW 56501, 'No existe el capítulo "Fundamentos de C#" en acc_academic.Capitulos.', 1;

    DECLARE @TipoDocumentacion INT = 1;
    DECLARE @TipoConcepto INT = 2;
    DECLARE @TipoEjemplo INT = 5;
    DECLARE @TipoGlosario INT = 14;

    DECLARE @NivelGeneral INT = 1;
    DECLARE @NivelPrincipiante INT = 2;
    DECLARE @NivelIntermedio INT = 3;

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.ContenidoCapitulos
        WHERE CapituloId = @CapituloId
          AND Titulo IN
          (
              N'Qué es C# y por qué aprenderlo',
              N'Características principales del lenguaje',
              N'Primer vistazo a un programa en C#',
              N'Glosario inicial de C#'
          )
    )
        THROW 56502, 'Ya existe al menos uno de los contenidos previstos para el capítulo "Fundamentos de C#".', 1;

    DECLARE @Html_01 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Qué es C# y por qué aprenderlo</h1>
        <p>C# es un lenguaje de programación pensado para construir software con una sintaxis clara, un sistema de tipos fuerte y una integración estrecha con el ecosistema .NET. Aprenderlo no significa solo memorizar palabras reservadas, sino adquirir una herramienta real para modelar problemas y convertirlos en soluciones ejecutables.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>En este contenido</strong></p>
        <ul>
            <li>qué lugar ocupa C# dentro del desarrollo</li>
            <li>por qué se considera un lenguaje profesional y versátil</li>
            <li>en qué escenarios suele utilizarse</li>
            <li>qué confusiones conviene evitar desde el inicio</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Delimitación del lenguaje</h2>
        <p>C# es un lenguaje, no una aplicación, no un framework y no una metodología. Su trabajo es permitirte expresar lógica, estructura y comportamiento de forma que luego puedan compilarse y ejecutarse dentro de un entorno técnico concreto.</p>
        <p>Eso significa que cuando estudias C# estás aprendiendo una forma de construir programas, no solo una serie de comandos. Aprendes cómo representar datos, cómo tomar decisiones, cómo organizar código y cómo expresar relaciones entre partes del sistema.</p>
    </section>

    <section class="capitulo-section">
        <h2>Por qué aprenderlo</h2>
        <p>C# resulta valioso para una ruta formativa porque combina exigencia técnica con una sintaxis razonablemente legible. No obliga a escribir todo de forma extremadamente abstracta desde el primer momento, pero tampoco simplifica tanto que oculte conceptos importantes.</p>
        <ul>
            <li><strong>Permite construir software real:</strong> web, escritorio, servicios, juegos y más.</li>
            <li><strong>Ayuda a formar orden técnico:</strong> el lenguaje empuja a pensar en tipos, estructura y claridad.</li>
            <li><strong>Tiene continuidad profesional:</strong> no se estudia solo como ejercicio académico aislado.</li>
            <li><strong>Se integra bien con .NET:</strong> eso amplifica sus escenarios de uso.</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Escenarios de uso</h2>
        <table>
            <thead>
                <tr>
                    <th>Escenario</th>
                    <th>Uso habitual de C#</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Aplicaciones web</td>
                    <td>backend, APIs, servicios y lógica de negocio</td>
                </tr>
                <tr>
                    <td>Aplicaciones de escritorio</td>
                    <td>herramientas administrativas, sistemas internos, utilidades</td>
                </tr>
                <tr>
                    <td>Servicios</td>
                    <td>procesamiento, integración y automatización</td>
                </tr>
                <tr>
                    <td>Videojuegos</td>
                    <td>programación de comportamiento y lógica en motores como Unity</td>
                </tr>
            </tbody>
        </table>
        <p>Esta amplitud no significa que un mismo conocimiento básico resuelva cualquier proyecto por sí solo, pero sí muestra que el lenguaje tiene alcance suficiente para crecer con la formación del alumno.</p>
    </section>

    <section class="capitulo-section">
        <h2>Modelo conceptual</h2>
        <blockquote>
            C# puede entenderse como una herramienta de expresión técnica: te permite decirle a un sistema qué datos existen, qué reglas se aplican y qué acciones deben ejecutarse.
        </blockquote>
        <p>La ventaja de partir de esta idea es que evita pensar el lenguaje como una lista de trucos. Lo ubica mejor como un medio para estructurar soluciones de forma verificable.</p>
    </section>

    <section class="capitulo-section">
        <h2>Precisión terminológica</h2>
        <ul>
            <li><strong>C#</strong> es el lenguaje.</li>
            <li><strong>.NET</strong> es la plataforma y el ecosistema donde ese lenguaje vive con otras piezas.</li>
            <li><strong>Programar en C#</strong> no significa dominar de inmediato todo el ecosistema .NET.</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y límites</h2>
        <p>Este contenido introduce el lenguaje como dominio inicial. No profundiza todavía en orientación a objetos avanzada, acceso a datos, frameworks web ni herramientas de productividad del ecosistema.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>creer que aprender C# es aprender automáticamente todo .NET</li>
            <li>pensar que un lenguaje solo sirve para un tipo de proyecto</li>
            <li>reducir su estudio a copiar sintaxis sin entender qué problema ayuda a resolver</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Síntesis conceptual</h2>
        <p>C# es un lenguaje moderno y profesional que sirve para construir software en distintos contextos. Su estudio inicial debe orientarse a entender su papel como herramienta de expresión técnica y no solo como una lista de instrucciones.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicación conceptual</h2>
        <p>Este contenido ayuda a ubicar por qué C# aparece en la ruta de aprendizaje y qué tipo de decisiones permite empezar a modelar dentro de un programa.</p>
    </section>
</article>';

    DECLARE @Html_02 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Características principales del lenguaje</h1>
        <p>Las características de C# no deben memorizarse como etiquetas sueltas. Sirven para entender cómo se comporta el lenguaje, qué exige al programador y por qué resulta adecuado para construir sistemas con cierto orden técnico.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Ejes de lectura</strong></p>
        <ul>
            <li>tipado fuerte</li>
            <li>orientación a objetos</li>
            <li>sintaxis clara y estructurada</li>
            <li>integración con el ecosistema .NET</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Tipado fuerte</h2>
        <p>C# trabaja con tipos definidos. Esto significa que los datos no se usan de cualquier forma sin control. Un número, una cadena o un valor booleano no deberían tratarse como si fueran la misma clase de información.</p>
        <p>Esta restricción no es una molestia arbitraria. Su objetivo es detectar errores de forma más temprana y hacer más legible la intención del código.</p>
    </section>

    <section class="capitulo-section">
        <h2>Orientación a objetos</h2>
        <p>El lenguaje está preparado para modelar entidades, responsabilidades y relaciones a través de clases, objetos, propiedades y métodos. Esto no significa que todo programa deba volverse complejo desde el inicio, pero sí que el lenguaje ofrece una base estructural para organizar software de forma escalable.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintaxis clara</h2>
        <p>La sintaxis de C# busca ser explícita. Los bloques, las instrucciones y las declaraciones tienen una forma relativamente consistente. Esa claridad ayuda a seguir el flujo del programa y a distinguir mejor dónde empieza o termina una parte de la lógica.</p>
    </section>

    <section class="capitulo-section">
        <h2>Integración con .NET</h2>
        <p>C# no vive solo. Su potencia práctica aumenta porque se ejecuta y se apoya en un ecosistema amplio. Eso permite usar bibliotecas, herramientas, compilación, depuración y distintos modelos de aplicación dentro de una misma plataforma.</p>
    </section>

    <section class="capitulo-section">
        <h2>Clasificación resumida</h2>
        <table>
            <thead>
                <tr>
                    <th>Característica</th>
                    <th>Qué aporta</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Tipado fuerte</td>
                    <td>control sobre el tipo de información y detección temprana de errores</td>
                </tr>
                <tr>
                    <td>Orientación a objetos</td>
                    <td>organización de entidades, comportamiento y relaciones</td>
                </tr>
                <tr>
                    <td>Sintaxis clara</td>
                    <td>lectura más estable del flujo del programa</td>
                </tr>
                <tr>
                    <td>Integración con .NET</td>
                    <td>acceso a herramientas, bibliotecas y escenarios de uso amplios</td>
                </tr>
            </tbody>
        </table>
    </section>

    <section class="capitulo-section">
        <h2>Precisión terminológica</h2>
        <p>Decir que C# es orientado a objetos no significa que solo pueda programarse con clases complejas desde el primer día. Del mismo modo, decir que tiene tipado fuerte no significa que el lenguaje sea rígido sin utilidad; significa que protege la coherencia del programa.</p>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y límites</h2>
        <p>Estas características no sustituyen el aprendizaje práctico. Entenderlas ayuda a leer mejor el lenguaje, pero su sentido real se afianza cuando empiezas a escribir código y a ver por qué ciertas reglas existen.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>repetir que C# es orientado a objetos sin comprender qué problema resuelve esa organización</li>
            <li>pensar que tipado fuerte significa escribir código más difícil sin beneficio real</li>
            <li>confundir claridad sintáctica con simplicidad absoluta del lenguaje</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Síntesis conceptual</h2>
        <p>Las principales características de C# explican por qué el lenguaje favorece una escritura clara, controlada y estructurada. No son adornos descriptivos; son rasgos que condicionan cómo se construye el software.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicación conceptual</h2>
        <p>Este contenido ayuda a interpretar mejor por qué el lenguaje exige ciertas formas de declarar datos, organizar código y apoyarse en el ecosistema .NET.</p>
    </section>
</article>';

    DECLARE @Html_03 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Primer vistazo a un programa en C#</h1>
        <p>Antes de estudiar muchos elementos del lenguaje conviene saber cómo luce un programa sencillo y qué hace cada una de sus partes. Este primer vistazo no busca agotar el tema, sino construir una lectura ordenada del código inicial.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Elementos a observar</strong></p>
        <ul>
            <li>la estructura general del archivo</li>
            <li>el punto de entrada del programa</li>
            <li>la instrucción que escribe en consola</li>
            <li>el recorrido básico desde compilación hasta ejecución</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Ejemplo base</h2>
        <pre><code>using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hola desde ACC!");
    }
}</code></pre>
        <p>Este ejemplo es pequeño, pero ya contiene varios elementos importantes: una referencia de espacio de nombres, una clase, un método principal y una instrucción ejecutable.</p>
    </section>

    <section class="capitulo-section">
        <h2>Anatomía inicial</h2>
        <h3><code>using System;</code></h3>
        <p>Hace disponible un espacio de nombres que contiene utilidades comunes, entre ellas <code>Console</code>.</p>

        <h3><code>class Program</code></h3>
        <p>Declara una clase llamada <code>Program</code>. En esta etapa basta con entender que la clase funciona como una estructura donde se organiza código relacionado.</p>

        <h3><code>Main</code></h3>
        <p>Representa el punto de entrada del programa en esta forma clásica. Cuando el programa se ejecuta, esa es la ruta inicial que comienza a correr.</p>

        <h3><code>Console.WriteLine</code></h3>
        <p>Escribe una salida en la consola. En este caso, muestra el mensaje inicial para comprobar que el programa puede ejecutarse correctamente.</p>
    </section>

    <section class="capitulo-section">
        <h2>Flujo mínimo de ejecución</h2>
        <ol>
            <li>el código se compila</li>
            <li>el programa inicia en <code>Main</code></li>
            <li>se ejecuta la instrucción <code>Console.WriteLine</code></li>
            <li>la salida aparece en consola</li>
        </ol>
        <p>Este recorrido es sencillo, pero ya permite entender que un programa no es texto suelto: es una secuencia estructurada de instrucciones que se ejecutan con un orden definido.</p>
    </section>

    <section class="capitulo-section">
        <h2>Modelo conceptual</h2>
        <blockquote>
            Un primer programa funciona como una radiografía mínima del lenguaje: muestra sus piezas básicas antes de estudiar escenarios más complejos.
        </blockquote>
    </section>

    <section class="capitulo-section">
        <h2>Precisión terminológica</h2>
        <ul>
            <li>un archivo de código no es automáticamente un programa ejecutándose</li>
            <li><code>Main</code> no es cualquier método; en este formato actúa como punto de entrada</li>
            <li><code>Console.WriteLine</code> no compila por sí solo fuera del contexto correcto del lenguaje y sus referencias</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y límites</h2>
        <p>Este ejemplo muestra una forma clásica y pedagógica de iniciar en C#. Existen formas modernas más compactas, pero para una base inicial conviene conservar una estructura que haga visibles las partes principales.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>copiar el ejemplo sin entender qué hace cada línea</li>
            <li>pensar que imprimir texto equivale ya a comprender cómo funciona el lenguaje</li>
            <li>creer que todo programa real mantendrá siempre esta misma estructura mínima</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Síntesis conceptual</h2>
        <p>El primer programa en C# sirve para identificar estructura, punto de entrada y salida básica. Su valor no está en la complejidad del resultado, sino en la claridad con la que presenta la forma inicial del lenguaje.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicación conceptual</h2>
        <p>Este contenido ayuda a leer programas pequeños con menos confusión y a reconocer que cada pieza del archivo cumple una función dentro de la ejecución.</p>
    </section>
</article>';

    DECLARE @Html_04 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Glosario inicial de C#</h1>
        <p>Este glosario no intenta definir todo el lenguaje. Reúne los términos mínimos que aparecen al inicio del estudio de C# para sostener una comprensión consistente del capítulo.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Uso recomendado</strong></p>
        <p>Consulta este glosario cuando aparezca un término técnico nuevo en los contenidos del capítulo y necesites una definición breve, funcional y alineada al contexto.</p>
    </nav>

    <section class="capitulo-section">
        <h2>Términos base</h2>
        <h3>Lenguaje de programación</h3>
        <p>Sistema formal que permite expresar instrucciones, datos y comportamiento de un programa.</p>

        <h3>C#</h3>
        <p>Lenguaje de programación usado para construir software dentro del ecosistema .NET.</p>

        <h3>.NET</h3>
        <p>Plataforma y ecosistema técnico donde C# puede compilarse, ejecutarse y apoyarse en bibliotecas y herramientas.</p>

        <h3>Compilar</h3>
        <p>Proceso de traducir el código fuente a una forma ejecutable o útil para su posterior ejecución.</p>

        <h3>Consola</h3>
        <p>Entorno de entrada y salida textual donde un programa puede mostrar mensajes y recibir datos.</p>
    </section>

    <section class="capitulo-section">
        <h2>Elementos del programa</h2>
        <h3>Clase</h3>
        <p>Estructura que permite organizar datos y comportamiento dentro del código.</p>

        <h3>Método</h3>
        <p>Bloque de código que realiza una acción o define un comportamiento específico.</p>

        <h3>Main</h3>
        <p>Método que en la forma clásica actúa como punto de entrada del programa.</p>

        <h3>Instrucción</h3>
        <p>Unidad concreta de acción que el programa puede ejecutar.</p>

        <h3>Tipo</h3>
        <p>Categoría de dato que determina cómo puede usarse una información en el programa.</p>
    </section>

    <section class="capitulo-section">
        <h2>Términos de trabajo inicial</h2>
        <h3>Variable</h3>
        <p>Nombre asociado a un espacio donde se guarda un valor que puede cambiar.</p>

        <h3>Sintaxis</h3>
        <p>Conjunto de reglas de escritura del lenguaje.</p>

        <h3>Proyecto</h3>
        <p>Conjunto organizado de archivos, configuraciones y referencias que forman una unidad de desarrollo.</p>

        <h3>Espacio de nombres</h3>
        <p>Agrupación lógica de tipos y componentes, usada para ordenar mejor el código.</p>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y límites</h2>
        <p>Las definiciones aquí reunidas son deliberadamente breves. Su función es sostener la lectura del capítulo, no reemplazar desarrollos técnicos más amplios cuando el alumno avance a temas posteriores.</p>
    </section>

    <section class="capitulo-section">
        <h2>Síntesis conceptual</h2>
        <p>Un glosario inicial reduce fricción terminológica. Permite que el estudiante diferencie lenguaje, plataforma, estructura del programa y conceptos operativos sin mezclar niveles de significado.</p>
    </section>
</article>';

    INSERT INTO acc_academic.ContenidoCapitulos
    (
        Tipo,
        Titulo,
        Subtitulo,
        Descripcion,
        Duracion,
        Dificultad,
        Nivel,
        IconoBadge,
        FechaActualizacion,
        FechaCreacion,
        CapituloId,
        HtmlBody
    )
    VALUES
    (
        @TipoDocumentacion,
        N'Qué es C# y por qué aprenderlo',
        N'Lenguaje moderno, profesional y versátil',
        N'Introduce C# como lenguaje de programación, explica por qué aparece en la ruta formativa y aclara en qué tipos de software puede utilizarse.',
        N'12-14 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-code',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_01
    ),
    (
        @TipoConcepto,
        N'Características principales del lenguaje',
        N'Tipado fuerte, orientación a objetos y claridad estructural',
        N'Desarrolla los rasgos principales de C# para entender cómo condicionan la escritura del código y la forma en que se organiza una solución.',
        N'12-15 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-list-ul',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_02
    ),
    (
        @TipoEjemplo,
        N'Primer vistazo a un programa en C#',
        N'Estructura mínima, punto de entrada y salida en consola',
        N'Explica cómo leer un primer programa en C#, identificando sus partes principales y el recorrido básico desde el código hasta la ejecución.',
        N'10-12 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-terminal',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_03
    ),
    (
        @TipoGlosario,
        N'Glosario inicial de C#',
        N'Términos mínimos para leer el capítulo con precisión',
        N'Reúne definiciones breves y funcionales de los términos técnicos más frecuentes al comenzar a estudiar C# dentro de ACC.',
        N'6-8 min',
        NULL,
        @NivelGeneral,
        N'fas fa-book',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_04
    );

    PRINT CONCAT('Se insertaron 4 contenidos para el capítulo IdCapitulo=', @CapituloId, '.');

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO