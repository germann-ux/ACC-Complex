-- Insercion de contenidos del capitulo de biblioteca (propuesta final, no ejecutada automaticamente)
-- Capitulo objetivo: Evolucion de C#

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @CapituloTitulo NVARCHAR(100) = N'Evolucion de C#';
    DECLARE @CapituloId INT;

    SELECT TOP (1) @CapituloId = c.IdCapitulo
    FROM acc_academic.Capitulos c
    WHERE c.TituloCapitulo = @CapituloTitulo;

    IF @CapituloId IS NULL
        THROW 56701, 'No existe el capitulo "Evolucion de C#" en acc_academic.Capitulos.', 1;

    DECLARE @TipoDocumentacion INT = 1;
    DECLARE @TipoModeloMental INT = 3;
    DECLARE @TipoReferencias INT = 12;

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
              N'Origen y evolucion de C#',
              N'Como cambio C# con el tiempo',
              N'Vigencia de C# en distintos contextos'
          )
    )
        THROW 56702, 'Ya existe al menos uno de los contenidos previstos para el capitulo "Evolucion de C#".', 1;

    DECLARE @Html_01 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Origen y evolucion de C#</h1>
        <p>C# no surgio como una pieza aislada ni quedo detenido en su forma inicial. Nacio dentro de un contexto tecnico concreto, ligado al ecosistema de Microsoft y a la necesidad de contar con un lenguaje moderno que pudiera crecer junto con la plataforma .NET.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Este contenido organiza</strong></p>
        <ul>
            <li>el contexto de aparicion del lenguaje</li>
            <li>su relacion con el ecosistema .NET</li>
            <li>la idea general de su evolucion</li>
            <li>por que esa evolucion importa para entender su lugar actual</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Contexto de origen</h2>
        <p>C# aparece como respuesta a una necesidad de desarrollo mas estructurado, moderno y consistente con una plataforma capaz de sostener aplicaciones de distinto tipo. Su origen no debe entenderse solo como fecha o anecdotario, sino como parte de una decision tecnica y estrategica: construir un lenguaje con sintaxis clara, enfoque profesional y capacidad de crecimiento.</p>
    </section>

    <section class="capitulo-section">
        <h2>Relacion con .NET desde el inicio</h2>
        <p>Desde sus primeras etapas, C# se desarrollo en estrecha relacion con .NET. Eso significa que su historia no puede leerse completamente separada de la evolucion de la plataforma. El lenguaje gana sentido no solo por sus palabras reservadas o su sintaxis, sino por el ecosistema donde se compila, se ejecuta y se expande.</p>
    </section>

    <section class="capitulo-section">
        <h2>Evolucion como proceso tecnico</h2>
        <p>La evolucion de C# no consiste solamente en agregar caracteristicas nuevas. Tambien implica ajustar el lenguaje a nuevas necesidades de expresion, productividad, mantenimiento y claridad. Esto explica por que un lenguaje puede seguir vigente: no permanece igual, sino que se adapta a escenarios cambiantes.</p>
    </section>

    <section class="capitulo-section">
        <h2>Del origen a la consolidacion</h2>
        <p>Con el tiempo, C# dejo de verse solo como una opcion asociada a un entorno especifico y se convirtio en una herramienta madura para escenarios diversos. Esa consolidacion depende tanto de su evolucion interna como del crecimiento de .NET y de la ampliacion de sus usos en web, servicios, escritorio, automatizacion y videojuegos.</p>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <ul>
            <li>la historia de C# no es identica a la historia completa de .NET</li>
            <li>el origen de un lenguaje no explica por si solo su vigencia actual</li>
            <li>evolucion no significa ruptura total con todo lo anterior, sino ajuste progresivo a nuevas demandas</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido no pretende reconstruir una cronologia exhaustiva por versiones. Su objetivo es ofrecer una lectura estructural del origen y la evolucion del lenguaje dentro de un dominio conceptual mas amplio.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>pensar que la evolucion del lenguaje es solo una lista de novedades sintacticas</li>
            <li>confundir la historia de C# con cualquier tecnologia del ecosistema .NET</li>
            <li>creer que un lenguaje antiguo deja de ser util solo por tener trayectoria larga</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>El origen y la evolucion de C# muestran que el lenguaje forma parte de un proceso tecnico mayor. Su valor no depende solo de como nacio, sino de como ha sabido mantenerse alineado con nuevas necesidades del desarrollo de software.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicacion conceptual</h2>
        <p>Este contenido ayuda a entender por que C# sigue siendo una herramienta vigente y por que su historia debe leerse en relacion con la plataforma y los escenarios donde se utiliza.</p>
    </section>
