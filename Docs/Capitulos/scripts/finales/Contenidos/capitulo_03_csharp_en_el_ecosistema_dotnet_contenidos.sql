-- Insercion de contenidos del capitulo de biblioteca (propuesta final, no ejecutada automaticamente)
-- Capitulo objetivo: C# en el ecosistema .NET

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @CapituloTitulo NVARCHAR(100) = N'C# en el ecosistema .NET';
    DECLARE @CapituloId INT;

    SELECT TOP (1) @CapituloId = c.IdCapitulo
    FROM acc_academic.Capitulos c
    WHERE c.TituloCapitulo = @CapituloTitulo;

    IF @CapituloId IS NULL
        THROW 56601, 'No existe el capitulo "C# en el ecosistema .NET" en acc_academic.Capitulos.', 1;

    DECLARE @TipoDocumentacion INT = 1;
    DECLARE @TipoErroresComunes INT = 8;
    DECLARE @TipoReferencias INT = 12;
    DECLARE @TipoFAQ INT = 13;

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
              N'Diferencias entre C# y .NET',
              N'Que compone el ecosistema .NET',
              N'Donde se usa C# dentro del ecosistema',
              N'Confusiones comunes: C#, .NET, ASP.NET y Framework'
          )
    )
        THROW 56602, 'Ya existe al menos uno de los contenidos previstos para el capitulo "C# en el ecosistema .NET".', 1;

    DECLARE @Html_01 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Diferencias entre C# y .NET</h1>
        <p>Una de las confusiones mas frecuentes al empezar es tratar C# y .NET como si fueran exactamente lo mismo. Estan estrechamente relacionados, pero no ocupan el mismo nivel dentro del ecosistema tecnico.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Preguntas que resuelve este contenido</strong></p>
        <ul>
            <li>que es C#</li>
            <li>que es .NET</li>
            <li>como se relacionan sin ser equivalentes</li>
            <li>por que esta diferencia importa al estudiar y al trabajar</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Diferencia principal</h2>
        <p><strong>C#</strong> es un lenguaje de programacion. <strong>.NET</strong> es una plataforma y un ecosistema de ejecucion, bibliotecas y herramientas donde ese lenguaje puede usarse junto con otros componentes.</p>
        <p>Esto significa que C# define como escribes una parte importante del programa, mientras que .NET aporta el entorno tecnico que permite compilar, ejecutar y ampliar esa solucion.</p>
    </section>

    <section class="capitulo-section">
        <h2>Relacion entre ambos</h2>
        <p>No son conceptos aislados. C# vive y se desarrolla con gran cercania a .NET. Sin embargo, esa cercania no elimina la diferencia de rol:</p>
        <ul>
            <li>el lenguaje expresa la logica y la estructura</li>
            <li>la plataforma aporta runtime, bibliotecas y herramientas</li>
            <li>el desarrollo real combina ambos niveles</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Comparacion estructurada</h2>
        <table>
            <thead>
                <tr>
                    <th>Elemento</th>
                    <th>Funcion</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>C#</td>
                    <td>lenguaje para escribir codigo</td>
                </tr>
                <tr>
                    <td>.NET</td>
                    <td>plataforma para ejecutar, soportar y ampliar aplicaciones</td>
                </tr>
                <tr>
                    <td>Relacion</td>
                    <td>C# se apoya en .NET para desarrollarse de forma practica</td>
                </tr>
            </tbody>
        </table>
    </section>

    <section class="capitulo-section">
        <h2>Modelo conceptual</h2>
        <blockquote>
            Si C# es el idioma con el que escribes una parte esencial del software, .NET es el entorno tecnico que permite que ese idioma se convierta en una aplicacion funcional.
        </blockquote>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <ul>
            <li>decir "programo en C#" enfatiza el lenguaje</li>
            <li>decir "trabajo en .NET" enfatiza la plataforma y su ecosistema</li>
            <li>ninguna de las dos frases es falsa, pero no apuntan exactamente al mismo objeto tecnico</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido aclara la diferencia base entre lenguaje y plataforma. No entra todavia a versionado historico, familias de frameworks ni detalles de instalacion o tooling especifico.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>usar C# y .NET como sinonimos absolutos</li>
            <li>pensar que aprender el lenguaje equivale a dominar todo el ecosistema</li>
            <li>creer que la plataforma reemplaza la necesidad de comprender el lenguaje</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>C# y .NET trabajan juntos, pero cumplen papeles diferentes. Distinguir lenguaje y plataforma es indispensable para entender con precision el ecosistema tecnico donde se desarrolla el software.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicacion conceptual</h2>
        <p>Este contenido ayuda a interpretar mejor documentacion, conversaciones tecnicas y decisiones de aprendizaje sin mezclar niveles distintos del ecosistema.</p>
    </section>
