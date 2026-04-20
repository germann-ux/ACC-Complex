-- Insercion de contenidos del capitulo de biblioteca (propuesta final, no ejecutada automaticamente)
-- Capitulo objetivo: Fundamentos de C#

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
        THROW 56501, 'No existe el capitulo "Fundamentos de C#" en acc_academic.Capitulos.', 1;

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
              N'Que es C# y por que aprenderlo',
              N'Caracteristicas principales del lenguaje',
              N'Primer vistazo a un programa en C#',
              N'Glosario inicial de C#'
          )
    )
        THROW 56502, 'Ya existe al menos uno de los contenidos previstos para el capitulo "Fundamentos de C#".', 1;

    DECLARE @Html_01 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Que es C# y por que aprenderlo</h1>
        <p>C# es un lenguaje de programacion pensado para construir software con una sintaxis clara, un sistema de tipos fuerte y una integracion estrecha con el ecosistema .NET. Aprenderlo no significa solo memorizar palabras reservadas, sino adquirir una herramienta real para modelar problemas y convertirlos en soluciones ejecutables.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>En este contenido</strong></p>
        <ul>
            <li>que lugar ocupa C# dentro del desarrollo</li>
            <li>por que se considera un lenguaje profesional y versatil</li>
            <li>en que escenarios suele utilizarse</li>
            <li>que confusiones conviene evitar desde el inicio</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Delimitacion del lenguaje</h2>
        <p>C# es un lenguaje, no una aplicacion, no un framework y no una metodologia. Su trabajo es permitirte expresar logica, estructura y comportamiento de forma que luego puedan compilarse y ejecutarse dentro de un entorno tecnico concreto.</p>
        <p>Eso significa que cuando estudias C# estas aprendiendo una forma de construir programas, no solo una serie de comandos. Aprendes como representar datos, como tomar decisiones, como organizar codigo y como expresar relaciones entre partes del sistema.</p>
    </section>

    <section class="capitulo-section">
        <h2>Por que aprenderlo</h2>
        <p>C# resulta valioso para una ruta formativa porque combina exigencia tecnica con una sintaxis razonablemente legible. No obliga a escribir todo de forma extremadamente abstracta desde el primer momento, pero tampoco simplifica tanto que oculte conceptos importantes.</p>
        <ul>
            <li><strong>Permite construir software real:</strong> web, escritorio, servicios, juegos y mas.</li>
            <li><strong>Ayuda a formar orden tecnico:</strong> el lenguaje empuja a pensar en tipos, estructura y claridad.</li>
            <li><strong>Tiene continuidad profesional:</strong> no se estudia solo como ejercicio academico aislado.</li>
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
                    <td>backend, APIs, servicios y logica de negocio</td>
                </tr>
                <tr>
                    <td>Aplicaciones de escritorio</td>
                    <td>herramientas administrativas, sistemas internos, utilidades</td>
                </tr>
                <tr>
                    <td>Servicios</td>
                    <td>procesamiento, integracion y automatizacion</td>
                </tr>
                <tr>
                    <td>Videojuegos</td>
                    <td>programacion de comportamiento y logica en motores como Unity</td>
                </tr>
            </tbody>
        </table>
        <p>Esta amplitud no significa que un mismo conocimiento basico resuelva cualquier proyecto por si solo, pero si muestra que el lenguaje tiene alcance suficiente para crecer con la formacion del alumno.</p>
    </section>

    <section class="capitulo-section">
        <h2>Modelo conceptual</h2>
        <blockquote>
            C# puede entenderse como una herramienta de expresion tecnica: te permite decirle a un sistema que datos existen, que reglas se aplican y que acciones deben ejecutarse.
        </blockquote>
        <p>La ventaja de partir de esta idea es que evita pensar el lenguaje como una lista de trucos. Lo ubica mejor como un medio para estructurar soluciones de forma verificable.</p>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <ul>
            <li><strong>C#</strong> es el lenguaje.</li>
            <li><strong>.NET</strong> es la plataforma y el ecosistema donde ese lenguaje vive con otras piezas.</li>
            <li><strong>Programar en C#</strong> no significa dominar de inmediato todo el ecosistema .NET.</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido introduce el lenguaje como dominio inicial. No profundiza todavia en orientacion a objetos avanzada, acceso a datos, frameworks web ni herramientas de productividad del ecosistema.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>creer que aprender C# es aprender automaticamente todo .NET</li>
            <li>pensar que un lenguaje solo sirve para un tipo de proyecto</li>
            <li>reducir su estudio a copiar sintaxis sin entender que problema ayuda a resolver</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>C# es un lenguaje moderno y profesional que sirve para construir software en distintos contextos. Su estudio inicial debe orientarse a entender su papel como herramienta de expresion tecnica y no solo como una lista de instrucciones.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicacion conceptual</h2>
        <p>Este contenido ayuda a ubicar por que C# aparece en la ruta de aprendizaje y que tipo de decisiones permite empezar a modelar dentro de un programa.</p>
    </section>
