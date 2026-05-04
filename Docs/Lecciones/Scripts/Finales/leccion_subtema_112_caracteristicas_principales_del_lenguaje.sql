-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_112_caracteristicas_principales_del_lenguaje.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 112;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Caracteristicas principales del lenguaje';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Presenta rasgos importantes de C#, como tipado fuerte, enfoque orientado a objetos, sintaxis clara e integracion con .NET.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-112-caracteristicas-csharp';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST112VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = N'Rasgos base de C#';
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = N'Muestra algunas caracteristicas centrales del lenguaje y como se conectan con su forma de trabajo.';
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
        <p>Si quieres profundizar mÃ¡s en las caracteristicas principales de C# y verlas con mayor detalle, puedes ver contenido mÃ¡s amplio en el capÃ­tulo sobre fundamentos del lenguaje haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_CARACTERISTICAS_CSHARP">aquÃ­</a>.</p>
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
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Un lenguaje no solo se mide por lo que puede hacer, sino por que tan bien te ayuda a escribir codigo claro, ordenado y mantenible.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a reconocer algunos rasgos que hacen a C# una herramienta fuerte para programar.</p><p>La meta es que empieces a identificar que aporta el lenguaje mas alla de su nombre o su popularidad.</p>';
    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 57002, 'NivelBloom invalido.', 1;
    IF ISJSON(@OrdenSecciones) <> 1
        THROW 57003, 'OrdenSecciones legacy no es JSON valido.', 1;
    IF EXISTS (SELECT [value] FROM OPENJSON(@OrdenSecciones) GROUP BY [value] HAVING COUNT(*) > 1)
        THROW 57004, 'OrdenSecciones legacy contiene tokens duplicados.', 1;
    IF EXISTS
    (
        SELECT 1
        FROM OPENJSON(@OrdenSecciones) j
        LEFT JOIN
        (
            VALUES
                (N'video'),
                (N'teoria'),
                (N'practica'),
                (N'ejemplo'),
                (N'actividad'),
                (N'compilador'),
                (N'mermaid'),
                (N'charpTip'),
                (N'charpDialog')
        ) permitidas(Token) ON permitidas.Token = CAST(j.[value] AS NVARCHAR(50))
        WHERE permitidas.Token IS NULL
    )
        THROW 57005, 'OrdenSecciones legacy contiene tokens no soportados por BloquesLeccion.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') AND NULLIF(LTRIM(RTRIM(ISNULL(@Teoria, N''))), N'') IS NULL
        THROW 57006, 'El bloque teoria requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') AND NULLIF(LTRIM(RTRIM(ISNULL(@Practica, N''))), N'') IS NULL
        THROW 57007, 'El bloque practica requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') AND NULLIF(LTRIM(RTRIM(ISNULL(@Ejemplo, N''))), N'') IS NULL
        THROW 57008, 'El bloque ejemplo requiere contenido.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') AND NULLIF(LTRIM(RTRIM(ISNULL(@MermaidCodigo, N''))), N'') IS NULL
        THROW 57009, 'El bloque mermaid requiere codigo.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') AND NULLIF(LTRIM(RTRIM(ISNULL(@CharpTip, N''))), N'') IS NULL
        THROW 57010, 'El bloque charpTip requiere texto.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') AND NULLIF(LTRIM(RTRIM(ISNULL(@CharpDialog, N''))), N'') IS NULL
        THROW 57011, 'El bloque charpDialog requiere texto.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@UrlActividad, N''))), N'') IS NULL)
        THROW 57012, 'El bloque actividad requiere flag y URL.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') AND @TieneCompilador = 0
        THROW 57013, 'El bloque compilador requiere flag.', 1;
    IF EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL)
        THROW 57014, 'El bloque video requiere flag y VideoId.', 1;
    DECLARE @LeccionesARemover TABLE (IdLeccion INT PRIMARY KEY);

    INSERT INTO @LeccionesARemover (IdLeccion)
    SELECT IdLeccion
    FROM acc_academic.Lecciones
    WHERE SubtemaId = @SubtemaId
      AND (TituloLeccion = @TituloLeccion OR IdLeccion < 4000);

    DELETE b
    FROM acc_academic.BloquesLeccion b
    INNER JOIN @LeccionesARemover r ON r.IdLeccion = b.LeccionId;

    DELETE l
    FROM acc_academic.Lecciones l
    INNER JOIN @LeccionesARemover r ON r.IdLeccion = l.IdLeccion;
    INSERT INTO acc_academic.Lecciones
    (
        TituloLeccion,
        DescripcionLeccion,
        SubtemaId
    )
    VALUES
    (
        @TituloLeccion,
        @DescripcionLeccion,
        @SubtemaId
    );
    DECLARE @LeccionId INT = CONVERT(INT, SCOPE_IDENTITY());
    ;WITH SourceBlocks AS
    (
        SELECT
            TRY_CONVERT(INT, j.[key]) AS SourceOrden,
            CAST(j.[value] AS NVARCHAR(50)) AS Token
        FROM OPENJSON(@OrdenSecciones) j
    ),
    MappedBlocks AS
    (
        SELECT
            ROW_NUMBER() OVER (ORDER BY SourceOrden) AS Orden,
            CASE Token
                WHEN N'teoria' THEN N'TextoHtml'
                WHEN N'practica' THEN N'TextoHtml'
                WHEN N'ejemplo' THEN N'TextoHtml'
                WHEN N'mermaid' THEN N'Mermaid'
                WHEN N'charpTip' THEN N'CharpTip'
                WHEN N'charpDialog' THEN N'CharpDialog'
                WHEN N'actividad' THEN N'ActividadExterna'
                WHEN N'compilador' THEN N'Compilador'
                WHEN N'video' THEN N'Video'
            END AS TipoBloque,
            CASE Token
                WHEN N'teoria' THEN N'Teoria'
                WHEN N'practica' THEN N'Practica'
                WHEN N'ejemplo' THEN N'Ejemplo'
                WHEN N'mermaid' THEN NULLIF(@MermaidTitulo, N'')
                ELSE NULL
            END AS Titulo,
            CASE Token
                WHEN N'teoria' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Teoria, N''), 'json'), N'"}')
                WHEN N'practica' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Practica, N''), 'json'), N'"}')
                WHEN N'ejemplo' THEN CONCAT(N'{"html":"', STRING_ESCAPE(ISNULL(@Ejemplo, N''), 'json'), N'"}')
                WHEN N'mermaid' THEN CONCAT(N'{"codigo":"', STRING_ESCAPE(ISNULL(@MermaidCodigo, N''), 'json'), N'","descripcion":"', STRING_ESCAPE(ISNULL(@MermaidDescripcion, N''), 'json'), N'"}')
                WHEN N'charpTip' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL(@CharpTip, N''), 'json'), N'"}')
                WHEN N'charpDialog' THEN CONCAT(N'{"texto":"', STRING_ESCAPE(ISNULL(@CharpDialog, N''), 'json'), N'"}')
                WHEN N'actividad' THEN CONCAT(N'{"url":"', STRING_ESCAPE(ISNULL(@UrlActividad, N''), 'json'), N'","textoBoton":"Ver actividad interactiva"}')
                WHEN N'compilador' THEN N'{"lenguaje":"csharp","codigoInicial":"","instrucciones":""}'
                WHEN N'video' THEN CONCAT(N'{"videoId":"', STRING_ESCAPE(ISNULL(@VideoId, N''), 'json'), N'","proveedor":"youtube"}')
            END AS ConfiguracionJson
        FROM SourceBlocks
    )
    INSERT INTO acc_academic.BloquesLeccion
    (
        LeccionId,
        TipoBloque,
        Orden,
        ConfiguracionJson,
        Titulo,
        NivelBloom,
        EsObligatorio,
        Puntaje
    )
    SELECT
        @LeccionId,
        TipoBloque,
        Orden,
        ConfiguracionJson,
        Titulo,
        NULLIF(@NivelBloom, N''),
        CAST(0 AS BIT),
        NULL
    FROM MappedBlocks;
    COMMIT TRAN;
    SELECT
        l.IdLeccion,
        l.TituloLeccion,
        l.SubtemaId,
        COUNT(b.IdBloqueLeccion) AS TotalBloques
    FROM acc_academic.Lecciones l
    LEFT JOIN acc_academic.BloquesLeccion b ON b.LeccionId = l.IdLeccion
    WHERE l.IdLeccion = @LeccionId
    GROUP BY l.IdLeccion, l.TituloLeccion, l.SubtemaId;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    THROW;
END CATCH;
GO