</article>';

    DECLARE @Html_02 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Que compone el ecosistema .NET</h1>
        <p>Hablar de .NET de forma precisa requiere ver sus piezas principales. La plataforma no es un solo elemento aislado, sino un conjunto de componentes que permiten desarrollar, compilar, ejecutar y sostener aplicaciones en distintos escenarios.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Piezas principales</strong></p>
        <ul>
            <li>runtime</li>
            <li>SDK</li>
            <li>bibliotecas base</li>
            <li>frameworks y modelos de aplicacion</li>
            <li>herramientas de desarrollo</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Runtime</h2>
        <p>El runtime es la parte que permite que una aplicacion se ejecute. Gestiona aspectos clave del comportamiento del programa y ofrece soporte para que el codigo compilado pueda funcionar dentro de un entorno controlado.</p>
    </section>

    <section class="capitulo-section">
        <h2>SDK</h2>
        <p>El SDK agrupa herramientas necesarias para desarrollar: compilar, crear proyectos, restaurar dependencias y trabajar con la linea de comandos. Es decir, no esta orientado solo a ejecutar, sino a construir.</p>
    </section>

    <section class="capitulo-section">
        <h2>Bibliotecas base</h2>
        <p>.NET incorpora un conjunto amplio de bibliotecas reutilizables. Gracias a ellas no hace falta crear desde cero operaciones comunes de colecciones, archivos, cadenas, redes, tareas asincronas o acceso a datos basico.</p>
    </section>

    <section class="capitulo-section">
        <h2>Frameworks y modelos de aplicacion</h2>
        <p>Sobre la plataforma se apoyan distintos modelos para construir aplicaciones concretas. Algunos orientan el desarrollo web, otros escritorio, servicios o escenarios de integracion. Esto muestra que .NET no se limita a un solo tipo de producto.</p>
    </section>

    <section class="capitulo-section">
        <h2>Herramientas</h2>
        <p>El ecosistema tambien incluye herramientas de edicion, depuracion, pruebas, compilacion y automatizacion que facilitan el trabajo tecnico. No son el lenguaje mismo, pero forman parte del contexto real donde ese lenguaje se usa.</p>
    </section>

    <section class="capitulo-section">
        <h2>Clasificacion resumida</h2>
        <table>
            <thead>
                <tr>
                    <th>Componente</th>
                    <th>Funcion principal</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Runtime</td>
                    <td>ejecutar aplicaciones</td>
                </tr>
                <tr>
                    <td>SDK</td>
                    <td>desarrollar, compilar y gestionar proyectos</td>
                </tr>
                <tr>
                    <td>Bibliotecas</td>
                    <td>aportar funcionalidad reutilizable</td>
                </tr>
                <tr>
                    <td>Frameworks y modelos</td>
                    <td>resolver tipos concretos de aplicacion</td>
                </tr>
                <tr>
                    <td>Herramientas</td>
                    <td>facilitar el flujo de desarrollo</td>
                </tr>
            </tbody>
        </table>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <p>No conviene llamar ".NET" solo al runtime ni solo al SDK. Ambos pertenecen al ecosistema, pero no agotan su significado. El termino refiere a una plataforma mas amplia.</p>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido ofrece una vista estructural del ecosistema, no un inventario exhaustivo de herramientas o frameworks. Su funcion es ordenar mentalmente las piezas principales.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>El ecosistema .NET esta formado por componentes que cumplen funciones distintas pero complementarias: ejecutar, desarrollar, reutilizar y especializar aplicaciones en escenarios concretos.</p>
    </section>
</article>';

    DECLARE @Html_03 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Donde se usa C# dentro del ecosistema</h1>
        <p>Comprender el ecosistema .NET tambien implica ubicar donde encaja C# de forma practica. El lenguaje no aparece en abstracto: se usa dentro de modelos de aplicacion y contextos concretos que determinan su papel tecnico.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Escenarios representativos</strong></p>
        <ul>
            <li>web</li>
            <li>servicios y APIs</li>
            <li>escritorio</li>
            <li>automatizacion y herramientas internas</li>
            <li>videojuegos e integraciones</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Aplicaciones web</h2>
        <p>En web, C# suele participar en backend, logica de negocio, APIs, validaciones, autenticacion y manejo de datos. En este contexto, el lenguaje se integra con frameworks del ecosistema que permiten exponer funcionalidad a clientes web o sistemas externos.</p>
    </section>

    <section class="capitulo-section">
        <h2>Servicios y APIs</h2>
        <p>Un uso muy comun es construir servicios que reciben datos, aplican reglas y devuelven respuestas a otras aplicaciones. Aqui C# actua como lenguaje de implementacion dentro de procesos tecnicos orientados a integracion y automatizacion.</p>
    </section>

    <section class="capitulo-section">
        <h2>Escritorio</h2>
        <p>Tambien puede emplearse para aplicaciones de escritorio orientadas a gestion, administracion, operacion interna o herramientas especializadas. En estos casos el lenguaje convive con modelos de interfaz y componentes visuales del ecosistema.</p>
    </section>

    <section class="capitulo-section">
        <h2>Otros escenarios</h2>
        <p>C# tambien aparece en automatizaciones, procesamiento interno, tareas programadas, integraciones y ciertos motores de videojuegos. Esto refuerza la idea de que el lenguaje no se limita a una sola salida profesional.</p>
    </section>

    <section class="capitulo-section">
        <h2>Mapa resumido</h2>
        <table>
            <thead>
                <tr>
                    <th>Escenario</th>
                    <th>Papel habitual de C#</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Web</td>
                    <td>backend, APIs, logica y servicios</td>
                </tr>
                <tr>
                    <td>Servicios</td>
                    <td>procesamiento, reglas e integracion</td>
                </tr>
                <tr>
                    <td>Escritorio</td>
                    <td>interfaces y logica de aplicaciones internas</td>
                </tr>
                <tr>
                    <td>Automatizacion</td>
                    <td>utilidades, tareas y procesos tecnicos</td>
                </tr>
                <tr>
                    <td>Juegos</td>
                    <td>logica y comportamiento dentro de motores compatibles</td>
                </tr>
            </tbody>
        </table>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido no detalla frameworks ni patrones de cada escenario. Su funcion es ubicar de forma general donde actua C# dentro del ecosistema y por que esa ubicacion importa para el aprendizaje.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>C# se usa en distintos escenarios del ecosistema .NET porque el lenguaje funciona como medio de implementacion comun para varios modelos de aplicacion. Esa versatilidad explica parte de su valor formativo y profesional.</p>
    </section>