</article>';

    DECLARE @Html_02 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Como cambio C# con el tiempo</h1>
        <p>La evolucion de un lenguaje no se mide solo por cuantos anos tiene, sino por la forma en que responde a nuevas necesidades de expresion, mantenimiento y productividad. Observar como cambio C# permite entender por que no quedo congelado en su forma inicial.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Idea central</strong></p>
        <ul>
            <li>los lenguajes cambian porque cambian los problemas y los contextos</li>
            <li>la evolucion de C# se relaciona con claridad, capacidad expresiva y soporte a nuevos escenarios</li>
            <li>esa evolucion ayuda a explicar su continuidad actual</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>De lenguaje inicial a lenguaje maduro</h2>
        <p>En sus primeros pasos, C# ya ofrecio una base estructurada y profesional. Sin embargo, un lenguaje no permanece util solo por empezar bien. Necesita ampliar sus recursos, mejorar su expresividad y adaptarse a proyectos que cambian en tamano, complejidad y estilo de desarrollo.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sentido de los cambios</h2>
        <p>Los cambios del lenguaje tienen sentido cuando permiten escribir mejor, comprender mejor o resolver problemas con menos friccion. Por eso no conviene mirar la evolucion como una acumulacion arbitraria de caracteristicas, sino como una respuesta a necesidades tecnicas reales.</p>
    </section>

    <section class="capitulo-section">
        <h2>Modelo mental de evolucion</h2>
        <blockquote>
            Un lenguaje madura cuando conserva su identidad tecnica pero mejora su capacidad para expresar soluciones sin volverse innecesariamente torpe o limitado.
        </blockquote>
        <p>Desde esta perspectiva, la evolucion de C# puede entenderse como un proceso de refinamiento: el lenguaje se mantiene reconocible, pero gana recursos para enfrentar nuevos estilos de trabajo y nuevas expectativas del desarrollo moderno.</p>
    </section>

    <section class="capitulo-section">
        <h2>Ejes de cambio</h2>
        <ul>
            <li><strong>expresividad:</strong> escribir ideas complejas con mas claridad</li>
            <li><strong>productividad:</strong> reducir friccion repetitiva en tareas comunes</li>
            <li><strong>integracion:</strong> convivir mejor con el crecimiento del ecosistema</li>
            <li><strong>mantenibilidad:</strong> facilitar codigo mas legible y sostenible</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Relacion con la practica actual</h2>
        <p>Entender que C# cambio con el tiempo ayuda a evitar una vision estatica del lenguaje. Tambien permite interpretar mejor por que cierta documentacion, ejemplos o estilos de codigo pueden variar segun el momento historico o la version usada.</p>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido no enumera detalle por detalle cada version del lenguaje. Su objetivo es ofrecer un modelo mental de evolucion que ayude a comprender por que un lenguaje sigue siendo util cuando sabe transformarse sin perder coherencia.</p>
    </section>

    <section class="capitulo-section">
        <h2>Errores comunes o confusiones frecuentes</h2>
        <ul>
            <li>asumir que un lenguaje estable no debe cambiar</li>
            <li>pensar que cada cambio vuelve inutil todo lo aprendido antes</li>
            <li>confundir crecimiento del lenguaje con complejidad sin direccion</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>C# cambio con el tiempo porque el desarrollo de software tambien cambio. Esa adaptacion explica parte importante de su madurez y de su permanencia como herramienta tecnica relevante.</p>
    </section>

    <section class="capitulo-section">
        <h2>Aplicacion conceptual</h2>
        <p>Este contenido ayuda a interpretar mejor diferencias entre estilos, ejemplos y practicas del lenguaje sin asumir que toda variacion responde a contradicciones o errores.</p>
    </section>
</article>';

    DECLARE @Html_03 NVARCHAR(MAX) = N'
