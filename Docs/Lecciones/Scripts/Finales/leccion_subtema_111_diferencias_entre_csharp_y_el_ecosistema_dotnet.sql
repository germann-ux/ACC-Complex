-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_111_diferencias_entre_csharp_y_el_ecosistema_dotnet.sql
-- Modelo vigente: acc_academic.Lecciones + acc_academic.BloquesLeccion
USE [ACC_Academic];
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
BEGIN TRY
    BEGIN TRAN;
    DECLARE @SubtemaId INT = 111;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 57001, 'No existe el SubTemaId objetivo en acc_academic.SubTemas.', 1;
    DECLARE @TituloLeccion NVARCHAR(100) = N'Diferencias entre C# y el ecosistema .NET';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Aclara la diferencia entre C#, .NET, .NET Framework, .NET Core y ASP.NET para evitar confusiones frecuentes.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-111-diferencias-ecosistema-dotnet';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST111VIDPEND02';
    DECLARE @MermaidTitulo NVARCHAR(160) = N'Mapa basico del ecosistema';
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = N'Muestra que C# es el lenguaje, .NET es la plataforma y ASP.NET pertenece al area web de ese ecosistema.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart TD
    A["Ecosistema .NET"] --> B["C# = lenguaje"]
    A --> C[".NET = plataforma"]
    C --> D[".NET Framework"]
    C --> E[".NET Core"]
    C --> F[".NET actual"]
    C --> G["ASP.NET = tecnologia web"]

    style C fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>Nombres relacionados, pero no iguales</h3>
    <p>Cuando una persona empieza a estudiar C#, suele encontrarse con varios nombres parecidos: C#, .NET, .NET Framework, .NET Core y ASP.NET. Como todos aparecen juntos, es comun pensar que significan exactamente lo mismo.</p>
    <p>No es asi. Estan relacionados, pero cada uno ocupa un lugar distinto dentro del ecosistema.</p>

    <h3>Que es cada cosa</h3>
    <ul>
        <li><strong>C#:</strong> es el lenguaje de programacion.</li>
        <li><strong>.NET:</strong> es la plataforma general donde C# trabaja para construir y ejecutar aplicaciones.</li>
        <li><strong>.NET Framework:</strong> fue la base clasica de la plataforma, muy asociada a Windows.</li>
        <li><strong>.NET Core:</strong> fue una evolucion mas moderna y multiplataforma de esa plataforma.</li>
        <li><strong>.NET actual:</strong> representa el camino unificado que continua despues de .NET Core.</li>
        <li><strong>ASP.NET:</strong> es la tecnologia del ecosistema .NET enfocada en desarrollo web.</li>
    </ul>

    <h3>Por que esta diferencia importa</h3>
    <p>Si no separas estos conceptos, se vuelve mas dificil entender documentacion, cursos y proyectos reales. Un tutorial puede hablar del lenguaje, otro de la plataforma y otro de la parte web, aunque todos usen nombres parecidos.</p>
    <img src="https://placehold.co/1200x675?text=Csharp+NET+y+ASP.NET" alt="Mapa simple de lenguaje, plataforma y tecnologia web (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>C# es el lenguaje. .NET es la plataforma. ASP.NET pertenece al area web del mismo ecosistema. Esa separacion evita muchas confusiones desde el inicio.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en la diferencia entre C#, .NET, .NET Framework, .NET Core y ASP.NET, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre el ecosistema de C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_ECOSISTEMA_DOTNET">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <ol>
        <li>Escribe con tus palabras la diferencia entre C# y .NET.</li>
        <li>Explica de forma general que cambia entre .NET Framework y .NET Core.</li>
        <li>Indica donde entra ASP.NET dentro del ecosistema.</li>
        <li>Describe por que estos nombres pueden confundirse si no se separan bien desde el principio.</li>
    </ol>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si distingues lenguaje, plataforma y tecnologia web, y si explicas esa diferencia sin mezclar sus funciones.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo de frases comunes</h3>
    <p>Una persona puede decir: "Estoy aprendiendo C#". Otra puede decir: "Trabajo con .NET". Otra mas puede decir: "Desarrollo en ASP.NET".</p>
    <p>Las tres frases estan relacionadas, pero no apuntan exactamente a la misma cosa.</p>
    <ul>
        <li>Si aprendes <strong>C#</strong>, aprendes el lenguaje.</li>
        <li>Si trabajas con <strong>.NET</strong>, trabajas dentro de la plataforma.</li>
        <li>Si desarrollas en <strong>ASP.NET</strong>, estas en la parte web de ese ecosistema.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Lenguaje+plataforma+y+web" alt="Diferencia entre lenguaje, plataforma y tecnologia web (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>Cuando distingues bien estos nombres, entiendes mejor de que habla cada recurso o proyecto que encuentras.</p>
    </div>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Cuando un nombre tecnico te confunda, primero pregunta si estas viendo un lenguaje, una plataforma o una tecnologia mas especifica.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a ordenar nombres que suelen confundirse mucho cuando alguien empieza con C#.</p><p>La meta es que puedas distinguir con claridad de que se esta hablando en cada caso.</p>';
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