</article>';

    DECLARE @Html_04 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Confusiones comunes: C#, .NET, ASP.NET y Framework</h1>
        <p>La precision terminologica es indispensable en este dominio. Muchas dudas de principiante nacen porque varios nombres del ecosistema aparecen juntos y parecen apuntar a la misma cosa cuando en realidad pertenecen a niveles tecnicos distintos.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Confusiones frecuentes</strong></p>
        <ul>
            <li>C# y .NET como sinonimos</li>
            <li>.NET y ASP.NET como equivalentes</li>
            <li>.NET Framework y .NET actual como si fueran la misma pieza sin matices</li>
            <li>framework como palabra generica para cualquier componente del ecosistema</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>C# no es .NET</h2>
        <p>El lenguaje y la plataforma trabajan juntos, pero no son el mismo objeto tecnico. Confundirlos complica la lectura de documentacion y vuelve imprecisas las decisiones de aprendizaje.</p>
    </section>

    <section class="capitulo-section">
        <h2>ASP.NET no equivale a .NET completo</h2>
        <p>ASP.NET forma parte del ecosistema para escenarios web. Por eso no debe tomarse como nombre total de toda la plataforma ni como sinonimo universal de desarrollo en .NET.</p>
    </section>

    <section class="capitulo-section">
        <h2>.NET Framework y .NET actual</h2>
        <p>Historicamente han existido nombres cercanos que generan confusiones. Para una comprension inicial conviene distinguir entre familias historicas y la plataforma moderna, sin asumir que todo lo que lleve ".NET" se refiere exactamente a la misma linea tecnologica.</p>
    </section>

    <section class="capitulo-section">
        <h2>Uso impreciso de la palabra framework</h2>
        <p>En conversaciones informales a veces se llama framework a cualquier herramienta, libreria o plataforma. Esa flexibilidad coloquial no siempre es util en estudio tecnico. Conviene nombrar cada pieza segun su funcion real.</p>
    </section>

    <section class="capitulo-section">
        <h2>FAQ breve</h2>
        <h3>Si aprendo C#, ya aprendi .NET?</h3>
        <p>No. Aprendes una parte esencial del trabajo, pero no agotas la plataforma ni sus modelos de aplicacion.</p>

        <h3>Si trabajo con ASP.NET, trabajo con C#?</h3>
        <p>Con frecuencia si, pero la frase enfatiza el marco web del ecosistema, no solo el lenguaje.</p>

        <h3>Por que importa esta precision?</h3>
        <p>Porque mejora la comprension del ecosistema y evita mezclar componentes que cumplen funciones distintas.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>Gran parte de la confusion inicial en este dominio surge por nombres cercanos pero no equivalentes. La claridad terminologica mejora la forma en que se estudia, se pregunta y se documenta el trabajo tecnico.</p>
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
        @TipoFAQ,
        N'Diferencias entre C# y .NET',
        N'Lenguaje y plataforma en niveles tecnicos distintos',
        N'Aclara la diferencia entre C# y .NET para separar con precision el lenguaje de programacion de la plataforma donde se ejecuta y se desarrolla software.',
        N'10-12 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-question-circle',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_01
    ),
    (
        @TipoDocumentacion,
        N'Que compone el ecosistema .NET',
        N'Runtime, SDK, bibliotecas, frameworks y herramientas',
        N'Presenta las piezas principales del ecosistema .NET para entender como se organiza la plataforma y que funcion cumple cada componente en el desarrollo real.',
        N'12-15 min',
        NULL,
        @NivelIntermedio,
        N'fas fa-cubes',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_02
    ),
    (
        @TipoReferencias,
        N'Donde se usa C# dentro del ecosistema',
        N'Web, servicios, escritorio, automatizacion y otros contextos',
        N'Ubica los principales escenarios donde C# se utiliza dentro del ecosistema .NET para conectar el lenguaje con contextos concretos de uso profesional.',
        N'10-12 min',
        NULL,
        @NivelGeneral,
        N'fas fa-project-diagram',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_03
    ),
    (
        @TipoErroresComunes,
        N'Confusiones comunes: C#, .NET, ASP.NET y Framework',
        N'Errores de nomenclatura y de nivel conceptual al iniciar',
        N'Reune las confusiones mas frecuentes del ecosistema para corregir mezclas terminologicas comunes entre lenguaje, plataforma, frameworks y familias tecnologicas.',
        N'9-11 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-exclamation-triangle',
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
