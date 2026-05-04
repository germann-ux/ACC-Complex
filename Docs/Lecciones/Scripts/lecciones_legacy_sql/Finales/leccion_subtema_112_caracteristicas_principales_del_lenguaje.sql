-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 112
-- Subtema: Caracteristicas principales del lenguaje

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 112;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 54301, 'No existe el SubTemaId=112 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Caracteristicas principales del lenguaje';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Presenta rasgos importantes de C#, como tipado fuerte, enfoque orientado a objetos, sintaxis clara e integracion con .NET.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-112-caracteristicas-csharp';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST112VIDPEND01';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Rasgos base de C#';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra algunas caracteristicas centrales del lenguaje y como se conectan con su forma de trabajo.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart TD
    A["C#"] --> B["Tipado fuerte"]
    A --> C["Orientado a objetos"]
    A --> D["Sintaxis clara"]
    A --> E["Integracion con .NET"]

    style A fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';

    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Rasgos que definen al lenguaje</h3>
    <p>C# no solo importa por los proyectos que puede crear. Tambien importa por la forma en que ayuda a escribir programas con orden, claridad y estructura.</p>
    <p>Algunas de sus caracteristicas explican por que se usa tanto en entornos de desarrollo profesional y por que resulta una buena base para aprender.</p>

    <h3>Tipado fuerte</h3>
    <p>Esto significa que el lenguaje distingue con claridad el tipo de dato que estas usando, como numeros, texto o valores logicos. Eso ayuda a detectar errores antes y a escribir codigo con mas control.</p>

    <h3>Orientado a objetos</h3>
    <p>C# permite organizar los programas en partes como clases y objetos. Esto ayuda a separar responsabilidades y a construir sistemas de forma mas ordenada.</p>

    <h3>Sintaxis clara</h3>
    <p>La forma de escribir C# busca ser legible. Eso no significa que todo sea simple al principio, pero si que el lenguaje esta pensado para que el codigo se pueda leer y mantener mejor.</p>

    <h3>Integracion con .NET y herramientas</h3>
    <p>C# trabaja de forma natural con .NET y con herramientas como Visual Studio. Eso facilita crear, ejecutar, depurar y mantener proyectos dentro de un ecosistema amplio.</p>
    <img src="https://placehold.co/1200x675?text=Caracteristicas+principales+de+Csharp" alt="Rasgos principales del lenguaje C# (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>Estas caracteristicas no son adorno. Son parte de lo que hace que C# sea un lenguaje util para construir software con mas orden y claridad.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar más en las caracteristicas principales de C# y verlas con mayor detalle, puedes ver contenido más amplio en el capítulo sobre fundamentos del lenguaje haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_CARACTERISTICAS_CSHARP">aquí</a>.</p>
    </div>
</div>';

    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplos breves de lo que significan</h3>
    <p>Si una variable guarda un numero, C# espera que se use como numero y no como texto. Eso se relaciona con el tipado fuerte.</p>
    <p>Si un sistema tiene una parte para alumnos y otra para materias, esa forma de organizarlo se relaciona con el enfoque orientado a objetos.</p>
    <p>Si el codigo esta acomodado y sus elementos tienen una estructura facil de seguir, eso se relaciona con una sintaxis clara.</p>
    <p>Si ademas trabajas dentro de .NET y una herramienta te ayuda a compilar o depurar, ya estas viendo la integracion del lenguaje con su ecosistema.</p>
    <img src="https://placehold.co/1200x675?text=Tipado+objetos+sintaxis+y+ecosistema" alt="Ejemplos simples de caracteristicas de C# (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>Estas caracteristicas se entienden mejor cuando se ven en practica, pero desde ahora ya puedes reconocer para que sirve cada una.</p>
    </div>
</div>';

    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <ol>
        <li>Explica con tus palabras que significa que C# tenga tipado fuerte.</li>
        <li>Describe por que organizar un programa en clases se relaciona con el enfoque orientado a objetos.</li>
        <li>Indica por que una sintaxis clara ayuda al momento de leer o mantener codigo.</li>
        <li>Menciona una ventaja de que C# trabaje junto a .NET y herramientas como Visual Studio.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si relacionas cada caracteristica con una utilidad concreta dentro del trabajo de programacion.</p>
    </div>
</div>';

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Un lenguaje no solo se mide por lo que puede hacer, sino por que tan bien te ayuda a escribir codigo claro, ordenado y mantenible.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a reconocer algunos rasgos que hacen a C# una herramienta fuerte para programar.</p><p>La meta es que empieces a identificar que aporta el lenguaje mas alla de su nombre o su popularidad.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 54302, 'Ya existe una leccion con este titulo para SubtemaId=112.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 54303, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 54304, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 54305, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 54306, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 54307, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 543071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 54308, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 54309, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 54310, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 54311, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 54312, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 54313, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 54314, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 54315, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 54316, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 54317, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 54318, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 54319, 'Sin video, limpiar flag/VideoId.', 1;

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