<article class="capitulo">
    <header>
        <h1>Vigencia de C# en distintos contextos</h1>
        <p>La vigencia de un lenguaje se observa en su capacidad para seguir siendo util en contextos reales. En el caso de C#, esa vigencia se explica por su relacion con el ecosistema .NET, por su adaptabilidad y por la diversidad de escenarios donde sigue resultando una opcion solida.</p>
    </header>

    <nav class="capitulo-nav">
        <p><strong>Factores de vigencia</strong></p>
        <ul>
            <li>continuidad del ecosistema</li>
            <li>uso en varios tipos de aplicacion</li>
            <li>capacidad de evolucion del lenguaje</li>
            <li>valor formativo y profesional</li>
        </ul>
    </nav>

    <section class="capitulo-section">
        <h2>Vigencia no significa moda pasajera</h2>
        <p>Cuando se dice que un lenguaje sigue vigente, no se afirma que sea el unico ni que sirva para todo mejor que cualquier alternativa. Se afirma que mantiene utilidad tecnica real, adopcion suficiente y capacidad de resolver problemas actuales en contextos concretos.</p>
    </section>

    <section class="capitulo-section">
        <h2>Contextos donde conserva relevancia</h2>
        <table>
            <thead>
                <tr>
                    <th>Contexto</th>
                    <th>Razon de vigencia</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Web y APIs</td>
                    <td>integracion con modelos de desarrollo estables y productivos</td>
                </tr>
                <tr>
                    <td>Servicios</td>
                    <td>uso continuo en aplicaciones orientadas a negocio e integracion</td>
                </tr>
                <tr>
                    <td>Escritorio</td>
                    <td>presencia en soluciones administrativas e internas</td>
                </tr>
                <tr>
                    <td>Automatizacion</td>
                    <td>capacidad para construir herramientas y procesos tecnicos</td>
                </tr>
                <tr>
                    <td>Videojuegos</td>
                    <td>presencia en motores y entornos donde sigue teniendo papel practico</td>
                </tr>
            </tbody>
        </table>
    </section>

    <section class="capitulo-section">
        <h2>Valor formativo</h2>
        <p>C# mantiene vigencia tambien como lenguaje de formacion porque obliga a pensar con orden tecnico, diferencia tipos, permite trabajar con estructuras claras y ofrece continuidad hacia escenarios profesionales reales. Eso lo vuelve util tanto para aprender fundamentos como para profundizar despues.</p>
    </section>

    <section class="capitulo-section">
        <h2>Relacion con su evolucion</h2>
        <p>La vigencia no se sostiene por inercia. Se sostiene porque el lenguaje y su ecosistema han sabido ajustarse a nuevas exigencias del desarrollo. Esta relacion entre evolucion y permanencia es una de las ideas centrales de este capitulo.</p>
    </section>

    <section class="capitulo-section">
        <h2>Precision terminologica</h2>
        <ul>
            <li>vigente no significa obligatorio</li>
            <li>usado en muchos contextos no significa adecuado para cualquier decision sin analisis</li>
            <li>tener historia larga no implica estar tecnologicamente agotado</li>
        </ul>
    </section>

    <section class="capitulo-section">
        <h2>Consideraciones y limites</h2>
        <p>Este contenido no compara de forma exhaustiva a C# con todos los lenguajes actuales. Su foco es explicar por que sigue teniendo valor dentro de contextos concretos y no tratar de convertir esa vigencia en una afirmacion absoluta.</p>
    </section>

    <section class="capitulo-section">
        <h2>Sintesis conceptual</h2>
        <p>C# sigue vigente porque conserva utilidad real, continuidad ecosistemica y capacidad de adaptacion. Su presencia en varios contextos confirma que no se trata solo de un lenguaje historico, sino de una herramienta activa en el desarrollo actual.</p>
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
        N'Origen y evolucion de C#',
        N'Contexto de aparicion y crecimiento del lenguaje',
        N'Explica el origen de C# en relacion con su contexto tecnico y muestra como su evolucion se conecta con la expansion del ecosistema .NET.',
        N'11-13 min',
        NULL,
        @NivelPrincipiante,
        N'fas fa-history',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_01
    ),
    (
        @TipoModeloMental,
        N'Como cambio C# con el tiempo',
        N'Evolucion del lenguaje como respuesta a nuevas necesidades',
        N'Ofrece un modelo conceptual para entender la evolucion de C# no como una lista de novedades, sino como un proceso de ajuste tecnico y maduracion.',
        N'10-12 min',
        NULL,
        @NivelIntermedio,
        N'fas fa-chart-line',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_02
    ),
    (
        @TipoReferencias,
        N'Vigencia de C# en distintos contextos',
        N'Permanencia del lenguaje en web, servicios, escritorio y otros escenarios',
        N'Expone por que C# mantiene vigencia en distintos contextos del desarrollo actual y relaciona esa permanencia con su evolucion y con el ecosistema .NET.',
        N'9-11 min',
        NULL,
        @NivelGeneral,
        N'fas fa-compass',
        SYSUTCDATETIME(),
        SYSUTCDATETIME(),
        @CapituloId,
        @Html_03
    );

    PRINT CONCAT('Se insertaron 3 contenidos para el capitulo IdCapitulo=', @CapituloId, '.');

    COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO
