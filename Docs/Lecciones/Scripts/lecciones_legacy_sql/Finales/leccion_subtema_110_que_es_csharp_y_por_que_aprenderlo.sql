-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 110
-- Subtema: Que es C# y por que aprenderlo?

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 110;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 54101, 'No existe el SubTemaId=110 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Que es C# y por que aprenderlo?';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Presenta C# como un lenguaje de programacion moderno y versatil, y muestra en que tipos de proyectos se usa.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-110-intro-csharp';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST110VIDPEND01';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Usos comunes de C#';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra algunos caminos donde C# se usa con frecuencia, desde aplicaciones web hasta videojuegos y servicios.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart TD
    A["C#"] --> B["Aplicaciones web"]
    A --> C["Programas de escritorio"]
    A --> D["Servicios y APIs"]
    A --> E["Videojuegos"]
    A --> F["Aplicaciones moviles"]

    style A fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Una herramienta para construir programas</h3>
    <p>C# es un lenguaje de programacion. Sirve para escribir instrucciones que una computadora puede ejecutar y asi convertir una idea en un programa real.</p>
    <p>En este submodulo ya no solo piensas como se organiza un sistema: ahora empiezas a construirlo. C# sera una de las herramientas principales para hacerlo.</p>

    <h3>Por que C# es importante</h3>
    <p>C# se usa en proyectos profesionales porque permite crear programas de distintos tipos con una sintaxis clara y un ecosistema muy amplio alrededor de .NET.</p>
    <ul>
        <li><strong>Es moderno:</strong> sigue evolucionando y se adapta a formas actuales de desarrollo.</li>
        <li><strong>Es versatil:</strong> puede usarse en web, escritorio, servicios, videojuegos y mas.</li>
        <li><strong>Es profesional:</strong> se usa en entornos reales de trabajo y en proyectos grandes.</li>
        <li><strong>Es buena base para aprender:</strong> ayuda a escribir codigo con orden y claridad.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Introduccion+a+Csharp" alt="Panorama general de C# (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>Aprender C# no es solo aprender palabras nuevas del lenguaje. Es empezar a usar una herramienta real para construir software.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en qué es C# y en qué tipos de proyectos se usa, puedes ver contenido más detallado en el capítulo sobre introducción a C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_INTRO_CSHARP">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplos de uso</h3>
    <p>Una escuela podria usar C# para un sistema que registre alumnos, materias y calificaciones.</p>
    <p>Una empresa podria usarlo para una aplicacion de escritorio que controle inventario o ventas.</p>
    <p>Tambien puede aparecer en una API que conecte datos entre sistemas o en un videojuego hecho con Unity.</p>
    <img src="https://placehold.co/1200x675?text=Web+escritorio+servicios+y+juegos" alt="Ejemplos de uso de C# (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>C# no se limita a un solo tipo de programa. Por eso vale la pena conocerlo desde el inicio de esta etapa de codificacion.</p>
    </div>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <p>Lee los siguientes casos y responde cual podria hacerse con C#:</p>
    <ol>
        <li>Un sistema escolar para registrar alumnos y materias.</li>
        <li>Una aplicacion de escritorio para controlar inventario.</li>
        <li>Un servicio que reciba datos y los envie a otra aplicacion.</li>
    </ol>
    <p>Despues, escribe con tus palabras por que aprender un lenguaje como C# puede ser util en la construccion de software.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si reconoces que C# puede usarse en varios tipos de proyecto y explicas su utilidad como herramienta real de desarrollo.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Aprender un lenguaje no es memorizar todo de una vez; es empezar a entender como usarlo para resolver problemas reales.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a dar tu primer paso en el terreno de la codificacion con C#.</p><p>La meta es que entiendas que tipo de herramienta es y por que aparece ahora en tu proceso de formacion.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 54102, 'Ya existe una leccion con este titulo para SubtemaId=110.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 54103, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 54104, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 54105, 'OrdenSecciones contiene tokens duplicados.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES (N'video'), (N'teoria'), (N'mermaid'), (N'practica'), (N'ejemplo'),
                   (N'actividad'), (N'compilador'), (N'charpTip'), (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 54106, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 54107, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 541071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 54108, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 54109, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 54110, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 54111, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 54112, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 54113, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 54114, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 54115, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 54116, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 54117, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 54118, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 54119, 'Sin video, limpiar flag/VideoId.', 1;

    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion, DescripcionLeccion, TieneActividad, UrlActividad, TieneCompilador,
        OrdenSecciones, SubtemaId, MermaidTitulo, MermaidDescripcion, MermaidCodigo,
        Teoria, Practica, Ejemplo, CharpTip, CharpDialog,
        NivelBloom, VideoId, TieneVideo
    )
    VALUES
    (
        @TituloLeccion, @DescripcionLeccion, @TieneActividad, @UrlActividad, @TieneCompilador,
        @OrdenSecciones, @SubtemaId, @MermaidTitulo, @MermaidDescripcion, @MermaidCodigo,
        @Teoria, @Practica, @Ejemplo, @CharpTip, @CharpDialog,
        @NivelBloom, @VideoId, @TieneVideo
    );

    COMMIT TRAN;

    SELECT TOP (1)
        IdLeccion, TituloLeccion, SubtemaId, NivelBloom, OrdenSecciones, MermaidTitulo, TieneActividad, TieneCompilador, TieneVideo
    FROM acc_academic.Lecciones
    WHERE SubtemaId = @SubtemaId
      AND TituloLeccion = @TituloLeccion
    ORDER BY IdLeccion DESC;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO
