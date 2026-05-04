-- Insercion de leccion (propuesta final, no ejecutada automaticamente)
-- SubtemaId objetivo: 111
-- Subtema: Historia breve y evolucion de C#
-- Leccion 2 de 2 para este subtema

USE [ACC_Academic];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRAN;

    DECLARE @SubtemaId INT = 111;

    IF NOT EXISTS (SELECT 1 FROM acc_academic.SubTemas WHERE Id_SubTema = @SubtemaId)
        THROW 54501, 'No existe el SubTemaId=111 en acc_academic.SubTemas.', 1;

    DECLARE @TituloLeccion NVARCHAR(100) = N'Diferencias entre C# y el ecosistema .NET';
    DECLARE @DescripcionLeccion NVARCHAR(500) = N'Aclara la diferencia entre C#, .NET, .NET Framework, .NET Core y ASP.NET para evitar confusiones frecuentes.';
    DECLARE @NivelBloom NVARCHAR(20) = N'Comprender';
    DECLARE @OrdenSecciones NVARCHAR(MAX) = N'["charpDialog","video","teoria","mermaid","ejemplo","practica","actividad","charpTip"]';

    DECLARE @TieneActividad BIT = 1;
    DECLARE @UrlActividad NVARCHAR(MAX) = N'https://pendiente.local/actividad/subtema-111-diferencias-ecosistema-dotnet';
    DECLARE @TieneCompilador BIT = 0;
    DECLARE @TieneVideo BIT = 1;
    DECLARE @VideoId NVARCHAR(20) = N'ST111VIDPEND02';

    DECLARE @MermaidTitulo NVARCHAR(200) = N'Mapa basico del ecosistema';
    DECLARE @MermaidDescripcion NVARCHAR(500) = N'Muestra que C# es el lenguaje, .NET es la plataforma y ASP.NET pertenece al area web de ese ecosistema.';
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
        <p>Si quieres profundizar más en la diferencia entre C#, .NET, .NET Framework, .NET Core y ASP.NET, puedes ver contenido más detallado en el capítulo sobre el ecosistema de C# haciendo clic <a href="Capitulo/Contenido/ID_CONTENIDO_PENDIENTE_ECOSISTEMA_DOTNET">aquí</a>.</p>
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

    DECLARE @CharpTip NVARCHAR(MAX) = N'<p><strong>Tip Charp:</strong> Cuando un nombre tecnico te confunda, primero pregunta si estas viendo un lenguaje, una plataforma o una tecnologia mas especifica.</p>';
    DECLARE @CharpDialog NVARCHAR(MAX) = N'<p>En esta leccion vas a ordenar nombres que suelen confundirse mucho cuando alguien empieza con C#.</p><p>La meta es que puedas distinguir con claridad de que se esta hablando en cada caso.</p>';

    IF EXISTS
    (
        SELECT 1
        FROM acc_academic.Lecciones
        WHERE SubtemaId = @SubtemaId
          AND TituloLeccion = @TituloLeccion
    )
        THROW 54502, 'Ya existe una leccion con este titulo para SubtemaId=111.', 1;

    IF @NivelBloom NOT IN (N'Recordar', N'Comprender', N'Aplicar', N'Analizar', N'Evaluar', N'Crear')
        THROW 54503, 'NivelBloom invalido.', 1;

    IF ISJSON(@OrdenSecciones) <> 1
        THROW 54504, 'OrdenSecciones no es JSON valido.', 1;

    IF EXISTS
    (
        SELECT j.[value]
        FROM OPENJSON(@OrdenSecciones) j
        GROUP BY j.[value]
        HAVING COUNT(*) > 1
    )
        THROW 54505, 'OrdenSecciones contiene tokens duplicados.', 1;

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
        THROW 54506, 'OrdenSecciones contiene tokens fuera del conjunto permitido.', 1;

    DECLARE @SecTeoria BIT      = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'teoria') THEN 1 ELSE 0 END;
    DECLARE @SecMermaid BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'mermaid') THEN 1 ELSE 0 END;
    DECLARE @SecEjemplo BIT     = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'ejemplo') THEN 1 ELSE 0 END;
    DECLARE @SecPractica BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'practica') THEN 1 ELSE 0 END;
    DECLARE @SecCharpTip BIT    = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpTip') THEN 1 ELSE 0 END;
    DECLARE @SecCharpDialog BIT = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'charpDialog') THEN 1 ELSE 0 END;
    DECLARE @SecActividad BIT   = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'actividad') THEN 1 ELSE 0 END;
    DECLARE @SecCompilador BIT  = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'compilador') THEN 1 ELSE 0 END;
    DECLARE @SecVideo BIT       = CASE WHEN EXISTS (SELECT 1 FROM OPENJSON(@OrdenSecciones) WHERE [value] = N'video') THEN 1 ELSE 0 END;

    IF @SecTeoria = 1 AND NULLIF(LTRIM(RTRIM(@Teoria)), N'') IS NULL THROW 54507, 'Falta Teoria.', 1;
    IF @SecMermaid = 1 AND NULLIF(LTRIM(RTRIM(@MermaidCodigo)), N'') IS NULL THROW 545071, 'Falta MermaidCodigo.', 1;
    IF @SecEjemplo = 1 AND NULLIF(LTRIM(RTRIM(@Ejemplo)), N'') IS NULL THROW 54508, 'Falta Ejemplo.', 1;
    IF @SecPractica = 1 AND NULLIF(LTRIM(RTRIM(@Practica)), N'') IS NULL THROW 54509, 'Falta Practica.', 1;
    IF @SecCharpTip = 1 AND NULLIF(LTRIM(RTRIM(@CharpTip)), N'') IS NULL THROW 54510, 'Falta CharpTip.', 1;
    IF @SecCharpDialog = 1 AND NULLIF(LTRIM(RTRIM(@CharpDialog)), N'') IS NULL THROW 54511, 'Falta CharpDialog.', 1;
    IF @SecCharpTip = 1 AND @CharpTip LIKE N'%<div%' THROW 54512, 'CharpTip no debe incluir <div>.', 1;
    IF @SecCharpDialog = 1 AND @CharpDialog LIKE N'%<div%' THROW 54513, 'CharpDialog no debe incluir <div>.', 1;
    IF @SecActividad = 1 AND (@TieneActividad = 0 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NULL) THROW 54514, 'Actividad requiere flag y URL.', 1;
    IF @SecActividad = 0 AND (@TieneActividad = 1 OR NULLIF(LTRIM(RTRIM(@UrlActividad)), N'') IS NOT NULL) THROW 54515, 'Sin actividad, limpiar flag/URL.', 1;
    IF @SecCompilador = 1 AND @TieneCompilador = 0 THROW 54516, 'Compilador requiere flag.', 1;
    IF @SecCompilador = 0 AND @TieneCompilador = 1 THROW 54517, 'Sin compilador, flag debe ser 0.', 1;
    IF @SecVideo = 1 AND (@TieneVideo = 0 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NULL OR @VideoId LIKE N'%youtube%' OR @VideoId LIKE N'%http%') THROW 54518, 'Video requiere flag y VideoId limpio.', 1;
    IF @SecVideo = 0 AND (@TieneVideo = 1 OR NULLIF(LTRIM(RTRIM(ISNULL(@VideoId, N''))), N'') IS NOT NULL) THROW 54519, 'Sin video, limpiar flag/VideoId.', 1;

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