</article>';

    DECLARE @Html_02 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Caracteristicas principales del lenguaje</h1>
        <p>Las caracteristicas de C# no deben memorizarse como etiquetas sueltas. Sirven para entender como se comporta el lenguaje, que exige al programador y por que resulta adecuado para construir sistemas con cierto orden tecnico.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Ejes de lectura</strong></p>
        <ul>
            <li>tipado fuerte</li>
            <li>orientacion a objetos</li>
            <li>sintaxis clara y estructurada</li>
            <li>integracion con el ecosistema .NET</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Tipado fuerte</h2>
        <p>C# trabaja con tipos definidos. Esto significa que los datos no se usan de cualquier forma sin control. Un numero, una cadena o un valor booleano no deberian tratarse como si fueran la misma clase de informacion.</p>
        <p>Esta restriccion no es una molestia arbitraria. Su objetivo es detectar errores de forma mas temprana y hacer mas legible la intencion del codigo.</p>
    </section>

    <section class="capitulo-section">
        <h2>Orientacion a objetos</h2>
        <p>El lenguaje esta preparado para modelar entidades, responsabilidades y relaciones a traves de clases, objetos, propiedades y metodos. Esto no significa que todo programa deba volverse complejo desde el inicio, pero si que el lenguaje ofrece una base estructural para organizar software de forma escalable.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintaxis clara</h2>
        <p>La sintaxis de C# busca ser explicita. Los bloques, las instrucciones y las declaraciones tienen una forma relativamente consistente. Esa claridad ayuda a seguir el flujo del programa y a distinguir mejor donde empieza o termina una parte de la logica.</p>
    </section>

    <section class="capitulo-section">
        <h2>Integracion con .NET</h2>
        <p>C# no vive solo. Su potencia practica aumenta porque se ejecuta y se apoya en un ecosistema amplio. Eso permite usar bibliotecas, herramientas, compilacion, depuracion y distintos modelos de aplicacion dentro de una misma plataforma.</p>
    </section>

    <section class="capitulo-section">
        <h2>Clasificacion resumida</h2>
        <table>
            <thead>
                <tr>
                    <th>Caracteristica</th>
                    <th>Que aporta</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Tipado fuerte</td>
                    <td>control sobre el tipo de informacion y deteccion temprana de errores</td>
                </tr>
                <tr>
                    <td>Orientacion a objetos</td>
                    <td>organizacion de entidades, comportamiento y relaciones</td>
                </tr>
                <tr>
                    <td>Sintaxis clara</td>
                    <td>lectura mas estable del flujo del programa</td>
                </tr>
                <tr>
                    <td>Integracion con .NET</td>
                    <td>acceso a herramientas, bibliotecas y escenarios de uso amplios</td>
                </tr>
            </tbody>
        </table>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <p>Decir que C# es orientado a objetos no significa que solo pueda programarse con clases complejas desde el primer dia. Del mismo modo, decir que tiene tipado fuerte no significa que el lenguaje sea rigido sin utilidad; significa que protege la coherencia del programa.</p>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Estas caracteristicas no sustituyen el aprendizaje practico. Entenderlas ayuda a leer mejor el lenguaje, pero su sentido real se afianza cuando empiezas a escribir codigo y a ver por que ciertas reglas existen.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>repetir que C# es orientado a objetos sin comprender que problema resuelve esa organizacion</li>
            <li>pensar que tipado fuerte significa escribir codigo mas dificil sin beneficio real</li>
            <li>confundir claridad sintactica con simplicidad absoluta del lenguaje</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>Las principales caracteristicas de C# explican por que el lenguaje favorece una escritura clara, controlada y estructurada. No son adornos descriptivos; son rasgos que condicionan como se construye el software.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicacion conceptual</h2>
        <p>Este contenido ayuda a interpretar mejor por que el lenguaje exige ciertas formas de declarar datos, organizar codigo y apoyarse en el ecosistema .NET.</p>
    </section>
</article>';

    DECLARE @Html_03 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Primer vistazo a un programa en C#</h1>
        <p>Antes de estudiar muchos elementos del lenguaje conviene saber como luce un programa sencillo y que hace cada una de sus partes. Este primer vistazo no busca agotar el tema, sino construir una lectura ordenada del codigo inicial.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Elementos a observar</strong></p>
        <ul>
            <li>la estructura general del archivo</li>
            <li>el punto de entrada del programa</li>
            <li>la instruccion que escribe en consola</li>
            <li>el recorrido basico desde compilacion hasta ejecucion</li>
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
        <p>Este ejemplo es pequeno, pero ya contiene varios elementos importantes: una referencia de espacio de nombres, una clase, un metodo principal y una instruccion ejecutable.</p>
    </section>

    <section class="capitulo-section">
        <h2>Anatomia inicial</h2>
        <h3><code>using System;</code></h3>
        <p>Hace disponible un espacio de nombres que contiene utilidades comunes, entre ellas <code>Console</code>.</p>

        <h3><code>class Program</code></h3>
        <p>Declara una clase llamada <code>Program</code>. En esta etapa basta con entender que la clase funciona como una estructura donde se organiza codigo relacionado.</p>

        <h3><code>Main</code></h3>
        <p>Representa el punto de entrada del programa en esta forma clasica. Cuando el programa se ejecuta, esa es la ruta inicial que comienza a correr.</p>

        <h3><code>Console.WriteLine</code></h3>
        <p>Escribe una salida en la consola. En este caso, muestra el mensaje inicial para comprobar que el programa puede ejecutarse correctamente.</p>
    </section>

    <section class="capitulo-section">
        <h2>Flujo minimo de ejecucion</h2>
        <ol>
            <li>el codigo se compila</li>
            <li>el programa inicia en <code>Main</code></li>
            <li>se ejecuta la instruccion <code>Console.WriteLine</code></li>
            <li>la salida aparece en consola</li>
        </ol>
        <p>Este recorrido es sencillo, pero ya permite entender que un programa no es texto suelto: es una secuencia estructurada de instrucciones que se ejecutan con un orden definido.</p>
    </section>

    <section class="capitulo-section">
        <h2>Modelo conceptual</h2>
        <blockquote>
            Un primer programa funciona como una radiografia minima del lenguaje: muestra sus piezas basicas antes de estudiar escenarios mas complejos.
        </blockquote>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <ul>
            <li>un archivo de codigo no es automaticamente un programa ejecutandose</li>
            <li><code>Main</code> no es cualquier metodo; en este formato actua como punto de entrada</li>
            <li><code>Console.WriteLine</code> no compila por si solo fuera del contexto correcto del lenguaje y sus referencias</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este ejemplo muestra una forma clasica y pedagogica de iniciar en C#. Existen formas modernas mas compactas, pero para una base inicial conviene conservar una estructura que haga visibles las partes principales.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>copiar el ejemplo sin entender que hace cada linea</li>
            <li>pensar que imprimir texto equivale ya a comprender como funciona el lenguaje</li>
            <li>creer que todo programa real mantendra siempre esta misma estructura minima</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>El primer programa en C# sirve para identificar estructura, punto de entrada y salida basica. Su valor no esta en la complejidad del resultado, sino en la claridad con la que presenta la forma inicial del lenguaje.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicacion conceptual</h2>
        <p>Este contenido ayuda a leer programas pequenos con menos confusion y a reconocer que cada pieza del archivo cumple una funcion dentro de la ejecucion.</p>
    </section>
