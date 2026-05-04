-- Script final de insercion de leccion con bloques interactivos
-- Generado desde legacy: leccion_subtema_111_historia_breve_y_evolucion_de_csharp.sql
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
    DECLARE @TituloLeccion NVARCHAR(100) = N'Historia breve y evolucion de C#';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Explica el origen de C#, su relacion con .NET y la diferencia entre .NET Framework, .NET Core y ASP.NET.';
    DECLARE @NivelBloom NVARCHAR(64) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';
    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-111-historia-csharp';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(200) = N'ST111VIDPEND01';
    DECLARE @MermaidTitulo NVARCHAR(160) = N'Evolucion del ecosistema C#';
    DECLARE @MermaidDescripcion NVARCHAR(MAX) = N'Muestra el paso de C# junto a .NET, desde .NET Framework hasta .NET Core y el modelo actual de .NET.';
    DECLARE @MermaidCodigo NVARCHAR(MAX) = N'flowchart LR
    A["C# nace con Microsoft"] --> B["C# sobre .NET Framework"]
    B --> C["C# evoluciona con .NET"]
    C --> D[".NET Core moderniza la plataforma"]
    D --> E[".NET actual unifica el camino"]

    style D fill:#1e1e2a,stroke:#9926fe,stroke-width:2px,color:#f8fafc';
    DECLARE @Teoria NVARCHAR(MAX) = N'
<div class="leccion-teoria">
    <h3>De donde viene C#</h3>
    <p>C# nacio en el entorno de Microsoft como un lenguaje pensado para crear software de forma moderna, clara y estructurada. Desde el inicio estuvo ligado a .NET, que es la plataforma con la que suele trabajar.</p>
    <p>Eso significa que C# y .NET estan muy relacionados, pero no son lo mismo: C# es el lenguaje; .NET es la plataforma donde ese lenguaje se usa para construir y ejecutar aplicaciones.</p>

    <h3>Que fue cambiando con el tiempo</h3>
    <p>Al principio, muchos proyectos con C# trabajaban sobre <strong>.NET Framework</strong>, que fue durante aÃ±os la base clasica del ecosistema y estuvo muy ligada a Windows.</p>
    <p>Despues aparecio <strong>.NET Core</strong>, una etapa mas moderna que abrio el camino hacia desarrollo multiplataforma y una estructura mas flexible.</p>
    <p>Con el tiempo, el ecosistema siguio avanzando hasta el modelo actual de <strong>.NET</strong>, donde el camino se simplifica y se busca una plataforma mas unificada.</p>

    <h3>Lo que suele confundirse</h3>
    <ul>
        <li><strong>C#:</strong> es el lenguaje de programacion.</li>
        <li><strong>.NET:</strong> es la plataforma general donde C# se apoya para crear aplicaciones.</li>
        <li><strong>.NET Framework:</strong> fue la base clasica de .NET, muy asociada a Windows.</li>
        <li><strong>.NET Core:</strong> fue una evolucion mas moderna y multiplataforma de la plataforma.</li>
        <li><strong>ASP.NET:</strong> es una tecnologia del ecosistema .NET para crear aplicaciones web.</li>
        <li><strong>ASP.NET Core:</strong> es la version web ligada al modelo moderno de .NET Core y .NET.</li>
    </ul>
    <img src="https://placehold.co/1200x675?text=Historia+y+evolucion+de+Csharp" alt="Relacion entre C#, .NET y sus etapas (pendiente)">

    <div class="alert alert-info">
        <p class="alert-title">Idea clave</p>
        <p>No todo lo que suena parecido significa lo mismo. C# es el lenguaje; .NET es la plataforma; ASP.NET es una tecnologia web dentro de ese ecosistema.</p>
    </div>

    <div class="fomentador">
        <p>Si quieres profundizar mÃ¡s en la historia de C# y en la diferencia entre .NET Framework, .NET Core y ASP.NET, puedes ver contenido mÃ¡s detallado en el capÃ­tulo sobre evoluciÃ³n de C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_HISTORIA_CSHARP">aquÃ­</a>.</p>
    </div>
</div>';
    DECLARE @Practica NVARCHAR(MAX) = N'
<div class="leccion-practicas">
    <h3>Practica guiada</h3>
    <ol>
        <li>Escribe con tus palabras cual es la diferencia entre C# y .NET.</li>
        <li>Explica que cambia entre .NET Framework y .NET Core de forma general.</li>
        <li>Indica donde entra ASP.NET dentro de este ecosistema.</li>
    </ol>
    <p>Despues, responde por que puede ser confuso leer estos nombres si no se separan bien desde el principio.</p>
    <div class="alert alert-success">
        <p class="alert-title">Criterio de logro</p>
        <p>Esta correcto si distingues lenguaje, plataforma y tecnologia web, y si reconoces que la evolucion del ecosistema cambio el alcance de C# con el tiempo.</p>
    </div>
</div>';
    DECLARE @Ejemplo NVARCHAR(MAX) = N'
<div class="leccion-ejemplos">
    <h3>Ejemplo de confusion comun</h3>
    <p>Una persona puede decir: "Voy a aprender ASP.NET" o "Voy a trabajar con .NET". Eso no significa exactamente lo mismo que decir "Voy a aprender C#".</p>
    <p>Si aprende <strong>C#</strong>, aprende el lenguaje. Si trabaja con <strong>.NET</strong>, trabaja en la plataforma. Si usa <strong>ASP.NET</strong>, entra al area web de ese ecosistema.</p>
    <p>Por eso conviene separar bien cada nombre desde el inicio.</p>
    <img src="https://placehold.co/1200x675?text=Csharp+NET+y+ASP.NET" alt="Diferencia entre lenguaje, plataforma y tecnologia web (pendiente)">

    <div class="alert alert-success">
        <p class="alert-title">Lo importante</p>
        <p>Entender esta diferencia evita muchas confusiones cuando empieces a leer documentacion, ver tutoriales o trabajar con proyectos reales.</p>
    </div>
</div>';
    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Cuando leas sobre C#, .NET o ASP.NET, primero pregunta: estoy viendo un lenguaje, una plataforma o una tecnologia especifica?</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a entender de donde viene C# y como fue creciendo junto a .NET.</p><p>La meta es que dejes de ver estos nombres como si fueran lo mismo y empieces a ubicarlos con claridad.</p>';
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