</article>';

    DECLARE @Html_04 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Glosario inicial de C#</h1>
        <p>Este glosario no intenta definir todo el lenguaje. Reune los terminos minimos que aparecen al inicio del estudio de C# para sostener una comprension consistente del capitulo.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Uso recomendado</strong></p>
        <p>Consulta este glosario cuando aparezca un termino tecnico nuevo en los contenidos del capitulo y necesites una definicion breve, funcional y alineada al contexto.</p>
    </nav>

    <section class="capitulo-section">
        <h2>Terminos base</h2>
        <h3>Lenguaje de programacion</h3>
        <p>Sistema formal que permite expresar instrucciones, datos y comportamiento de un programa.</p>

        <h3>C#</h3>
        <p>Lenguaje de programacion usado para construir software dentro del ecosistema .NET.</p>

        <h3>.NET</h3>
        <p>Plataforma y ecosistema tecnico donde C# puede compilarse, ejecutarse y apoyarse en bibliotecas y herramientas.</p>

        <h3>Compilar</h3>
        <p>Proceso de traducir el codigo fuente a una forma ejecutable o util para su posterior ejecucion.</p>

        <h3>Consola</h3>
        <p>Entorno de entrada y salida textual donde un programa puede mostrar mensajes y recibir datos.</p>
    </section>

    <section class="capitulo-section">
        <h2>Elementos del programa</h2>
        <h3>Clase</h3>
        <p>Estructura que permite organizar datos y comportamiento dentro del codigo.</p>

        <h3>Metodo</h3>
        <p>Bloque de codigo que realiza una accion o define un comportamiento especifico.</p>

        <h3>Main</h3>
        <p>Metodo que en la forma clasica actua como punto de entrada del programa.</p>

        <h3>Instruccion</h3>
        <p>Unidad concreta de accion que el programa puede ejecutar.</p>

        <h3>Tipo</h3>
        <p>Categoria de dato que determina como puede usarse una informacion en el programa.</p>
    </section>

    <section class="capitulo-section">
        <h2>Terminos de trabajo inicial</h2>
        <h3>Variable</h3>
        <p>Nombre asociado a un espacio donde se guarda un valor que puede cambiar.</p>

        <h3>Sintaxis</h3>
        <p>Conjunto de reglas de escritura del lenguaje.</p>

        <h3>Proyecto</h3>
        <p>Conjunto organizado de archivos, configuraciones y referencias que forman una unidad de desarrollo.</p>

        <h3>Espacio de nombres</h3>
        <p>Agrupacion logica de tipos y componentes, usada para ordenar mejor el codigo.</p>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Las definiciones aqui reunidas son deliberadamente breves. Su funcion es sostener la lectura del capitulo, no reemplazar desarrollos tecnicos mas amplios cuando el alumno avance a temas posteriores.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>Un glosario inicial reduce friccion terminologica. Permite que el estudiante diferencie lenguaje, plataforma, estructura del programa y conceptos operativos sin mezclar niveles de significado.</p>
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
        N'Que es C# y por que aprenderlo',
        N'Lenguaje moderno, profesional y versatil',
        N'Introduce C# como lenguaje de programacion, explica por que aparece en la ruta formativa y aclara en que tipos de software puede utilizarse.',
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
        N'Caracteristicas principales del lenguaje',
        N'Tipado fuerte, orientacion a objetos y claridad estructural',
        N'Desarrolla los rasgos principales de C# para entender como condicionan la escritura del codigo y la forma en que se organiza una solucion.',
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
        N'Estructura minima, punto de entrada y salida en consola',
        N'Explica como leer un primer programa en C#, identificando sus partes principales y el recorrido basico desde el codigo hasta la ejecucion.',
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
        N'Terminos minimos para leer el capitulo con precision',
        N'Reune definiciones breves y funcionales de los terminos tecnicos mas frecuentes al comenzar a estudiar C# dentro de ACC.',
        N'6-8 min',
        NULL,
        @NivelGeneral,
        N'fas fa-book',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_04
    );

    PRINT CONCAT('Se insertaron 4 contenidos para el capitulo IdCapitulo=', @CapituloId, '.');

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO
